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
    public partial class Hakedis : Form
    {
        private SqlConnection baglanti;
        private SqlDataAdapter da;
        private string connectionString = "server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True";
        private string sqlQuery = "";
        public Hakedis()
        {
            InitializeComponent();
        }
        private void Hakedis_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            VeritabanıBaglanti();
        }

        private List<(TimeSpan start, TimeSpan end, string description)> breakTimes = new List<(TimeSpan, TimeSpan, string)>
        {
            (new TimeSpan(10, 0, 0), new TimeSpan(10, 15, 0), "Çay Molası"),
            (new TimeSpan(12, 30, 0), new TimeSpan(13, 0, 0), "Yemek"),
            (new TimeSpan(15, 0, 0), new TimeSpan(15, 15, 0), "Çay Molası"),
            (new TimeSpan(18, 0, 0), new TimeSpan(18, 30, 0), "Yemek"),
            (new TimeSpan(22, 0, 0), new TimeSpan(23, 30, 0), "Çay Molası")
        };

        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection(connectionString);
            baglanti.Open();

            int selectedMonth = dateTimePicker1.Value.Month;
            string selectedEmployeeName = ((dynamic)cmbxEmployee.SelectedItem)?.Text.ToString();

            string sqlQuery = $@"
SELECT e.id, e.name, e.iban, e.wage, w.start_time, w.end_time, w.date
FROM workflow_table w
JOIN employee_table e ON w.employee_id = e.id
WHERE MONTH(w.date) = {selectedMonth} ";


            // Eğer bir çalışan ismi seçildiyse, sorguya çalışan adı filtresi ekliyoruz
            if (!string.IsNullOrEmpty(selectedEmployeeName))
            {
                sqlQuery += $" AND e.name = '{selectedEmployeeName}' ";  // Çalışan adıyla filtreleme
            }

            sqlQuery += @"
    ORDER BY e.name;";

            // Verileri çekmek için SqlDataAdapter kullan
            da = new SqlDataAdapter(sqlQuery, baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);

            // Avansları almak için ayrı bir sorgu
            var employeeAdvances = new Dictionary<int, decimal>();
            var advanceQuery = $@"
    SELECT employee_id, SUM(advance_amount) AS TotalAdvance
    FROM advance_table
    WHERE MONTH(date) = {selectedMonth}  -- Seçilen aya göre filtreleme
    GROUP BY employee_id;";

            da = new SqlDataAdapter(advanceQuery, baglanti);
            DataTable advanceTablo = new DataTable();
            da.Fill(advanceTablo);

            foreach (DataRow row in advanceTablo.Rows)
            {
                int employeeId = Convert.ToInt32(row["employee_id"]);
                decimal totalAdvance = row["TotalAdvance"] != DBNull.Value ? Convert.ToDecimal(row["TotalAdvance"]) : 0m;

                employeeAdvances[employeeId] = totalAdvance;
            }

            // DataGridView'i ayarla
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Calisan", "Çalışan");
            dataGridView1.Columns.Add("ToplamSaat", "Toplam Saat");
            dataGridView1.Columns.Add("ToplamGun", "Toplam Gün");
            dataGridView1.Columns.Add("Avans", "Toplam Avans");
            dataGridView1.Columns.Add("Hakedis", "Toplam Hakediş");
            dataGridView1.Columns.Add("KalanOdeme", "Kalan Ödeme");
            dataGridView1.Columns.Add("Yevmiye", "Yevmiye");
            dataGridView1.Columns.Add("Iban", "IBAN");

            // Kolon genişliklerini ayarla
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].Width = 75;
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[5].Width = 75;
            dataGridView1.Columns[6].Width = 75;
            dataGridView1.Columns[7].Width = 200;

            // Her çalışanın toplam çalışma saatlerini hesapla
            var employeeTotalHours = new Dictionary<string, (TimeSpan totalHours, string iban, decimal totalAdvance, decimal wage)>();

            foreach (DataRow row in tablo.Rows)
            {
                string employeeName = row["name"].ToString();
                string iban = row["iban"].ToString();
                int employeeId = Convert.ToInt32(row["id"]);
                decimal wage = Convert.ToDecimal(row["wage"]);

                // Get the date from the row
                DateTime workDate = Convert.ToDateTime(row["date"]); // Ensure this column is included in the query
                string workDurationString = GetWorkDuration(employeeName, workDate);

                // Parse the formatted work duration
                TimeSpan workDuration = TimeSpan.Zero;
                if (workDurationString != "-")
                {
                    string[] parts = workDurationString.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                    {
                        workDuration = new TimeSpan(hours, minutes, 0);
                    }
                }

                // Update total hours for the employee
                if (!employeeTotalHours.ContainsKey(employeeName))
                {
                    employeeTotalHours[employeeName] = (TimeSpan.Zero, iban, 0m, wage);
                }
                employeeTotalHours[employeeName] = (employeeTotalHours[employeeName].totalHours + workDuration, iban, employeeAdvances.ContainsKey(employeeId) ? employeeAdvances[employeeId] : 0m, wage);
            }


            // DataGridView'a verileri ekle
            foreach (var employee in employeeTotalHours)
            {
                var employeeRow = new DataGridViewRow();
                employeeRow.CreateCells(dataGridView1);
                employeeRow.Cells[0].Value = employee.Key;
                employeeRow.Cells[1].Value = employee.Value.totalHours.TotalHours.ToString("F2");
                decimal totalDays = (decimal)employee.Value.totalHours.TotalHours / 9;
                employeeRow.Cells[2].Value = totalDays.ToString("F2");
                employeeRow.Cells[3].Value = employee.Value.totalAdvance.ToString("C2");
                decimal toplamHakedis = (totalDays * employee.Value.wage);
                employeeRow.Cells[4].Value = toplamHakedis.ToString("C2");
                decimal remainingPayment = toplamHakedis - employee.Value.totalAdvance;
                employeeRow.Cells[5].Value = remainingPayment.ToString("C2");
                employeeRow.Cells[6].Value = employee.Value.wage.ToString("C2");
                employeeRow.Cells[7].Value = employee.Value.iban;
                dataGridView1.Rows.Add(employeeRow);
            }

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

                    cmbxEmployee.Items.Clear(); // Mevcut öğeleri temizleyin

                    while (reader.Read())
                    {
                        cmbxEmployee.Items.Add(new
                        {
                            Value = reader["id"],
                            Text = reader["name"]
                        });
                    }

                    cmbxEmployee.DisplayMember = "Text"; // Gösterilecek alan
                    cmbxEmployee.ValueMember = "Value"; // Değer alanı
                    cmbxEmployee.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Otomatik tamamlama modu
                    cmbxEmployee.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private string GetWorkDuration(string employeeName, DateTime date)
        {
            string duration = "-"; // Varsayılan değer
            string query = "SELECT w.start_time, w.end_time FROM workflow_table w " +
                           "JOIN employee_table e ON w.employee_id = e.id " +
                           "WHERE e.name = @employeeName AND CAST(w.date AS DATE) = @date";

            using (SqlCommand command = new SqlCommand(query, baglanti))
            {
                command.Parameters.AddWithValue("@employeeName", employeeName);
                command.Parameters.AddWithValue("@date", date.Date);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    TimeSpan totalWorkTime = TimeSpan.Zero;
                    while (reader.Read())
                    {
                        TimeSpan startTime = TimeSpan.Parse(reader["start_time"].ToString());
                        TimeSpan endTime = TimeSpan.Parse(reader["end_time"].ToString());

                        // 24 saatlik çalışma durumu kontrolü
                        if (startTime == endTime)
                        {
                            totalWorkTime = new TimeSpan(24, 0, 0);
                        }
                        else
                        {
                            if (endTime < startTime)
                            {
                                endTime = endTime.Add(new TimeSpan(24, 0, 0));
                            }
                            totalWorkTime += endTime - startTime;
                        }
                    }
                    TimeSpan totalBreakTime = GetTotalBreakTime(employeeName, date);
                    totalWorkTime -= totalBreakTime;

                    if (totalWorkTime.TotalMinutes > 0)
                    {
                        duration = $"{(int)totalWorkTime.TotalHours}:{totalWorkTime.Minutes:D2}";
                    }
                }
            }

            return duration;
        }



        private TimeSpan GetTotalBreakTime(string employeeName, DateTime date)
        {
            TimeSpan totalBreakTime = TimeSpan.Zero;

            foreach (var breakTime in breakTimes)
            {
                // Çalışanın çalışma saatleri içinde mola alıp almadığını kontrol et
                if (HasWorkedDuringBreakTime(employeeName, date, breakTime))
                {
                    totalBreakTime += breakTime.end - breakTime.start; // Mola süresini ekle
                }
            }

            return totalBreakTime;
        }


        private bool HasWorkedDuringBreakTime(string employeeName, DateTime date, (TimeSpan start, TimeSpan end, string description) breakTime)
        {
            // SQL sorgusu
            string query = @"SELECT COUNT(*) 
                            FROM workflow_table w 
                            JOIN employee_table e ON w.employee_id = e.id 
                            WHERE e.name = @employeeName 
                            AND CAST(w.date AS DATE) = @date 
                            AND (
                                (w.start_time < @breakEnd AND w.end_time > @breakStart) OR
                                (w.start_time < @breakStart AND w.end_time > @breakStart) OR
                                (w.start_time < @breakEnd AND w.end_time > @breakEnd)
                            )";

            using (SqlConnection tempConnection = new SqlConnection(connectionString))
            {
                tempConnection.Open();
                using (SqlCommand command = new SqlCommand(query, tempConnection))
                {
                    command.Parameters.AddWithValue("@employeeName", employeeName);
                    command.Parameters.AddWithValue("@date", date.Date);
                    command.Parameters.AddWithValue("@breakStart", breakTime.start);
                    command.Parameters.AddWithValue("@breakEnd", breakTime.end);

                    int count = (int)command.ExecuteScalar();
                    // Eğer end_time start_time'dan küçükse
                    if (count == 0)
                    {
                        string checkQuery = @"
                            SELECT COUNT(*) FROM workflow_table w 
                            JOIN employee_table e ON w.employee_id = e.id 
                            WHERE e.name = @employeeName 
                            AND CAST(w.date AS DATE) = @date 
                            AND w.start_time < @breakEnd 
                            AND w.end_time < @breakStart 
                            AND w.end_time <= w.start_time";

                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, tempConnection))
                        {
                            checkCommand.Parameters.AddWithValue("@employeeName", employeeName);
                            checkCommand.Parameters.AddWithValue("@date", date.Date);
                            checkCommand.Parameters.AddWithValue("@breakStart", breakTime.start);
                            checkCommand.Parameters.AddWithValue("@breakEnd", breakTime.end);

                            count += (int)checkCommand.ExecuteScalar();
                        }
                    }
                    return count > 0;
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnFilter_Click_1(object sender, EventArgs e)
        {
            // DateTimePicker'ten seçilen ayı al
            int selectedMonth = dateTimePicker1.Value.Month;
            string selectedEmployeeName = ((dynamic)cmbxEmployee.SelectedItem)?.Text.ToString();
            // Veritabanı bağlantısını ve sorguyu güncelle
            VeritabanıBaglanti();
        }
    }
}
