using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
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

namespace MB.Crammer
{
    public static class StorageIOHelper
    {
        public static XElement getDictionary(string dictionaryFilePath)
        {
            XElement dictionaryXML = null;
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (dictionaryFilePath.EndsWith(CrammerDictionary.DICT_EXTENSION) == false)
                    dictionaryFilePath += CrammerDictionary.DICT_EXTENSION;

                using (IsolatedStorageFileStream dictStream = storage.OpenFile(dictionaryFilePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(dictStream))
                    {
                        // Read the data.
                        StringBuilder dictContents = new StringBuilder(reader.ReadToEnd());
                        dictContents.Replace('\0', ' ');
                        dictionaryXML = XElement.Parse(dictContents.ToString());
                        reader.Close();
                    }
                    dictStream.Close();
                }
            }
            return (dictionaryXML);
        }

        public static void saveDictionary(string dictionaryFilePath, XElement dictContents)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (dictionaryFilePath.EndsWith(CrammerDictionary.DICT_EXTENSION) == false)
                    dictionaryFilePath += CrammerDictionary.DICT_EXTENSION;

                if (storage.FileExists(dictionaryFilePath))
                    storage.DeleteFile(dictionaryFilePath);

                using (IsolatedStorageFileStream dictStream = storage.OpenFile(dictionaryFilePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(dictStream))
                    {
                        // Read the data.
                        writer.Write(dictContents.ToString());
                        writer.Close();
                    }
                    dictStream.Close();
                }
            }
        }
    }
}
