using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper
{
    public static class DESHelper
    {
        public static string DESEncode(string content, string privateKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(privateKey.Substring(0, 8));
            byte[] rgbIV = Encoding.UTF8.GetBytes(privateKey.Substring(0, 8));
            byte[] inputByteArray = Encoding.UTF8.GetBytes(content);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);


            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();


            return Convert.ToBase64String(mStream.ToArray());
        }
        public static string DESEncode(string content)
        {
            string key = System.Configuration.ConfigurationManager.AppSettings["DESPrivateKey"];

            return DESEncode(content, key);
        }


        public static string DecryptDES(string encryptedString, string privateKey)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(privateKey.Substring(0, 8));

            byte[] btIV = Encoding.UTF8.GetBytes(privateKey.Substring(0, 8));

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);

                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                {
                    cs.Write(inData, 0, inData.Length);
                    cs.FlushFinalBlock();
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        public static string DecryptDES(string encryptedString)
        {
            string key = System.Configuration.ConfigurationManager.AppSettings["DESPrivateKey"];

            return DecryptDES(encryptedString, key);
        }
    }
}
