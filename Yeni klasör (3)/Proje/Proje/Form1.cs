using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
namespace Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string sifre="";
        int giris = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            dataGridView1.Hide();
            giris = 0;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //gizleme
            if (textBox2.UseSystemPasswordChar==true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="admin")
            {
                AdminGetir();
                sifre = dataGridView1.Rows[0].Cells[1].Value.ToString();
                if (textBox2.Text==sifre)
                {
                    adminPanel frm = new adminPanel();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("sifre yanlýþ");
                }
            }
            else if (textBox1.Text=="")
            {
                MessageBox.Show("Boþ býrakmayýnýz");
            }
            else
            {
               
                KullanýcýlarýGetir();
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString()==textBox1.Text)
                    {
                        if (textBox2.Text== dataGridView1.Rows[i].Cells[1].Value.ToString())
                        {
                            KullanýcýPaneli frm = new KullanýcýPaneli();
                            frm.Show();
                            this.Hide(); 
                            giris = 1;

                        }
                        
                        break;
                    }
                }
                if (giris==0)
                {
                    MessageBox.Show("Hatalý kullanýcý adý veya þifre","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

              
            }
        }

        void AdminGetir()
        {
                XmlDocument i = new XmlDocument();
                DataSet ds = new DataSet();
                //xml dosyamýzý okumak için bir reader oluþturuyoruz.
                XmlReader xmlFile;
                xmlFile = XmlReader.Create(@"admin.xml", new XmlReaderSettings());
                //içeriði Dataset e aktarýyoruz.
                ds.ReadXml(xmlFile);
                //datagridviewin kaynaðý olarak dataseti gösteriyoruz.
                dataGridView1.DataSource = ds.Tables[0];
                xmlFile.Close();
            
        }
        void KullanýcýlarýGetir()
        {
            XmlDocument i = new XmlDocument();
            DataSet ds = new DataSet();
            //xml dosyamýzý okumak için bir reader oluþturuyoruz.
            XmlReader xmlFile;
            xmlFile = XmlReader.Create(@"users.xml", new XmlReaderSettings());
            //içeriði Dataset e aktarýyoruz.
            ds.ReadXml(xmlFile);
            //datagridviewin kaynaðý olarak dataseti gösteriyoruz.
            dataGridView1.DataSource = ds.Tables[0];
            xmlFile.Close();

        }
    }
}