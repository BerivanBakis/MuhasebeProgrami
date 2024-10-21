using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkFlowPage workFlowPage = new WorkFlowPage();
            workFlowPage.FormClosed += (s, args) => this.Show();
            this.Hide();
            workFlowPage.Show();
        }

        private void btnPuantaj_Click(object sender, EventArgs e)
        {
            TimeTrackingPage timeTracking = new TimeTrackingPage();
            timeTracking.FormClosed += (s, args) => this.Show();
            this.Hide();
            timeTracking.Show();
        }

        private void btnAvans_Click(object sender, EventArgs e)
        {
            AdvancePage advancePage = new AdvancePage();
            advancePage.FormClosed += (s, args) => this.Show();
            this.Hide();
            advancePage.Show();
        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {
            EmployeeInformationPage employeeInformation = new EmployeeInformationPage();
            employeeInformation.FormClosed += (s, args) => this.Show();
            this.Hide();
            employeeInformation.Show();
        }

        private void btnHakedis_Click(object sender, EventArgs e)
        {
            Hakedis paymentPage = new Hakedis();
            paymentPage.FormClosed += (s, args) => this.Show();
            this.Hide();
            paymentPage.Show();
        }
    }
}
