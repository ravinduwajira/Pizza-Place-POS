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
    public partial class Invoice : Form
    {
       
        
       
        private void Invoice_Load(object sender, EventArgs e)
        {
            
        }

        public Invoice()
        {
            InitializeComponent();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Bitmap memoryImage = new Bitmap(panel1.Width, panel1.Height);
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);

            printDocument1.Print();
            Dashboard dshbdFrm = new Dashboard();
            dshbdFrm.Show();
            this.Hide();


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dashboard dshbdFrm = new Dashboard();
            dshbdFrm.Show();
            this.Hide();

        }
    }
}
