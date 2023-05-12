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
    public partial class AdminSign : Form
    {
        public AdminSign()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
            conn.Open();
            //MessageBox.Show("Connection Open");
            SqlCommand cm;
            string un = textBox1.Text;
            string pass = textBox3.Text;
            string phone = textBox2.Text;
            DateTime selectedDate = dateTimePicker1.Value;
            if (string.IsNullOrEmpty(un) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(phone))
            {
                label6.Text = "Please enter all values.";
            }
            else
            {
                string query = "INSERT INTO Admin (Name, Phone, DOB, Password) VALUES ('" + un + "', '" + phone + "', '" + selectedDate.ToString("yyyy-MM-dd") + "', '" + pass + "')";
                cm = new SqlCommand(query, conn);

                label6.Text = "Admin Successfully Registered.";
                cm.ExecuteNonQuery();
                cm.Dispose();
            }
            conn.Close();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the admin login screen
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
            this.Visible = false;
        }
    }
}
