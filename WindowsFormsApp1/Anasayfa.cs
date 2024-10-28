using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Cari;

namespace WindowsFormsApp1
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel3.Controls.Clear();
            //WorkFlowPage workflowPage = new WorkFlowPage();
            //workflowPage.TopLevel = false;
            //workflowPage.FormBorderStyle = FormBorderStyle.None;
            //workflowPage.Dock = DockStyle.Fill;
            //panel3.Controls.Add(workflowPage);
            //workflowPage.Show();
            panel3.Controls.Clear();
            FirmTransaction firmTransaction = new FirmTransaction();
            firmTransaction.TopLevel = false;
            firmTransaction.FormBorderStyle = FormBorderStyle.None;
            firmTransaction.Dock = DockStyle.Fill;
            panel3.Controls.Add(firmTransaction);
            firmTransaction.Show();
        }

        private void btnWorkflow_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            WorkFlowPage workflowPage = new WorkFlowPage();
            workflowPage.TopLevel = false; 
            workflowPage.FormBorderStyle = FormBorderStyle.None;  
            workflowPage.Dock = DockStyle.Fill; 
            panel3.Controls.Add(workflowPage); 
            workflowPage.Show();
        }

        private void btnTimeTracking_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            TimeTrackingPage timeTracking = new TimeTrackingPage();
            timeTracking.TopLevel = false;
            timeTracking.FormBorderStyle = FormBorderStyle.None;
            timeTracking.Dock = DockStyle.Fill;
            panel3.Controls.Add(timeTracking);
            timeTracking.Show();
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            AdvancePage advance = new AdvancePage();
            advance.TopLevel = false;
            advance.FormBorderStyle = FormBorderStyle.None;
            advance.Dock = DockStyle.Fill;
            panel3.Controls.Add(advance);
            advance.Show();
        }

        private void btnEmployyeInf_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            EmployeeInformationPage employyeInfo = new EmployeeInformationPage();
            employyeInfo.TopLevel = false;
            employyeInfo.FormBorderStyle = FormBorderStyle.None;
            employyeInfo.Dock = DockStyle.Fill;
            panel3.Controls.Add(employyeInfo);
            employyeInfo.Show();
        }

        private void btnHakedis_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Hakedis hakedis = new Hakedis();
            hakedis.TopLevel = false;
            hakedis.FormBorderStyle = FormBorderStyle.None;
            hakedis.Dock = DockStyle.Fill;
            panel3.Controls.Add(hakedis);
            hakedis.Show();
        }

        private void firmTransaction_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            FirmTransaction firmTransaction = new FirmTransaction();
            firmTransaction.TopLevel = false;
            firmTransaction.FormBorderStyle = FormBorderStyle.None;
            firmTransaction.Dock = DockStyle.Fill;
            panel3.Controls.Add(firmTransaction);
            firmTransaction.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
