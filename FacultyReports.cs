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
    public partial class FacultyReports : Form
    {
        public FacultyReports()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    AttendanceReport att = new AttendanceReport();
                    att.Show();
                    this.Visible = false;
                    break;

                case 1:
                    EvaluationReport att1 = new EvaluationReport();
                    att1.Show();
                    this.Visible = false;
                    break;

                case 2:
                    GradeReport grd = new GradeReport();
                    grd.Show();
                    this.Visible = false;
                    break;

                case 3:
                    GradeCount fac2 = new GradeCount();
                    fac2.Show();
                    this.Visible = false;

                    break;

                case 4:
                    FeedbackReport fac3 = new FeedbackReport();
                    fac3.Show();
                    this.Visible = false;

                    break;
                default:
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyOptions fac2 = new FacultyOptions();
            fac2.Show();
            this.Visible = false;
        }
    }
}
