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
    public partial class FrmMusteri : Form
    {
        public FrmMusteri()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-H0C41H8;Initial Catalog=SatisVT;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }


        void listele()
        {
            
            //Listele isimli bir metodd tanımlıyoruz ki her yerde listeleme için kod yazılması.
            //Datagridviewe veri tabanındaki bilgileri çeken kodlar
            SqlCommand komut = new SqlCommand("SELECT MUSTERIID AS 'Müşteri ID', MUSTERIAD AS 'Müşteri Adı', MUSTERISOYAD AS 'Müşteri Soyadı', MUSTERISEHIR AS 'Şehir', MUSTERIBAKIYE AS 'Bakiye' FROM TBLMUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }


        void temizle()
        {
            //butonların durumunu ayarlayan kodlar
            BtnListele.Enabled = true;
            BtnKaydet.Enabled = true;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;

            TxtID.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            CmbSehir.Text = "";
            TxtBakiye.Text = "";
        }


        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            listele();



            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLSEHIR", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbSehir.Items.Add(dr["SEHIRAD"]);
            }
            baglanti.Close();

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridviewe çift tıklanınca verileri textboxlara getiren kodlar
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            //butonların durumunu ayarlayan kodlar
            BtnListele.Enabled = true;
            BtnKaydet.Enabled = false;
            BtnGuncelle.Enabled = true;
            BtnSil.Enabled = true;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();

            temizle();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text != "" && TxtSoyad.Text != "" && TxtBakiye.Text != "" && CmbSehir.Text!="")
            {
                //Veritabanına yeni kayıt ekleyen kodlar
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO TBLMUSTERI (MUSTERIAD, MUSTERISOYAD,MUSTERISEHIR,MUSTERIBAKIYE) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", CmbSehir.Text.ToUpper());
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtBakiye.Text));
            
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                listele();

                temizle();

            }
            else
            {
                MessageBox.Show("Kayıt için tüm alanları doldurmanız gerekmektedir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("DELETE FROM TBLMUSTERI WHERE MUSTERIID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Müşteri başarıyla silindi.", "Başarılı.", MessageBoxButtons.OK);
            baglanti.Close();

            listele();
            temizle();
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLMUSTERI set MUSTERIAD=@P1, MUSTERISOYAD=@P2, MUSTERISEHIR=@P3,MUSTERIBAKIYE=@P4 where MUSTERIID=@P5", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            komut.Parameters.AddWithValue("@P5", TxtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Müşteri başarıyla güncellendi.", "Başarılı.", MessageBoxButtons.OK);
            baglanti.Close();

            listele();
            temizle();

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text != "")
            {
                SqlCommand komut = new SqlCommand("select * from TBLMUSTERI where MUSTERIAD=@P1", baglanti);
                komut.Parameters.AddWithValue("@P1", TxtAd.Text);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Lütfen ad kutucuğuna aramak istediğiniz müşteri adını giriniz.", "Hata", MessageBoxButtons.OK);
            }
        }
    }
}
