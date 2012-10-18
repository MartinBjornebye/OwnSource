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
    public partial class ManageEntries : PhoneApplicationPage
    {
        public ManageEntries()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFirst.Text) == true)
                    throw new Exception("You need to add a valid first value");

                if (string.IsNullOrEmpty(txtSecond.Text) == true)
                    throw new Exception("You need to add a valid second value");

                DictionaryEntry entry = new DictionaryEntry();
                entry.AEntry = txtFirst.Text;
                entry.BEntry = txtSecond.Text;

                CrammerDictionary crammerDict = (Application.Current as App).CurrentDictionary;
                if (crammerDict.entryExists(entry))
                {
                    txtFirst.Text = "";
                    txtSecond.Text = "";
                    throw new Exception("Entry already exists and will not be added");
                }

                crammerDict.addEntry(entry);
                crammerDict.save(true);
                txtFirst.Text = "";
                txtSecond.Text = "";
                txtFirst.Focus();

            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }

        private void cmdDone_Click(object sender, RoutedEventArgs e)
        {
            // We need to move back two frames to the start screen
            if ((Application.Current as App).RootFrame.BackStack.Count() > 1)
            {
                (Application.Current as App).RootFrame.RemoveBackEntry();
                (Application.Current as App).RootFrame.RemoveBackEntry();
            }
            this.NavigationService.GoBack();
        }
    }
}