using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Crammer
{
    class NewWords
    {
        #region Attributes
	    private List<int> mWordIndices = null;
	    private int mWordsKnownCount = 0;
	    private int mCurrentWord = -1;
        private int mInChamber = WordChamber.chamber(WordChamber.ChamberType.C0);
	    #endregion

        #region Properties
        public int InChamber
        {
            get { return (mInChamber); }
        }
        public int Capacity
        {
            get { return (mWordIndices.Count); }
        }
        public bool ChamberDone
        {
            get { return (mWordsKnownCount == mInChamber); }
        }
        
        #endregion

        public void init()
        {
            mWordIndices = Enumerable.Range(0, WordChamber.chamber(WordChamber.ChamberType.C0)).Select(n => -1).ToList();
	        mWordsKnownCount = 0;
	        mCurrentWord = -1;
	        mInChamber = WordChamber.chamber(WordChamber.ChamberType.C0);
        }

        public int this[int i]
        {
            get {
	            if ( i > WordChamber.chamber(WordChamber.ChamberType.C0) )
		            throw new Exception("Out of bounds in NewWords::operator[]");

	            return (mWordIndices[i]);
            }
        }

	    // Operations
        public void initIndices(int from, int howMany)
        {
            if (howMany > WordChamber.chamber(WordChamber.ChamberType.C0))
                throw new Exception("Out of bounds detection in NewWords chamber");

            mInChamber = howMany;
            for (int i = 0; i < howMany; i++)
                mWordIndices[i] = from + i;

            mWordsKnownCount = 0;
            mCurrentWord = -1;
        }

	    public void initIndices(List<int> arr, int start, int howMany)
        {
          if ( howMany > WordChamber.chamber(WordChamber.ChamberType.C0) )
	          throw new Exception("Out of bounds detection in NewWords chamber");

          mInChamber = howMany;
          for ( int i = 0; i < howMany; i++)
	         mWordIndices[i] = arr[start + i];

          mWordsKnownCount = 0;
          mCurrentWord = -1;
        }


	    public int pickNext()
        {
            mWordsKnownCount++;

            if (mCurrentWord < 0)
                mCurrentWord = 0;
            else if ((mCurrentWord + 1) >= mInChamber)
                mCurrentWord = 0;
            else
                mCurrentWord++; // One past current

            // There are no more words, which might indicate that there are fewer words than required, 
            // i.e. 1550 to be able to run through the entire algorithm.
            if (mWordIndices[mCurrentWord] == -1)
                throw new Exception("No more words available.\n" + 
									"It seems you have successfully completed the current dictionary!");

            return (mWordIndices[mCurrentWord]);
        }

	    public void skip()
        {
            mWordsKnownCount = 0;
        }



	    public void saveState(BinaryWriter bw)
        {
            bw.Write(mCurrentWord);
            bw.Write(mInChamber);
            bw.Write(mWordsKnownCount);
            for (int i = 0; i < mInChamber; i++)
            {
                bw.Write(mWordIndices[i]);
            }
        }

	    public void restoreState(BinaryReader br)
        {
            mCurrentWord = br.ReadInt32();
            mInChamber = br.ReadInt32();
            if (mInChamber > mWordIndices.Count())
                throw new Exception("Unexpected size for amount of words in chamber. Expected " + 
                                mWordIndices.Count() + " but found " + mInChamber);

            mWordsKnownCount = br.ReadInt32();
            for (int i = 0; i < mInChamber; i++)
            {
                mWordIndices[i] = br.ReadInt32();
            }
        }

	    public void reset()
        {
            mWordIndices.Clear();
            mCurrentWord = -1;
        }


    }
}
