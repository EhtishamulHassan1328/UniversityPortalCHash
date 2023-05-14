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
    public partial class ManageEvaluation : Form
    {
        public ManageEvaluation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // First, retrieve the values from the text boxes and check boxes
            string studentId = txtStudentId.Text.Trim();
            string courseName = txtCourseName.Text.Trim();
            string section = txtSection.Text.Trim();
            int marks;
            if (!int.TryParse(txtMarks.Text.Trim(), out marks))
            {
                label8.Text = "Please enter a valid number for marks.";
                return;
            }

            bool assignment = chkAssignment.Checked;
            bool quizzes = chkQuizzes.Checked;
            bool sessionals = chkSessionals.Checked;
            bool finals = chkFinals.Checked;

            // Check that all fields are filled
            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(section) || (!assignment && !quizzes && !sessionals && !finals))
            {
                label8.Text = "Please fill in all fields and select at least one evaluation option.";
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
                string courseQuery = "SELECT COUNT(*) FROM Courses WHERE CourseName = @CourseName";
                using (SqlCommand command = new SqlCommand(courseQuery, connection))
                {
                    command.Parameters.AddWithValue("@CourseName", courseName);
                    int courseCount = (int)command.ExecuteScalar();

                    if (courseCount == 0)
                    {
                        label8.Text = "Course name not found.";
                        return;
                    }
                }

                // Check if the evaluation record already exists
                string evalQuery = "SELECT * FROM Evaluation WHERE StudentId = @StudentId AND CourseName = @CourseName";
                using (SqlCommand command = new SqlCommand(evalQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@CourseName", courseName);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // If the record exists, update it with the new evaluation
                        reader.Close();
                        string updateQuery = "UPDATE Evaluation SET Section = @Section, Assignment = Assignment | @Assignment, Quizzes = Quizzes | @Quizzes, Sessionals = Sessionals | @Sessionals, Finals = Finals | @Finals, Marks = Marks + @Marks WHERE StudentId = @StudentId AND CourseName = @CourseName";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@StudentId", studentId);
                            updateCommand.Parameters.AddWithValue("@CourseName", courseName);
                            updateCommand.Parameters.AddWithValue("@Section", section);
                            updateCommand.Parameters.AddWithValue("@Assignment", assignment);
                            updateCommand.Parameters.AddWithValue("@Quizzes", quizzes);
                            updateCommand.Parameters.AddWithValue("@Sessionals", sessionals);
                            updateCommand.Parameters.AddWithValue("@Finals", finals);
                            updateCommand.Parameters.AddWithValue("@Marks", (int)marks);

                            updateCommand.ExecuteNonQuery();
                        }
                        label8.Text = "Evaluation updated successfully.";


                    }
                    else
                    {
                        // If the record does not exist, insert a new evaluation
                        reader.Close();
                        string insertQuery = "INSERT INTO Evaluation(StudentId, CourseName, Section, Assignment, Quizzes, Sessionals, Finals, Marks) VALUES (@StudentId, @CourseName, @Section, @Assignment, @Quizzes, @Sessionals, @Finals, @Marks)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@StudentId", studentId);
                            insertCommand.Parameters.AddWithValue("@CourseName", courseName);
                            insertCommand.Parameters.AddWithValue("@Section", section);
                            insertCommand.Parameters.AddWithValue("@Assignment", assignment);
                            insertCommand.Parameters.AddWithValue("@Quizzes", quizzes);
                            insertCommand.Parameters.AddWithValue("@Sessionals", sessionals);
                            insertCommand.Parameters.AddWithValue("@Finals", finals);
                            insertCommand.Parameters.AddWithValue("@Marks", marks);
                            insertCommand.ExecuteNonQuery();
                        }
                        label8.Text = "Evaluation added successfully.";
                    }
                }
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            FacultyOptions fac2 = new FacultyOptions();
            fac2.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteEvaluation fac2 = new DeleteEvaluation();
            fac2.Show();
            this.Visible = false;

        }
    }
}
