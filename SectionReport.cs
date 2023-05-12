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
    public partial class SectionReport : Form
    {
        public SectionReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT Section, StudId, CourseID, CourseName FROM registeredStudents ORDER BY Section";

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
            dataGridView1.Columns[0].HeaderText = "Section";
            dataGridView1.Columns[1].HeaderText = "Student ID";
            dataGridView1.Columns[2].HeaderText = "Course ID";
            dataGridView1.Columns[3].HeaderText = "Course Name";
        }

        private void SectionReport_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReportGenerate all = new ReportGenerate();
            all.Show();
            this.Visible = false;
        }
    }
}
