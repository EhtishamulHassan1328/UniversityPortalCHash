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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace checking
{
    public partial class AllocCourse : Form
    {
        public AllocCourse()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
            conn.Open();

            // Create a SQL command to select all entries from the "Courses" table
            SqlCommand cmd = new SqlCommand("SELECT Name From Faculty", conn);

            // Create a data adapter and fill a data table with the selected data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Bind the data table to a DataGridView control
            dataGridView1.DataSource = dataTable;

            // Close the database connection
            conn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            string facultyName = textBox1.Text;
            string courseID = textBox2.Text;
            string section = textBox3.Text;
            string coordinatorName = textBox4.Text;

            if (string.IsNullOrEmpty(facultyName) || string.IsNullOrEmpty(courseID) || string.IsNullOrEmpty(section) || string.IsNullOrEmpty(coordinatorName))
            {
                label5.Text = "Please enter all the values.";
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();

                // Check if the faculty member exists in the Faculty table
                SqlCommand cmd1 = new SqlCommand($"SELECT Name FROM Faculty WHERE Name='{facultyName}'", conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (!reader1.HasRows)
                {
                    label7.Text = "Faculty not found";
                    reader1.Close();
                    cmd1.Dispose();
                    return;
                }
                reader1.Close();
                cmd1.Dispose();

                // Check if the course ID and course name exist in the Courses table
                SqlCommand cmd2 = new SqlCommand($"SELECT CourseID, CourseName FROM Courses WHERE CourseID='{courseID}'", conn);
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

                // Check if the course coordinator's name exists in the Faculty table
                SqlCommand cmd3 = new SqlCommand($"SELECT Name FROM Faculty WHERE Name='{coordinatorName}'", conn);
                SqlDataReader reader3 = cmd3.ExecuteReader();
                if (!reader3.HasRows)
                {
                    label7.Text = "Course coordinator not found";
                    reader3.Close();
                    cmd3.Dispose();
                    return;
                }
                reader3.Close();
                cmd3.Dispose();

                // Check if the faculty member has already been allocated 3 courses
                SqlCommand cmd4 = new SqlCommand($"SELECT COUNT(*) FROM allocated_courses WHERE FacultyName='{facultyName}'", conn);
                int count = (int)cmd4.ExecuteScalar();
                cmd4.Dispose();

                if (count >= 3)
                {
                    label7.Text = "Course Allocation Limit Exceeded.";
                    return;
                }

                // Insert the values into the allocated_courses table
                SqlCommand cmd5 = new SqlCommand("INSERT INTO allocated_courses (FacultyName, CourseID, Section, CourseCoordinator) VALUES (@facultyName, @courseId, @section, @coordinatorName)", conn);
                cmd5.Parameters.AddWithValue("@facultyName", facultyName);
                cmd5.Parameters.AddWithValue("@courseId", courseID);
                cmd5.Parameters.AddWithValue("@section", section);
                cmd5.Parameters.AddWithValue("@coordinatorName", coordinatorName);
                cmd5.ExecuteNonQuery();
                cmd5.Dispose();
            }

            label7.Text = "Course Successfully Allocated.";
        }


        private void button5_Click(object sender, EventArgs e)
        {
            AllocateCourse all = new AllocateCourse();
            all.Show();
            this.Visible = false;
        }
    }
}
