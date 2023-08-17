using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Common
{
    public static class EncryptionHelper
    {
        #region Encrypt

        public static string Encrypt(string toEncrypt, bool useHashing = true)
        {

            byte[] keyArray;

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Get the key from config file

            string key = Constants.SecurityKey;

            //System.Windows.Forms.MessageBox.Show(key);

            if (useHashing)
            {

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

            }

            else

                keyArray = UTF8Encoding.UTF8.GetBytes(key);



            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;



            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        #endregion

    }
    public static class DecryptionHelper
    {
        #region Decrypt

        public static string Decrypt(string cipherString, bool useHashing = true)
        {

            byte[] keyArray;

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            // Get the key from config file

            string key = Constants.SecurityKey;

            //System.Windows.Forms.MessageBox.Show(key);

            if (useHashing)
            {

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

            }

            else

                keyArray = UTF8Encoding.UTF8.GetBytes(key);



            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;



            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);

        }


        #endregion
    }
}