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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int loginAtmp = 0;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            while (loginAtmp <= 5)
            {
                if (txtUsername.Text == "admin" && txtPassword.Text == "1234")
                {
                    MessageBox.Show("Login Successful", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Dashboard f1 = new Dashboard();
                    f1.Show();
                    break;
                }
                else
                {
                    MessageBox.Show("Incorrect username or password! \n" + "You have " + (4 - loginAtmp) + " login attempts left", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loginAtmp++;
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    if (loginAtmp == 5)
                    {
                        MessageBox.Show("Maximum login attempts exceeded.\n" + "The application will close immediately!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                }

            }
        }
    }
}
