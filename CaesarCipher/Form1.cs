using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaesarCipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string alphabet;
        private int alphabetLen;
        private int Key;
        private void Form1_Load(object sender, EventArgs e)
        {
            Key = 0;

            //İngiliz alfabesi ile 
            alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            alphabetLen = alphabet.Length;
            comboBoxKey.Items.Clear();
            for (int i = 0; i < alphabetLen; i++)
                comboBoxKey.Items.Add(i.ToString());
            comboBoxKey.SelectedIndex = 0;
        }


        private string EncodeCeasar(string plainText)
        {

            string chipherText = "";//Şifreli metin
            foreach (char c in plainText)//Düz metin içinde dolaşıyoruz
            {
                int index = alphabet.IndexOf(c);
                if (index >= 0)
                {
                    int newindex = (index + Key) % alphabetLen;
                    chipherText += alphabet[newindex];//harfin yeni değerini atıyoruz
                }
                else
                {
                    chipherText += c;
                }
            }
            return chipherText;
        }

        private string DecodeCeasar(string chipherText)
        {
            string plainText = ""; //Düz metin
            foreach (char c in chipherText)//Şifreli metin içinde dolaşıyoruz
            {
                int index = alphabet.IndexOf(c);
                if (index >= 0)
                {
                    int newindex = (index - Key + alphabetLen) % alphabetLen;
                    plainText += alphabet[newindex];//harfin yeni değerini atıyoruz
                }
                else
                {
                    plainText += c;
                }

            }
            return plainText;
        }



        private void comboBoxKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Key = Convert.ToInt32(comboBoxKey.SelectedItem.ToString());
            }
            catch
            {
                Key = 0;
            }
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            textBoxDecoded.Clear();
            textBoxDecoded.Text = EncodeCeasar(textBoxOriginal.Text.ToUpper());
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            textBoxOriginal.Clear();
            textBoxOriginal.Text = DecodeCeasar(textBoxDecoded.Text.ToUpper());
        }

     

        private void radioButtonEng_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEng.Checked)
                //İngiliz alfabesi ile 
                alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            else
                //Türk alfabesi ile 
                alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            alphabetLen = alphabet.Length;
            comboBoxKey.Items.Clear();
            for (int i = 0; i < alphabetLen; i++)
                comboBoxKey.Items.Add(i.ToString());
            comboBoxKey.SelectedIndex = 0;
        }
    }
}

