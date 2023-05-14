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
    public partial class DeleteEvaluation : Form
    {
        public DeleteEvaluation()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ManageEvaluation fac = new ManageEvaluation();
            fac.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the values from the text boxes
            string studentId = textBox2.Text.Trim();
            string courseName = textBox1.Text.Trim();

            // Check that both fields are filled
            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(courseName))
            {
                label6.Text = "Please fill in both fields.";
                return;
            }

            // Delete the record from the Evaluation table
            string connectionString = "Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Evaluation WHERE StudentId = @StudentId AND CourseName = @CourseName";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@CourseName", courseName);

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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
