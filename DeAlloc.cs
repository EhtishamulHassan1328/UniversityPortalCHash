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
    public partial class DeAlloc : Form
    {
        public DeAlloc()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string facultyName = textBox1.Text.Trim();
            string courseID = textBox2.Text.Trim();

            //label6.Text = "Hello";

            // Check if facultyName or courseID is empty
            if (string.IsNullOrEmpty(facultyName) || string.IsNullOrEmpty(courseID))
            {
                label7.Text = "Please enter both Faculty Name and Course ID.";
                return;
            }

            // Check if the record exists in the table
            string query = "SELECT COUNT(*) FROM allocated_courses WHERE FacultyName = @facultyName AND CourseID = @courseID";
            int count;
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@facultyName", facultyName);
                    cmd.Parameters.AddWithValue("@courseID", courseID);
                    count = (int)cmd.ExecuteScalar();
                }
                conn.Close();
            }

            if (count == 0)
            {
                label7.Text = "Record not found in database.";
                return;
            }

            // Delete the row
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                query = "DELETE FROM allocated_courses WHERE FacultyName = @facultyName AND CourseID = @courseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@facultyName", facultyName);
                    cmd.Parameters.AddWithValue("@courseID", courseID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            label7.Text = "Record successfully deleted.";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AllocateCourse all = new AllocateCourse();
            all.Show();
            this.Visible = false;
        }
    }
}
