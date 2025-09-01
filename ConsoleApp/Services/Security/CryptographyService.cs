using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp.Services.Security
{
    public class CryptographyService : ICryptographyService
    {
        public string GenerateIV()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                return ByteArrayToHexString(aes.IV);
            }
        }

        public string Decrypt(string encryptedText, string password, string iv)
        {
            return DecryptStringFromBytes(StringToByteArray(encryptedText), GetAesKey(password), GetAesKey(iv, true));
        }

        public T DecryptObjectM<T>(string encryptedText, string password, string iv)
        {
            var decryptedText  = Decrypt(encryptedText, password, iv);
            return System.Text.Json.JsonSerializer.Deserialize<T>(decryptedText ) ??
                throw new Exception("Can't decrypt!");
        }

        public string Encrypt(string plainText, string password, string iv)
        {
            var byteArrayResult = EncryptStringToBytes(plainText, GetAesKey(password), GetAesKey(iv, true)) ?? throw new Exception("Can't encrypt!"); 
        
            return ByteArrayToHexString(byteArrayResult);
        }

        public string Encrypt<T>(T obj, string password, string iv)
        {
            var text = System.Text.Json.JsonSerializer.Serialize(obj);

            return Encrypt(text, password, iv);
        }

        private byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            byte[] encrypted;

            // Create an Aes object with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                // Create a new MemoryStream object to contain the encrypted bytes.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Create a CryptoStream object to perform the encryption.
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Encrypt the plaintext.
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            return encrypted;
        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            string decrypted;

            // Create an Aes object with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                // Create a new MemoryStream object to contain the decrypted bytes.
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    // Create a CryptoStream object to perform the decryption.
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        // Decrypt the ciphertext.
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            decrypted = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }

        private byte[] GetAesKey(string stringKey, bool iv = false)
        {
            var byteKey = Encoding.ASCII.GetBytes(stringKey);

            if (iv)
            {
                return SHA256.Create().ComputeHash(byteKey).Take(16).ToArray();
            }
            else
            {
                return SHA256.Create().ComputeHash(byteKey);
            }
        }

        private string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
