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
    public partial class StudentOptions : Form
    {
        public StudentOptions()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StudentLogin fac2 = new StudentLogin();
            fac2.Show();
            this.Visible=false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewEvaluation fac2 = new ViewEvaluation();
            fac2.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewAttendance fac2 = new ViewAttendance();
            fac2.Show();
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegisterCourse fac2 = new RegisterCourse();
            fac2.Show();
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StudentFeedback fac2 = new StudentFeedback();
            fac2.Show();
            this.Visible = false;
        }
    }
}
