using System;
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
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;

using MB.Crammer;

namespace Crammer
{
    public partial class EditEntries : PhoneApplicationPage
    {
        #region Constants
        private const string DEFAULT_SEARCH_FIELD_TEXT = "[Type to Search]";
        #endregion

        #region Attributes
        private CrammerDictionary mCurrentDict = null;
        private DictionaryEntry mCurrentEntry = null;
        #endregion

        public EditEntries()
        {
            InitializeComponent();
        }

        private void ContentPanel_Loaded(object sender, RoutedEventArgs e)
        {
            mCurrentDict = (Application.Current as App).CurrentDictionary;
            populateAll();
        }

        private void populateAll()
        {
            if (mCurrentDict == null)
                return;

            listEntries.Items.Clear();
            foreach (DictionaryEntry entry in mCurrentDict.Entries)
            {
                listEntries.Items.Add(entry.AEntry);
            }
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text == DEFAULT_SEARCH_FIELD_TEXT)
                return;

            listEntries.Items.Clear();
            IEnumerable<DictionaryEntry> entries = mCurrentDict.Entries.Where((c) => c.AEntry.ToLower().Contains(txtSearch.Text.ToLower()) ||
                                                                                     c.BEntry.ToLower().Contains(txtSearch.Text.ToLower()));
            if (entries == null || entries.Count() == 0)
                return;

            foreach (DictionaryEntry entry in entries)
            {
                listEntries.Items.Add(entry.AEntry);
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = DEFAULT_SEARCH_FIELD_TEXT;
            txtSearch.TextAlignment = System.Windows.TextAlignment.Center;
            populateAll();
        }

        private void listEntries_DoubleTap(object sender, GestureEventArgs e)
        {
            object selectedItem = listEntries.SelectedItem;
            if (selectedItem == null)
                return;

            string aEntry = selectedItem as string;
            if ( aEntry == null )
                return;

            IEnumerable<DictionaryEntry> entries = mCurrentDict.Entries.Where((c) => c.AEntry == aEntry);
            if (entries == null || entries.Count() == 0)
                return;

            cmdDelete.IsEnabled = true;
            cmdSave.IsEnabled = true;
            mCurrentEntry = entries.First();
            txtFirst.Text = mCurrentEntry.AEntry;
            txtSecond.Text = mCurrentEntry.BEntry;
        }


        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mCurrentEntry == null)
                    return;

                if (string.IsNullOrEmpty(txtFirst.Text))
                    throw new Exception("First value cannot be empty");

                if (string.IsNullOrEmpty(txtSecond.Text))
                    throw new Exception("Second value cannot be empty");

                mCurrentEntry.AEntry = txtFirst.Text;
                mCurrentEntry.BEntry = txtSecond.Text;
                mCurrentDict.save(false);
                populateAll();
                cmdClearFields_Click(null, null);
            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }

        private void cmdDone_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "EditedEntries";

            // We need to move back two frames to the start screen
            if ((Application.Current as App).RootFrame.BackStack.Count() > 0)
            {
                (Application.Current as App).RootFrame.RemoveBackEntry();
            }
            this.NavigationService.GoBack();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFirst.Text))
                    throw new Exception("First value cannot be empty");

                if (string.IsNullOrEmpty(txtSecond.Text))
                    throw new Exception("Second value cannot be empty");

                DictionaryEntry entry = new DictionaryEntry();
                entry.AEntry = txtFirst.Text;
                entry.BEntry = txtSecond.Text;

                IEnumerable<DictionaryEntry> entryHits = mCurrentDict.Entries.Where((c) => c.AEntry.ToLower() == txtFirst.Text.ToLower());
                if (entryHits != null && entryHits.Count() > 0)
                {
                    txtFirst.Text = "";
                    txtSecond.Text = "";
                    (Application.Current as App).ErrorMessage = "Entry already exists and will not be added";
                    this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
                    return;
                }

                mCurrentDict.addEntry(entry);
                mCurrentDict.save(true);

                listEntries.Items.Add(entry.AEntry);

                cmdClearFields_Click(null, null);
            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mCurrentEntry == null)
                    throw new Exception("Double click list to select an entry to delete");

                mCurrentDict.removeEntry(mCurrentEntry);
                mCurrentDict.save(true);

                listEntries.Items.Remove(mCurrentEntry.AEntry);

                cmdClearFields_Click(null, null);
                mCurrentEntry = null;
            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }



        private void cmdClearFields_Click(object sender, RoutedEventArgs e)
        {
            txtFirst.Text = "";
            txtSecond.Text = "";
            txtFirst.Focus();
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            cmdClearFields.IsEnabled = false;
        }



        private void txtFirst_TextChanged(object sender, TextChangedEventArgs e)
        {
            enableAdd();
            enableSave();
            enableClear();
        }

        private void txtSecond_TextChanged(object sender, TextChangedEventArgs e)
        {
            enableAdd();
            enableSave();
            enableClear();
        }

        private void enableAdd()
        {
            if (cmdAdd.IsEnabled == false)
            {
                if (string.IsNullOrEmpty(txtFirst.Text) == false &&
                    string.IsNullOrEmpty(txtSecond.Text) == false)
                {
                    if (mCurrentEntry == null || 
                        (mCurrentEntry.AEntry != txtFirst.Text ||
                        mCurrentEntry.BEntry != txtSecond.Text ) )
                    cmdAdd.IsEnabled = true;
                }
            }
            else if (cmdAdd.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(txtFirst.Text) ||
                    string.IsNullOrEmpty(txtSecond.Text))
                    cmdAdd.IsEnabled = false;

                if ( mCurrentEntry != null &&
                    (mCurrentEntry.AEntry == txtFirst.Text &&
                    mCurrentEntry.BEntry == txtSecond.Text) )
                    cmdAdd.IsEnabled = false;
            }
        }

        private void enableSave()
        {
            if (cmdSave.IsEnabled == false)
            {
                if (string.IsNullOrEmpty(txtFirst.Text) == false &&
                    string.IsNullOrEmpty(txtSecond.Text) == false)
                {
                    if ( mCurrentEntry == null ||
                        (mCurrentEntry.AEntry != txtFirst.Text ||
                        mCurrentEntry.BEntry != txtSecond.Text))
                      cmdSave.IsEnabled = true;
                }
            }
            else if (cmdAdd.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(txtFirst.Text) ||
                    string.IsNullOrEmpty(txtSecond.Text))
                    cmdSave.IsEnabled = false;
            }
        }

        private void enableClear()
        {
            if (cmdClearFields.IsEnabled == false)
            {
                if (string.IsNullOrEmpty(txtFirst.Text) == false ||
                    string.IsNullOrEmpty(txtSecond.Text) == false)
                {
                    cmdClearFields.IsEnabled = true;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text == DEFAULT_SEARCH_FIELD_TEXT)
                return;

            if (txtSearch.Text.Length > 1)
            {
                listEntries.Items.Clear();
                IEnumerable<DictionaryEntry> entries = mCurrentDict.Entries.Where((c) => c.AEntry.ToLower().Contains(txtSearch.Text.ToLower()) ||
                                                                                         c.BEntry.ToLower().Contains(txtSearch.Text.ToLower()));
                if (entries == null || entries.Count() == 0)
                    return;

                foreach (DictionaryEntry entry in entries)
                {
                    listEntries.Items.Add(entry.AEntry);
                }
            }
        }

        private void txtSearch_Tap(object sender, GestureEventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.TextAlignment = System.Windows.TextAlignment.Left;
        }


    }
}