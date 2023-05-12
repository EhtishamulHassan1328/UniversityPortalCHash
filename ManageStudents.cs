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
    public partial class ManageStudents : Form
    {
        public ManageStudents()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            addStudent.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Establish a database connection
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GG6QOPE;Initial Catalog=FlexDB;Integrated Security=True");
            conn.Open();

            // Create a SQL command to select all entries from the "Courses" table
            SqlCommand cmd = new SqlCommand("SELECT * FROM registeredStudents", conn);

            // Create a data adapter and fill a data table with the selected data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Bind the data table to a DataGridView control
            dataGridView1.DataSource = dataTable;

            // Close the database connection
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteStudent delStudent = new DeleteStudent();
            delStudent.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminOptions adm = new AdminOptions();
            adm.Show();
            this.Visible = false;
        }
    }
}
