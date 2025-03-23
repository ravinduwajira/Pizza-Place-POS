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
    public partial class Cashier : Form
    {
        Invoice BillV;
        public Cashier(Invoice bv)
        {
            InitializeComponent();
            this.BillV = bv;
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-N69JLGVQ;Initial Catalog=PPPOSDB;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        private void Cashier_Load(object sender, EventArgs e)
        {
            timer1.Start();
            txtLCname.ReadOnly = true;
            txtLpoints.ReadOnly = true;
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


        
        public void btnAddtoCart_Click(object sender, EventArgs e)
        {
            
            try
            {

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter the Product Quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }

                dgvCart.Rows.Add(comboBox1.Text, int.Parse(txtCIprice.Text), int.Parse(txtQty.Text), int.Parse(txtCIprice.Text) * int.Parse(txtQty.Text));

                BillV.dgvBill.Rows.Add(comboBox1.Text, int.Parse(txtCIprice.Text), int.Parse(txtQty.Text), int.Parse(txtCIprice.Text) * int.Parse(txtQty.Text));

               
                int sum = 0;
                for (int i = 0; i < dgvCart.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dgvCart.Rows[i].Cells[3].Value);
                }
                txtCBill.Text = sum.ToString();
                int lp = int.Parse(txtLpoints.Text);
                int sleProfit = (sum * 10 / 100) - lp;
                
                if ( lp > 100)
                {
                    txtLD.Text = txtLpoints.Text;
                    int finalBill = (sum - lp);
                    txtFbill.Text = finalBill.ToString();
                    sleProfit = (sum * 10 / 100) - lp;
                    txtSleProfit.Text = sleProfit.ToString();

                }
                else
                {
                    txtLD.Text = "0";
                    txtFbill.Text = sum.ToString();
                    sleProfit = (sum * 10 / 100);
                    txtSleProfit.Text = sleProfit.ToString();
                    txtELP.Text = (int.Parse(txtFbill.Text) / 100).ToString();
                }
                
                con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTB set ProductQuantity='"+ (int.Parse(txtAPQty.Text) - int.Parse(txtQty.Text)) + "' where Name='" + comboBox1.Text + "'", con);               
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

       

        private void txtLCID_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CustomerName,LoyaltyPoints from LoyalCustomerTB where CustomerID='" + txtLCID.Text + "'", con);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                txtLCname.Text = srd.GetValue(0).ToString();
                txtLpoints.Text = srd.GetValue(1).ToString();
            }
            con.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Price,ProductQuantity from ProductTB where Name='" + comboBox1.Text + "'", con);
                SqlDataReader srd = cmd.ExecuteReader();
                while (srd.Read())
                {
                    txtCIprice.Text = srd.GetValue(0).ToString();
                    txtAPQty.Text = srd.GetValue(1).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            
            if (comboBox2.Text=="Normal")
            {
                txtLCID.ReadOnly = true;
                txtLCID.Clear();
                txtLCname.Clear();
                txtLpoints.Text = "0";
            }
            else
            {
                txtLCID.ReadOnly = false;               
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            
            try
            {
                if (string.IsNullOrEmpty(txtCash.Text))
                {
                    MessageBox.Show("Please enter the Cash Amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCash.Focus();
                    return;
                }

                int change = int.Parse(txtCash.Text) - int.Parse(txtFbill.Text);
                con.Open();
                string query = "Insert into SalesRecordsTB values('" + datetime.ToString("MM/dd/yyyy hh:mm tt") + "', '" + txtCBill.Text + "','" + txtLD.Text + "','" + txtFbill.Text + "','"+ (int.Parse(txtFbill.Text))*0.1 + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                int lp = int.Parse(txtLpoints.Text);
                if (lp > 100)
                {

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("update LoyalCustomerTB set LoyaltyPoints='" + 0 + "' where CustomerID='" + txtLCID.Text + "'", con);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("update LoyalCustomerTB set LoyaltyPoints='" + (int.Parse(txtLpoints.Text) + (int.Parse(txtFbill.Text)/100)) + "' where CustomerID='" + txtLCID.Text + "'", con);
                    cmd2.ExecuteNonQuery();
                }

                int chnge = int.Parse(txtCash.Text) - int.Parse(txtFbill.Text);



                MessageBox.Show(string.Format("Transaction successful!, Change for Customer is Rs. {0}", chnge), "Pizza Place POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BillV.lbl1.Text = txtLCname.Text;
                BillV.lbl2.Text = txtCBill.Text;
                BillV.lbl3.Text = txtLD.Text;
                BillV.lbl4.Text = txtFbill.Text;
                BillV.lbl5.Text = txtCash.Text;
                BillV.lbl6.Text = chnge.ToString();
                BillV.lbl7.Text = txtELP.Text;
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
     
        }

                   

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            lblTime.Text = datetime.ToString("MM/dd/yyyy hh:mm tt");
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Invoice invce = new Invoice();
            invce.Hide();
            Dashboard dshbdFrm = new Dashboard();
            dshbdFrm.Show();
            this.Hide();
        }


        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
