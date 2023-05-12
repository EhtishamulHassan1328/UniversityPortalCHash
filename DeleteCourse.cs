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
    public partial class DeleteCourse : Form
    {
        public DeleteCourse()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string courseID = textBox1.Text.Trim();

            // Check if courseID is empty
            if (string.IsNullOrEmpty(courseID))
            {
                label4.Text = "Please enter a course ID.";
                return;
            }

            // Check if courseID exists in the table
            string query = "SELECT COUNT(*) FROM Courses WHERE CourseID = @courseID";
            int count;
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@courseID", courseID);
                    count = (int)cmd.ExecuteScalar();
                }
                conn.Close();
            }

            if (count == 0)
            {
                label4.Text = "Course not found in database.";
                return;
            }

            // Delete the course
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                query = "DELETE FROM Courses WHERE CourseID = @courseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@courseID", courseID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            label4.Text = "Course successfully deleted.";
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OfferCourses man1 = new OfferCourses();
            man1.Show();
            this.Visible = false;
        }
    }
}
