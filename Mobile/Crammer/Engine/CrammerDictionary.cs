////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Source:      CrammerDictionary.cs
// Author:      Martin Bjornebye
// Description: This class wraps management of the Crammer dictionaries, such as adding, editing and 
//              removing entries.
//
// Created:     2011.01.08 14:01:11
////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;


namespace MB.Crammer
{
    public class CrammerDictionary
    {

        #region Constants
        private const string DEFAULT_DICTIONARY = "Dictionary/Worlds Capitals.crammerdict";
        private const string DICT_CONTENT = "DictionaryContent";
        public const string DICT_EXTENSION = ".crammerdict";
        private const string DICT_ROOT = "Root";
        private const string DICT_HEADER = "Header";
        private const string DICT_TITLE = "Title";
        private const string DICT_ENTRIES = "Entries";
        private const string DICT_FONTS = "Fonts";
        public const string DICT_ROW = "r";
        public const string DICT_A = "a";
        public const string DICT_B = "b";
        public const string DICT_STAMP = "s";
        public const string DICT_ACTIVE = "on";
        public const string ACTIVE = "1";
        public const string INACTIVE = "0";
        private const string DEFAULT_FONT = "Times New Roman";
        public const float DEFAULT_FONT_SIZE = 32;
        private const int DEFAULT_A_FONT_COLOR = -16777216;
        private const int DEFAULT_B_FONT_COLOR = -16744448;

        private const string FONT_A = "a";
        private const string FONT_B = "b";
        private const string FONT_NAME = "Name";
        private const string FONT_SIZE = "Size";
        private const string FONT_COLOR = "Color";
        private const string FONT_BOLD = "Bold";

        #endregion

        #region Attributes
        private XElement mDictionary = null;
        private List<DictionaryEntry> mEntries = new List<DictionaryEntry>();
        private List<DictionaryEntry> mInactiveEntries = new List<DictionaryEntry>();
        private string mDictionaryTitle = "";
        private string mDictionaryName = "";
        private string mStateFile = "";
        //private string mFont1 = new Font(DEFAULT_FONT, DEFAULT_FONT_SIZE, FontStyle.Bold);
        //private string mFont2 = new Font(DEFAULT_FONT, DEFAULT_FONT_SIZE, FontStyle.Bold);
        private Color mColor1 = Colors.White;
        private Color mColor2 = Colors.Purple;
        private double mFontSizeNormal = 32;
        private double mFontSizeNew = 40;
        #endregion

        #region Properties

        public bool Empty
        {
            get { return (mEntries.Count == 0); }
        }

        public string DictionaryFile
        {
            get { return mDictionaryName; }
            set { mDictionaryName = value; }
        }

        public string DictionaryTitle
        {
            get { return mDictionaryTitle; }
            set { mDictionaryTitle = value; }
        }

        public List<DictionaryEntry> Entries
        {
            get { return (mEntries); }
        }

        public List<DictionaryEntry> InactiveEntries
        {
            get { return (mInactiveEntries); }
        }

        public string StateFile
        {
            get { return mStateFile; }
            set { mStateFile = value; }
        }
        //public string Font1
        //{
        //    get { return mFont1; }
        //    set { mFont1 = value; }
        //}

        //public Font Font2
        //{
        //    get { return mFont2; }
        //    set { mFont2 = value; }
        //}

        public Color Color1
        {
            get { return mColor1; }
            set { mColor1 = value; }
        }

        public Color Color2
        {
            get { return mColor2; }
            set { mColor2 = value; }
        }
        public double FontSizeNormal
        {
            get { return mFontSizeNormal; }
            set { mFontSizeNormal = value; }
        }

        public double FontSizeNew
        {
            get { return mFontSizeNew; }
            set { mFontSizeNew = value; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Load a Crammer dictionary
        /// </summary>
        /// <param name="dictionary"></param>
        public void load(bool activeOnly)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.TryGetValue<string>(CrammerConstants.CURRENT_DICT_NAME, out mDictionaryName))
            {
                mDictionary = XElement.Load(DEFAULT_DICTIONARY);
            }
            else
            {
                //if (!settings.TryGetValue<XElement>(mDictionaryName, out mDictionary))
                //    throw new Exception("Failed to load dictionary: " + mDictionaryName);
                mDictionary = StorageIOHelper.getDictionary(mDictionaryName);
            }

            getDictionaryTitle();
            createStateFile();

            IEnumerable<XElement> rows = mDictionary.Descendants(DICT_ROW);
            mEntries.Clear();
            mInactiveEntries.Clear();
            foreach (XElement row in rows)
            {
                DictionaryEntry entry = new DictionaryEntry(row);
                if (activeOnly )
                {
                    if (entry.Active)
                        mEntries.Add(entry);
                    else
                        mInactiveEntries.Add(entry);
                }
                else
                {
                    mEntries.Add(entry);
                }
            }

            setFonts();
        }


        public void reLoad()
        {
            load(true);
        }

