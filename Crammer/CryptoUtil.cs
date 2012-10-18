using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MB.Crammer
{
    class CryptoUtil
    {

        #region Constants
        private const string TPW = "madrasman5";
        #endregion

        private void encryptFile(string inputFile, string cryptFile)
        {
            FileStream fsCrypt = null;
            CryptoStream cryptStream = null;

            try
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(TPW);
                fsCrypt = new FileStream(cryptFile, FileMode.Create);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                cryptStream = new CryptoStream(fsCrypt,
                                                RMCrypto.CreateEncryptor(key, key),
                                                CryptoStreamMode.Write);

                byte[] inBytes = File.ReadAllBytes(inputFile);
                cryptStream.Write(inBytes, 0, inBytes.Length);

            }
            finally
            {
                if (cryptStream != null)
                    cryptStream.Close();

                if ( fsCrypt != null )
                    fsCrypt.Close();
            }
        }
        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void decryptFile(string inputFile, string outputFile)
        {
            FileStream fsCrypt = null;
            FileStream fsOut = null;
            CryptoStream cryptStream = null;

            try
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(TPW);
                fsCrypt = new FileStream(inputFile, FileMode.Open);
                RijndaelManaged RMCrypto = new RijndaelManaged();

                cryptStream = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                byte[] inBytes = File.ReadAllBytes(inputFile);
                cryptStream.Read(inBytes, 0, inBytes.Length);

                fsOut = new FileStream(outputFile, FileMode.Create);
                fsOut.Write(inBytes, 0, inBytes.Length);

            }
            finally
            {
                if ( fsOut != null )
                    fsOut.Close();

                if ( cryptStream != null )
                    cryptStream.Close();
            }
        }


        //private void DecryptFile(string inputFile, string outputFile)
        //{

        //    {
        //        string password = @"myKey123"; // Your Key Here

        //        UnicodeEncoding UE = new UnicodeEncoding();
        //        byte[] key = UE.GetBytes(password);

        //        FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

        //        RijndaelManaged RMCrypto = new RijndaelManaged();

        //        CryptoStream cs = new CryptoStream(fsCrypt,
        //            RMCrypto.CreateDecryptor(key, key),
        //            CryptoStreamMode.Read);

        //        FileStream fsOut = new FileStream(outputFile, FileMode.Create);

        //        int data;
        //        while ((data = cs.ReadByte()) != -1)
        //            fsOut.WriteByte((byte)data);

        //        fsOut.Close();
        //        cs.Close();
        //        fsCrypt.Close();

        //    }
        //}

    }
}
