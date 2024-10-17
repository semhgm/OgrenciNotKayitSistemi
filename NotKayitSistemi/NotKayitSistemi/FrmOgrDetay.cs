using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotKayitSistemi
{
    public partial class FrmOgrDetay : Form
    {
        public FrmOgrDetay()
        {
            InitializeComponent();
        }
        public string numara;
        SqlConnection baglanti = new SqlConnection(@"Data Source=SEMIH\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True");



        private void FrmOgrDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * From TBLDERS Where OGRNO =@p1",baglanti);
            cmd.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = cmd.ExecuteReader(); 
            while(dr.Read())
            {
                LblIsım.Text = dr[2].ToString()+" "+ dr[3].ToString();
                LblSinav1.Text= dr[4].ToString();
                LblSinav2.Text= dr[5].ToString();
                LblSinav3.Text= dr[6].ToString();
                LblOrt.Text= dr[7].ToString();
                LblDurum.Text= dr[8].ToString();

            }
            baglanti.Close();


        }
    }
}
