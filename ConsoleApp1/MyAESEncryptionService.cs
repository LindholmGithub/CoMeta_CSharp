using System;
using System.IO;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class MyAESEncryptionService
    {
        private byte[] _key;
        private byte[] _iv;
        private AesCryptoServiceProvider _aes;

        public MyAESEncryptionService(string key, string iv)
        {
            _key = Convert.FromBase64String(key);
            _iv = Convert.FromBase64String(iv);
            _aes = new AesCryptoServiceProvider();
            _aes.Key = _key;
            _aes.IV = _iv;
        }

        public MyAESEncryptionService(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
            _aes = new AesCryptoServiceProvider();
            _aes.Key = _key;
            _aes.IV = _iv;
        }

        
        
        public string DecryptMessage(byte[] encryptedMessage)
        {
            string clearText;
            //Decryption
            ICryptoTransform decrypter = _aes.CreateDecryptor();
            using (MemoryStream msDecrypt = new MemoryStream(encryptedMessage)) //Remember that encrypted is a byte[]
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypter, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        clearText = srDecrypt.ReadToEnd();
                    }
                }
            }
            return clearText;
        }

        public string getKey()
        {
            return Convert.ToBase64String(_key);
        }

        public string getIv()
        {
            return Convert.ToBase64String(_iv);
        }
    }
}