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
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace WindowsFormsApp1
{
    public partial class WorkFlowPage : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        String workflowId;
        private string sqlQuery = "";
        public WorkFlowPage()
        {
            InitializeComponent();
            this.Text = "İş Akışı Sayfası";
        }

        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection("server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True");

            baglanti.Open();
            if (sqlQuery.Length == 0)
            {
                sqlQuery = "SELECT w.id, e.name, w.stage, w.project, w.job_description, w.start_time, w.end_time, w.date FROM workflow_table w JOIN employee_table e ON w.employee_id = e.id ORDER BY w.id DESC;";
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

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            VeritabanıBaglanti();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                workflowId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtAsama.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtProje.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtGorevTanimi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                girisSaati.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                cikisSaati.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                tarih.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

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
            }
        }


        private void cikisSaati_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxEmployee.SelectedItem != null)
            {
                // Seçilen çalışanın id'sini al
                string employeeId = ((dynamic)cmbxEmployee.SelectedItem).Value.ToString();
            }
        }

        private void girisSaati_ValueChanged(object sender, EventArgs e)
        {

        }
        private void girisSaati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true; // Tab tuşunun varsayılan işlevini engelle
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cikisSaati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        // Diğer inputlar için de aynı şekilde devam et
        private void txtAsama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtProje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE from workflow_table where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(workflowId));
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

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string employeeId = ((dynamic)cmbxEmployee.SelectedItem).Value.ToString();
            string sorgu = "INSERT INTO workflow_table(employee_id, stage, project, job_description, start_time, end_time, date, end_date) VALUES (@employee_id, @asama, @proje, @gorevTanimi, @girisSaati, @cikisSaati, @tarih, @end_date)";
            komut = new SqlCommand(sorgu, baglanti);

            // Parametreleri ekle
            komut.Parameters.AddWithValue("@employee_id", employeeId);
            komut.Parameters.AddWithValue("@asama", txtAsama.Text);
            komut.Parameters.AddWithValue("@proje", txtProje.Text);
            komut.Parameters.AddWithValue("@gorevTanimi", txtGorevTanimi.Text);
            komut.Parameters.AddWithValue("@girisSaati", girisSaati.Text);
            komut.Parameters.AddWithValue("@cikisSaati", cikisSaati.Text);
            komut.Parameters.AddWithValue("@tarih", tarih.Value);

            // end_date hesaplama
            DateTime startTime = DateTime.Parse(girisSaati.Text);
            DateTime endTime = DateTime.Parse(cikisSaati.Text);
            DateTime dateValue = tarih.Value.Date; // sadece tarihi al

            DateTime endDate;
            if (endTime <= startTime)
            {
                endDate = dateValue.AddDays(1); // date + 1 gün
            }
            else
            {
                endDate = dateValue; // normal durumda date kullanılır
            }

            // end_date'yi DATE olarak ekle
            komut.Parameters.AddWithValue("@end_date", endDate.Date); // sadece tarihi al

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeritabanıBaglanti();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            string employeeId = ((dynamic)cmbxEmployee.SelectedItem).Value.ToString();
            string sorgu = "UPDATE workflow_table SET employee_id=@employee_id, stage=@asama, project=@proje, job_description=@gorevTanimi, start_time=@girisSaati, end_time=@cikisSaati, date=@tarih, end_date=@end_date WHERE id=@id";
            komut = new SqlCommand(sorgu, baglanti);

            // Parametreleri ekle
            komut.Parameters.AddWithValue("@employee_id", employeeId);
            komut.Parameters.AddWithValue("@asama", txtAsama.Text);
            komut.Parameters.AddWithValue("@proje", txtProje.Text);
            komut.Parameters.AddWithValue("@gorevTanimi", txtGorevTanimi.Text);
            komut.Parameters.AddWithValue("@girisSaati", girisSaati.Text);
            komut.Parameters.AddWithValue("@cikisSaati", cikisSaati.Text);
            komut.Parameters.AddWithValue("@tarih", tarih.Value);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(workflowId));

            // end_date hesaplama
            DateTime startTime = DateTime.Parse(girisSaati.Text);
            DateTime endTime = DateTime.Parse(cikisSaati.Text);
            DateTime dateValue = tarih.Value.Date; // sadece tarihi al

            DateTime endDate;
            if (endTime <= startTime)
            {
                endDate = dateValue.AddDays(1); // date + 1 gün
            }
            else
            {
                endDate = dateValue; // normal durumda date kullanılır
            }

            // end_date'yi DATE olarak ekle
            komut.Parameters.AddWithValue("@end_date", endDate.Date); // sadece tarihi al

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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = false;
            Microsoft.Office.Interop.Excel.Workbook xlWbook = xlapp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWbook.Worksheets[1];

            // DataGridView başlıklarını Excel'e aktarma ve biçimlendirme
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                xlsheet.Cells[1, j + 1] = dataGridView1.Columns[j].HeaderText;

                // Başlık hücresinin arka plan rengini turuncu yapma
                xlsheet.Cells[1, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);

                // Sütun genişliğini ayarlama
                xlsheet.Columns[j + 1].ColumnWidth = 18;
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
            string fileName = "WorkflowData.xlsx";
            string fullPath = System.IO.Path.Combine(downloadsPath, fileName);

            // Dosya var mı kontrol et ve gerekiyorsa isimlendirmeyi ayarla
            int fileIndex = 1;
            while (System.IO.File.Exists(fullPath))
            {
                fullPath = System.IO.Path.Combine(downloadsPath, $"WorkflowData ({fileIndex}).xlsx");
                fileIndex++;
            }

            // Excel dosyasını kaydetme
            xlWbook.SaveAs(fullPath);
            xlWbook.Close();
            xlapp.Quit();

            MessageBox.Show("Excel dosyası indirilenler klasörüne kaydedildi: " + fullPath);
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            string selectedEmployee = ((dynamic)cmbxEmployee.SelectedItem)?.Text.ToString();

            string query = "SELECT w.id, e.name, w.stage, w.project, w.job_description, w.start_time, w.end_time, w.date FROM workflow_table w JOIN employee_table e ON w.employee_id = e.id  WHERE 1=1 ";

            if (!string.IsNullOrEmpty(selectedEmployee))
            {
                query += " AND e.name = '" + selectedEmployee + "' ORDER BY w.id DESC ";
            }

            sqlQuery = query;
            VeritabanıBaglanti();
        }
    }
}
