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
    public partial class FacultySign : Form
    {
        public FacultySign()
        {
            InitializeComponent();
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
            string query = "INSERT INTO Faculty (Name, Phone, DOB, Password) VALUES ('" + un + "', '" + phone + "', '" + selectedDate.ToString("yyyy-MM-dd") + "', '" + pass + "')";
            cm = new SqlCommand(query, conn);
            cm.ExecuteNonQuery();
            cm.Dispose();
            conn.Close();


            string eventName = "Faculty SignUp";
            DateTime eventDate = DateTime.Now;
            string userId = un;
            string ipAddress = "127.0.0.1";


            using (SqlCommand cmd1 = new SqlCommand("INSERT INTO auditLog (EventName, EventDate, UserId, EventDetails) VALUES (@EventName, @EventDate, @UserId, @EventDetails)", conn))
            {
                cmd1.Parameters.AddWithValue("@EventName", eventName);
                cmd1.Parameters.AddWithValue("@EventDate", eventDate);
                cmd1.Parameters.AddWithValue("@UserId", userId);
                cmd1.Parameters.AddWithValue("@EventDetails", "IP Address: " + ipAddress);

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the faculty login screen
            FacultyLogin facultyLogin = new FacultyLogin();
            facultyLogin.Show();
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
