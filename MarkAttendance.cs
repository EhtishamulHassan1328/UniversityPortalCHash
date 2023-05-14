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
    public partial class MarkAttendance : Form
    {
        private SqlConnection conn;

        public MarkAttendance()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=localhost;Initial Catalog=FlexDB;Integrated Security=True");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // First, retrieve the values from the text boxes and check boxes
            string studentId = txtStudentId.Text.Trim();
            string courseId = txtCourseId.Text.Trim();
            string section = txtSection.Text.Trim();
            DateTime date = dtpDate.Value;

            bool present = chkPresent.Checked;
            bool absent = chkAbsent.Checked;
            bool late = chkLate.Checked;

            // Check that all fields are filled
            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(section) || (!present && !absent && !late))
            {
                label8.Text = "Please fill in all fields and select one attendance option.";
                return;
            }

            // Next, check if the student and course exist in the database
            string connectionString = "Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the student exists
                string studentQuery = "SELECT COUNT(*) FROM Student WHERE StudentId = @StudentId";
                using (SqlCommand command = new SqlCommand(studentQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    int studentCount = (int)command.ExecuteScalar();

                    if (studentCount == 0)
                    {
                        label8.Text = "Student ID not found.";
                        return;
                    }
                }

                // Check if the course exists
                string courseQuery = "SELECT COUNT(*) FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand command = new SqlCommand(courseQuery, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    int courseCount = (int)command.ExecuteScalar();

                    if (courseCount == 0)
                    {
                        label8.Text = "Course ID not found.";
                        return;
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Check if the attendance record already exists
                string attendanceExistQuery = "SELECT COUNT(*) FROM Attendance WHERE StudId = @StudentId AND CourseID = @CourseID AND CAST(AttendanceDate AS DATE) = CAST(@Date AS DATE)";
                using (SqlCommand command = new SqlCommand(attendanceExistQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@Date", date);

                    int attendanceCount = (int)command.ExecuteScalar();

                    if (attendanceCount > 0)
                    {
                        label8.Text = "Attendance record already exists.";
                        return;
                    }
                }

                // If the attendance record does not exist, insert it
                string attendanceQuery = "INSERT INTO Attendance (StudId, CourseID, Section, AttendanceDate, IsPresent, IsAbsent, IsLate) VALUES (@StudentId, @CourseID, @Section, @Date, @Present, @Absent, @Late)";
                using (SqlCommand command = new SqlCommand(attendanceQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@Section", section);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Present", present);
                    command.Parameters.AddWithValue("@Absent", absent);
                    command.Parameters.AddWithValue("@Late", late);

                    command.ExecuteNonQuery();
                }

                label8.Text = "Attendance recorded successfully.";
            }

        }


     

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyOptions fac = new FacultyOptions();
            fac.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteAttendance fac = new DeleteAttendance();
            fac.Show();
            this.Visible = false;
        }
    }
}
