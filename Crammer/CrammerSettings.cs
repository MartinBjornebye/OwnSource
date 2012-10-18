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
    public partial class CrammerSettings : Form
    {
        public CrammerSettings()
        {
            InitializeComponent();
        }

        private void CrammerSettings_Load(object sender, EventArgs e)
        {
            chkLargeUnits.Checked = Properties.Settings.Default.LargeChambers;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LargeChambers != chkLargeUnits.Checked)
            {
                if (MessageBox.Show("Changing unit size for entries to propagate through will force all status history to be deleted.\r\n" +
                                      "Do you want to proceed?", "Crammer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                CrammerDictionary.removeStateFiles();
                Properties.Settings.Default.LargeChambers = chkLargeUnits.Checked;
                Properties.Settings.Default.Save();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
