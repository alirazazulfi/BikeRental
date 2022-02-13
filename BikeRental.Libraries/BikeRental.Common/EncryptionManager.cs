using System.Security.Cryptography;
namespace BikeRental.Common
{
    public static class EncryptionManager
    {
        public static string Key { get; } = "huYiSGD8oEcz3L+pnlRaCXmQ0ZuTlchj3uY5GOpVIcQ=";
        public static string IV { get; } = "p+3K0+TcAjzy36uyGpImBw==";

        #region ----- Public Methods -----

        public static string Encrypt(string plainText)
        {
            /* Check arguments.*/
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException("plainText");
            byte[] encrypted;
            /* Create an AesCryptoServiceProvider object with the specified key and IV.*/
            using (AesCryptoServiceProvider aesAlg = new())
            {
                aesAlg.Key = Convert.FromBase64String(Key);
                aesAlg.IV = Convert.FromBase64String(IV);
                /* Create an encryptor to perform the stream transform.*/
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                /* Create the streams used for encryption.*/
                using (MemoryStream msEncrypt = new())
                {
                    using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new(csEncrypt))
                        {
                            swEncrypt.Write(plainText);/* Write all data to the stream.*/
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);/* Return the encrypted bytes from the memory stream.*/
        }

        public static string Decrypt(string encryptedValue)
        {
            /* Check arguments.*/
            if (encryptedValue == null || encryptedValue.Length <= 0) return encryptedValue;//throw new ArgumentNullException("encryptedValue");

            /* Declare the string used to hold the decrypted text.*/
            string plaintext = null;
            /* Create an AesCryptoServiceProvider object with the specified key and IV.*/
            using (AesCryptoServiceProvider aesAlg = new())
            {
                aesAlg.Key = Convert.FromBase64String(Key);
                aesAlg.IV = Convert.FromBase64String(IV);
                /* Create a decryptor to perform the stream transform.*/
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                /* Create the streams used for decryption.*/
                try
                {
                    using (MemoryStream msDecrypt = new(Convert.FromBase64String(encryptedValue)))
                    {
                        using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new(csDecrypt))
                            {
                                try
                                {
                                    plaintext = srDecrypt.ReadToEnd();/* Read the decrypted bytes from the decrypting stream and place them in a string.*/
                                }
                                catch
                                {
                                    plaintext = encryptedValue;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = encryptedValue;
                }
            }
            return plaintext;/* Return the decrypted string.*/
        }
        #endregion

        #region ----- Private Methods -----

        #endregion

    }
}
