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
    public partial class FrmKategoriler : Form
    {
        public FrmKategoriler()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-H0C41H8;Initial Catalog=SatisVT;Integrated Security=True");

        private void BtnListele_Click(object sender, EventArgs e)
        {
            //datagridview e veritabanını dolduran listeleme kodları
            SqlCommand komut = new SqlCommand("select KATEGORIID AS 'Kategori ID', KATEGORIADI AS 'Kategori Adı' from TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //buton görünürlük ayarları
            TxtAd.Text = "";
            TxtID.Text = "";
            BtnKaydet.Enabled = true;
            BtnSil.Enabled = false;
            BtnGuncelle.Enabled = false;


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridviewe çift tıklayınca verileri textbxa yazan kodlar
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            BtnKaydet.Enabled = false;
            BtnSil.Enabled = true;
            BtnGuncelle.Enabled = true;

        }

        private void FrmKategoriler_Load(object sender, EventArgs e)
        {
            //Form yüklenirken datagridview 1'e veri yükleyen kodlar
            SqlCommand komut = new SqlCommand("select KATEGORIID AS 'Kategori ID', KATEGORIADI AS 'Kategori Adı' from TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if(TxtAd.Text!="")
            {
                //Veritabanına yeni kayıt ekleyen kodlar
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO TBLKATEGORI (KATEGORIADI) VALUES (@P1)", baglanti);
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //datagridview e veritabanını dolduran listeleme kodları
                SqlCommand komut2 = new SqlCommand("select KATEGORIID AS 'Kategori ID', KATEGORIADI AS 'Kategori Adı' from TBLKATEGORI", baglanti);
                SqlDataAdapter da = new SqlDataAdapter(komut2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                //buton görünürlük ayarları
                TxtAd.Text = "";
                TxtID.Text = "";
                BtnKaydet.Enabled = true;
                BtnSil.Enabled = false;
                BtnGuncelle.Enabled = false;

            }
            else
            {
                MessageBox.Show("Kayıt için kategori adı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLKATEGORI WHERE KATEGORIID=@P1", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //datagridview e veritabanını dolduran listeleme kodları
            SqlCommand komut2 = new SqlCommand("select KATEGORIID AS 'Kategori ID', KATEGORIADI AS 'Kategori Adı' from TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //buton görünürlük ayarları
            TxtAd.Text = "";
            TxtID.Text = "";
            BtnKaydet.Enabled = true;
            BtnSil.Enabled = false;
            BtnGuncelle.Enabled = false;

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLKATEGORI SET KATEGORIADI=@P1 WHERE KATEGORIID=@P2", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //datagridview e veritabanını dolduran listeleme kodları
            SqlCommand komut2 = new SqlCommand("select KATEGORIID AS 'Kategori ID', KATEGORIADI AS 'Kategori Adı' from TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //buton görünürlük ayarları
            TxtAd.Text = "";
            TxtID.Text = "";
            BtnKaydet.Enabled = true;
            BtnSil.Enabled = false;
            BtnGuncelle.Enabled = false;
        }
    }
}


//Data Source=DESKTOP-H0C41H8;Initial Catalog=SatisVT;Integrated Security=True