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
    public partial class ExportDict : Form
    {

        #region Attributes
        private CrammerDictionary mDictionary = null;
        #endregion

        public ExportDict(CrammerDictionary dict)
        {
            InitializeComponent();
            mDictionary = dict;
        }


        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPath.Text))
                    throw new Exception("No valid file name provided");

                if (string.IsNullOrEmpty(Path.GetDirectoryName(txtPath.Text)))
                    throw new Exception("Please provide a valid directory to the file-name");

                if (string.IsNullOrEmpty(Path.GetExtension(txtPath.Text)))
                    throw new Exception("Please provide a valid extension to the file-name");

                if (File.Exists(txtPath.Text))
                {
                    if (MessageBox.Show("File: " + txtPath.Text + " already exists. Overwrite?", "Crammer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;

                    File.Delete(txtPath.Text);
                }

                if (rbCSV.Checked)
                    mDictionary.exportToTextDelimited(txtPath.Text, ";");
                else if (rbTab.Checked)
                    mDictionary.exportToTextDelimited(txtPath.Text, "\t");
                else if (rbXml.Checked)
                    mDictionary.exportToXml(txtPath.Text);
                else
                {
                    if ( string.IsNullOrEmpty(txtDelimiter.Text))
                        throw new Exception("No delimiter value provided");

                    mDictionary.exportToTextDelimited(txtPath.Text, txtDelimiter.Text);
                }

                MessageBox.Show("Successfully exported dictionary to file:\n" + txtPath.Text, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustom.Checked)
            {
                txtDelimiter.Enabled = true;
                txtDelimiter.Focus();
            }
            else
                txtDelimiter.Enabled = false;
        }

        private void rbCSV_CheckedChanged(object sender, EventArgs e)
        {
            txtDelimiter.Enabled = rbCustom.Checked;
        }

        private void rbTab_CheckedChanged(object sender, EventArgs e)
        {
            txtDelimiter.Enabled = rbCustom.Checked;
        }

        private void rbXml_CheckedChanged(object sender, EventArgs e)
        {
            txtDelimiter.Enabled = rbCustom.Checked;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(txtPath.Text))
                {
                    txtPath.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    if (Directory.Exists(txtPath.Text))
                    {
                        txtPath.Text = folderBrowserDialog1.SelectedPath;
                    }
                    else
                    {
                        string fileName = txtPath.Text;
                        txtPath.Text = Path.Combine(folderBrowserDialog1.SelectedPath, fileName);
                    }
                }
            }
        }
    }
}