        /// <summary>
        /// Saves the current contents by overwriting the existing dictionary file with updated entries.
        /// By default, the statefile is truncated so user has to start over.
        /// </summary>
        public void save(bool truncateStateFile)
        {
            if (string.IsNullOrEmpty(mDictionaryTitle))
                throw new Exception("This dictionary does not have a valid name");

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(DICT_CONTENT))
                settings.Remove(DICT_CONTENT);

            if (truncateStateFile)
            {
                if (settings.Contains(mStateFile))
                    settings.Remove(mStateFile);
            }

            create(mEntries);
        }

        /// <summary>
        /// Creates a new dictionary based on a list of entries and a file name
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="entries"></param>
        public void create(List<DictionaryEntry> entries)
        {
            XElement nodeEntries = null;
            XDocument dictionaryFile = new XDocument(
                            new XDeclaration("1.0", "UTF-16", null),
                            new XElement(DICT_ROOT,
                            new XElement(DICT_HEADER,
                                new XElement(DICT_TITLE, mDictionaryTitle),
                                new XElement(DICT_FONTS,
                                   new XElement(FONT_A,
                                        new XAttribute(FONT_NAME, "Default"),
                                        new XAttribute(FONT_SIZE, mFontSizeNormal.ToString()),
                                        new XAttribute(FONT_COLOR, mColor1.ToString()),
                                        new XAttribute(FONT_BOLD, "False")),
                                        //new XAttribute(FONT_NAME, mFont1.Name),
                                        //new XAttribute(FONT_SIZE, mFont1.Size.ToString()),
                                        //new XAttribute(FONT_COLOR, mColor1.ToArgb().ToString()),
                                        //new XAttribute(FONT_BOLD, mFont1.Bold.ToString())),
                                   new XElement(FONT_B,
                                        new XAttribute(FONT_NAME, "Default"),
                                        new XAttribute(FONT_SIZE, mFontSizeNew.ToString()),
                                        new XAttribute(FONT_COLOR, mColor2.ToString()),
                                        new XAttribute(FONT_BOLD, "False")))),
                                        //new XAttribute(FONT_NAME, mFont2.Name),
                                        //new XAttribute(FONT_SIZE, mFont2.Size.ToString()),
                                        //new XAttribute(FONT_COLOR, mColor2.ToArgb().ToString()),
                                        //new XAttribute(FONT_BOLD, mFont2.Bold.ToString())))),
                            nodeEntries = new XElement(DICT_ENTRIES)));

            foreach (DictionaryEntry entry in entries)
            {
                XElement entryElem = new XElement(DICT_ROW,
                                        new XAttribute(DICT_A, entry.AEntry),
                                        new XAttribute(DICT_B, entry.BEntry),
                                        new XAttribute(DICT_STAMP, entry.Stamp.Ticks.ToString()),
                                        new XAttribute(DICT_ACTIVE, entry.Active ? ACTIVE : INACTIVE));

                nodeEntries.Add(entryElem);
            }

            mDictionary = dictionaryFile.Root;

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            string fileList = "";
            if ( settings.Contains(CrammerConstants.DICTIONARY_LIST))
                fileList = settings[CrammerConstants.DICTIONARY_LIST] as string;

            fileList += mDictionaryTitle + ";";
            settings[CrammerConstants.DICTIONARY_LIST] = fileList;
            settings[CrammerConstants.CURRENT_DICT_NAME] = mDictionaryTitle; 
            //settings[mDictionaryTitle] = mDictionary;
            settings.Save();

            StorageIOHelper.saveDictionary(mDictionaryTitle, mDictionary);
        }


        /// <summary>
        /// Adds a specific entry
        /// </summary>
        /// <param name="entry"></param>
        public void addEntry(DictionaryEntry entry)
        {
            // Check if entry already exists
            if (entryExists(entry))
                throw new Exception("An entry with values: " + entry.AEntry + " / " + entry.BEntry + " already exists");

            mEntries.Add(entry);
        }

        public void removeEntry(DictionaryEntry entry)
        {
            if (!entryExists(entry))
                throw new Exception("An entry with values: " + entry.AEntry + " / " + entry.BEntry + " does not exist in the dictionary");

            mEntries.Remove(entry);
            save(true);
        }

        public bool entryExists(DictionaryEntry entry)
        {
            IEnumerable<DictionaryEntry> matches = from c in mEntries 
                                                   where c.AEntry == entry.AEntry
                                                   select c;
            return ( matches.Count() > 0 );
        }

        public void updateTimeStamp(int wordIndex)
        {
            if (wordIndex < 0 || wordIndex >= mEntries.Count)
                throw new Exception("Word index out of bounds");

            DictionaryEntry currentEntry = mEntries[wordIndex];
            currentEntry.Stamp = DateTime.Now;
            save(false);
        }

        /// <summary>
        /// Sets the font for either of the a or b entries
        /// </summary>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="isFontA"></param>
        public void setFont()
        {
            XElement header = mDictionary.Element(DICT_HEADER);
            if (header == null)
                throw new Exception("Failed to find a Header entry in the dictionary");

            //addFontElement(header, FONT_A, mFont1, mColor1);
            //addFontElement(header, FONT_B, mFont2, mColor2);
            addFontElement(header, FONT_A);
            addFontElement(header, FONT_B);

            //mDictionary.Save(mDictionaryName);
        }

