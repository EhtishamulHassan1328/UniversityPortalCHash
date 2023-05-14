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
    public partial class ViewAttendance : Form
    {
        public ViewAttendance()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string courseId = textBox1.Text;
            string studentId = textBox2.Text;

            if (string.IsNullOrWhiteSpace(courseId) || string.IsNullOrWhiteSpace(studentId))
            {
                label3.Text = "Please enter a course ID and student ID.";
                return;
            }

            string query = "SELECT * FROM Attendance WHERE CourseID = @CourseID AND StudId = @StudId";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@StudId", studentId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            label3.Text = $"No attendance record found for {studentId} in {courseId}.";
                            dataGridView1.DataSource = null;
                        }
                        else
                        {
                            label3.Text = "";
                            dataGridView1.DataSource = dt;
                            dataGridView1.Columns["StudId"].HeaderText = "Student ID";
                            dataGridView1.Columns["CourseID"].HeaderText = "Course ID";
                            dataGridView1.Columns["Section"].HeaderText = "Section";
                            dataGridView1.Columns["AttendanceDate"].HeaderText = "Attendance Date";
                            dataGridView1.Columns["IsPresent"].HeaderText = "Is Present";
                            dataGridView1.Columns["IsAbsent"].HeaderText = "Is Absent";
                            dataGridView1.Columns["IsLate"].HeaderText = "Is Late";
                        }
                    }
                }
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StudentOptions s1 = new StudentOptions();
            s1.Show();
            this.Visible = false;
        }
    }
}
