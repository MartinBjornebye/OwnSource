using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;

using MB.Crammer;

namespace Crammer
{
    public partial class DictHistory : PhoneApplicationPage
    {
        public DictHistory()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(CrammerConstants.DICTIONARY_LIST) == false)
            {
                listDictionaries.Items.Add("[No Dictionaries Found]");
                return;
            }

            string fileDelimiter = settings[CrammerConstants.DICTIONARY_LIST] as string;
            List<string> fileList = fileDelimiter.Split(new char[]{';'}).ToList();
            foreach (string dictionary in fileList)
            {
                listDictionaries.Items.Add(dictionary);
            }

            //IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
            //string[] dictionaries = storage.GetFileNames();
            //if (dictionaries == null || dictionaries.Length == 0)
            //    listDictionaries.Items.Add("[No Dictionaries Found]");
            //else
            //{
            //    foreach (string dictionary in dictionaries)
            //    {
            //        if (dictionary.EndsWith(CrammerConstants.STATE_FILE_EXT))
            //            continue;

            //        listDictionaries.Items.Add(dictionary);
            //    }
            //}

        }

        private void listDictionaries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}