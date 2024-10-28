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

namespace WindowsFormsApp1.Cari
{
    public partial class FirmTransaction : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        String transactionId;
        private string sqlQuery = "";
        public FirmTransaction()
        {
            InitializeComponent();
        }
        private void FirmTransaction_Load(object sender, EventArgs e)
        {
            LoadFirmData();
            LoadTransactionType();
            VeritabanıBaglanti();
        }

        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection("server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True");

            baglanti.Open();
            if (sqlQuery.Length == 0)
            {
                sqlQuery = @"SELECT TOP (1000) 
                    ftt.[id],
                    ftt.[date] AS [Tarih],
                    ft.[name] AS [Firma Adı],
                    ttt.[name] AS [İşlem Türü],
                    ftt.[amount] AS [Miktar],
                    ftt.[method] AS [Ödeme Yöntemi],
                    ftt.[description] AS [Açıklama],
                    (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 3 AND [date] <= ftt.[date]), 0)) 
                    - 
                    (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 4 AND [date] <= ftt.[date]), 0 )) 
                    AS [Borç Bakiyesi],
                        (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 1 AND [date] <= ftt.[date]), 0)) 
                    - 
                    (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 2 AND [date] <= ftt.[date]), 0 )) 
                    AS [Alacak Bakiyesi]
                    FROM  firm_transaction_table AS ftt JOIN transaction_type_table AS ttt ON ftt.[transaction_type_id] = ttt.[id] JOIN firm_table AS ft ON ftt.[firm_id] = ft.[id]  ORDER BY ftt.date;
                    ";
            }

            da = new SqlDataAdapter(sqlQuery, baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        private void LoadFirmData()
        {
            string connectionString = "server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True";
            string query = "SELECT id, name FROM firm_table";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    cmbxFirms.Items.Clear(); 
                    while (reader.Read())
                    {
                        cmbxFirms.Items.Add(new
                        {
                            Value = reader["id"],
                            Text = reader["name"]
                        });
                    }
                    cmbxFirms.DisplayMember = "Text"; 
                    cmbxFirms.ValueMember = "Value";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void LoadTransactionType()
        {
            string connectionString = "server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True";
            string query = "SELECT id, name FROM transaction_type_table";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    cmbxTransactionType.Items.Clear();
                    while (reader.Read())
                    {
                        cmbxTransactionType.Items.Add(new
                        {
                            Value = reader["id"],
                            Text = reader["name"]
                        });
                    }
                    cmbxTransactionType.DisplayMember = "Text";
                    cmbxTransactionType.ValueMember = "Value";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void cmbxTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string firmId = ((dynamic)cmbxFirms.SelectedItem).Value.ToString();
            string transactionTypeId = ((dynamic)cmbxTransactionType.SelectedItem).Value.ToString();
            string sorgu = "INSERT INTO firm_transaction_table([firm_id],[date],[amount],[description],[transaction_type_id],[method]) VALUES (@firm_id, @date, @amount, @description, @transaction_type_id, @method)";
            komut = new SqlCommand(sorgu, baglanti);

            // Parametreleri ekle
            komut.Parameters.AddWithValue("@firm_id", firmId);
            komut.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@amount", txtAmount.Text);
            komut.Parameters.AddWithValue("@description", txtAciklama.Text);
            komut.Parameters.AddWithValue("@transaction_type_id", transactionTypeId);
            komut.Parameters.AddWithValue("@method", txtMethod.Text);


            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeritabanıBaglanti();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                
                transactionId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtAmount.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtMethod.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtAciklama.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                string firmName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                foreach (var item in cmbxFirms.Items)
                {
                    if (((dynamic)item).Text == firmName)
                    {
                        cmbxFirms.SelectedItem = item;
                        break;
                    }
                }
                string transactionType = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                foreach (var item in cmbxTransactionType.Items)
                {
                    if (((dynamic)item).Text == transactionType)
                    {
                        cmbxTransactionType.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            string selectedFirm = ((dynamic)cmbxFirms.SelectedItem)?.Text.ToString();

            string query = @"SELECT TOP (1000) 
                    ftt.[id],
                    ftt.[date] AS [Tarih],
                    ft.[name] AS [Firma Adı],
                    ttt.[name] AS [İşlem Türü],
                    ftt.[amount] AS [Miktar],
                    ftt.[method] AS [Ödeme Yöntemi],
                    ftt.[description] AS [Açıklama],
                    (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 3 AND [date] <= ftt.[date]), 0)) 
                    - 
                    (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 4 AND [date] <= ftt.[date]), 0 )) 
                    AS [Borç Bakiyesi],
                        (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 1 AND [date] <= ftt.[date]), 0)) 
                    - 
                    (COALESCE( (SELECT SUM(amount) FROM firm_transaction_table WHERE firm_id = ftt.firm_id AND transaction_type_id = 2 AND [date] <= ftt.[date]), 0 )) 
                    AS [Alacak Bakiyesi]
                    FROM  firm_transaction_table AS ftt JOIN transaction_type_table AS ttt ON ftt.[transaction_type_id] = ttt.[id] JOIN firm_table AS ft ON ftt.[firm_id] = ft.[id]
                    ";

            if (!string.IsNullOrEmpty(selectedFirm))
            {
                query += " where ft.name = '" + selectedFirm + "' ORDER BY ftt.date";
            }

            sqlQuery = query;
            VeritabanıBaglanti();
        }
    }
}
