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
    public partial class TimeTrackingPage : Form
    {
        private SqlConnection baglanti;
        private SqlDataAdapter da;
        private string connectionString = "server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True";
        private string sqlQuery = "";
        private int currentMonth = 10;

        private List<(TimeSpan start, TimeSpan end, string description)> breakTimes = new List<(TimeSpan, TimeSpan, string)>
        {
            (new TimeSpan(10, 0, 0), new TimeSpan(10, 15, 0), "Çay Molası"),
            (new TimeSpan(12, 30, 0), new TimeSpan(13, 0, 0), "Yemek"),
            (new TimeSpan(15, 0, 0), new TimeSpan(15, 15, 0), "Çay Molası"),
            (new TimeSpan(18, 0, 0), new TimeSpan(18, 30, 0), "Yemek"),
            (new TimeSpan(22, 0, 0), new TimeSpan(23, 30, 0), "Çay Molası")
        };
        public TimeTrackingPage()
        {
            InitializeComponent();
            this.Text = "Puantaj Sayfası";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            VeritabanıBaglanti();
        }

        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection(connectionString);
            baglanti.Open();

            if(sqlQuery.Length == 0)
            {
                sqlQuery = "SELECT DISTINCT e.name FROM workflow_table w JOIN employee_table e ON w.employee_id = e.id ORDER BY e.name;";
            }

            // Çalışanların bilgilerini ve iş akışını çeken sorgu
            da = new SqlDataAdapter(sqlQuery, baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);

            // DataGridView'i ayarla
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Calisan", "Çalışanlar");

            // Günleri sütun olarak ekle
            int daysInMonth = DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                dataGridView1.Columns.Add($"Day{day}", $"{day}/{dateTimePicker1.Value.Month}");
            }

            dataGridView1.Columns[0].Width = 100; // Çalışanlar sütunu
            for (int day = 1; day <= daysInMonth; day++)
            {
                dataGridView1.Columns[day].Width = 50; // Her gün için sütun
            }

            // Her çalışan için bir satır ekleyin ve çalışma sürelerini ayarlayın
            foreach (DataRow row in tablo.Rows)
            {
                string employeeName = row["name"].ToString();
                var employeeRow = new DataGridViewRow();
                employeeRow.CreateCells(dataGridView1);
                employeeRow.Cells[0].Value = employeeName; // Çalışan ismini ekle

                // Çalışma sürelerini al
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, day);
                    string duration = GetWorkDuration(employeeName, date); // Çalışma süresini al
                    employeeRow.Cells[day].Value = duration; // Süreyi hücreye ekle
                }

                dataGridView1.Rows.Add(employeeRow); // Satırı DataGridView'a ekle
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
                    cmbxEmployee.AutoCompleteSource = AutoCompleteSource.ListItems; // Veri kaynağı
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
            // Hücreye tıklandığında yapılacak işlemler
        }

        private void buttonRetrieve_Click(object sender, EventArgs e)
        {
            string selectedEmployee = ((dynamic)cmbxEmployee.SelectedItem)?.Text.ToString();
            int selectedMonth = dateTimePicker1.Value.Month;

            string query = "SELECT DISTINCT e.name FROM workflow_table w JOIN employee_table e ON w.employee_id = e.id WHERE 1=1 ";

            if (!string.IsNullOrEmpty(selectedEmployee))
            {
                query += " AND e.name = '"+selectedEmployee+"' ";
            }

            query += " AND MONTH(w.date) = "+selectedMonth;
            sqlQuery = query;
            VeritabanıBaglanti();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = false;
            Microsoft.Office.Interop.Excel.Workbook xlWbook = xlapp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWbook.Worksheets[1];

            // DataGridView başlıklarını Excel'e aktarma ve biçimlendirme
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                xlsheet.Cells[1, j + 1].NumberFormat = "@"; // Metin formatı
                xlsheet.Cells[1, j + 1] = dataGridView1.Columns[j].HeaderText;

                // Başlık hücresinin arka plan rengini turuncu yapma
                xlsheet.Cells[1, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);

                // Sütun genişliğini ayarlama
                xlsheet.Columns[j + 1].ColumnWidth = 8;
            }

            // DataGridView'daki verileri Excel'e aktarma
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    xlsheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }

            // Tablo oluşturma
            var tableRange = xlsheet.Range[xlsheet.Cells[1, 1], xlsheet.Cells[dataGridView1.Rows.Count + 1, dataGridView1.Columns.Count]];
            var listObject = xlsheet.ListObjects.Add(
                Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange,
                tableRange,
                Type.Missing,
                Microsoft.Office.Interop.Excel.XlYesNoGuess.xlYes, // Doğru kullanımı
                Type.Missing);
            listObject.Name = "DataGridViewTable";
            listObject.TableStyle = "TableStyleMedium9"; // Tablo stilini ayarlama

            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string fileName = "Puantaj.xlsx";
            string fullPath = System.IO.Path.Combine(downloadsPath, fileName);

            // Dosya var mı kontrol et ve gerekiyorsa isimlendirmeyi ayarla
            int fileIndex = 1;
            while (System.IO.File.Exists(fullPath))
            {
                fullPath = System.IO.Path.Combine(downloadsPath, $"Puantaj ({fileIndex}).xlsx");
                fileIndex++;
            }

            // Excel dosyasını kaydetme
            xlWbook.SaveAs(fullPath);
            xlWbook.Close();
            xlapp.Quit();

            MessageBox.Show("Excel dosyası indirilenler klasörüne kaydedildi: " + fullPath);
        }

        private void cmbxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
    }
}
