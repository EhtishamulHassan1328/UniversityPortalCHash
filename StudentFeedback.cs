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

namespace checking
{
    public partial class StudentFeedback : Form
    {
        public StudentFeedback()
        {
            InitializeComponent();
        }

        private void StudentFeedback_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            StudentOptions fac2 = new StudentOptions();
            fac2.Show();
            this.Visible = false;
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fname = textBox1.Text;
            string lname = textBox2.Text;
            string subject = textBox3.Text;
            string schedule = textBox4.Text;
            string room = textBox5.Text;
            string school = textBox6.Text;
            string message = richTextBox1.Text;

            if (string.IsNullOrWhiteSpace(fname) || string.IsNullOrWhiteSpace(lname) || string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please fill in the required fields (First Name, Last Name, Message).");
                return;
            }

            string connectionString = "Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True";
            string query = "INSERT INTO feedback (fname, lname, subject, schedule, room, school, message) VALUES (@Fname, @Lname, @Subject, @Schedule, @Room, @School, @Message)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Fname", fname);
                    cmd.Parameters.AddWithValue("@Lname", lname);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.Parameters.AddWithValue("@Schedule", schedule);
                    cmd.Parameters.AddWithValue("@Room", room);
                    cmd.Parameters.AddWithValue("@School", school);
                    cmd.Parameters.AddWithValue("@Message", message);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thank you for your feedback!");
                    }
                    else
                    {
                        MessageBox.Show("Error: Feedback not submitted.");
                    }
                }
            }
        }
    }
    }
