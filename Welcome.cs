using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace checking
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f1 = new StudentLogin();
            f1.Show();



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    // Show the student login screen
                    StudentLogin studentLogin = new StudentLogin();
                    studentLogin.Show();
                    this.Visible = false;

                    break;
                case 1:
                    // Show the faculty login screen
                    FacultyLogin facultyLogin = new FacultyLogin();
                    facultyLogin.Show();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    // Show the student login screen
                    StudentSign studentSignUp = new StudentSign();
                    studentSignUp.Show();
                    this.Visible= false;
                    break;
                case 1:
                    // Show the faculty login screen
                    FacultySign facultySignUp = new FacultySign();
                    facultySignUp.Show();
                    this.Visible = false;

                    break;
                case 2:
                    // Show the admin login screen
                    AdminSign adminSignUp = new AdminSign();
                    adminSignUp.Show();
                    this.Visible = false;

                    break;
                default:
                    break;
            }

        }
    }
}
