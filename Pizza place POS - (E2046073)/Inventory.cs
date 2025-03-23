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
    public partial class Inventory : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-N69JLGVQ;Initial Catalog=PPPOSDB;Integrated Security=True");
        SqlCommand cmd;
        public Inventory()
        {
            InitializeComponent();
        }
        
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPName.Text))
                {
                    MessageBox.Show("Please enter the Product Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Please enter the Product Price.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPQTY.Text))
                {
                    MessageBox.Show("Please enter the Product Quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPQTY.Focus();
                    return;
                }
                con.Open();
                string query = "Insert into ProductTB values('" + txtPName.Text + "','" + int.Parse(txtPrice.Text) + "','" + int.Parse(txtPQTY.Text) + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            txtPName.Clear();
            txtPQTY.Clear();
            txtPrice.Clear();
            MessageBox.Show("Product successfully registered ", "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dshbdFrm = new Dashboard();
            dshbdFrm.Show();
            this.Hide();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
