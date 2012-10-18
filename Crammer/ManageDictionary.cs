using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.Crammer
{
    public partial class ManageDictionary : Form
    {
        #region Attributes
        private CrammerDictionary mDictionary = new CrammerDictionary();
        private bool mDirty = false;
        #endregion

        #region Properties
        public bool Dirty
        {
            get { return mDirty; }
            set { mDirty = value; }
        }
        #endregion

        public ManageDictionary(CrammerDictionary dictionary)
        {
            InitializeComponent();

            // Load all entries, even non-active
            mDictionary.load(dictionary.DictionaryFile, false); 
        }

        private void ManageDictionary_Load(object sender, EventArgs e)
        {
            fillGridRows(mDictionary.Entries);
        }

        #region Button Handlers


        private void toolStripBtnClearFilters_Click(object sender, EventArgs e)
        {
            toolStripTxtAFilter.Text = "";
            toolStripTxtBFilter.Text = "";

            fillGridRows(mDictionary.Entries);
        }

        /// <summary>
        /// A new entry is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddEntry_Click(object sender, EventArgs e)
        {
            try
            {
                DictionaryEntry entry = new DictionaryEntry();
                entry.AEntry = txtFirst.Text;
                entry.BEntry = txtSecond.Text;

                if (mDictionary.entryExists(entry))
                {
                    MessageBox.Show("Entry already exists and will not be added", "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtFirst.Text = "";
                    txtSecond.Text = "";
                    cmdAddEntry.Enabled = false;
                    return;
                }

                cmdSave.Enabled = true;
                mDictionary.addEntry(entry);
                //mDictionary.save();
                //setDirty();

                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = entry.AEntry;
                dataGridView1.Rows[n].Cells[1].Value = entry.BEntry;
                dataGridView1.Rows[n].Cells[2].Value = entry.Active;
                dataGridView1.Rows[n].Cells[3].Value = entry;

                txtFirst.Text = "";
                txtSecond.Text = "";
                cmdAddEntry.Enabled = false;
                txtFirst.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        /// <summary>
        /// Save the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DictionaryEntry entry = (DictionaryEntry)dataGridView1.Rows[i].Cells[3].Value;
                string a = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string b = dataGridView1.Rows[i].Cells[1].Value.ToString();
                bool active = (bool)dataGridView1.Rows[i].Cells[2].Value;

                if (entry.AEntry != a || entry.BEntry != b || entry.Active != active)
                {
                    entry.AEntry = a;
                    entry.BEntry = b;
                    entry.Active = active;
                }
            }

            mDictionary.save(true);
            setDirty();
            cmdSave.Enabled = false;
        }

        #endregion


        #region Private Helpers

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cmdSave.Enabled == false)
                cmdSave.Enabled = true;
        }


        private void fillGridRows(IEnumerable<DictionaryEntry> entries)
        {
            dataGridView1.Rows.Clear();
            foreach (DictionaryEntry entry in entries)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = entry.AEntry;
                dataGridView1.Rows[n].Cells[1].Value = entry.BEntry;
                dataGridView1.Rows[n].Cells[2].Value = entry.Active;
                dataGridView1.Rows[n].Cells[3].Value = entry;
            }
        }

        private void txtFirst_TextChanged(object sender, EventArgs e)
        {
            enableAddButtonWhenTwoValues();

            if (string.IsNullOrEmpty(txtFirst.Text) || string.IsNullOrEmpty(txtSecond.Text))
                cmdAddEntry.Enabled = false;
        }

        private void txtSecond_TextChanged(object sender, EventArgs e)
        {
            enableAddButtonWhenTwoValues();

            if (string.IsNullOrEmpty(txtFirst.Text) || string.IsNullOrEmpty(txtSecond.Text))
                cmdAddEntry.Enabled = false;
        }

        private void enableAddButtonWhenTwoValues()
        {
            if (string.IsNullOrEmpty(txtFirst.Text) == false && string.IsNullOrEmpty(txtSecond.Text) == false)
            {
                cmdAddEntry.Enabled = true;
                AcceptButton = cmdAddEntry;
            }
        }

        #endregion

        #region Context Menu

        private void timestampEntryMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                if (rows != null)
                {
                    setDirty();
                    cmdSave.Enabled = true;
                    foreach (DataGridViewRow row in rows)
                    {
                        DictionaryEntry entry = (DictionaryEntry)row.Cells[3].Value;
                        entry.Stamp = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void removeMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        DictionaryEntry entry = (DictionaryEntry)row.Cells[3].Value;
                        mDictionary.removeEntry(entry);
                        cmdSave.Enabled = true;
                        dataGridView1.Rows.Remove(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void activateMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                if (rows != null)
                {
                    cmdSave.Enabled = true;
                    foreach (DataGridViewRow row in rows)
                    {
                        row.Cells[2].Value = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void deactivateMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                if (rows != null)
                {
                    cmdSave.Enabled = true;
                    foreach (DataGridViewRow row in rows)
                    {
                        row.Cells[2].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void allActiveMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                cmdSave.Enabled = true;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[2].Value = true;
            }
        }

        private void deactivateAllMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                cmdSave.Enabled = true;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[2].Value = false;
            }
        }

        /// <summary>
        /// Finds duplicates in the dictionary based on the A entry and displays them in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findDuplicatesMenuItem_Click(object sender, EventArgs e)
        {
            var query = from c in mDictionary.Entries
                        group c by c.AEntry into g
                        where g.Count() > 1
                        select new { Entry = g.Key, EntryCount = g.Count() };

            if (query == null || query.Count() == 0)
            {
                MessageBox.Show("No duplicates found", "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<DictionaryEntry> duplicateEntries = new List<DictionaryEntry>();
            foreach (var item in query)
            {
                IEnumerable<DictionaryEntry> entries = from c in mDictionary.Entries
                                                       where c.AEntry == item.Entry
                                                       select c;
                foreach (DictionaryEntry entry in entries)
                {
                    duplicateEntries.Add(entry);
                }

            }

            fillGridRows(duplicateEntries);
        }

        
        #endregion


        private void searchEntries()
        {
            if (string.IsNullOrEmpty(toolStripTxtAFilter.Text) && string.IsNullOrEmpty(toolStripTxtBFilter.Text))
            {
                fillGridRows(mDictionary.Entries);
                return;
            }

            if (string.IsNullOrEmpty(toolStripTxtAFilter.Text) == false && string.IsNullOrEmpty(toolStripTxtBFilter.Text) == false)
            {
                IEnumerable<DictionaryEntry> matches = from c in mDictionary.Entries
                                                       where c.AEntry.ToLower().Contains(toolStripTxtAFilter.Text.ToLower())
                                                       where c.BEntry.ToLower().Contains(toolStripTxtBFilter.Text.ToLower())
                                                       select c;
                fillGridRows(matches);
            }
            else if (string.IsNullOrEmpty(toolStripTxtAFilter.Text) == false)
            {
                IEnumerable<DictionaryEntry> matches = from c in mDictionary.Entries
                                                       where c.AEntry.ToLower().Contains(toolStripTxtAFilter.Text.ToLower())
                                                       select c;
                fillGridRows(matches);
            }
            else if (string.IsNullOrEmpty(toolStripTxtBFilter.Text) == false)
            {
                IEnumerable<DictionaryEntry> matches = from c in mDictionary.Entries
                                                       where c.BEntry.ToLower().Contains(toolStripTxtBFilter.Text.ToLower())
                                                       select c;
                fillGridRows(matches);
            }
        }

        private void setDirty()
        {
            if (mDirty == false)
                mDirty = true;
        }

        private void ManageDictionary_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cmdSave.Enabled == true)
            {
                if (MessageBox.Show("You have made some changes without saving. Do you want to skip saving?", "Crammer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

        }

        private void toolStripTxtAFilter_TextChanged(object sender, EventArgs e)
        {
            searchEntries();
        }

        private void toolStripTxtBFilter_TextChanged(object sender, EventArgs e)
        {
            searchEntries();
        }



    }
}
