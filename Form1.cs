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

namespace LINQ2SQL
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(LINQ2SQL.Properties.Settings.Default.database1ConnectionString);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(con);

            var selectQuery =
                from a in dc.GetTable<Employee>()
                select a;
            dataGridView1.DataSource = selectQuery;

            //// TODO: This line of code loads data into the 'database1DataSet.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter.Fill(this.database1DataSet.Employee);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //create the new employee
            DataClasses1DataContext dc = new DataClasses1DataContext(con);
            Employee NewEmp = new Employee();
            NewEmp.Code = 5;
            NewEmp.Name = "ddddd";
            NewEmp.Adress = "ddddd";

            //add the new employee to database
            dc.Employees.InsertOnSubmit(NewEmp);

            //save changes to database
            dc.SubmitChanges();

            //rebind datagridview to display the new employee
            var selectQuery =
                from a in dc.GetTable<Employee>()
                select a;
            dataGridView1.DataSource = selectQuery;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(con);

            //get employee to update
            Employee employee = dc.Employees.FirstOrDefault(emp => emp.Code.Equals("2"));

            //update employee
            employee.Name = "csharp";
            employee.Adress = "csharp";

            //Save changes to Database.
            dc.SubmitChanges();

            //rebind datagridview to display the new employee
            var selectQuery =
                from a in dc.GetTable<Employee>()
                select a;
            dataGridView1.DataSource = selectQuery;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(con);

            //get the employee to delete
            Employee DeleteEmployee = dc.Employees.FirstOrDefault(emp => emp.Code.Equals("5"));

            //delete the employee
            dc.Employees.DeleteOnSubmit(DeleteEmployee);

            //save changes to database
            dc.SubmitChanges();

            //rebind datagridview to display the new employee
            var selectQuery =
                from a in dc.GetTable<Employee>()
                select a;
            dataGridView1.DataSource = selectQuery;
        }
    }
}
