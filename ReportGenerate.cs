using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checking
{
    public partial class ReportGenerate : Form
    {
        public ReportGenerate()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    OfferReport offer = new OfferReport();
                    offer.Show();
                    this.Visible = false;

                    break;
                case 1:
                    SectionReport secrep = new SectionReport();
                    secrep.Show();
                    this.Visible = false;

                    break;
                case 2:
                    // Show the admin login screen
                    AdminLogin adminLogin = new AdminLogin();
                    adminLogin.Show();
                    this.Visible = false;

                    break;
                default:
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminOptions adm = new AdminOptions();
            adm.Show();
            this.Visible = false;
        }
    }
}
