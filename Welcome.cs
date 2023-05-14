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

        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            string eventName = "Button Click";
            DateTime eventDate = DateTime.Now;
            string userId = "User123";
            string ipAddress = "127.0.0.1";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO auditLog (EventName, EventDate, UserId, EventDetails) VALUES (@EventName, @EventDate, @UserId, @EventDetails)", conn))
                {
                    cmd.Parameters.AddWithValue("@EventName", eventName);
                    cmd.Parameters.AddWithValue("@EventDate", eventDate);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@EventDetails", "IP Address: " + ipAddress);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}
