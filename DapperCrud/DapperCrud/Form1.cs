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
using Dapper;

namespace DapperCrud
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = .; Initial Catalog = DapperDB; Integrated Security = True");
        int empId = 0;

        class Employee
        {
            public int EmpId { get; set; }
            public string Name { get; set; } 
            public string Mobile { get; set; }
            public string Address { get; set; }

        }
        void FillDataGridView()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@SearchText", txtSearch.Text.Trim());
            List<Employee> list = con.Query<Employee>("EmpViewOrSearch", param, commandType: CommandType.StoredProcedure).ToList<Employee>();
            dgvEmployee.DataSource = list;
            dgvEmployee.Columns[0].Visible = false;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Clear();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpId", empId);
                param.Add("@Name", txtName.Text.Trim());
                param.Add("@Mobile", txtMobile.Text.Trim());
                param.Add("@Address", txtAddress.Text.Trim());
                con.Execute("EmpAddOrEdit", param, commandType: CommandType.StoredProcedure);
                if (empId == 0)
                    MessageBox.Show("Saved successfully");
                else
                    MessageBox.Show("Updated Successfully");
                FillDataGridView();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@EmpId", empId);
                    con.Execute("EmpDeleteById",param, commandType: CommandType.StoredProcedure);
                    Clear();
                    FillDataGridView();
                    MessageBox.Show("Deleted Succesfully");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        void Clear()
        {
            txtName.Text = txtMobile.Text = txtAddress.Text = txtSearch.Text = "";
            empId = 0;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvEmployee_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(dgvEmployee.CurrentRow.Index != -1)
                {
                    empId = Convert.ToInt32(dgvEmployee.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = dgvEmployee.CurrentRow.Cells[1].Value.ToString();
                    txtMobile.Text = dgvEmployee.CurrentRow.Cells[2].Value.ToString();
                    txtAddress.Text = dgvEmployee.CurrentRow.Cells[3].Value.ToString();
                    btnSave.Text = "Update";
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
