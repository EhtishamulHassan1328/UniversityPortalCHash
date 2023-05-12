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
    public partial class AdminOptions : Form
    {
        public AdminOptions()
        {
            InitializeComponent();
        }

        private void AdminOptions_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AllocateCourse allocCourse = new AllocateCourse();
            allocCourse.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OfferCourses offerCourse = new OfferCourses();
            offerCourse.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageStudents manStud = new ManageStudents();
            manStud.Show();
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportGenerate repgen = new ReportGenerate();
            repgen.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminLogin adm = new AdminLogin();
            adm.Show();
            this.Visible = false;
        }
    }
}
