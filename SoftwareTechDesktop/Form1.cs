using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SoftwareTechDesktop
{
    public partial class Form1 : Form
    {
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter da1;

        public Form1()
        {
            InitializeComponent();

            baglanti = new OleDbConnection();
            komut = new OleDbCommand();
            da1 = new OleDbDataAdapter();

            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            baglanti.ConnectionString = baglan;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login(textBox1.Text, textBox2.Text);
        }

        // Kullanıcı girişi işlemi ve ogr_no alma işlemi
        private bool Login(string kullaniciadi, string sifre)
        {
            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            baglanti.ConnectionString = baglan;

            // Sorguya ogr_no ve kullanici_tipi dahil edildi
            string sorgu = "SELECT ogr_no, kullanici_tipi FROM giris WHERE kullanici_adi = @kullanici_adi AND sifre = @sifre";

            try
            {
                using (var baglanti = new OleDbConnection(baglan))
                {
                    using (var komut = new OleDbCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@kullanici_adi", kullaniciadi);
                        komut.Parameters.AddWithValue("@sifre", sifre);

                        baglanti.Open();

                        // Veritabanındaki sonucu okumak için DataReader kullanıyoruz
                        OleDbDataReader reader = komut.ExecuteReader();

                        // Eğer kayıt varsa
                        if (reader.Read())
                        {
                            string ogrNo = reader["ogr_no"].ToString(); // ogr_no'yu alıyoruz
                            string kullaniciTipi = reader["kullanici_tipi"].ToString(); // kullanici_tipi'ni alıyoruz

                            // Öğrenci ise öğrenci ana sayfasına yönlendir
                            if (kullaniciTipi == "ogrenci")
                            {
                                OgrAnaSayfa ogrAnaSayfa = new OgrAnaSayfa();
                                ogrAnaSayfa.ogrNo = ogrNo; // ogr_no'yu öğrenci ana sayfasına gönderiyoruz
                                ogrAnaSayfa.Show();
                                this.Hide();
                            }
                            // Admin ise admin ana sayfasına yönlendir
                            else if (kullaniciTipi == "admin")
                            {
                                AdminAnaSayfa adminAnaSayfa = new AdminAnaSayfa();
                                adminAnaSayfa.Show();
                                this.Hide();
                            }

                            return true; // Giriş başarılı
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false; // Giriş başarısız
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Giriş sırasında hata oluştu
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Şifreyi gösterme/gizleme
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else if (textBox2.UseSystemPasswordChar == false)
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
