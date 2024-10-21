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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WindowsFormsApp1
{
    public partial class PaymentPage : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        String advanceId;
        private string sqlQuery = "";
        public PaymentPage()
        {
            InitializeComponent();
        }


        void VeritabanıBaglanti()
        {
            baglanti = new SqlConnection("server=localhost\\SQLEXPRESS;Initial Catalog=FerganiMuhasebeDB;Integrated Security=True");

            baglanti.Open();
            if (sqlQuery.Length == 0)
            {
                sqlQuery = "SELECT " +
                    "e.id AS ÇalışanID, " +
                    "e.name AS Kişi, " +
                    "e.iban AS IBAN, " +
                    "e.wage AS [Yevmiye], " +
                    "COALESCE(SUM(total_hours) / 9.0, 0) AS [Toplam Çalışma Günü] " +
                    "FROM " +
                    "employee_table e " +
                    "LEFT JOIN " +
                    "( " +
                    "    SELECT " +
                    "        employee_id, " +
                    "        SUM(DATEDIFF(MINUTE, start_time, end_time)) / 60.0 AS total_hours " +
                    "    FROM " +
                    "        workflow_table " +
                    "    GROUP BY " +
                    "        employee_id " +
                    ") w ON e.id = w.employee_id " +
                    "GROUP BY " +
                    "e.id, e.name, e.iban;";

            }
            da = new SqlDataAdapter(sqlQuery, baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }
        private void PaymentPage_Load(object sender, EventArgs e)
        {
            VeritabanıBaglanti();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
