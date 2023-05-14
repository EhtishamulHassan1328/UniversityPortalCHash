using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checking
{
    public partial class FacultyLogin : Form
    {
        public FacultyLogin()
        {
            InitializeComponent();
        }

        private void FacultyLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
            conn.Open();
            SqlCommand cmd;

            string name = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                label4.Text = "Please enter both username and password.";
                return;
            }
            else
            {
                string query = "SELECT * FROM Faculty WHERE Name = @Name AND Password = @Password";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader res = cmd.ExecuteReader();

                if (!res.HasRows)
                {
                    label4.Text = "Username or Password is incorrect.";
                    return;
                }
                else
                {
                    FacultyOptions facultyOption = new FacultyOptions();
                    facultyOption.Show();
                    this.Visible = false;
                    conn.Close();

                    string eventName = "Faculty Login";
                    DateTime eventDate = DateTime.Now;
                    string userId = name;
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
            }

            conn.Close();
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
            this.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
