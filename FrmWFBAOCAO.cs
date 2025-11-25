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
    public partial class FrmWFBAOCAO : Form
    {
        public FrmWFBAOCAO()
        {
            InitializeComponent();
         
        }
        private void FrmBCBH_Load_1(object sender, EventArgs e)
        {
            btnBCBH_Click(sender, e);

        }

        private void btnBCBH_Click(object sender, EventArgs e)
        {
            FrmBCBH frm = new FrmBCBH();
            frm.TopLevel = false;
            panelPD.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FrmMORONG frm = new FrmMORONG();
            frm.TopLevel = false;
            panelPD.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnBCNH_Click(object sender, EventArgs e)
        {
            FrmBCNH frm = new FrmBCNH();
            frm.TopLevel = false;
            panelPD.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
    }
}
