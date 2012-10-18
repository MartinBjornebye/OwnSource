////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Source:      WordChamber.cs
// Author:      Martin Bjornebye
// Description: Defines chambers through which entries will propagate based on the users feedback whether
//              they are known or not.
//
// Created:     2010.12.31 13:12:27
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Crammer
{
    class WordChamber
    {
        #region Enumerations
        public enum ChamberType
        {
            C0,
            C1,
            C2,
            C3,
            C4,
            C5,
            C6
        };
        #endregion

        #region Constants
        private const int BUFFER_SPACE_FOR_PROPAGATION_PURPOSES = 9;

        public const int K0_CHECK = 5;
        public const int K1_CHECK = 20;
        public const int K2_CHECK = 40;
        public const int K3_CHECK = 80;
        public const int K4_CHECK = 160;
        public const int K5_CHECK = 320;

        public const int BIG_K0_CHECK = 10;
        public const int BIG_K1_CHECK = 50;
        public const int BIG_K2_CHECK = 100;
        public const int BIG_K3_CHECK = 200;
        public const int BIG_K4_CHECK = 400;
        public const int BIG_K5_CHECK = 800;
        #endregion

        #region Attributes
        private List<int> mWordIndices = null;
	    private int mWordsKnownCount = 0;
	    private int mCurrentWord = -1;
	    private int mInChamber = 0;
        private ChamberType mIdentity;
 	    #endregion

        #region Static Properties
        public static int BufferUpSize
        {
            get
            {
                //return (Properties.LargeChambers == true ? BUFFER_SPACE_FOR_PROPAGATION_PURPOSES * 2 : BUFFER_SPACE_FOR_PROPAGATION_PURPOSES);
                return (BUFFER_SPACE_FOR_PROPAGATION_PURPOSES * 2);
            }
        }
        #endregion


        #region Properties
	    public int InChamber
        {
            get { return (mInChamber); }
        }
        public bool Full 
        {
            get { return (mInChamber == mWordIndices.Count); }
        }
        public bool Empty
        { 
            get { return (mInChamber == 0); } 
        }
        public int First
        {
            get { return (mWordIndices[0]); }
        }
        public ChamberType ID
        {
            get { return (mIdentity); }
        }

        public List<int> getIndices() { return (mWordIndices); }
        #endregion


        public void init(ChamberType ID)
        {
            int capacity = WordChamber.chamber(ID) + BufferUpSize;
            mWordIndices = Enumerable.Range(0, capacity).Select(n => -1).ToList();
            mCurrentWord = -1;
            mInChamber = 0;
            mWordsKnownCount = 0;
            mIdentity = ID;
        }

        public void init(ChamberType ID, int capacity)
        {
            mWordIndices = Enumerable.Range(0, capacity).Select(n => -1).ToList();
            mCurrentWord = -1;
            mInChamber = 0;
            mWordsKnownCount = 0;
            mIdentity = ID;
        }

	    public int removeFirst()
        {
          if (mWordIndices[0] == -1 )
	         return (-1);

          int nTemp = mWordIndices[0];
          for ( int i = 1; i < mInChamber; i++ )
            mWordIndices[i - 1] = mWordIndices[i];

          mInChamber--;
          return (nTemp);
        }

	    public void putLast(int index)
        {
          if (mInChamber < mWordIndices.Count)
          {
	         mInChamber++;
	         mWordIndices[mInChamber - 1] = index;
          }
          else
          {
	         // Make room for a new one
             List<int> tmp = new List<int>(mInChamber + 1);
	         tmp = mWordIndices;
             tmp[mWordIndices.Count] = index;
	         mWordIndices.Clear();
	         mWordIndices = tmp;
	         mInChamber++;
           }
        }

	    // Operations
	    public void append(NewWords wc)
        {
          // Is there space enough to accomodate the entries in the wc parameter
          if ( (mWordIndices.Count - mInChamber) < wc.InChamber)
            return; 

          int src = 0;
          for ( int i = mInChamber; i < ( wc.InChamber + mInChamber); i++ )
          {
	         if ( i >= mWordIndices.Count )
		        throw new Exception("Out of bounds in WordChamber::append chamber " + mIdentity + " with index " + i);

            mWordIndices[i] = wc[src++];
          }
          mInChamber += wc.InChamber;
        }

        /// <summary>
        /// Appends another word index
        /// </summary>
        /// <param name="ind"></param>
	    public void append(int ind)
        {
          if ( (mInChamber + 1) > mWordIndices.Capacity)
	         throw new Exception("Out of bounds");

          // Is there space enough to accomodate the
          // entries in the wc parameter
          if (InChamber >= mWordIndices.Count)
            return;

          mWordIndices[mInChamber] = ind;
          mInChamber++;
        }

        /// <summary>
        /// Initializes the word index array
        /// </summary>
        /// <param name="from"></param>
        /// <param name="maxNum"></param>
	    public void initIndices(int from, int maxNum)
        {
            if ( maxNum > mWordIndices.Count )
                throw new Exception("Out of bounds");

            if (maxNum > 0 && maxNum > mWordIndices.Count)
                return;

            int limit = mWordIndices.Count; // Default, fill up all slots
            if (maxNum > 0)
                limit = maxNum;

            for (int i = 0; i < limit; i++)
                mWordIndices[i] = from + i;

            mInChamber = limit;
            mWordsKnownCount = 0;
        }

	    public void reset()
        {
            mCurrentWord = -1;
            mInChamber = 0;
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

        public int this[int i]
        {
            get {
	            if ( i > chamber(ChamberType.C0) )
		            throw new Exception("Out of bounds in NewWords::operator[]");

	            return (mWordIndices[i]);
            }
        }

        public static int chamber(ChamberType c)
        {
            switch (c)
            {
                case ChamberType.C0:
                    return (Properties.LargeChambers == true ? BIG_K0_CHECK : K0_CHECK);
                case ChamberType.C1:
                    return (Properties.LargeChambers == true ? BIG_K1_CHECK : K1_CHECK);
                case ChamberType.C2:
                    return (Properties.LargeChambers == true ? BIG_K2_CHECK : K2_CHECK);
                case ChamberType.C3:
                    return (Properties.LargeChambers == true ? BIG_K3_CHECK : K3_CHECK);
                case ChamberType.C4:
                    return (Properties.LargeChambers == true ? BIG_K4_CHECK : K4_CHECK);
                case ChamberType.C5:
                    return (Properties.LargeChambers == true ? BIG_K5_CHECK : K5_CHECK);

                default:
                    throw new Exception("Invalid chamber-type");
            }
        }


    }
}
