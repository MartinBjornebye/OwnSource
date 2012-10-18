using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MB.Crammer
{
    public class DictionaryEntry
    {
        #region Attributes
        private string mA = "";
        private string mB = "";
        private DateTime mStamp = DateTime.Now;
        private bool mActive = true;
        #endregion

        #region Properties
        public string AEntry
        {
            get { return mA; }
            set { mA = value; }
        }
        public string BEntry
        {
            get { return mB; }
            set { mB = value; }
        }
        public DateTime Stamp
        {
            get { return mStamp; }
            set { mStamp = value; }
        }
        public bool Active
        {
            get { return mActive; }
            set { mActive = value; }
        }
        #endregion

        public DictionaryEntry()
        {
        }

        public DictionaryEntry(XElement entry)
        {
            if (entry.HasAttributes == false)
                throw new Exception("Detected entry without valid values");

            if ( entry.Attribute(CrammerDictionary.DICT_A) == null )
                throw new Exception("Detected entry without a valid question value");

            if (entry.Attribute(CrammerDictionary.DICT_B) == null)
                throw new Exception("Detected entry without a valid answer value");

            if (entry.Attribute(CrammerDictionary.DICT_STAMP) == null)
                throw new Exception("Detected entry without a valid stamp value");

            if (entry.Attribute(CrammerDictionary.DICT_ACTIVE) == null)
                throw new Exception("Detected entry without a valid active flag");

            mA = entry.Attribute(CrammerDictionary.DICT_A).Value;
            mB = entry.Attribute(CrammerDictionary.DICT_B).Value;
            long ticks = long.Parse(entry.Attribute(CrammerDictionary.DICT_STAMP).Value);
            mStamp = new DateTime(ticks);
            mActive = entry.Attribute(CrammerDictionary.DICT_ACTIVE).Value == CrammerDictionary.ACTIVE;
        }
    }
}
