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
    public partial class GenerateGrade : Form
    {
        public GenerateGrade()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string courseName = textBox1.Text;
            if (string.IsNullOrEmpty(courseName))
            {
                MessageBox.Show("Please enter a course name.");
                return;
            }

            string query = $"SELECT Evaluation.StudentId, Evaluation.Marks, CASE WHEN Evaluation.Marks BETWEEN 90 AND 100 THEN 'A+' WHEN Evaluation.Marks BETWEEN 86 AND 89 THEN 'A' WHEN Evaluation.Marks BETWEEN 82 AND 85 THEN 'A-' WHEN Evaluation.Marks BETWEEN 78 AND 81 THEN 'B+' WHEN Evaluation.Marks BETWEEN 74 AND 77 THEN 'B' WHEN Evaluation.Marks BETWEEN 70 AND 73 THEN 'B-' WHEN Evaluation.Marks BETWEEN 66 AND 69 THEN 'C+' WHEN Evaluation.Marks BETWEEN 62 AND 65 THEN 'C' WHEN Evaluation.Marks BETWEEN 58 AND 61 THEN 'C-' WHEN Evaluation.Marks BETWEEN 50 AND 53 THEN 'D+' WHEN Evaluation.Marks BETWEEN 50 AND 52 THEN 'D' ELSE 'F' END AS Grade FROM Evaluation INNER JOIN Courses ON Evaluation.CourseName = Courses.CourseName WHERE Courses.CourseName = @CourseName ORDER BY Evaluation.StudentId";

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
                            label4.Text = $"Evaluation of {courseName} is not present.";
                            dataGridView1.DataSource = null;
                            return;
                        }
                        else
                        {
                            label4.Text = "";
                            dataGridView1.DataSource = dt;
                        }

                        // Insert grades into the Grades table
                        using (SqlCommand insertCmd = new SqlCommand("INSERT INTO Grades (StudentId, CourseName, Grade) VALUES (@StudentId, @CourseName, @Grade)", conn))
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                insertCmd.Parameters.Clear();
                                insertCmd.Parameters.AddWithValue("@StudentId", row["StudentId"]);
                                insertCmd.Parameters.AddWithValue("@CourseName", courseName);
                                insertCmd.Parameters.AddWithValue("@Grade", row["Grade"]);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                conn.Close();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyOptions fac = new FacultyOptions();
            fac.Show();
            this.Visible = false;
        }

        private void GenerateGrade_Load(object sender, EventArgs e)
        {

        }
    }
}
