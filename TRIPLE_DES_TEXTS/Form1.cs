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
using System.Security.Cryptography;

namespace TRIPLE_DES_TEXTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = encrypt(textBox2.Text, textBox1.Text);
        }
        public string encrypt(string key, string plaintxt)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] keys = Encoding.UTF8.GetBytes(key);
                ICryptoTransform encryptor = des.CreateEncryptor(keys, keys);
                var ms =  new MemoryStream();
                var cs = new CryptoStream(ms,encryptor,CryptoStreamMode.Write);
                byte[] input = Encoding.UTF8.GetBytes(plaintxt);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string decrypt(string key, string ciphertxt)
        {
            byte[] buffer = Convert.FromBase64String(ciphertxt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] keys = Encoding.UTF8.GetBytes(key);
                ICryptoTransform encryptor = des.CreateDecryptor(keys, keys);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(buffer, 0, buffer.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = decrypt(textBox2.Text, textBox3.Text);

        }
    }
}
