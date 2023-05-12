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
    public partial class DeleteStudent : Form
    {
        public DeleteStudent()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studID = textBox1.Text.Trim();

            // Check if studID is empty
            if (string.IsNullOrEmpty(studID))
            {
                label4.Text = "Please enter a student ID.";
                return;
            }

            // Check if studID exists in the table
            string query = "SELECT COUNT(*) FROM registeredStudents WHERE StudId = @studID";
            int count;
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studID", studID);
                    count = (int)cmd.ExecuteScalar();
                }
                conn.Close();
            }

            if (count == 0)
            {
                label4.Text = "Student not found in database.";
                return;
            }

            // Delete the row
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                query = "DELETE FROM registeredStudents WHERE StudId = @studID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studID", studID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            label4.Text = "Student successfully deleted.";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ManageStudents man1 = new ManageStudents();
            man1.Show();
            this.Visible = false;
        }
    }
}
