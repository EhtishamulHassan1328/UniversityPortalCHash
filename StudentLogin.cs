﻿using System;
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
    public partial class StudentLogin : Form
    {

        public StudentLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Define a regular expression pattern for the username
            string pattern = @"^\d{2}I-\d{4}$";

            // Get the user's input from a text box control
            string username = textBox1.Text;

            if (Regex.IsMatch(username, pattern))
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
                conn.Open();
                MessageBox.Show("Connection Open");
                SqlCommand cm;

                string un = textBox1.Text;
                string pass = textBox2.Text;

                string query = "SELECT * FROM Student WHERE Name = '" + un + "' AND Password = '" + pass + "'";
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


            else
            {
                MessageBox.Show("Username must be in the format 'XXI-XXXX'.");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
            this.Visible = false;
        }
    }
}
