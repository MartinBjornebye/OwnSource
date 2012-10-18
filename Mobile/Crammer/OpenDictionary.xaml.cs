using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;

using MB.Crammer;

namespace Crammer
{
    public partial class OpenDictionary : PhoneApplicationPage
    {
        public OpenDictionary()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listDictionaries.SelectedIndex < 0)
                    return;

                // Check if a similarly named dictionary already exists
                string dictionaryFilePath = listDictionaries.SelectedItem as string;
                if (dictionaryFilePath.EndsWith(CrammerDictionary.DICT_EXTENSION) == false)
                    dictionaryFilePath += CrammerDictionary.DICT_EXTENSION;
                
                CrammerDictionary crammerDict = (Application.Current as App).CurrentDictionary;
                crammerDict.DictionaryFile = dictionaryFilePath;
                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                if (appSettings == null)
                    throw new Exception("Failed to retrieve a storage handle");

                appSettings[CrammerConstants.CURRENT_DICT_NAME] = dictionaryFilePath;
                appSettings.Save();

                PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "OpenedDictionary";
                if ((Application.Current as App).RootFrame.BackStack.Count() > 0)
                    (Application.Current as App).RootFrame.RemoveBackEntry();

                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            listDictionaries.Items.Clear();
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string[] crammerDicts = storage.GetFileNames("*" + CrammerDictionary.DICT_EXTENSION);
                if (crammerDicts == null || crammerDicts.Length == 0)
                {
                    listDictionaries.Items.Add("[No Dictionaries Found]");
                    return;
                }

                foreach (string dictionary in crammerDicts)
                {
                    if ( dictionary.EndsWith(CrammerDictionary.DICT_EXTENSION))
                        listDictionaries.Items.Add(dictionary.Remove(dictionary.Length - CrammerDictionary.DICT_EXTENSION.Length));
                    else
                        listDictionaries.Items.Add(dictionary);
                }
            }
        }

        private void listDictionaries_DoubleTap(object sender, GestureEventArgs e)
        {
            cmdOpen_Click(sender, e);
        }
    }
}