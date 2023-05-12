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
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
            conn.Open();

            SqlCommand cm;

            string courseID = textBox1.Text;
            string courseCode = textBox2.Text;
            string courseName = textBox3.Text;
            int creditHours = int.Parse(textBox4.Text);
            string semester = textBox5.Text;
            string preReqCourseID = textBox6.Text;


            string query = "INSERT INTO Courses (CourseID, CourseCode, CourseName, CreditHours, Semester, PreRequisiteCourseID) " +
                            $"VALUES ('{courseID}', '{courseCode}', '{courseName}', {creditHours}, '{semester}', '{preReqCourseID}')";

            // Execute the SQL query using ADO.NET
            // (Assuming you have already opened a database connection and created a command object)
            cm = new SqlCommand(query, conn);
            cm.ExecuteNonQuery();
            cm.Dispose();
            conn.Close();

       

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OfferCourses man1 = new OfferCourses();
            man1.Show();
            this.Visible = false;
        }
    }
}
