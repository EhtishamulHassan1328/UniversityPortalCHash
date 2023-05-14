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
    public partial class SetDistribution : Form
    {
        public SetDistribution()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                label12.Text = "Please fill all entries";
                return;
            }

            string campus_name = textBox1.Text;
            string semester = textBox2.Text;
            string course_name = textBox3.Text;
            int assignments_weight = int.Parse(textBox4.Text);
            int quizzes_weight = int.Parse(textBox5.Text);
            int sessional_weight = int.Parse(textBox6.Text);
            int finals_weight = int.Parse(textBox7.Text);

            // Check if course exists in Courses table
            bool courseExists = false;
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                string query = $"SELECT CourseName FROM Courses WHERE CourseName='{course_name}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                courseExists = reader.HasRows;
                reader.Close();
                conn.Close();
            }

            if (!courseExists)
            {
                label12.Text = "Course does not exist in Courses table";
                return;
            }

            // Check if mark distribution already exists for the course
            bool markDistributionExists = false;
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                string query = $"SELECT course_name FROM marks_distribution WHERE course_name='{course_name}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                markDistributionExists = reader.HasRows;
                reader.Close();
                conn.Close();
            }

            if (markDistributionExists)
            {
                label12.Text = "Mark distribution already exists for this course";
                return;
            }

            int total_weight = assignments_weight + quizzes_weight + sessional_weight + finals_weight;
            if (total_weight != 100)
            {
                label12.Text = "Total weightage must be 100";
                return;
            }

            // Insert mark distribution into marks_distribution table
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                string query = "INSERT INTO marks_distribution (campus_name, semester, course_name, assignments_weight, quizzes_weight, sessional_weight, finals_weight, total_weight) " +
                                $"VALUES ('{campus_name}', '{semester}', '{course_name}', {assignments_weight}, {quizzes_weight}, {sessional_weight}, {finals_weight}, {total_weight})";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            label12.Text = "Mark distribution set successfully";
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FacultyOptions man1 = new FacultyOptions();
            man1.Show();
            this.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
