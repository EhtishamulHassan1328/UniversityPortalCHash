using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace checking
{
    public partial class AdminLogin : Form
    {


        public AdminLogin()
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
            string pass = textBox2.Text;

            if (string.IsNullOrWhiteSpace(un) || string.IsNullOrWhiteSpace(pass))
            {
                label4.Text = "Please enter both username and password.";
                return;
            }
            else
            {
                string query = "SELECT * FROM Admin WHERE Name = '" + un + "' AND Password = '" + pass + "'";
                cm = new SqlCommand(query, conn);

                SqlDataReader res = cm.ExecuteReader();

                if (!res.HasRows)
                {
                    //MessageBox.Show("No such user found");
                    label4.Text = "Username or Password is incorrect.";
                    return;
                }
                else
                {

                    //MessageBox.Show("Successfully logged in!");
                    AdminOptions adminOption = new AdminOptions();
                    adminOption.Show();
                    this.Visible = false;
                    conn.Close();

                    string eventName = "Admin Login";
                    DateTime eventDate = DateTime.Now;
                    string userId = un;
                    string ipAddress = "127.0.0.1";


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
                    //break;
                }

                //Console.WriteLine("After method call, value of res : {0}", res);
                //cm.Dispose();
            }

            conn.Close();
            this.Close();
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
            this.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
