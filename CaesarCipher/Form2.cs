using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CaesarCipher
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";     
        int alphabetLen;
        private void btnDecode_Click(object sender, EventArgs e)
        {
            alphabetLen = alphabet.Length;
            dataGridViewBruteForce.Rows.Clear();

            DateTime start = DateTime.Now;
            DecodeWithBruteForce();
            DateTime end = DateTime.Now;
            TimeSpan sure = end - start;
            MessageBox.Show(textBoxCipherText.Text.Length + " karekterlik metine, Brute Force " + (sure.Milliseconds / 1000.0).ToString("#.###") + " saniye sürede tamamlandı");

            DecodeWithLetterFrequency();

        }

        private void DecodeWithLetterFrequency()
        {
            chart2.Series.Clear();
            chart2.Titles.Add("Harfler");
            Series series = this.chart2.Series.Add("HARFLER");
            chart2.ChartAreas[0].AxisX.Interval = 1;
            List<MyChar> FrequencyIndexList = TextOrderByFrequency(textBoxCipherText.Text);
            comboBox2.Items.Clear();
            foreach (MyChar mychar in FrequencyIndexList)
            {
                
                series.Points.AddXY(mychar.Value,mychar.Frequency);
                if(mychar.Frequency>0)
                comboBox2.Items.Add(mychar.Value);
              //  MessageBox.Show(mychar.Value + "  " + mychar.Frequency);
            }
        }

        private List<MyChar> TextOrderByFrequency(string p)
        {

            List<MyChar> tempChars = new List<MyChar>();
            string cipherText = textBoxCipherText.Text;
            foreach (char c in alphabet)
            {
                int count = cipherText.Split(c).Length-1;
                int index = alphabet.IndexOf(c);
                MyChar mychar = new MyChar(count,index,c.ToString()) ;
                tempChars.Add(mychar);
            }



            List<MyChar> OrderedList = tempChars.OrderByDescending(c => c.Frequency).ToList();

            return OrderedList;
        }
        private class MyChar
        {
            public int Frequency { get; set; }
            public int Index { get; set; }
            public string Value { get; set; }
            public MyChar(int _f,int _index,string _v)
            {
                this.Frequency = _f;
                this.Index = _index;
                this.Value = _v;
            }

        }
 

        private void DecodeWithBruteForce()
        {
            dataGridViewBruteForce.Rows.Clear();
            dataGridViewBruteForce.ColumnCount = 2;
            dataGridViewBruteForce.Columns[0].HeaderText = "KEY";
            dataGridViewBruteForce.Columns[1].HeaderText = "PREDICTION";

            dataGridViewBruteForce.Columns[0].Width = 100;
            dataGridViewBruteForce.Columns[1].Width = 700;
            dataGridViewBruteForce.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int key = 1; key < alphabetLen; key++)
            {
                DecodeTextBF(key);
            }
        }

        private void DecodeTextBF(int key)
        {
            dataGridViewBruteForce.Rows.Add(key.ToString(), DecodeCeasar(textBoxCipherText.Text, key));
        }
        private string DecodeCeasar(string chipherText, int key)
        {
            string plainText = ""; //Düz metin
            foreach (char c in chipherText)//Şifreli metin içinde dolaşıyoruz
            {
                int index = alphabet.IndexOf(c);
                if (index >= 0)
                {
                    int newindex = (index - key + alphabetLen) % alphabetLen;
                    plainText += alphabet[newindex];//harfin yeni değerini atıyoruz
                }
                else
                {
                    plainText += c;
                }

            }
            return plainText;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (char c in alphabet)
            {
                comboBox1.Items.Add(c);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string plainChar = comboBox2.SelectedItem.ToString();
            string targetChar = comboBox1.SelectedItem.ToString();


            int plainCharIndex = alphabet.IndexOf(plainChar);
            int targetCharIndex = alphabet.IndexOf(targetChar);


            textBox1.Text = DecodeCeasar(textBoxCipherText.Text,plainCharIndex-targetCharIndex);

            //MessageBox.Show(plainCharIndex.ToString() + " --- " + targetCharIndex.ToString());
        }
    }
}
