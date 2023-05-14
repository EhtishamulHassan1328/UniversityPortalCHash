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
    public partial class ViewEvaluation : Form
    {
        public ViewEvaluation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string courseName = textBox1.Text;
            string studentId = textBox2.Text;

            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(studentId))
            {
                label3.Text = "Please enter a course name and student ID.";
                return;
            }

            string query = "SELECT * FROM Evaluation WHERE CourseName = @CourseName AND StudentId = @StudentId";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            label3.Text = $"No evaluation record found for {studentId} in {courseName}.";
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
            StudentOptions s1= new StudentOptions();
            s1.Show();
            this.Visible = false;
        }
    }
}
