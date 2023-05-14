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
    public partial class EvaluationReport : Form
    {
        public EvaluationReport()
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

            string query = $"SELECT * FROM Evaluation WHERE CourseName = @CourseName";

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
                            label3.Text = $"No evaluation record found for {courseName}.";
                            dataGridView1.DataSource = null;
                        }
                        else
                        {
                            label3.Text = "";
                            dataGridView1.DataSource = dt;
                            dataGridView1.Columns["EvaluationId"].HeaderText = "ID";
                            dataGridView1.Columns["StudentId"].HeaderText = "Student ID";
                            dataGridView1.Columns["CourseName"].HeaderText = "Course Name";
                            dataGridView1.Columns["Section"].HeaderText = "Section";
                            dataGridView1.Columns["Assignment"].HeaderText = "Assignment";
                            dataGridView1.Columns["Quizzes"].HeaderText = "Quizzes";
                            dataGridView1.Columns["Sessionals"].HeaderText = "Sessionals";
                            dataGridView1.Columns["Finals"].HeaderText = "Finals";
                            dataGridView1.Columns["Marks"].HeaderText = "Marks";
                        }
                    }
                }
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyReports fac=new FacultyReports();
            fac.Show();
            this.Visible = false;

        }
    }
}
