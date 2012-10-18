using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.Crammer
{
    public partial class DictionaryHistory : Form
    {
        #region Attributes
        private string mSelectedDictionary = "";
        #endregion

        #region Properties
        public string SelectedDictionary
        {
            get { return mSelectedDictionary; }
            set { mSelectedDictionary = value; }
        }
        #endregion

        public DictionaryHistory()
        {
            InitializeComponent();
        }

        private void DictionaryHistory_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string file in Properties.Settings.Default.DictionaryHistory)
            {
                if ( string.IsNullOrEmpty(file) == false )
                    listBox1.Items.Add(file);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try{

                if ( listBox1.Items.Count == 0 )
                    return;

                if (listBox1.SelectedItems.Count == 0)
                    return;

                string selectedPath = (string)listBox1.SelectedItem;
                if (File.Exists(selectedPath) == false)
                    throw new Exception("Dictionary with path:\n" + selectedPath + "\nno longer seems to exist or is no longer accessible");

                mSelectedDictionary = selectedPath;
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0 || listBox1.SelectedItems.Count == 0)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            listBox1_DoubleClick(sender, e);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void removeMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0 || listBox1.SelectedItems.Count == 0)
                return;

            string item = (string)listBox1.SelectedItem;
            Properties.Settings.Default.DictionaryHistory.Remove(item);
            listBox1.Items.Remove(item);
        }
    }
}
