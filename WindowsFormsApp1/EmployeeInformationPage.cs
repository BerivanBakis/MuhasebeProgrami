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

namespace WindowsFormsApp1
{
    public partial class EmployeeInformationPage : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        String employeeId;
        public EmployeeInformationPage()
        {
            InitializeComponent();
            this.Text = "Çalışan Bilgisi Sayfası";
        }

        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection("server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True");

            baglanti.Open();
            da = new SqlDataAdapter("SELECT id, name AS [Ad Soyad], iban as [IBAN], wage as [Yevmiye], is_active as [Aktiflik] FROM employee_table ORDER BY id;", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        private void EmployeeInformationPage_Load(object sender, EventArgs e)
        {
            VeritabanıBaglanti();
        }

     
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO employee_table(name, iban, wage, is_active) VALUES (@name, @iban, @wage, @is_active)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@name", txtName.Text);
            komut.Parameters.AddWithValue("@iban", txtIban.Text);
            komut.Parameters.AddWithValue("@wage", txtWage.Text);
            komut.Parameters.AddWithValue("@is_active", chckActive.Checked);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            VeritabanıBaglanti();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE from employee_table where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(employeeId));
            baglanti.Open();
            int satirSayisi = komut.ExecuteNonQuery();
            baglanti.Close();
            if (satirSayisi > 0)
            {
                MessageBox.Show("Silme işlemi başarılı.");
            }
            else
            {
                MessageBox.Show("Silme işlemi başarısız. Lütfen kontrol edin.");
            }
            VeritabanıBaglanti();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE employee_table SET name=@name, iban=@iban, wage=@wage, is_active=@is_active WHERE id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@name", txtName.Text);
            komut.Parameters.AddWithValue("@iban", txtIban.Text);
            komut.Parameters.AddWithValue("@wage", txtWage.Text);
            komut.Parameters.AddWithValue("@is_active", chckActive.Checked);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(employeeId));
            baglanti.Open();
            komut.ExecuteNonQuery();
            int satirSayisi = komut.ExecuteNonQuery();
            baglanti.Close();
            if (satirSayisi > 0)
            {
                MessageBox.Show("Güncelleme işlemi başarılı.");
            }
            else
            {
                MessageBox.Show("Güncelleme işlemi başarısız. Lütfen kontrol edin.");
            }
            VeritabanıBaglanti();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                employeeId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtIban.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtWage.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                chckActive.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[4].Value);

            }
        }

        private void txtWage_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
