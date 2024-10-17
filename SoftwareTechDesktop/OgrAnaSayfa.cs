using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace SoftwareTechDesktop
{
    public partial class OgrAnaSayfa : Form
    {
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter da1;

        public string ogrNo;
        public OgrAnaSayfa()
        {
            InitializeComponent();

            baglanti = new OleDbConnection();
            komut = new OleDbCommand();
            da1 = new OleDbDataAdapter();

            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            baglanti.ConnectionString = baglan;
        }

        private void OgrAnaSayfa_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

            // Veritabanı bağlantısı ve sorgu
            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            string sorgu = "SELECT * FROM notlar WHERE ogr_no = @ogr_no";// ogr_no'ya göre filtreleme yapıyoruz

            // Veritabanı bağlantısı ve veri çekme işlemi
            using (OleDbConnection baglanti = new OleDbConnection(baglan))
            {
                using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                {
                    // Parametreyi ekliyoruz (ogr_no)
                    komut.Parameters.AddWithValue("@ogr_no", ogrNo);

                    // DataAdapter ve DataSet ile veriyi alıyoruz
                    da1 = new OleDbDataAdapter(komut);
                    DataSet al = new DataSet();
                    da1.Fill(al, "notlar");

                    // Veriyi DataGridView'e bağlıyoruz
                    dataGridView1.DataSource = al.Tables["notlar"];
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        string talep;
        private void button4_Click(object sender, EventArgs e)
        {
            // Talep değerini doğru şekilde atıyoruz
            talep = "Kursumu Dondurmak İstiyorum.";

            // E-postayı gönderiyoruz
            SendEmail("emirhanneren24@gmail.com"); // Alıcı mail adresi
        }

        private void button5_Click(object sender, EventArgs e)
        {
            talep = "Kursumu İptal Etmek İstiyorum.";

            SendEmail("emirhanneren24@gmail.com");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            talep = "Ek Ders Almak İstiyorum";

            SendEmail("emirhanneren24@gmail.com");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            talep = "Yeni Kurs Almak İstiyorum";

            SendEmail("emirhanneren24@gmail.com");
        }
        private void button11_Click(object sender, EventArgs e)
        {
            talep = "Konu Tekrarına İhtiyacım Var.";

            SendEmail("emirhanneren24@gmail.com");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            talep = "Bu Ay Ödeme Yapamayacağım.";

            SendEmail("emirhanneren24@gmail.com");
        }

        private void SendEmail(string toAddress)
        {
            // SMTP ve MailMessage sınıflarını kullanarak mail gönderme
            string fromMail = "taleprobotu@gmail.com"; // Gönderici mail adresi
            //string fromPassword = "emeren20034"; // Gönderici mail şifresi
            string mailSubject = talep;

            // Talep ve ogrNo'yu mailBody içinde kullanıyoruz
            string mailBody = talep + " Ben " + ogrNo + " nolu öğrencinizim. Teşekkür ederim.\n\nSoftware Tech.";

            // MailMessage nesnesi oluştur
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromMail);
            mail.To.Add(toAddress);
            mail.Subject = mailSubject;
            mail.Body = mailBody;

            // SMTP client ayarları
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; // SMTP port, genellikle TLS için 587 kullanılır
            smtpClient.EnableSsl = true; // Güvenli bağlantı için
            smtpClient.Credentials = new NetworkCredential(fromMail, "xzbn zjyk pxjy itqf");

            // E-posta gönder
            try
            {
                smtpClient.Send(mail);
                MessageBox.Show("Mail başarıyla gönderildi. Size en yakın zamanda ulaşacağız!" , "Talebiniz Alındı!" , MessageBoxButtons.OK , MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderme hatası: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            string sorgu = "SELECT * FROM notlar WHERE ogr_no = @ogr_no";// ogr_no'ya göre filtreleme yapıyoruz

            // Veritabanı bağlantısı ve veri çekme işlemi
            using (OleDbConnection baglanti = new OleDbConnection(baglan))
            {
                using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                {
                    // Parametreyi ekliyoruz (ogr_no)
                    komut.Parameters.AddWithValue("@ogr_no", ogrNo);

                    // DataAdapter ve DataSet ile veriyi alıyoruz
                    da1 = new OleDbDataAdapter(komut);
                    DataSet al = new DataSet();
                    da1.Fill(al, "notlar");

                    // Veriyi DataGridView'e bağlıyoruz
                    dataGridView1.DataSource = al.Tables["notlar"];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            string sorgu = "SELECT * FROM ders";

            // Veritabanı bağlantısı ve veri çekme işlemi
            using (OleDbConnection baglanti = new OleDbConnection(baglan))
            {
                using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                {
                    // DataAdapter ve DataSet ile veriyi alıyoruz
                    da1 = new OleDbDataAdapter(komut);
                    DataSet al = new DataSet();
                    da1.Fill(al, "ders");

                    // Veriyi DataGridView'e bağlıyoruz
                    dataGridView1.DataSource = al.Tables["ders"];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string baglan = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\softwaretech.mdb";
            string sorgu = "SELECT * FROM odeme WHERE ogr_no = @ogr_no";// ogr_no'ya göre filtreleme yapıyoruz

            // Veritabanı bağlantısı ve veri çekme işlemi
            using (OleDbConnection baglanti = new OleDbConnection(baglan))
            {
                using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                {
                    // Parametreyi ekliyoruz (ogr_no)
                    komut.Parameters.AddWithValue("@ogr_no", ogrNo);

                    // DataAdapter ve DataSet ile veriyi alıyoruz
                    da1 = new OleDbDataAdapter(komut);
                    DataSet al = new DataSet();
                    da1.Fill(al, "odeme");

                    // Veriyi DataGridView'e bağlıyoruz
                    dataGridView1.DataSource = al.Tables["odeme"];
                }
            }
        }
    }
}
