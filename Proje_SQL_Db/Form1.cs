using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_SQL_Db
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FrmKategoriler fr = new FrmKategoriler();
            fr.Show();
            this.Hide();

        }
        
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-H0C41H8;Initial Catalog=SatisVT;Integrated Security=True");

        private void BtnMusteri_Click(object sender, EventArgs e)
        {
            FrmMusteri fr = new FrmMusteri();
            fr.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //manuel şekilde kritik seviyedeki ürünleri getiren kodlar

            //baglanti.Open();
            //SqlCommand komut = new SqlCommand("select URUNID AS 'Ürün ID',URUNMARKA as 'Ürün Markası', URUNADI as 'Ürün Adı', URUNSTOK as 'Ürün Stoğu' from TBLURUN where URUNSTOK<=5",baglanti);
            //SqlDataAdapter da = new SqlDataAdapter(komut);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //baglanti.Close();


            //prosedür kullanarak kritik seviyedeki ürünleri getiren kodlar
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Execute test4", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();


            //grafiğe veri çekme işlemleri

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT KATEGORIADI, COUNT(*) FROM TBLKATEGORI INNER JOIN TBLURUN ON TBLKATEGORI.KATEGORIID=TBLURUN.URUNKATEGORI GROUP BY KATEGORIADI", baglanti);
            SqlDataReader dr = komut2.ExecuteReader();
            while(dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();

        }
    }
}
