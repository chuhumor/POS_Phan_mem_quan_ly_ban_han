using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBDS
{
    public partial class FrmPDFRAME : Form
    {
        public FrmPDFRAME()
        {
            InitializeComponent();
        }
        private void FrmPRODUCT_Load_1(object sender, EventArgs e)
        {
            btnPRODUCT_Click(sender, e);

        }
     
        private void btnPRODUCT_Click(object sender, EventArgs e)
        {
           // btnCTPRODUCT.BackColor = Color.White;
           // btnCTPRODUCT.ForeColor = Color.Black;
          // btnCTPRODUCT.CustomBorderThickness = new Padding(0, 0, 0, 4);
           // btnCTPRODUCT.BorderColor = Color.FromArgb(112, 173, 71);

         //   btnNHOMPD.BackColor = Color.White;
         //   btnNHOMPD.ForeColor = Color.Gray;
          //  btnNHOMPD.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            FrmPRODUCT frm = new FrmPRODUCT();
            frm.TopLevel = false;
            panelPD.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void panelPD_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNHOMPD_Click(object sender, EventArgs e)
        {
            //btnNHOMPD.BackColor = Color.White;
           // btnNHOMPD.ForeColor = Color.Black;
         //   btnNHOMPD.CustomBorderThickness = new Padding(0, 0, 0, 4);
         //   btnNHOMPD.BorderColor = Color.FromArgb(112, 173, 71);

         //   btnCTPRODUCT.BackColor = Color.White;
          //  btnCTPRODUCT.ForeColor = Color.Gray;
          //  btnCTPRODUCT.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            FrmNHOMPD frm = new FrmNHOMPD();
            frm.TopLevel = false;
            panelPD.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
