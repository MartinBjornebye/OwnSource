using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO.IsolatedStorage;            
using System.Xml.Linq;
using System.Text;
using System.Xml;

namespace MB.Crammer
{
    public class PickEngine
    {
        private NewWords mNewWordsChamber = new NewWords();
        private WordChamber mFirstChamber = new WordChamber();
        private WordChamber mSecondChamber = new WordChamber();
        private WordChamber mThirdChamber = new WordChamber();
        private WordChamber mFourthChamber = new WordChamber();
        private WordChamber mFifthChamber = new WordChamber();
        private WordChamber mCompletedChamber = new WordChamber();

        private CrammerDictionary mCrammerDict = null;	// Reference to Crammer Database
        private StateManager mStateManager = null;

        #region Properties
        public CrammerDictionary CrammerDict
        {
            get { return mCrammerDict; }
            set { mCrammerDict = value; }
        }

        internal NewWords NewWordsChamber
        {
            get { return mNewWordsChamber; }
            set { mNewWordsChamber = value; }
        }
        internal WordChamber FirstChamber
        {
            get { return mFirstChamber; }
            set { mFirstChamber = value; }
        }
        internal WordChamber SecondChamber
        {
            get { return mSecondChamber; }
            set { mSecondChamber = value; }
        }
        internal WordChamber ThirdChamber
        {
            get { return mThirdChamber; }
            set { mThirdChamber = value; }
        }
        internal WordChamber FourthChamber
        {
            get { return mFourthChamber; }
            set { mFourthChamber = value; }
        }
        internal WordChamber FifthChamber
        {
            get { return mFifthChamber; }
            set { mFifthChamber = value; }
        }
        internal WordChamber CompletedChamber
        {
            get { return mCompletedChamber; }
            set { mCompletedChamber = value; }
        }

        #endregion

        public PickEngine(CrammerDictionary dict, bool cancelRestore)
        {
            mCrammerDict = dict;
            mStateManager = new StateManager(this);

            // Are we able to restore from a previous session?
            // If not, initialize for a fresh one.
            init(cancelRestore);

        }

        public void close()
        {
            mStateManager.saveState();
            cleanUp();
        }

        public void reset()
        {
            cleanUp();
            mStateManager.reset();

            init(true);
        }

        public void execute()
        {
            while (true)
            {
                if (chamberCheck(mFirstChamber))
                    break;
                else if (chamberCheck(mSecondChamber))
                    break;
                else if (chamberCheck(mThirdChamber))
                    break;
                else if (chamberCheck(mFourthChamber))
                    break;
                else if (chamberCheck(mFifthChamber))
                    break;
                else
                {
                    if (newWords())
                        break;
                }
            }
        }

        /// <summary>
        /// Does the chamber we're checking have more space for additional entries?
        /// </summary>
        /// <param name="chamber"></param>
        /// <returns></returns>
        private bool chamberCheck(WordChamber chamber)
        {
            if (chamber.InChamber < WordChamber.chamber(chamber.ID))
                return (false);

            mStateManager.CurrentWord = chamber.First;
            mStateManager.NewWordsInUse = false;
            return (true);
        }

        /// <summary>
        /// Determines which chambers to work on based on the number of entries in them.
        /// The knowsEntry variable indicates if the user has answered that he knows it.
        /// </summary>
        /// <param name="yes"></param>
        public void answer(bool knowsEntry)
        {
            if (mFirstChamber.InChamber >= WordChamber.chamber(WordChamber.ChamberType.C1))
                handleAnswer(knowsEntry, mFirstChamber, mSecondChamber);
            else if (mSecondChamber.InChamber >= WordChamber.chamber(WordChamber.ChamberType.C2))
                handleAnswer(knowsEntry, mSecondChamber, mThirdChamber);
            else if (mThirdChamber.InChamber >= WordChamber.chamber(WordChamber.ChamberType.C3))
                handleAnswer(knowsEntry, mThirdChamber, mFourthChamber);
            else if (mFourthChamber.InChamber >= WordChamber.chamber(WordChamber.ChamberType.C4))
                handleAnswer(knowsEntry, mFourthChamber, mFifthChamber);
            else if (mFifthChamber.InChamber >= WordChamber.chamber(WordChamber.ChamberType.C5))
                handleAnswer(knowsEntry, mFifthChamber, mCompletedChamber);
            else
            {
                if (!knowsEntry)
                    mNewWordsChamber.skip();
            }
        }

        /// <summary>
        /// Picks up new fresh words
        /// </summary>
        /// <returns></returns>
        private bool newWords()
        {
            if (mStateManager.InSystem < mStateManager.TotalWords)
                mStateManager.NewWordsInUse = true;

            if (mStateManager.Start <= mStateManager.TotalWords)
            {
                // Should we fill up with new words first?
                if (mNewWordsChamber.ChamberDone)
                {
                    mFirstChamber.append(mNewWordsChamber);
                    int nNewWords = mStateManager.determineNewWordAmount(WordChamber.chamber(WordChamber.ChamberType.C0)); 

                    // Special case which indicates that we're done, i.e. 
                    // there are no more words in the database.
                    if (nNewWords == 0 && !mStateManager.ReachedEnd)
                        throw new FinishedException("No more words available.\n" +
                                            "It seems you have successfully completed the current dictionary!");

                    // Have we reached the end of new words and have to squeeze out
                    // the remainder by using the last chamber? Is mInSystem has
                    // the same value as total words, that is the case.
                    if (mStateManager.InSystem == mStateManager.TotalWords)
                    {
                        mNewWordsChamber.initIndices(mCompletedChamber.getIndices(), mStateManager.Start, nNewWords);
                    }
                    else
                    {
                        mNewWordsChamber.initIndices(mStateManager.Start, nNewWords);
                    }

                    mStateManager.Start += nNewWords;

                    // Increase in-system counter if new words are not done
                    if (mStateManager.InSystem < mStateManager.TotalWords)
                        mStateManager.InSystem += nNewWords;

                    return (false);
                }
                else
                {
                    mStateManager.CurrentWord = mNewWordsChamber.pickNext();
                }
            }
            else
            {
                // Start all over to squeeze out the last words
                mStateManager.setEndReachedState();
            }
            return (true);
        }

