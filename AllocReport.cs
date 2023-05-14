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
    public partial class AllocReport : Form
    {
        public AllocReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT ac.FacultyName, ac.CourseID, c.CourseName, ac.Section, ac.CourseCoordinator " +
                           "FROM allocated_courses ac " +
                           "JOIN Courses c ON ac.CourseID = c.CourseID " +
                           "ORDER BY ac.Section";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                conn.Close();
            }

            // Format the DataGridView to match the desired output
            dataGridView1.Columns[0].HeaderText = "Faculty Name";
            dataGridView1.Columns[1].HeaderText = "Course ID";
            dataGridView1.Columns[2].HeaderText = "Course Name";
            dataGridView1.Columns[3].HeaderText = "Section";
            dataGridView1.Columns[4].HeaderText = "Course Coordinator";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReportGenerate att = new ReportGenerate();
            att.Show();
            this.Visible= false;

        }
    }
}
