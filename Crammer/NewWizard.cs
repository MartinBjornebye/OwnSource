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
    public partial class NewWizard : Form
    {
        #region Attributes
        private List<DictionaryEntry> mDictEntries = new List<DictionaryEntry>();
        private CrammerDictionary mCrammerDict = new CrammerDictionary();
        #endregion

        #region Properties
        public string DictionaryTitle
        {
            get { return (txtTitle.Text); }
        }
        public string DictionaryPath
        {
            get { return (txtDictPath.Text); }
        }

        internal List<DictionaryEntry> DictionaryEntries
        {
            get { return mDictEntries; }
            set { mDictEntries = value; }
        }
        #endregion

        public NewWizard()
        {
            InitializeComponent();
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPath)
            {
                tabControl1.SelectedTab = tabImport;
            }
            else if (tabControl1.SelectedTab == tabImport)
            {
                tabControl1.SelectedTab = tabTitle;
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabImport)
            {
                tabControl1.SelectedTab = tabPath;
            }
            else if (tabControl1.SelectedTab == tabTitle)
            {
                tabControl1.SelectedTab = tabImport;
            }
        }

        private void cmdFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    tabControl1.SelectedTab = tabTitle;
                    throw new Exception("You must provide a title to the dictionary");
                }

                if (string.IsNullOrEmpty(txtDictPath.Text))
                {
                    tabControl1.SelectedTab = tabPath;
                    throw new Exception("You must provide a valid path for the dictionary");
                }

                if (File.Exists(txtDictPath.Text))
                {
                    if (MessageBox.Show("Dictionary " + txtDictPath.Text + " already exists. Do you want to overwrite?",
                                        "Crammer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;

                    File.Delete(txtDictPath.Text);
                }

                string dir = Path.GetDirectoryName(txtDictPath.Text);
                if (string.IsNullOrEmpty(dir))
                {
                    tabControl1.SelectedTab = tabPath;
                    throw new Exception("You haven't specified a directory for the dictionary");
                }

                if (string.IsNullOrEmpty(txtImportFile.Text) == false)
                {
                  importFile();
                }

                mCrammerDict.DictionaryTitle = txtTitle.Text;
                mCrammerDict.create(txtDictPath.Text, mDictEntries);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab == tabTitle)
            {
                cmdNext.Visible = true;
                AcceptButton = cmdNext;
                cmdBack.Enabled = false;
                cmdFinish.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabImport)
            {
                cmdBack.Enabled = true;
                cmdNext.Visible = true;
                AcceptButton = cmdNext;
                cmdFinish.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPath)
            {
                cmdFinish.Visible = true;
                AcceptButton = cmdFinish;
                cmdNext.Visible = false;
                cmdBack.Enabled = true;
            }
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void chkImport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImport.Checked)
            {
                cmdBrowse.Enabled = true;
                txtImportFile.Enabled = true;
                grpDividers.Enabled = true;
            }
            else
            {
                cmdBrowse.Enabled = false;
                txtImportFile.Enabled = false;
                grpDividers.Enabled = false;
            }
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult dlgResult = openFileDialog1.ShowDialog();
                if (dlgResult == System.Windows.Forms.DialogResult.OK )
                {
                    txtImportFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            string txtTmp = txtDictPath.Text;
            txtDictPath.Text = Path.Combine(folderBrowserDialog1.SelectedPath, txtTmp);
        }


        /// <summary>
        /// Imports an external text file with delimited entries to create a new dictionary.
        /// </summary>
        private void importFile()
        {
            mDictEntries.Clear();

            string importFile = txtImportFile.Text;
            if ( string.IsNullOrEmpty(importFile))
                throw new Exception("Please provide a valid file as input for the dictionary");

            if ( File.Exists(importFile) == false)
                throw new Exception("File " + importFile + " is not valid or is not accessible");

            string[] delimiter = null;
            if (rbCSV.Checked)
                delimiter = new string[] {";"};
            else
            {
                if (string.IsNullOrEmpty(txtDivider.Text))
                    throw new Exception("You must specify a delimiter string");

                delimiter = new string[] { txtDivider.Text };
            }

            string[] fileLines = File.ReadAllLines(importFile, Encoding.Default);
            if (fileLines.Length == 0)
                throw new Exception("File " + importFile + " does not contain any valid lines");

            // Use current date ticks and increment by one to distinguish entries which may otherwise 
            // get an exact same tick value depending on the speed of the processor.
            long currentTimeStampTicks = DateTime.Now.Ticks;
            foreach (string line in fileLines)
            {
                string entry = line.Trim();
                if (string.IsNullOrEmpty(entry))
                    continue;

                string[] elems = entry.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (elems.Length < 2)
                    continue;

                DictionaryEntry dictEntry = new DictionaryEntry();

                if ( string.IsNullOrEmpty(elems[0]) == false )
                    dictEntry.AEntry = elems[0].Trim();
                else
                    dictEntry.AEntry = elems[0];

                if ( string.IsNullOrEmpty(elems[1]) == false )
                    dictEntry.BEntry = elems[1].Trim();
                else
                    dictEntry.BEntry = elems[1];

                dictEntry.Stamp = new DateTime(currentTimeStampTicks++);

                mDictEntries.Add(dictEntry);
            }

            if (mDictEntries.Count == 0)
            {
                tabControl1.SelectedTab = tabImport;
                throw new Exception("File " + importFile + " did not yield any valid entries");
            }

            MessageBox.Show("Successfully imported " + mDictEntries.Count + " entries from:\r\n" + importFile, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void NewWizard_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabTitle;
            AcceptButton = cmdNext;
            txtTitle.Focus();
        }

        private void rbCSV_CheckedChanged(object sender, EventArgs e)
        {
            txtDivider.Enabled = !rbCSV.Checked;
        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustom.Checked == true)
            {
                txtDivider.Enabled = rbCustom.Checked;
                txtDivider.Focus();
            }

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
                return;

            txtDictPath.Text = txtTitle.Text + CrammerDictionary.DICT_EXTENSION;
        }


    }
}
