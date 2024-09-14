using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Lim.Generic
{
    public static class EncryptAES
    {
        //todo : Key & IV 이동
        private const string KeyValue = "01234567890123456789012345678901";
        private const string InitVecValue = "0123456789012345";

        private static readonly byte[] Key;
        private static readonly byte[] IV;

        static EncryptAES()
        {
            Key = Encoding.UTF8.GetBytes(KeyValue);
            IV = Encoding.UTF8.GetBytes(InitVecValue);
        }
        
        public static byte[] Encrypt(string data)
        {
            try
            {
                using Aes aes = Aes.Create();
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream ms = new MemoryStream();
                using CryptoStream cs = new CryptoStream(ms,encryptor,CryptoStreamMode.Write);
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(data);
                }

                return ms.ToArray();
            }
            catch (Exception e)
            {
                Debug.LogError($"Encrypt Error : {e}");
                return null;
            }
        }

        public static string Decrypt(byte[] data)
        {
            try
            {
                using Aes aes = Aes.Create();
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using MemoryStream ms = new MemoryStream(data);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Debug.LogError($"Decrypt Error : {e}");
                return null;
            }
        }
    }
}