        public static void removeStateFiles()
        {
            //foreach (string stateFile in Properties.Settings.Default.DictionaryHistory)
            //{
            //    if (File.Exists(stateFile))
            //        File.Delete(stateFile);
            //}
        }

        #region Sorting Functions

        public void randomShuffle()
        {
            mEntries = mEntries.OrderBy(emp => Guid.NewGuid()).ToList();
            save(true);
        }

        public void sortAscending()
        {
            mEntries = mEntries.OrderBy(c => c.AEntry).ToList();
            save(true);
        }

        public void sortDescending()
        {
            mEntries = mEntries.OrderByDescending(c => c.AEntry).ToList();
            save(true);
        }

        public void sortByTimestampAscending()
        {
            mEntries = mEntries.OrderBy(c => c.Stamp).ToList();
            save(true);
        }

        public void sortByTimestampDescending()
        {
            mEntries = mEntries.OrderByDescending(c => c.Stamp).ToList();
            save(true);
        }
        #endregion


        #endregion

        #region Private Functions

        /// <summary>
        /// Picks up the Dictionary title
        /// </summary>
        private void getDictionaryTitle()
        {
            XElement header = mDictionary.Element(DICT_HEADER);
            if (header == null)
                throw new Exception("Failed to find a Header entry in the dictionary");

            XElement title = header.Element(DICT_TITLE);
            if (title == null)
                throw new Exception("Failed to find a Title entry in the dictionary");

            mDictionaryTitle = title.Value;
        }

        //private void addFontElement(XElement header, string fontName, Font f, Color c)
        private void addFontElement(XElement header, string fontName)
        {
            XElement fontSection = header.Element(DICT_FONTS);
            if (fontSection == null)
            {
                fontSection = new XElement(DICT_FONTS);
                header.Add(fontSection);
            }

            XElement font = fontSection.Element(fontName);
            if (font == null)
            {
                font = new XElement(fontName,
                            new XAttribute(FONT_NAME, "Default"),
                            new XAttribute(FONT_SIZE, "20"),
                            new XAttribute(FONT_COLOR, "FFEEAA"),
                            new XAttribute(FONT_BOLD, "False"));
                            //new XAttribute(FONT_NAME, f.Name),
                            //new XAttribute(FONT_SIZE, f.Size.ToString()),
                            //new XAttribute(FONT_COLOR, c.ToArgb().ToString()),
                            //new XAttribute(FONT_BOLD, f.Bold.ToString()));
                fontSection.Add(font);
            }
            else 
            {
                font.SetAttributeValue(FONT_NAME, "Default");
                font.SetAttributeValue(FONT_SIZE, mFontSizeNormal.ToString());
                font.SetAttributeValue(FONT_COLOR, "FFEEAA");
                font.SetAttributeValue(FONT_BOLD, "False");
                //font.SetAttributeValue(FONT_NAME, f.Name);
                //font.SetAttributeValue(FONT_SIZE, f.Size.ToString());
                //font.SetAttributeValue(FONT_COLOR, c.ToArgb().ToString());
                //font.SetAttributeValue(FONT_BOLD, f.Bold.ToString());
            }
        }


        private void setFonts()
        {
            setFont(FONT_A);
            setFont(FONT_B);
        }

        private void setFont(string font)
        {
            XElement header = mDictionary.Element(DICT_HEADER);
            if (header == null)
                throw new Exception("Failed to find a Header entry in the dictionary");

            XElement fontSection = header.Element(DICT_FONTS);
            if (fontSection == null)
                throw new Exception("No Fonts section in the header");

            XElement f1 = fontSection.Element(font);
            if (f1 == null)
                throw new Exception("No Font " + font + " section in the header");

            if (font == FONT_A)
            {
                bool boldTrue = bool.Parse(f1.Attribute(FONT_BOLD).Value);
                mFontSizeNormal = MiscUtil.safeFloatConvert(f1.Attribute(FONT_SIZE).Value);

                //mFont1 = new Font(f1.Attribute(FONT_NAME).Value,
                //             MiscUtil.safeFloatConvert(f1.Attribute(FONT_SIZE).Value),
                //             boldTrue == true ? FontStyle.Bold : FontStyle.Regular);

                //mColor1 = Color.FromArgb(int.Parse(f1.Attribute(FONT_COLOR).Value));
            }
            else
            {
                bool boldTrue = bool.Parse(f1.Attribute(FONT_BOLD).Value);
                mFontSizeNew = MiscUtil.safeFloatConvert(f1.Attribute(FONT_SIZE).Value);
                //mFont2 = new Font(f1.Attribute(FONT_NAME).Value,
                //             MiscUtil.safeFloatConvert(f1.Attribute(FONT_SIZE).Value),
                //             boldTrue == true ? FontStyle.Bold : FontStyle.Regular);

                //mColor2 = Color.FromArgb(int.Parse(f1.Attribute(FONT_COLOR).Value));
            }
        }

        private void createStateFile()
        {
            mStateFile = mDictionaryTitle + CrammerConstants.STATE_FILE_EXT;
        }


        #endregion

    }
}
