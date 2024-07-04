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
                    MessageBox.Show("sifre yanl��");
                }
            }
            else if (textBox1.Text=="")
            {
                MessageBox.Show("Bo� b�rakmay�n�z");
            }
            else
            {
               
                Kullan�c�lar�Getir();
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString()==textBox1.Text)
                    {
                        if (textBox2.Text== dataGridView1.Rows[i].Cells[1].Value.ToString())
                        {
                            Kullan�c�Paneli frm = new Kullan�c�Paneli();
                            frm.Show();
                            this.Hide(); 
                            giris = 1;

                        }
                        
                        break;
                    }
                }
                if (giris==0)
                {
                    MessageBox.Show("Hatal� kullan�c� ad� veya �ifre","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

              
            }
        }

        void AdminGetir()
        {
                XmlDocument i = new XmlDocument();
                DataSet ds = new DataSet();
                //xml dosyam�z� okumak i�in bir reader olu�turuyoruz.
                XmlReader xmlFile;
                xmlFile = XmlReader.Create(@"admin.xml", new XmlReaderSettings());
                //i�eri�i Dataset e aktar�yoruz.
                ds.ReadXml(xmlFile);
                //datagridviewin kayna�� olarak dataseti g�steriyoruz.
                dataGridView1.DataSource = ds.Tables[0];
                xmlFile.Close();
            
        }
        void Kullan�c�lar�Getir()
        {
            XmlDocument i = new XmlDocument();
            DataSet ds = new DataSet();
            //xml dosyam�z� okumak i�in bir reader olu�turuyoruz.
            XmlReader xmlFile;
            xmlFile = XmlReader.Create(@"users.xml", new XmlReaderSettings());
            //i�eri�i Dataset e aktar�yoruz.
            ds.ReadXml(xmlFile);
            //datagridviewin kayna�� olarak dataseti g�steriyoruz.
            dataGridView1.DataSource = ds.Tables[0];
            xmlFile.Close();

        }
    }
}