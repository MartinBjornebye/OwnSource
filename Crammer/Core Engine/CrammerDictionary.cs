////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Source:      CrammerDictionary.cs
// Author:      Martin Bjornebye
// Description: This class wraps management of the Crammer dictionaries, such as adding, editing and 
//              removing entries.
//
// Created:     2011.01.08 14:01:11
////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace MB.Crammer
{
    public class CrammerDictionary
    {

        #region Constants
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
        public const float DEFAULT_FONT_SIZE = 20;
        private const int DEFAULT_A_FONT_COLOR = -16777216;
        private const int DEFAULT_B_FONT_COLOR = -16744448;

        private const string FONT_A = "a";
        private const string FONT_B = "b";
        private const string FONT_NAME = "Name";
        private const string FONT_SIZE = "Size";
        private const string FONT_COLOR = "Color";
        private const string FONT_BOLD = "Bold";

        public const string STATE_FILE_EXT = ".statefile";
        private const int OUTPUT_ALLOC_SIZE = 5000000;

        #endregion

        #region Attributes
        private XElement mDictionary = null;
        private List<DictionaryEntry> mEntries = new List<DictionaryEntry>();
        private List<DictionaryEntry> mInactiveEntries = new List<DictionaryEntry>();
        private string mDictionaryTitle = "";
        private string mDictionaryFile = "";
        private string mStateFile = "";
        private Font mFont1 = new Font(DEFAULT_FONT, DEFAULT_FONT_SIZE, FontStyle.Bold);
        private Font mFont2 = new Font(DEFAULT_FONT, DEFAULT_FONT_SIZE, FontStyle.Bold);
        private Color mColor1 = Color.FromArgb(DEFAULT_A_FONT_COLOR);
        private Color mColor2 = Color.FromArgb(DEFAULT_B_FONT_COLOR);
        #endregion

        #region Properties

        public bool Empty
        {
            get { return (mEntries.Count == 0); }
        }

        public string DictionaryFile
        {
            get { return mDictionaryFile; }
            set { mDictionaryFile = value; }
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
        public Font Font1
        {
            get { return mFont1; }
            set { mFont1 = value; }
        }

        public Font Font2
        {
            get { return mFont2; }
            set { mFont2 = value; }
        }

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
        #endregion

        #region Public Functions

        /// <summary>
        /// Load a Crammer dictionary
        /// </summary>
        /// <param name="dictionary"></param>
        public void load(string dictionary, bool activeOnly)
        {
            if (string.IsNullOrEmpty(dictionary))
                throw new Exception("No dictionary name given");

            if (File.Exists(dictionary) == false)
                throw new Exception("Dictionary file: " + dictionary + " does not exist or is not available");

            mDictionaryFile = dictionary;
            createStateFile(dictionary);

            mDictionary = XElement.Load(dictionary);
            getDictionaryTitle();

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
            load(mDictionaryFile, true);
        }

        /// <summary>
        /// Saves the current contents by overwriting the existing dictionary file with updated entries.
        /// By default, the statefile is truncated so user has to start over.
        /// </summary>
        public void save(bool truncateStateFile)
        {
            if (string.IsNullOrEmpty(mDictionaryFile))
                throw new Exception("This dictionary does not have a file name and is not valid");

            if (File.Exists(mDictionaryFile))
                File.Delete(mDictionaryFile);

            if (truncateStateFile)
            {
                if (File.Exists(mStateFile))
                    File.Delete(mStateFile);
            }

            create(mDictionaryFile, mEntries);
        }

        /// <summary>
        /// Creates a new dictionary based on a list of entries and a file name
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="entries"></param>
        public void create(string dictionary, List<DictionaryEntry> entries)
        {
            XElement nodeEntries = null;
            XDocument dictionaryFile = new XDocument(
                            new XDeclaration("1.0", "UTF-16", null),
                            new XElement(DICT_ROOT,
                            new XElement(DICT_HEADER,
                                new XElement(DICT_TITLE, mDictionaryTitle),
                                new XElement(DICT_FONTS,
                                   new XElement(FONT_A,
                                        new XAttribute(FONT_NAME, mFont1.Name),
                                        new XAttribute(FONT_SIZE, mFont1.Size.ToString()),
                                        new XAttribute(FONT_COLOR, mColor1.ToArgb().ToString()),
                                        new XAttribute(FONT_BOLD, mFont1.Bold.ToString())),
                                   new XElement(FONT_B,
                                        new XAttribute(FONT_NAME, mFont2.Name),
                                        new XAttribute(FONT_SIZE, mFont2.Size.ToString()),
                                        new XAttribute(FONT_COLOR, mColor2.ToArgb().ToString()),
                                        new XAttribute(FONT_BOLD, mFont2.Bold.ToString())))),
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

            
            dictionaryFile.Save(dictionary);
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

            addFontElement(header, FONT_A, mFont1, mColor1);
            addFontElement(header, FONT_B, mFont2, mColor2);
            mDictionary.Save(mDictionaryFile);
        }

        /// <summary>
        /// Iterate over all dictionaries, create related statefile names and delete them
        /// </summary>
        public static void removeStateFiles()
        {
            string stateFilePath = "";
            foreach (string dictionaryFile in Properties.Settings.Default.DictionaryHistory)
            {
                string withoutExtension = Path.GetFileNameWithoutExtension(dictionaryFile);
                stateFilePath = Path.Combine(Path.GetDirectoryName(dictionaryFile), withoutExtension) + CrammerDictionary.STATE_FILE_EXT;
                if (File.Exists(stateFilePath))
                    File.Delete(stateFilePath);
            }
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

        #region Export Functions

        /// <summary>
        /// Export to Xml format
        /// </summary>
        /// <param name="xmlFile"></param>
        public void exportToXml(string xmlFile)
        {
            XElement nodeEntries = null;
            XDocument dictionaryFile = new XDocument(
                            new XDeclaration("1.0", "UTF-16", null),
                            new XElement(DICT_ROOT,
                            nodeEntries = new XElement(DICT_ENTRIES)));

            foreach (DictionaryEntry entry in mEntries)
            {
                XElement entryElem = new XElement(DICT_ROW,
                                        new XAttribute(DICT_A, entry.AEntry),
                                        new XAttribute(DICT_B, entry.BEntry));

                nodeEntries.Add(entryElem);
            }

            dictionaryFile.Save(xmlFile);
        }

        /// <summary>
        /// Export dictionary to delimited text format
        /// </summary>
        /// <param name="outputFile"></param>
        /// <param name="delimiter"></param>
        public void exportToTextDelimited(string outputFile, string delimiter)
        {
            StringBuilder sb = new StringBuilder(OUTPUT_ALLOC_SIZE);
            foreach (DictionaryEntry entry in mEntries)
            {
                sb.Append(entry.AEntry + delimiter + entry.BEntry + "\r\n");
            }

            File.WriteAllText(outputFile, sb.ToString());
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

        private void addFontElement(XElement header, string fontName, Font f, Color c)
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
                            new XAttribute(FONT_NAME, f.Name),
                            new XAttribute(FONT_SIZE, f.Size.ToString()),
                            new XAttribute(FONT_COLOR, c.ToArgb().ToString()),
                            new XAttribute(FONT_BOLD, f.Bold.ToString()));
                fontSection.Add(font);
            }
            else 
            {
                font.SetAttributeValue(FONT_NAME, f.Name);
                font.SetAttributeValue(FONT_SIZE, f.Size.ToString());
                font.SetAttributeValue(FONT_COLOR, c.ToArgb().ToString());
                font.SetAttributeValue(FONT_BOLD, f.Bold.ToString());
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


                mFont1 = new Font(f1.Attribute(FONT_NAME).Value,
                             MiscUtil.safeFloatConvert(f1.Attribute(FONT_SIZE).Value),
                             boldTrue == true ? FontStyle.Bold : FontStyle.Regular);

                if (f1.Attribute(FONT_COLOR) != null)
                {
                    int colorValue = 0;
                    if ( int.TryParse(f1.Attribute(FONT_COLOR).Value, out colorValue))
                      mColor1 = Color.FromArgb(int.Parse(f1.Attribute(FONT_COLOR).Value));
                }
            }
            else
            {
                bool boldTrue = bool.Parse(f1.Attribute(FONT_BOLD).Value);
                mFont2 = new Font(f1.Attribute(FONT_NAME).Value,
                             MiscUtil.safeFloatConvert(f1.Attribute(FONT_SIZE).Value),
                             boldTrue == true ? FontStyle.Bold : FontStyle.Regular);

                if (f1.Attribute(FONT_COLOR) != null)
                {
                    int colorValue = 0;
                    if (int.TryParse(f1.Attribute(FONT_COLOR).Value, out colorValue))
                        mColor2 = Color.FromArgb(int.Parse(f1.Attribute(FONT_COLOR).Value));
                }
            }
        }

        private void createStateFile(string dictionary)
        {
            // Derive a state file from dictionary
            string tmpDir = Path.GetDirectoryName(dictionary);
            string bareFileName = Path.GetFileNameWithoutExtension(dictionary);
            mStateFile = Path.Combine(tmpDir, bareFileName) + STATE_FILE_EXT;
        }


        #endregion

    }
}
