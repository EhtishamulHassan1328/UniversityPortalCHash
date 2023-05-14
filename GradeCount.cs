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
    public partial class GradeCount : Form
    {
        public GradeCount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string courseName = textBox1.Text;

            if (string.IsNullOrWhiteSpace(courseName))
            {
                label3.Text = "Please enter a course name.";
                return;
            }

            string query = $"SELECT Grade, COUNT(*) as Count FROM Grades WHERE CourseName = @CourseName GROUP BY Grade";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            label3.Text = $"No grades record found for {courseName}.";
                            dataGridView1.DataSource = null;
                        }
                        else
                        {
                            label3.Text = "";
                            dataGridView1.DataSource = dt;
                            dataGridView1.Columns["Grade"].HeaderText = "Grade";
                            dataGridView1.Columns["Count"].HeaderText = "Count";
                        }
                    }
                }
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyReports fac = new FacultyReports();
            fac.Show();
            this.Visible = false;
        }
    }
}
