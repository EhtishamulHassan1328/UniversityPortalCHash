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
    public partial class FacultyOptions : Form
    {
        public FacultyOptions()
        {
            InitializeComponent();
        }

        private void FacultyOptions_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MarkAttendance mark = new MarkAttendance();
            mark.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyLogin fac2 = new FacultyLogin();
            fac2.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetDistribution fac2 = new SetDistribution();
            fac2.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManageEvaluation fac2 = new ManageEvaluation();
            fac2.Show();
            this.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenerateGrade fac2 = new GenerateGrade();
            fac2.Show();
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FacultyReports fac2 = new FacultyReports();
            fac2.Show();
            this.Visible = false;
        }
    }
}
