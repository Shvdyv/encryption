using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Szyfr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    
    class Cezar
    {
        static int shift = 3;   

        
        static char[] alph_pol = new char[18] { 'ą', 'ć', 'ę', 'ł', 'ń', 'ś', 'ó', 'ż', 'ź', 'Ą', 'Ć', 'Ę', 'Ł', 'Ń', 'Ś', 'Ó', 'Ż', 'Ź' };

        
        public static string encrypt(string value)
        {
            char[] buffer = value.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                int letterIndex;
                if (!alph_pol.Contains(letter))  
                {
                    letterIndex = letter + shift;

                    
                    if (letterIndex > 126)
                    {
                        letterIndex = letterIndex - 95;
                    }
                    else if (letter < 32)
                    {
                        letterIndex = letterIndex + 95;
                    }
                    letter = (char)letterIndex;
                }

                buffer[i] = letter;
            }
            return new string(buffer);
        }

       
        public static string decrypt(string value)
        {
            int shiftDecrypt = shift * (-1);
            char[] buffer = value.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                int letterIndex;
                if (!alph_pol.Contains(letter)) 
                {
                    letterIndex = letter + shiftDecrypt;

                    
                    if (letterIndex > 126)
                    {
                        letterIndex = letterIndex - 95;
                    }
                    else if (letterIndex < 32)
                    {
                        letterIndex = letterIndex + 95;
                    }
                    letter = (char)letterIndex;
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
    }

    
    class RsaImpl
    {
        public System.Text.UnicodeEncoding byteConverter;
        public RSACryptoServiceProvider rsaCSP;
        public byte[] plainTextBytes;
        public byte[] encryptedTextBytes; 

        public RsaImpl()
        {
            rsaCSP = new RSACryptoServiceProvider();
            byteConverter = new System.Text.UnicodeEncoding();
        }

        
        public String encryption(byte[] plainTextBytes)
        {
            try
            {
                encryptedTextBytes = rsaCSP.Encrypt(plainTextBytes, false);
                String encryptedString = byteConverter.GetString(encryptedTextBytes);
                return encryptedString;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        
        public String decryption(byte[] data)
        {
            try
            {
                byte[] decryptedBytes = rsaCSP.Decrypt(data, false);
                String decryptedString = byteConverter.GetString(decryptedBytes);
                return decryptedString;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        
        public String decryption()
        {
            try
            {
                byte[] data = encryptedTextBytes;
                byte[] decryptedBytes = rsaCSP.Decrypt(data, false);
                String decryptedString = byteConverter.GetString(decryptedBytes);
                return decryptedString;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] StringToByteArray(String hex)
        {
            try
            {
                int NumberChars = hex.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                return bytes;
            }
            catch (Exception)
            {
                MessageBox.Show("Wprowadzony tekst zaszyfrowany nie jest w postaci szesnastkowej.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}