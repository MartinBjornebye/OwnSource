using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.Crammer
{
    public partial class DictionaryOptions : Form
    {
        #region Attributes
        private CrammerDictionary mDictionary = null;
        private Font mFont1;
        private Font mFont2;
        private Color mColor1;
        private Color mColor2;
        #endregion

        #region Properties
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

        public DictionaryOptions(CrammerDictionary dictionary)
        {
            InitializeComponent();
            mDictionary = dictionary;
            Font1 = mDictionary.Font1;
            Font2 = mDictionary.Font2;
            Color1 = mDictionary.Color1;
            Color2 = mDictionary.Color2;
            label1.Font = Font1;
            label2.Font = Font2;
        }

        private void cmdSetFont1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.Font = mFont1;
            fontDialog1.Color = mColor1;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mFont1 = fontDialog1.Font;
                mColor1 = fontDialog1.Color;
                label1.Font = mFont1;
                label1.ForeColor = mColor1;
            }
        }

        private void cmdSetFont2_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.Font = mFont2;
            fontDialog1.Color = mColor2;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mFont2 = fontDialog1.Font;
                mColor2 = fontDialog1.Color;
                label2.Font = mFont2;
                label2.ForeColor = mColor2;
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            mDictionary.Font1 = Font1;
            mDictionary.Font2 = Font2;
            mDictionary.Color1 = Color1;
            mDictionary.Color2 = Color2;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void DictionaryOptions_Load(object sender, EventArgs e)
        {
            
        }
    }
}
