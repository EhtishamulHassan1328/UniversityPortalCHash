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
    public partial class RegisterCourse : Form
    {
        public RegisterCourse()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentID = textBox1.Text;
            string courseID = textBox2.Text;
            string courseName = textBox3.Text;
            string section = textBox4.Text;

            if (string.IsNullOrEmpty(studentID) || string.IsNullOrEmpty(courseID) || string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(section))
            {
                label7.Text = "Please enter all values.";
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();

                // Check if the student ID exists in the Student table
                SqlCommand cmd1 = new SqlCommand($"SELECT StudentId FROM Student WHERE StudentId='{studentID}'", conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (!reader1.HasRows)
                {
                    label7.Text = "Student not found";
                    reader1.Close();
                    cmd1.Dispose();
                    return;
                }
                reader1.Close();
                cmd1.Dispose();

                // Check if the course ID and course name exist in the Courses table
                SqlCommand cmd2 = new SqlCommand($"SELECT CourseID, CourseName FROM Courses WHERE CourseID='{courseID}' AND CourseName='{courseName}'", conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (!reader2.HasRows)
                {
                    label7.Text = "Course not found";
                    reader2.Close();
                    cmd2.Dispose();
                    return;
                }
                reader2.Close();
                cmd2.Dispose();

                // Insert the values into the registeredStudents table
                SqlCommand cmd3 = new SqlCommand("INSERT INTO registeredStudents (StudId, CourseID, CourseName, Section) VALUES (@studId, @courseId, @courseName, @section)", conn);
                cmd3.Parameters.AddWithValue("@studId", studentID);
                cmd3.Parameters.AddWithValue("@courseId", courseID);
                cmd3.Parameters.AddWithValue("@courseName", courseName);
                cmd3.Parameters.AddWithValue("@section", section);
                cmd3.ExecuteNonQuery();
                cmd3.Dispose();


            }

            label7.Text = "Registration successful";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
            conn.Open();

            // Create a SQL command to select all entries from the "Courses" table
            SqlCommand cmd = new SqlCommand("SELECT CourseID,CourseName From Courses", conn);

            // Create a data adapter and fill a data table with the selected data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Bind the data table to a DataGridView control
            dataGridView2.DataSource = dataTable;

            // Close the database connection
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StudentOptions st1=new StudentOptions();
            st1.Show();
            this.Visible= false;
        }
    }
}
