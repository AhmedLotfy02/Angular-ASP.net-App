using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace WebApi.Controllers
{   
    public class Encryption
    {

        
       
        public static string DecryptionM(string key,byte[] ciphertext) {

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;          // in bits
                aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                                             
                key = key.Substring(0, (key.Length) / 4);
                byte[] Key2 = Encoding.UTF8.GetBytes(key);
                aes.Key = Key2;
                byte[] plainText = null;

                using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(ciphertext, 0, ciphertext.Length);
                        }

                        plainText = ms.ToArray();
                    }
                    
                    string s = System.Text.Encoding.Unicode.GetString(plainText);
                    Console.WriteLine(s);
                return s;
                
            }
        }
    
        public static byte[] EncryptionM(string key,string raw,int choice)
        {
            byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(raw);

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;          // in bits
                aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                                              
                byte[] cipherText = null;
                byte[] plainText = null;
                key = key.Substring(0, (key.Length) / 4);
                byte[] Key2 = Encoding.UTF8.GetBytes(key);
                aes.Key = Key2;
                
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                        }

                        cipherText = ms.ToArray();
                    }
                    return cipherText;

               
               
            }
        }

        
       
    }
}