        /// <summary>
        /// If user knows the entry, it is moved from one chamber to the next.
        /// If not, it is moved at the back of the first chamber to make it re-occur again within 
        /// a reasonable time of iterating through the dictionary entries.
        /// </summary>
        /// <param name="knowsEntry"></param>
        /// <param name="w"></param>
        /// <param name="next"></param>
        private void handleAnswer(bool knowsEntry, WordChamber w, WordChamber next)
        {
            if (mStateManager.CurrentWord < 0)
                throw new Exception("Current word index is not valid");

            if (knowsEntry)
            {
                mStateManager.CurrentWord = w.removeFirst();
                next.putLast(mStateManager.CurrentWord);

                // If next is the sixth chamber, increase counter of known words.
                if (next.ID > WordChamber.ChamberType.C5)
                    mStateManager.KnownWords++;
            }
            else
            {
                mFirstChamber.putLast(w.removeFirst());
            }
        }


        public string getWord()
        {
            if (mStateManager.CurrentWord < 0 || mCrammerDict.Empty)
                //throw new Exception("Current word index is not valid");
                return ("");

            if (!mStateManager.SwapSequence)
                return mCrammerDict.Entries[mStateManager.CurrentWord].AEntry;
            else
                return mCrammerDict.Entries[mStateManager.CurrentWord].BEntry;
        }

        /// <summary>
        /// Picks up a so called "native" entry which is basically equivalent to the answer
        /// </summary>
        /// <returns></returns>
        public string getNative()
        {
            if (mStateManager.CurrentWord < 0)
                throw new Exception("Current word index is not valid");

            if (!mStateManager.SwapSequence)
                return mCrammerDict.Entries[mStateManager.CurrentWord].BEntry;
            else
                return mCrammerDict.Entries[mStateManager.CurrentWord].AEntry;
        }

        public string getInSystem()
        {
            return (mStateManager.InSystem.ToString());
        }

        public string getDoneWords()
        {
            return (mStateManager.KnownWords.ToString());
        }


        public bool newWordsActive()
        {
            return (mStateManager.NewWordsInUse);
        }

        public bool done()
        {
            return (mStateManager.done());
        }

        #region Save/Restore
        public bool restored()
        {
            return (mStateManager.restored());
        }

        #endregion

        public void removeStateFile()
        {
            mStateManager.removeStateFile();
        }

        public bool swappedSequence()
        {
            return (mStateManager.SwapSequence);
        }

        public void toggleSequence()
        {
            mStateManager.SwapSequence = !mStateManager.SwapSequence;
        }

        private void setTotalWords()
        {
            mStateManager.TotalWords = mCrammerDict.Entries.Count;
        }

        public void updateTimeStamp()
        {
            mCrammerDict.updateTimeStamp(mStateManager.CurrentWord);
        }

        private void cleanUp()
        {
            mNewWordsChamber.reset();
            mFirstChamber.reset();
            mSecondChamber.reset();
            mThirdChamber.reset();
            mFourthChamber.reset();
            mFifthChamber.reset();
            mCompletedChamber.reset();
        }

        private void init(bool cleanState)
        {
            mNewWordsChamber.init();
            mFirstChamber.init(WordChamber.ChamberType.C1);
            mSecondChamber.init(WordChamber.ChamberType.C2);
            mThirdChamber.init(WordChamber.ChamberType.C3);
            mFourthChamber.init(WordChamber.ChamberType.C4);
            mFifthChamber.init(WordChamber.ChamberType.C5);

            setTotalWords();
            mCompletedChamber.init(WordChamber.ChamberType.C6, mStateManager.TotalWords);

            int C0ChamberSize = WordChamber.chamber(WordChamber.ChamberType.C0);

            // Should we ignore already saved state?
            if (!cleanState)
            {
                if (!mStateManager.restoreState())
                {
                    mStateManager.NewWordsInUse = true;
                    mNewWordsChamber.initIndices(mStateManager.Start, mStateManager.TotalWords > C0ChamberSize ? C0ChamberSize : mStateManager.TotalWords); // Fill up with starter words
                    mStateManager.Start += (mStateManager.TotalWords > C0ChamberSize ? C0ChamberSize : mStateManager.TotalWords);
                }
            }
            else
            {
                mStateManager.NewWordsInUse = true;
                mNewWordsChamber.initIndices(mStateManager.Start, mStateManager.TotalWords > C0ChamberSize ? C0ChamberSize : mStateManager.TotalWords); // Fill up with starter words
                mStateManager.Start += mStateManager.TotalWords > C0ChamberSize ? C0ChamberSize : mStateManager.TotalWords;
            }
        }

    }
}
