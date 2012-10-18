using System;
//using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MB.Crammer
{
    class DictionaryHeader
    {
        #region Attributes
        private string mTitle = "";
        //private Font mNormal;
        //private Font mCheck;
        #endregion

        public string Title
        {
            get { return (mTitle); }
            set { mTitle = value; }
        }

        //public Font Normal
        //{
        //    get { return (mNormal); }
        //    set { mNormal = value; }
        //}

        //public Font Check
        //{
        //    get { return (mCheck); }
        //    set { mCheck = value; }
        //}
    }
}
