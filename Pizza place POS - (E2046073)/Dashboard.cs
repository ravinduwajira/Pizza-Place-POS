using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza_place_POS____E2046073_
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnCreg_Click(object sender, EventArgs e)
        {
            CustomerReg cregfrm = new CustomerReg();
            cregfrm.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Inventory inventfrm = new Inventory();
            inventfrm.Show();
            this.Hide();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            Invoice invce = new Invoice();
            invce.Show();
            Cashier cshfrm = new Cashier( invce) ;
            cshfrm.Show();
            this.Hide();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Reports rpts = new Reports();
            rpts.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form1 loginfrm = new Form1();
            loginfrm.Show();
            this.Hide();
        }
    }
}
