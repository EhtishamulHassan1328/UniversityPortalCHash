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
            MessageBox.Show("Connection Open");
            SqlCommand cm;

            string un = textBox1.Text;
            string pass = textBox2.Text;

            string query = "SELECT * FROM Faculty WHERE Name = '" + un + "' AND Password = '" + pass + "'";
            cm = new SqlCommand(query, conn);

            SqlDataReader res = cm.ExecuteReader();

            if (!res.HasRows)
            {
                MessageBox.Show("No such user found");
            }
            else
            {

                MessageBox.Show("Successfully logged in!");
            }

            Console.WriteLine("After method call, value of res : {0}", res);
            cm.Dispose();
            conn.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
            this.Visible = false;
        }
    }
}
