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
    public partial class DeleteAttendance : Form
    {
        public DeleteAttendance()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the values from the text boxes
            string studentId = textBox2.Text.Trim();
            string courseId = textBox1.Text.Trim();

            // Check that both fields are filled
            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(courseId))
            {
                label6.Text = "Please fill in both fields.";
                return;
            }

            // Delete the record from the Attendance table
            string connectionString = "Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Attendance WHERE StudId = @StudentId AND CourseID = @CourseID";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@CourseID", courseId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        label6.Text = "Record deleted successfully.";
                    }
                    else
                    {
                        label6.Text = "Record not found.";
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MarkAttendance fac = new MarkAttendance();
            fac.Show();
            this.Visible = false;
        }
    }
}
