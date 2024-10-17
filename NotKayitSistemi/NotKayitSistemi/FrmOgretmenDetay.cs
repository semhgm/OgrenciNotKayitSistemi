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

namespace NotKayitSistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SEMIH\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet1.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet1.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into TBLDERS (OGRID,OGRAD,OGRSOYAD)" +
                " values(@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mskId.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("öğrenci sisteme eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet1.TBLDERS);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            mskId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();    
            txtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string durum;

            double ortalama,s1,s2,s3;
            s1 = Convert.ToDouble(txtSinav1.Text);
            s2 = Convert.ToDouble(txtSinav2.Text);
            s3 = Convert.ToDouble(txtSinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblOrt.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                durum = "True";
                lblDurum.Text = "geçti";
            
            }
            else
            {
                durum = "False";
                lblDurum.Text = "kaldı";

            }
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("UPDATE TBLDERS SET OGRS1 = @P1,OGRS2=@P2,OGRS3=@P3,ORT=@P4, DURUM=@P5 WHERE OGRID=@P6", baglanti);
            cmd.Parameters.AddWithValue("@p1", txtSinav1.Text);
            cmd.Parameters.AddWithValue("@p2", txtSinav2.Text);
            cmd.Parameters.AddWithValue("@p3", txtSinav3.Text);
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(lblOrt.Text));
            cmd.Parameters.AddWithValue("@p5", durum);
            cmd.Parameters.AddWithValue("@p6", mskId.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("öğrenci notları güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet1.TBLDERS);


        }


    }
}
