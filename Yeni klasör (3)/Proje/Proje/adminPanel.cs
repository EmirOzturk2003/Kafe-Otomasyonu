using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Proje
{
    public partial class adminPanel : Form
    {
        public adminPanel()
        {
            InitializeComponent();
        }
        bool kayit=true;
        bool sil = true;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="" && textBox2.Text!="")
            {
               

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (textBox1.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                    {
                        kayit = false;
                        break;
                    }
                }

                if (kayit == true)
                {
                    XDocument xDoc = XDocument.Load(@"users.xml");
                    XElement rootElement = xDoc.Root;
                    XElement newElement = new XElement("Users");
                    XElement Kullanici_AdiElement = new XElement("Kullanici_Adi", textBox1.Text);
                    XElement SifreElement = new XElement("Sifre", textBox2.Text);
                    newElement.Add(Kullanici_AdiElement, SifreElement);
                    rootElement.Add(newElement);
                    xDoc.Save(@"users.xml");
                    MessageBox.Show("Kullanıcı Eklendi");
                    KullanıcılarıGetir();
                }
                else
                {
                    kayit = true;
                    MessageBox.Show(textBox1.Text + " adlı kullanıcı zaten var","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Boş bırakmayınız", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void adminPanel_Load(object sender, EventArgs e)
        {
            UrunleriGetir();
            UrunleriComboBoxaGetir();
            KullanıcılarıGetir();
            dataGridView1.Hide();
           dataGridView2.Hide();
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox3.Hide();



        }
        void KullanıcılarıGetir()
        {

            XmlDocument i = new XmlDocument();
            DataSet ds = new DataSet();
            //xml dosyamızı okumak için bir reader oluşturuyoruz.
            XmlReader xmlFile;
            xmlFile = XmlReader.Create(@"users.xml", new XmlReaderSettings());
            //içeriği Dataset e aktarıyoruz.
            ds.ReadXml(xmlFile);
            //datagridviewin kaynağı olarak dataseti gösteriyoruz.
            dataGridView1.DataSource = ds.Tables[0];
            xmlFile.Close();

        }
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
            for (int i = 0; i < dataGridView2.RowCount-1; i++)
            {
                comboBox1.Items.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (textBox4.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    kayit = true;   
                    break;
                }
            }
            if (kayit==true)
            {
                int kayıtsayisi = dataGridView1.RowCount;
                XDocument xdosya = XDocument.Load(@"users.xml");
                xdosya.Root.Elements().Where(x => x.Element("Kullanici_Adi").Value.ToString() == textBox4.Text).Remove();
                xdosya.Save(@"users.xml");
                KullanıcılarıGetir();
                if (kayıtsayisi==dataGridView1.RowCount)
                {
                    MessageBox.Show("KULLANICI BULUNAMADI", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Kullanıcı Silinmiştir");
                }

            }
            else
            {  
                MessageBox.Show(textBox4.Text + " adlı kullanıcı zaten kayıtlı değil");
                kayit=false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
            groupBox2.Hide();
            groupBox3.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Show();
            groupBox1.Hide();
            groupBox3.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox3.Show();
            groupBox2.Hide();
            groupBox1.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text!="" && textBox3.Text!="")
            {
             XDocument xdosya = XDocument.Load(@"urunler.xml");
            XElement element = xdosya.Element("Urunler").Elements("Urunler").FirstOrDefault(x => x.Element("Urun_Adi").Value == comboBox1.Text);
            if (element!=null)
            {
                element.SetElementValue("Urun_Fiyatı", textBox3.Text);
                xdosya.Save(@"urunler.xml");
                MessageBox.Show("Ürun fiyatı Güncellendi");
                UrunleriGetir();
            }
            }
            else
            {
                MessageBox.Show("Boş bırakmayınız!!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }   
    
        private void button7_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            groupBox3.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
                this.Hide();
                frm.Show();
        }
    }
}
