using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using MB.Crammer;

namespace Crammer
{
    public partial class NewDictionary : PhoneApplicationPage
    {
        public NewDictionary()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDictionaryName.Text))
                    throw new Exception("No dictionary name was provided");

                // Check if a similarly named dictionary already exists
                IsolatedStorageSettings appStorage = IsolatedStorageSettings.ApplicationSettings;
                if (appStorage == null)
                    throw new Exception("Failed to retrieve a storage handle");

                string dictionaryName = "";
                if (appStorage.TryGetValue<string>(CrammerConstants.CURRENT_DICT_NAME, out dictionaryName))
                {
                    if ( txtDictionaryName.Text == dictionaryName )
                        throw new Exception("There is already a dictionary named: " + txtDictionaryName.Text);
                }

                CrammerDictionary crammerDict = (Application.Current as App).CurrentDictionary;
                crammerDict.DictionaryTitle = txtDictionaryName.Text;
                crammerDict.Entries.Clear();
                crammerDict.save(false);

                this.NavigationService.Navigate(new Uri("/ManageEntries.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void txtDictionaryName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDictionaryName.Text = "";
        }

    }
}