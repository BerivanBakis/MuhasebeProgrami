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
    public partial class AdvancePage : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        String advanceId;
        private string sqlQuery = "";
        public AdvancePage()
        {
            InitializeComponent();
        }

        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection("server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True");

            baglanti.Open();
            if (sqlQuery.Length == 0)
            {
                sqlQuery = "SELECT a.id, e.name AS [Ad Soyad], a.advance_amount AS [Avans Miktarı], a.date AS [Tarih], " +
                        "CASE WHEN a.is_cash = 0 THEN 'Nakit' WHEN a.is_cash = 1 THEN 'Banka' END AS [Nakit/Kart] " +
                        "FROM advance_table a " +
                        "JOIN employee_table e ON a.employee_id = e.id " +
                        "ORDER BY a.id;";
            }
            da = new SqlDataAdapter(sqlQuery, baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        private void LoadEmployeeData()
        {
            string connectionString = "server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True";
            string query = "SELECT id, name FROM employee_table where is_active = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    cmbxEmployee.Items.Clear();
                    cmbxFilter.Items.Clear();

                    while (reader.Read())
                    {
                        cmbxEmployee.Items.Add(new
                        {
                            Value = reader["id"],
                            Text = reader["name"]
                        });
                        cmbxFilter.Items.Add(new
                        {
                            Value = reader["id"],
                            Text = reader["name"]
                        });
                    }

                    cmbxEmployee.DisplayMember = "Text"; // Gösterilecek alan
                    cmbxEmployee.ValueMember = "Value"; // Değer alanı
                    cmbxEmployee.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Otomatik tamamlama modu
                    cmbxEmployee.AutoCompleteSource = AutoCompleteSource.ListItems; // Veri kaynağı

                    cmbxFilter.DisplayMember = "Text"; 
                    cmbxFilter.ValueMember = "Value"; 
                    cmbxFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbxFilter.AutoCompleteSource = AutoCompleteSource.ListItems; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void AdvancePage_Load_1(object sender, EventArgs e)
        {
            LoadEmployeeData();
            VeritabanıBaglanti();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                advanceId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtAdvance.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                string employeeName = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                // Combobox'taki öğeleri kontrol et
                foreach (var item in cmbxEmployee.Items)
                {
                    if (((dynamic)item).Text == employeeName)
                    {
                        cmbxEmployee.SelectedItem = item; // Seçilen öğeyi combobox'ta göster
                        break;
                    }
                }
                if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Nakit")
                {
                    checkBox1.Checked = true; // Checkbox işaretli
                }
                else
                {
                    checkBox1.Checked = false; // Checkbox işareti kaldır
                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string employeeId = ((dynamic)cmbxEmployee.SelectedItem).Value.ToString();
            string sorgu = "INSERT INTO advance_table(employee_id, advance_amount, date, is_cash) VALUES (@employee_id, @advance_amount, @date, @is_cash)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@employee_id", employeeId);
            komut.Parameters.AddWithValue("@advance_amount", txtAdvance.Text);
            komut.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            int isCash = checkBox1.Checked ? 0 : 1;
            komut.Parameters.AddWithValue("@is_cash", isCash);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            VeritabanıBaglanti();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string employeeId = ((dynamic)cmbxEmployee.SelectedItem).Value.ToString();
            string sorgu = "UPDATE advance_table SET employee_id=@employee_id, advance_amount=@advance_amount, date=@date, is_cash=@is_cash WHERE id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@employee_id", employeeId);
            komut.Parameters.AddWithValue("@advance_amount", txtAdvance.Text);
            komut.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            int isCash = checkBox1.Checked ? 0 : 1;
            komut.Parameters.AddWithValue("@is_cash", isCash);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(advanceId));
            baglanti.Open();
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE from advance_table where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(advanceId));
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string selectedEmployee = ((dynamic)cmbxFilter.SelectedItem)?.Text.ToString();
            int selectedMonth = dateTimePicker2.Value.Month;

            string query = "SELECT a.id, e.name AS [Ad Soyad], a.advance_amount AS [Avans Miktarı], a.date AS [Tarih], " +
                        "CASE WHEN a.is_cash = 0 THEN 'Nakit' WHEN a.is_cash = 1 THEN 'Banka' END AS [Nakit/Kart] " +
                        "FROM advance_table a " +
                        "JOIN employee_table e ON a.employee_id = e.id WHERE 1=1";

            if (!string.IsNullOrEmpty(selectedEmployee))
            {
                query += " AND e.name = '" + selectedEmployee + "' ";
            }

            query += " AND MONTH(a.date) = " + selectedMonth;
            sqlQuery = query;
            VeritabanıBaglanti();
        }


        private void btnTimeTracking_Click(object sender, EventArgs e)
        {
            WorkFlowPage workFlowPage = new WorkFlowPage();
            workFlowPage.FormClosed += (s, args) => { Application.Exit(); };
            this.Close();
            workFlowPage.Show();
        }

        private void btnpuantaj_Click_1(object sender, EventArgs e)
        {
            TimeTrackingPage timetracking = new TimeTrackingPage();
            timetracking.FormClosed += (s, args) => { Application.Exit(); };
            this.Close();
            timetracking.Show();
        }

        private void btnavans_Click_1(object sender, EventArgs e)
        {
            AdvancePage advance = new AdvancePage();
            advance.FormClosed += (s, args) => { Application.Exit(); };
            this.Close();
            advance.Show();
        }

        private void btnpersonel_Click_1(object sender, EventArgs e)
        {
            EmployeeInformationPage employeeInformationpage = new EmployeeInformationPage();
            employeeInformationpage.FormClosed += (s, args) => { Application.Exit(); };
            this.Close();
            employeeInformationpage.Show();
        }

        private void btnhakedis_Click_1(object sender, EventArgs e)
        {
            Hakedis hakedispage = new Hakedis();
            hakedispage.FormClosed += (s, args) => { Application.Exit(); };
            this.Close();
            hakedispage.Show();
        }
    }
}
