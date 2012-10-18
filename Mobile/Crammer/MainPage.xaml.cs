using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using System.Diagnostics;

using MB.Crammer;

namespace Crammer
{
    public partial class MainPage : PhoneApplicationPage
    {

        #region Attributes
        private CrammerDictionary mCurrentDictionary = null;
        private PickEngine mEntryEngine = null;
        private bool mMainPageInitialized = false;

        private string mCurrentWord = "";
        private bool mCheckModeOn = false;
        private string mInSystem = "";
        private string mDoneWords = "";

        private double mNormalFontSize = 32;
        #endregion

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // If already initialized, do not restore
                if (mMainPageInitialized == false)
                {
                    mMainPageInitialized = true;
                    mCurrentDictionary = (Application.Current as App).CurrentDictionary;
                    loadDictionaryAndPickFirstWord(false);
                }

                mNormalFontSize = mCurrentDictionary.FontSizeNormal;
            }
            catch (Exception ex)
            {
                (Application.Current as App).ErrorMessage = ex.Message;
                this.NavigationService.Navigate(new Uri("/ErrorMessage.xaml", UriKind.Relative));
            }
        }


        private void cmdDontKnow_Click(object sender, RoutedEventArgs e)
        {
            noOption();
        }

        private void cmdVerify_Click(object sender, RoutedEventArgs e)
        {
            checkOption();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            yesOption();
        }


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
                txtVal.Text = "Congratulations! You have completed the dictionary.";
                mEntryEngine.reset();
            }
            else
            {
                mEntryEngine.answer(true);
                advanceWord(true);
            }
        }


        public void advanceWord(bool newSession)
        {
            try
            {

                if (mCurrentDictionary.Empty == false)
                {
                    if (newSession)
                        mEntryEngine.execute();
                }
            }
            catch (FinishedException)
            {
                string destination = "/CompletedMessage.xaml";
                destination += String.Format("?Total={0}", mCurrentDictionary.Entries.Count.ToString());
                this.NavigationService.Navigate(new Uri(destination, UriKind.Relative));
                mEntryEngine.reset();
                advanceWord(true);
                return;
            }
            catch (Exception)
            {
                txtVal.Text = "Error!";
                mEntryEngine.reset();
                advanceWord(true);
                return;
            }

            resetCheck();
            setRightFont();

            mInSystem = mEntryEngine.getInSystem();
            mDoneWords = mEntryEngine.getDoneWords();
            mCurrentWord = mEntryEngine.getWord();
            //labelTotal.Text = mCurrentDictionary.Entries.Count.ToString();
            //labelActive.Text = mInSystem;
            //labelInactive.Text = mCurrentDictionary.InactiveEntries.Count.ToString();
            //labelCompleted.Text = mDoneWords;
            txtVal.Text = mCurrentWord;
            showNewWords();
        }

        private void showNewWords()
        {
            if (mEntryEngine.newWordsActive())
            {
                //picNewWords.Visible = true;
                ContentPanel.Background = new SolidColorBrush(Colors.White);
                txtVal.Foreground = new SolidColorBrush(Colors.Black);
                txtVal.FontSize = mNormalFontSize + 4;
                txtVal.FontWeight = FontWeights.Bold;
            }
            else
            {
                ContentPanel.Background = new SolidColorBrush(Colors.Black);
                txtVal.Foreground = new SolidColorBrush(Colors.White);
                txtVal.FontSize = mNormalFontSize;
                txtVal.FontWeight = FontWeights.Normal;
                //picNewWords.Visible = false;
                //txtVal.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void setRightFont()
        {
            if (mCheckModeOn)
            {
                //txtVal.Font = mCurrentDictionary.Font2;
                //txtVal.ForeColor = mCurrentDictionary.Color2;
                txtVal.FontStyle = FontStyles.Italic;
            }
            else
            {
                txtVal.FontStyle = FontStyles.Normal;
                //txtVal.Font = mCurrentDictionary.Font1;
                //txtVal.ForeColor = mCurrentDictionary.Color1;
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

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (mEntryEngine != null)
                mEntryEngine.close();
        }


        private void loadDictionaryAndPickFirstWord(bool cancelRestore)
        {
            mCurrentDictionary.load(true);
            ApplicationTitle.Text = "Crammer - " + mCurrentDictionary.DictionaryTitle;

            if (mCurrentDictionary.Empty)
                clearUIFields();

            setRightFont();
            mEntryEngine = new PickEngine(mCurrentDictionary, cancelRestore);
            (Application.Current as App).EntryEngine = mEntryEngine;

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

            loadDictionaryAndPickFirstWord(true);
        }

        private void clearUIFields()
        {
            mInSystem = "";
            mDoneWords = "";
            mCurrentWord = "";
            //labelTotal.Text = "0";
            //labelActive.Text = "0";
            //labelCompleted.Text = "0";
            txtVal.Text = "";
        }

        private void Options_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            //mOptionsVisited = true;
            this.NavigationService.Navigate(new Uri("/OptionsPage.xaml", UriKind.Relative));
            e.Complete();
            e.Handled = true;
        }

        private void Status_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            string destination = "/StatusInfo.xaml";
            destination += String.Format("?Total={0}&Active={1}&Completed={2}",
                mCurrentDictionary.Entries.Count.ToString(), mInSystem, mDoneWords);
            
            this.NavigationService.Navigate(new Uri(destination, UriKind.Relative));
            e.Complete();
            e.Handled = true;
        }

        private void About_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            string destination = "/AboutPage.xaml";
            this.NavigationService.Navigate(new Uri(destination, UriKind.Relative));
            e.Complete();
            e.Handled = true;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey(CrammerConstants.OPTION_CHOSEN))
            {
                string optionChosen = (string)PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN];
                if (string.IsNullOrEmpty(optionChosen) == false)
                {
                    switch (optionChosen)
                    {
                        case "StartOver":
                            cleanState();
                            break;

                        case "Flip":
                            mEntryEngine.toggleSequence();
                            advanceWord(false);
                            break;

                        case "Shuffle":
                            mCurrentDictionary.randomShuffle();
                            loadDictionaryAndPickFirstWord(true);
                            break;

                        case "Ascending":
                            mCurrentDictionary.sortAscending();
                            loadDictionaryAndPickFirstWord(true);
                            break;

                        case "Descending":
                            mCurrentDictionary.sortDescending();
                            loadDictionaryAndPickFirstWord(true);
                            break;

                        case "TimestampAscending":
                            mCurrentDictionary.sortByTimestampAscending();
                            loadDictionaryAndPickFirstWord(true);
                            break;

                        case "TimestampDescending":
                            mCurrentDictionary.sortByTimestampDescending();
                            loadDictionaryAndPickFirstWord(true);
                            break;

                        case "OpenedDictionary":
                            loadDictionaryAndPickFirstWord(false);
                            break;

                        case "EditedEntries":
                            loadDictionaryAndPickFirstWord(true);
                            break;
                    }
                }
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }


    }
}