using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Szyfr
{
    public partial class Form1 : Form
    {
        string pomoc = "Do działania programu niezbędna jest para kluczy (publiczny do zaszyfrowania i prywatny do odszyfrowania). " +
            "Klucze można wygenerować za pomocą programu (przycisk Generuj klucze) albo skorzystać ze swoich. " +
            "W tym celu należy przygotować klucze w formacie XML według tego generowanego przez program.";
        RsaImpl rsaImpl;

        public Form1()
        {
            InitializeComponent();
            rsaImpl = new RsaImpl();
        }

        public static string filePath = @"C:\\GSzyfr.txt";
        public static string filePath2 = @"C:\\Szyfr.txt";

        private void decryptButton_Click(object sender, EventArgs e)
        {
            boxUnencText.Text = Szyfr.Cezar.decrypt(boxCipherText.Text);
            
    }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            boxCipherText.Text = Szyfr.Cezar.encrypt(boxUnencText.Text);
        }

        private void generateRSAkeys_Click(object sender, EventArgs e)
        {
            rsaImpl.rsaCSP = new RSACryptoServiceProvider();
            publicKeyBox.Text = rsaImpl.rsaCSP.ToXmlString(false);
            privateKeyBox.Text = rsaImpl.rsaCSP.ToXmlString(true);
        }

        private void rsaEncryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                rsaImpl.rsaCSP.FromXmlString(publicKeyBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Błędny klucz publiczny", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            byte[] bytes = rsaImpl.byteConverter.GetBytes(rsaPlainText.Text);
            string hex = BitConverter.ToString(bytes);
            rsaPlainTextBytes.Text = hex.Replace("-", "");

            
            rsaEncryptText.Text = rsaImpl.encryption(bytes);

            
            hex = BitConverter.ToString(rsaImpl.encryptedTextBytes);
            rsaEncryptedTextBytes.Text = hex.Replace("-", "");
        }

        private void rsaDecryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                rsaImpl.rsaCSP.FromXmlString(privateKeyBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Błędny klucz prywatny", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hex = rsaEncryptedTextBytes.Text;
            byte[] bytes = RsaImpl.StringToByteArray(hex);
            if (bytes != null)
                rsaDecryptText.Text = rsaImpl.decryption(bytes);
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pomoc, "Pomoc", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Create(filePath2);
            {
                AddText(fs, "klucz publiczny: "+publicKeyBox.Text);
                AddText(fs, "\nklucz prywatny: "+privateKeyBox.Text);
                
                AddText(fs, "\n"+rsaPlainText.Text);
                AddText(fs, "\n"+rsaPlainTextBytes.Text);
                AddText(fs, "\n"+rsaEncryptedTextBytes.Text);
                AddText(fs, "\n" + rsaDecryptText.Text);
                AddText(fs, "\n" + rsaEncryptText.Text);



                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());
                }

            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Create(filePath);
            {
                AddText(fs, boxUnencText.Text);
                AddText(fs, "\n" + boxCipherText.Text);


                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var frm2 = new Form2())
            {

                frm2.ShowDialog();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var frm2 = new Form2())
            {

                frm2.ShowDialog();

            }
        }
    }
}