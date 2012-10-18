using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO.IsolatedStorage;            // for isolated storage
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Crammer
{
    public class Settings
    {
        #region Attributes
        private const string mSettingsFilename = "CrammerSettings.xml";
        private XElement mDictionaryContents = null;
        #endregion

        #region Properties
        public XElement DictionaryContents
        {
            get { return mDictionaryContents; }
            set { mDictionaryContents = value; }
        }
        #endregion

        public Settings()
        {
        }

        public void save()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream stream = storage.CreateFile(mSettingsFilename); 
            XmlSerializer xml = new XmlSerializer(GetType()); 
            xml.Serialize(stream, this); 
            stream.Close(); 
            stream.Dispose();
        }

        public static Settings load()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication(); 
            Settings settings;
            if (storage.FileExists(mSettingsFilename))
            {
                IsolatedStorageFileStream stream =
                storage.OpenFile(mSettingsFilename, FileMode.Open); 
                XmlSerializer xml = new XmlSerializer(typeof(Settings)); 
                settings = xml.Deserialize(stream) as Settings; 
                stream.Close(); 
                stream.Dispose();
            }
            else
            {
                settings = new Settings();
            }
            return settings;
        }
    }


}
