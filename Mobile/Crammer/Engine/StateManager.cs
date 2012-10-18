using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO.IsolatedStorage;            

namespace MB.Crammer
{
    public class StateManager
    {
        #region Attributes
        private PickEngine mPickEngine = null;  // Reference to the pick engine
        private int mTotalWords = 0;			// Count of words in the database
        private int mKnownWords = 0;			// Count of completed words
        private int mStart = 0;				    // Index used for fetching new words
        private int mCurrentWord = 0;		    // Crammer database cursor (index)
        private int mInSystem = 0;			    // Count of words currently in the system
        private bool mRestoredState = false;	// Session restored from a previous session
        private bool mNewWordsInUse = false;	// Flag indicating if we're working on new words or not
        private bool mSwapSequence = false;		// Indicates which language is first in the sequence
        private bool mReachedEnd = false;       // End of dictionary marker
        #endregion

        #region Properties
        public int TotalWords
        {
            get { return mTotalWords; }
            set { mTotalWords = value; }
        }

        public int KnownWords
        {
            get { return mKnownWords; }
            set { mKnownWords = value; }
        }

        public int Start
        {
            get { return mStart; }
            set { mStart = value; }
        }

        public int CurrentWord
        {
            get { return mCurrentWord; }
            set { mCurrentWord = value; }
        }

        public int InSystem
        {
            get { return mInSystem; }
            set { mInSystem = value; }
        }

        public bool RestoredState
        {
            get { return mRestoredState; }
            set { mRestoredState = value; }
        }

        public bool NewWordsInUse
        {
            get { return mNewWordsInUse; }
            set { mNewWordsInUse = value; }
        }

        public bool SwapSequence
        {
            get { return mSwapSequence; }
            set { mSwapSequence = value; }
        }

        public bool ReachedEnd
        {
            get { return mReachedEnd; }
            set { mReachedEnd = value; }
        }
        #endregion

        public StateManager(PickEngine pickEngine)
        {
            mPickEngine = pickEngine;
        }

        public bool restored()
        {
            return (mRestoredState);
        }

        public bool done()
        {
            // This test will return true if the number of total words is less than the required 1500+
            // which it takes to run through the entire algorithm
            if (mCurrentWord >= mTotalWords)
                return (true);

            return (mTotalWords == mKnownWords);
        }

        public void removeStateFile()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(mPickEngine.CrammerDict.StateFile))
                    storage.DeleteFile(mPickEngine.CrammerDict.StateFile);
            }
        }

        public void setEndReachedState()
        {
            // Start all over to squeeze out the last words
            mStart = 0;
            mReachedEnd = true;

            // Make sure the following is only done once
            if (mInSystem < mTotalWords)
                mInSystem = mTotalWords;
        }

        public int determineNewWordAmount(int chamberMax)
        {
            return ((mTotalWords - mStart) < chamberMax ? mTotalWords - mStart : chamberMax);
        }

        public bool restoreState()
        {
            if (string.IsNullOrEmpty(mPickEngine.CrammerDict.StateFile))
                return (false);

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storage.FileExists(mPickEngine.CrammerDict.StateFile))
                    return (false);

                IsolatedStorageFileStream stream = null;
                try
                {
                    stream = storage.OpenFile(mPickEngine.CrammerDict.StateFile, FileMode.Open);
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        mTotalWords = br.ReadInt32();
                        mKnownWords = br.ReadInt32();
                        mStart = br.ReadInt32();
                        mCurrentWord = br.ReadInt32();
                        mNewWordsInUse = br.ReadBoolean();
                        mInSystem = br.ReadInt32();
                        mSwapSequence = br.ReadBoolean();
                        mReachedEnd = br.ReadBoolean();

                        mPickEngine.NewWordsChamber.restoreState(br);
                        mPickEngine.FirstChamber.restoreState(br);
                        mPickEngine.SecondChamber.restoreState(br);
                        mPickEngine.ThirdChamber.restoreState(br);
                        mPickEngine.FourthChamber.restoreState(br);
                        mPickEngine.FifthChamber.restoreState(br);
                        mPickEngine.CompletedChamber.restoreState(br);
                    }
                }
                catch (Exception)
                {
                    // Assume mismatch with chamber-size config and contents of .STA file.
                    // Delete it so that the dictionary will load successfully next time.
                    if (storage != null)
                        storage.DeleteFile(mPickEngine.CrammerDict.StateFile);
                    return (false);
                }
            }

            mRestoredState = true;
            return (true);
        }


        public void saveState()
        {
            if (string.IsNullOrEmpty(mPickEngine.CrammerDict.StateFile))
                throw new Exception("No state file available");

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(mPickEngine.CrammerDict.StateFile))
                    storage.DeleteFile(mPickEngine.CrammerDict.StateFile);

                IsolatedStorageFileStream stream = null;
                try
                {
                    stream = storage.OpenFile(mPickEngine.CrammerDict.StateFile, FileMode.CreateNew);
                    using (BinaryWriter bw = new BinaryWriter(stream))
                    {
                        bw.Write(mTotalWords);
                        bw.Write(mKnownWords);
                        bw.Write(mStart);
                        bw.Write(mCurrentWord);
                        bw.Write(mNewWordsInUse);
                        bw.Write(mInSystem);
                        bw.Write(mSwapSequence);
                        bw.Write(mReachedEnd);

                        mPickEngine.NewWordsChamber.saveState(bw);
                        mPickEngine.FirstChamber.saveState(bw);
                        mPickEngine.SecondChamber.saveState(bw);
                        mPickEngine.ThirdChamber.saveState(bw);
                        mPickEngine.FourthChamber.saveState(bw);
                        mPickEngine.FifthChamber.saveState(bw);
                        mPickEngine.CompletedChamber.saveState(bw);
                    }
                }
                catch (Exception)
                {
                    // Assume mismatch with chamber-size config and contents of .STA file.
                    // Delete it so that the dictionary will load successfully next time.
                    if (storage != null)
                        storage.DeleteFile(mPickEngine.CrammerDict.StateFile);

                    throw;
                }
            }
            mRestoredState = true;
        }


        public void reset()
        {
            mTotalWords = 0;
            mKnownWords = 0;
            mStart = 0;
            mInSystem = 0;
            mRestoredState = false;
            mNewWordsInUse = true;
            mReachedEnd = false;
        }
    }
}
