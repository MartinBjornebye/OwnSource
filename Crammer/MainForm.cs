//----------------------------------------------------------------------------------------------------------
// Source: 	MainForm.cs
// Description:	Main form of the Crammer application
// Author:	Martin Bjornebye
//----------------------------------------------------------------------------------------------------------

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
    public partial class MainForm : Form
    {
        #region Constants
        private const string CRAMMER_DICT_FILTER = "Crammer Dictionary Files (*.crammerdict)|*.crammerdict";
        private const string DICT_PREFIX = "Dictionary: {0}";
        #endregion

        #region Attributes
        private CrammerDictionary mCurrentDictionary = new CrammerDictionary();
        private PickEngine mEntryEngine = null;
        private string mCurrentWord = "";
        private bool mCheckModeOn = false;
        private string mInSystem = "";
        private string mDoneWords = "";
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }


        #region Crammer Settings

        private void dictionarySettingsMenuItem_Click(object sender, EventArgs e)
        {
            DictionaryOptions dlg = new DictionaryOptions(mCurrentDictionary);
            dlg.ShowDialog();
            if (mCurrentDictionary.Empty)
                return;

            if (mCheckModeOn)
            {
                mCurrentWord = mEntryEngine.getNative();
                txtVal.Font = mCurrentDictionary.Font2;
                txtVal.ForeColor = mCurrentDictionary.Color2;
            }
            else
            {
                mCurrentWord = mEntryEngine.getWord();
                txtVal.Font = mCurrentDictionary.Font1;
                txtVal.ForeColor = mCurrentDictionary.Color1;
            }

            mCurrentDictionary.setFont();

        }

        private void dictionaryHistoryMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DictionaryHistory dlg = new DictionaryHistory();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                if (mCurrentDictionary.DictionaryFile != dlg.SelectedDictionary)
                {
                    openDictionary(dlg.SelectedDictionary);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripBtnOptions_Click(object sender, EventArgs e)
        {
            bool largeChambers = Properties.Settings.Default.LargeChambers;
            CrammerSettings dlg = new CrammerSettings();
            dlg.ShowDialog();

            // If the chamber size has been changed, we need to start all over.
            if (largeChambers != Properties.Settings.Default.LargeChambers)
            {
                mCheckModeOn = false;
                mEntryEngine.reset();
                advanceWord(true);
            }
        }

        #endregion

        #region Dictionary Operations Menu

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewWizard dlg = new NewWizard();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.Save();
                    if (mEntryEngine != null)
                        mEntryEngine.close();

                    Properties.Settings.Default.CurrentDictionary = dlg.DictionaryPath;
                    openDictionary(dlg.DictionaryPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = CRAMMER_DICT_FILTER;
                if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                openDictionary(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void editDictionaryMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ManageDictionary md = new ManageDictionary(mCurrentDictionary);
                md.ShowDialog();

                // If entries have been changed, we have to reload the dictionary
                if ( md.Dirty )
                {
                    mCurrentDictionary.reLoad();
                    mCheckModeOn = false;
                    mEntryEngine.reset();
                    advanceWord(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void exportDictionaryMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mCurrentDictionary != null)
                {
                    ExportDict dlg = new ExportDict(mCurrentDictionary);
                    dlg.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void startAllOverMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you really want to start over?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                mCheckModeOn = false;
                mEntryEngine.reset();
                advanceWord(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void swapSequenceMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                mEntryEngine.toggleSequence();
                advanceWord(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void ascendingMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                mCurrentDictionary.sortAscending();
                loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, true);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void descendingMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                mCurrentDictionary.sortDescending();
                loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, true);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void byTimestampAscendingMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                mCurrentDictionary.sortByTimestampAscending();
                loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, true);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void byTimestampDescendingMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                mCurrentDictionary.sortByTimestampDescending();
                loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, true);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void randomizeMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mCurrentDictionary == null)
                    return;

                this.Cursor = Cursors.WaitCursor;
                mCurrentDictionary.randomShuffle();
                loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, true);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Private Helpers

        private void noOption()
        {
            if (mCurrentDictionary.Entries.Count == 0)
                return;

            if (mEntryEngine == null)
                return;

            mEntryEngine.answer(false);
            advanceWord(true);
        }

        /// <summary>
        /// User wants to check the entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOption()
        {
            if (mCurrentDictionary.Entries.Count == 0)
                return;

            if (mEntryEngine == null)
                return;

            if (!mCheckModeOn)
            {
                mCurrentWord = mEntryEngine.getNative();
                setCheck();
            }
            else
            {
                mCurrentWord = mEntryEngine.getWord();
                resetCheck();
            }

            txtVal.Text = mCurrentWord;
            showNewWords();
        }

        /// <summary>
        /// User knows the entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void yesOption()
        {
            if (mCurrentDictionary.Entries.Count == 0)
                return;

            if (mEntryEngine == null)
                return;

            if (mEntryEngine.done())
            {
                MessageBox.Show("You have finished the dictionary", "Congratulations!");
                mEntryEngine.reset();
            }
            else
            {
                mEntryEngine.answer(true);
                advanceWord(true);
            }
        }


        private void advanceWord(bool newSession)
        {
            try {

                if (mCurrentDictionary.Empty == false)
                {
                    if (newSession)
                        mEntryEngine.execute();
                }
            }
            catch (WarningException ex)
            {
                MessageBox.Show(ex.Message, "Congratulations!");
                mEntryEngine.reset();
                advanceWord(true);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                mEntryEngine.reset();
                advanceWord(true);
                return;
            }

            resetCheck();

            mInSystem = mEntryEngine.getInSystem();
            mDoneWords = mEntryEngine.getDoneWords();
            mCurrentWord = mEntryEngine.getWord();
            labelTotal.Text = mCurrentDictionary.Entries.Count.ToString();
            labelActive.Text = mInSystem;
            labelInactive.Text = mCurrentDictionary.InactiveEntries.Count.ToString();
            labelCompleted.Text = mDoneWords;
            txtVal.Text = mCurrentWord;
            showNewWords();
        }

        private void showNewWords()
        {
            if (mEntryEngine.newWordsActive())
            {
                picNewWords.Visible = true;
            }
            else
            {
                picNewWords.Visible = false;
            }
        }

        private void setRightFont()
        {
            if (mCheckModeOn)
            {
                txtVal.Font = mCurrentDictionary.Font2;
                txtVal.ForeColor = mCurrentDictionary.Color2;
            }
            else
            {
                txtVal.Font = mCurrentDictionary.Font1;
                txtVal.ForeColor = mCurrentDictionary.Color1;
            }
        }

        void resetCheck()
        {
            if (mCheckModeOn)
            {
                mCheckModeOn = false;
                setRightFont();
            }
        }

        void setCheck()
        {
            if (!mCheckModeOn)
            {
                mCheckModeOn = true;
                setRightFont();
            }
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            if (mEntryEngine != null)
                mEntryEngine.close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.CurrentDictionary) == false)
                {
                    if (File.Exists(Properties.Settings.Default.CurrentDictionary))
                    {
                        loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void openDictionary(string fileName)
        {
            try
            {
                Properties.Settings.Default.CurrentDictionary = fileName;
                if (Properties.Settings.Default.DictionaryHistory == null)
                {
                    Properties.Settings.Default.DictionaryHistory = new System.Collections.Specialized.StringCollection();
                    Properties.Settings.Default.DictionaryHistory.Add(fileName);
                }
                else
                {
                    if (Properties.Settings.Default.DictionaryHistory.Contains(fileName) == false)
                        Properties.Settings.Default.DictionaryHistory.Add(fileName);
                }

                loadDictionaryAndPickFirstWord(fileName, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crammer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        /// <summary>
        /// Load dictionary and get first entry
        /// </summary>
        /// <param name="fileName"></param>
        private void loadDictionaryAndPickFirstWord(string fileName, bool cancelRestore)
        {
            mCurrentDictionary.load(fileName, true);
            this.Text = string.Format(DICT_PREFIX, mCurrentDictionary.DictionaryTitle);

            if (mCurrentDictionary.Empty)
                clearUIFields();

            setRightFont();
            mEntryEngine = new PickEngine(mCurrentDictionary, cancelRestore);

            mCurrentWord = "";
            mDoneWords = "0";
            mInSystem = "0";

            if (!mEntryEngine.restored())
            {
                //mEntryEngine.reset();
                advanceWord(true);
            }
            else
            {
                advanceWord(false);
            }
        }

        private void cleanState()
        {
            if (mEntryEngine != null)
                mEntryEngine.removeStateFile();

            loadDictionaryAndPickFirstWord(Properties.Settings.Default.CurrentDictionary, true);  
        }

        private void clearUIFields()
        {
            mInSystem = "";
            mDoneWords = "";
            mCurrentWord = "";
            labelTotal.Text = "0";
            labelActive.Text = "0";
            labelCompleted.Text = "0";
            txtVal.Text = "";
        }

        /// <summary>
        /// Process the arrow key-strokes if Left, Up and Right. Else default handling
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Left):
                    noOption();
                    return true;

                case (Keys.Up):
                    checkOption();
                    return true;

                case (Keys.Right):
                    yesOption();
                    return true;

                case (Keys.F10):
                    mEntryEngine.updateTimeStamp();
                    txtVal.Focus();
                    return (true);

                case (Keys.E):
                    editDictionaryMenuItem_Click(null, null);
                    break;

                case (Keys.S):
                    dictionarySettingsMenuItem_Click(null, null);
                    break;

                case(Keys.R):
                    randomizeMenuItem_Click(null, null);
                    break;

                case (Keys.D):
                    ascendingMenuItem_Click(null, null);
                    break;

                case (Keys.T):
                    byTimestampDescendingMenuItem_Click(null, null);
                    break;
            }

            // Return the key to the base class if not used.
            return base.ProcessDialogKey(keyData);
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox dlg = new AboutBox();
            dlg.ShowDialog();
        }

        private void txtVal_Enter(object sender, EventArgs e)
        {
            cmdVerify.Focus();
        }

        private void cmdNo_Click(object sender, EventArgs e)
        {
            noOption();
        }

        private void cmdVerify_Click(object sender, EventArgs e)
        {
            checkOption();
        }

        private void cmdYes_Click(object sender, EventArgs e)
        {
            yesOption();
        }



    }
}
