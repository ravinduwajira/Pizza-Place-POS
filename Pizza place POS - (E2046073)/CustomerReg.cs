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
    public partial class CustomerReg : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-N69JLGVQ;Initial Catalog=PPPOSDB;Integrated Security=True");
        SqlCommand cmd;
        public CustomerReg()
        {
            InitializeComponent();
        }
        
        private void btnCreg_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (string.IsNullOrEmpty(txtCName.Text))
                {
                    MessageBox.Show("Please enter the Customer Name.", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCNO.Text))
                {
                    MessageBox.Show("Please enter the Customer Mobile Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCNO.Focus();
                    return;
                }
                
                
                con.Open();
                string query = "Insert into LoyalCustomerTB(CustomerName,ContactNo,LoyaltyPoints) values('" + txtCName.Text + "', '" + txtCNO.Text + "','" + 0 + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd2 = new SqlCommand("select CustomerID from LoyalCustomerTB where ContactNo='" + txtCNO.Text + "'", con);
                SqlDataReader srd = cmd2.ExecuteReader();
                while (srd.Read())
                {
                    txtCID.Text = srd.GetValue(0).ToString();
                }
                con.Close();


                MessageBox.Show("Customer successfully registered ", "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dshbdFrm = new Dashboard();
            dshbdFrm.Show();
            this.Hide();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCName.Clear();
            txtCNO.Clear();
            txtCID.Clear();
        }

        private void txtCNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
