using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Proje
{
    public partial class KullanıcıPaneli : Form
    {
        public KullanıcıPaneli()
        {
            InitializeComponent();
        }
        int urunfiyat;
        int hesap = 0;
        void UrunleriGetir()
        {
            XmlDocument i = new XmlDocument();
            DataSet ds = new DataSet();
            //xml dosyamızı okumak için bir reader oluşturuyoruz.
            XmlReader xmlFile;
            xmlFile = XmlReader.Create(@"urunler.xml", new XmlReaderSettings());
            //içeriği Dataset e aktarıyoruz.
            ds.ReadXml(xmlFile);
            //datagridviewin kaynağı olarak dataseti gösteriyoruz.
            dataGridView2.DataSource = ds.Tables[0];
            xmlFile.Close();
        }
        void UrunleriComboBoxaGetir()
        {
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
            }
        }
        private void KullanıcıPaneli_Load(object sender, EventArgs e)
        {
            dataGridView2.Hide();
            UrunleriGetir();
            UrunleriComboBoxaGetir();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < dataGridView2.RowCount-1; i++)
            {
                if (comboBox1.Text == dataGridView2.Rows[i].Cells[0].Value.ToString())
                {
                    
                    urunfiyat = Convert.ToInt16(dataGridView2.Rows[i].Cells[1].Value);
                    urunfiyat *= Convert.ToInt16(textBox1.Text);
                    break;
                }
            }
            richTextBox1.Text += "'"+comboBox1.Text+"'                 "+textBox1.Text+ "x                 Fiyatı:" + urunfiyat+"tl\n";
            hesap += urunfiyat;
            urunfiyat = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "\n                TOPLAM FİYAT:"+hesap+"tl";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            richTextBox1.Text = "";
            hesap = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
