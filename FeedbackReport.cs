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
    public partial class FeedbackReport : Form
    {
        public FeedbackReport()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyReports att = new FacultyReports();
            att.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please enter a first name and last name.");
                return;
            }

            string query = "SELECT * FROM feedback WHERE fname = @FirstName AND lname = @LastName";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show($"No feedback record found for {firstName} {lastName}.");
                            dataGridView1.DataSource = null;
                        }
                        else
                        {
                            dataGridView1.DataSource = dt;
                            dataGridView1.Columns["id"].HeaderText = "ID";
                            dataGridView1.Columns["fname"].HeaderText = "First Name";
                            dataGridView1.Columns["lname"].HeaderText = "Last Name";
                            dataGridView1.Columns["subject"].HeaderText = "Subject";
                            dataGridView1.Columns["schedule"].HeaderText = "Schedule";
                            dataGridView1.Columns["room"].HeaderText = "Room";
                            dataGridView1.Columns["school"].HeaderText = "School";
                            dataGridView1.Columns["message"].HeaderText = "Message";
                            dataGridView1.Columns["created_at"].HeaderText = "Created At";
                        }
                    }
                }
                conn.Close();
            }
        }

    }
}
