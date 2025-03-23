using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pizza_place_POS____E2046073_
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-N69JLGVQ;Initial Catalog=PPPOSDB;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        private void Reports_Load(object sender, EventArgs e)
        {
    
            this.salesRecordsTBTableAdapter2.Fill(this.pPPOSDBDataSet5.SalesRecordsTB);
            this.productTBTableAdapter1.Fill(this.pPPOSDBDataSet4.ProductTB);
            this.loyalCustomerTBTableAdapter.Fill(this.pPPOSDBDataSet.LoyalCustomerTB);
            con.Open();
            BindData();
            con.Close();
        }
        public void BindData()
        {
            cmd = new SqlCommand("select name from ProductTB", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        private void txtCID_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CustomerName,ContactNo,LoyaltyPoints from LoyalCustomerTB where CustomerID='" + txtCID.Text + "'", con);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                txtCName.Text = srd.GetValue(0).ToString();
                txtCNO.Text = srd.GetValue(1).ToString();
                txtLP.Text = srd.GetValue(2).ToString();
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update LoyalCustomerTB set CustomerName='" + txtCName.Text + "',ContactNo='" + txtCNO.Text + "',LoyaltyPoints='" + txtLP.Text + "' where CustomerID='" + txtCID.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.loyalCustomerTBTableAdapter.Fill(this.pPPOSDBDataSet.LoyalCustomerTB);
            txtCName.Clear();
            txtCNO.Clear();
            txtCID.Clear();
            MessageBox.Show("Customer Deatils successfully Updated! ", "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from LoyalCustomerTB where CustomerID='" + txtCID.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.loyalCustomerTBTableAdapter.Fill(this.pPPOSDBDataSet.LoyalCustomerTB);
            txtCName.Clear();
            txtCNO.Clear();
            txtCID.Clear();
            MessageBox.Show("Customer successfully deleted! ", "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Price,ProductQuantity from ProductTB where Name='" + comboBox1.Text + "'", con);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                txtPrice.Text = srd.GetValue(0).ToString();
                
                txtPQTY.Text = srd.GetValue(1).ToString();
            }
            con.Close();
             this.productTBTableAdapter1.Fill(this.pPPOSDBDataSet4.ProductTB);
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update ProductTB set Price='" + txtPrice.Text + "',ProductQuantity='" + txtPQTY.Text + "' where Name='" + comboBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.productTBTableAdapter1.Fill(this.pPPOSDBDataSet4.ProductTB);
            ;
            txtPQTY.Clear();
            txtPrice.Clear();
            MessageBox.Show("Product successfully Updated ", "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ProductTB where Name='" + comboBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.productTBTableAdapter1.Fill(this.pPPOSDBDataSet4.ProductTB);
            txtPQTY.Clear();
            txtPrice.Clear();
            MessageBox.Show("Product successfully Deleted ", "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLowSTK_Click(object sender, EventArgs e)
        {
            
           this.dataGridView2.Sort(this.dataGridView2.Columns[2], ListSortDirection.Ascending);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dshbdFrm = new Dashboard();
            dshbdFrm.Show();
            this.Hide();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
           

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from SalesRecordsTB where Time between '"+dateTimePicker1.Value.ToString("MM/dd/yyyy hh:mm tt") + "' and '"+dateTimePicker2.Value.ToString("MM/dd/yyyy hh:mm tt") + "' ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "SalesRecordsTB");
            dgvSalesR.DataSource = ds.Tables["SalesRecordsTB"];
            con.Close();

            int sum = 0;
            for (int i = 0; i < dgvSalesR.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dgvSalesR.Rows[i].Cells[4].Value);
            }
            txtProfit.Text = sum.ToString();
        }
    }
}
