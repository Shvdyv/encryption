using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Szyfr
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream fs = File.OpenRead(Form1.filePath))
            {


                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    richTextBox1.Text = temp.GetString(b);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream fs = File.OpenRead(Form1.filePath2))
            {


                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    richTextBox1.Text = temp.GetString(b);
                }
            }
        }
    }
}
