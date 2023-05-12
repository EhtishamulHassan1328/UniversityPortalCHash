using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace checking
{
    public partial class StudentSign : Form
    {
        public StudentSign()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Define a regular expression pattern for the username
            string pattern = @"^\d{2}I-\d{4}$";

            // Get the user's input from a text box control
            string username = textBox1.Text;

            // Use regular expression matching to validate the input
            if (Regex.IsMatch(username, pattern))
            {
                // The input is valid; proceed with storing it in the database
                // ...
                MessageBox.Show("Username Correct.");

                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
                conn.Open();
                MessageBox.Show("Connection Open");
                SqlCommand cm;
                string un = textBox1.Text;
                string pass = textBox3.Text;
                string phone = textBox2.Text;
                DateTime selectedDate = dateTimePicker1.Value;
                string query = "INSERT INTO Student (Name, Phone, DOB, Password) VALUES ('" + un + "', '" + phone + "', '" + selectedDate.ToString("yyyy-MM-dd") + "', '" + pass + "')";
                cm = new SqlCommand(query, conn);
                cm.ExecuteNonQuery();
                cm.Dispose();
                conn.Close();
            }
            else
            {
                // The input is not valid; display an error message to the user
                MessageBox.Show("Username must be in the format 'XXI-XXXX'.");
            }



        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StudentLogin studentLogin = new StudentLogin();
            studentLogin.Show();
            this.Visible = false;
        }

        private void StudentSign_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
            this.Visible = false;
        }
    }
}
