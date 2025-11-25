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

namespace QLBDS
{
    public partial class FrmMAIN : Form
    {
        private string _manv;
        private string _anh;
        private string _name;
        private string _role;

        FrmLOGIN f;
       // FrmBAN f1;
        public Guna.UI2.WinForms.Guna2Panel PanelBody
        {
            get { return panelbody; } // 'panelbody' là tên của Panel bạn đã đặt
        }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        //private string loggedInUsername;
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        string sql, constr;
   
        string userRight;

        string phannhom;
        public FrmMAIN(string manv, string anh, string name, string role)
        {
            InitializeComponent();
             phannhom = userRight;
            _manv = manv;
            _anh = anh;
            _name = name;
            _role = role;
            Mydashboard();
        }



        private void FrmMAIN_Load(object sender, EventArgs e)
        {

            //btnhome_Click(sender,e);



        }

 
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnproduct_Click(object sender, EventArgs e)
        {



            btnproduct.FillColor = Color.White;
            btnproduct.ForeColor = Color.FromArgb(112, 173, 71);
            btnproduct.Image = Properties.Resources.productgreen;
           
            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127,127,127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;

            FrmPDFRAME frm = new FrmPDFRAME();
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
           frm.BringToFront();
            frm.Show();

        }



        private void panelbody_Paint(object sender, PaintEventArgs e)
        {

        }

        public void btndonhang_Click(object sender, EventArgs e)
        {
            btndonhang.FillColor = Color.White;
            btndonhang.ForeColor = Color.FromArgb(112, 173, 71);
            btndonhang.Image = Properties.Resources.donhanggreen1;

            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;




            FrmHDBAN frm = new FrmHDBAN(f);
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
            

            frm.TenNhanVien = txtName.Text;
            frm.MaNhanVien = _manv;
            frm.ChucVu = txtChucvu.Text;
            if (frm.ShouldOpenFrmBAN)
            {
                FrmBAN frmBAN = new FrmBAN(f);
                // Truyền thông tin cần thiết từ FrmHDN sang FrmBAN (ví dụ: lblTENNV)
                frmBAN.lblTENNV.Text = frm.TenNhanVien;
                frmBAN.lblCHUCVU.Text = frm.ChucVu;
                frmBAN.ShowDialog();
            }

            }
        public void ActivateBtncustomerClick()
        {
            btncustomer_Click(btncustomer, EventArgs.Empty);
        }

        public void btnhome_Click(object sender, EventArgs e)
        {
            btnhome.FillColor = Color.White;
            btnhome.ForeColor = Color.FromArgb(112, 173, 71);
            btnhome.Image = Properties.Resources.homegreen;

            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;

            Mydashboard();
            
        }
        public  void  Mydashboard()
        {
            FrmDASHBOARD frm = new FrmDASHBOARD(f);
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
         //   frm.lbldailysale.Text = Dailysales();
            frm.BringToFront();
            frm.Show();
        }
        private void picaccount_Click(object sender, EventArgs e)
        {
            AccountMenu.Show(picaccount, new System.Drawing.Point(0, picaccount.Height));

        }

        private void btncustomer_Click(object sender, EventArgs e)
        {
            btncustomer.FillColor = Color.White;
            btncustomer.ForeColor = Color.FromArgb(112, 173, 71);
            btncustomer.Image = Properties.Resources.cusgreen;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127,127,127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;

            FrmCUS frm = new FrmCUS();
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnemploy_Click(object sender, EventArgs e)
        {
            btnemploy.FillColor = Color.White;
            btnemploy.ForeColor = Color.FromArgb(112, 173, 71);
            btnemploy.Image = Properties.Resources.nhanviengreen;

            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;


            //if (_role == "Quản lý")
            //{
            //    FrmEMPLOY frm = new FrmEMPLOY(f);
            //    frm.TopLevel = false;
            //    panelbody.Controls.Clear();
            //    panelbody.Controls.Add(frm);
            //    frm.BringToFront();
            //    frm.Show();
            //}
            //else if (_role == "Nhân viên")
            //{
            //    FrmUSERSET frm = new FrmUSERSET();
            //    frm.TopLevel = false;
            //    panelbody.Controls.Clear();
            //    panelbody.Controls.Add(frm);
            //    frm.BringToFront();
            //    frm.Show();
            //}
            FrmEMPLOY frm = new FrmEMPLOY();
            frm.TopLevel = false;
            panelbody.Controls.Clear(); // Xóa bất kỳ Control nào hiện tại trong panelbody
            panelbody.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void btncategory_Click(object sender, EventArgs e)
        {
            btncategory.FillColor = Color.White;
            btncategory.ForeColor = Color.FromArgb(112, 173, 71);
            btncategory.Image = Properties.Resources.category_green;


            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;

           
       

            FrmHDNHAP frm = new FrmHDNHAP(f);
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
            frm.TenNhanVien = txtName.Text;
            frm.MaNhanVien = _manv;
            if (frm.ShouldOpenFrmNHAP)
            {
                FrmNHAP frmNHAP = new FrmNHAP();
                // Truyền thông tin cần thiết từ FrmHDN sang FrmNHAP (ví dụ: lblTENNV)
                frmNHAP.lblTENNV.Text = frm.TenNhanVien;
                frmNHAP.ShowDialog();
            }
        }


     

        private void btnhelp_Click(object sender, EventArgs e)
        {
            btnhelp.FillColor = Color.White;
            btnhelp.ForeColor = Color.FromArgb(112, 173, 71);
            btnhelp.Image = Properties.Resources.helpfreen1;

            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;
        }
        //public void ShowEmployeeImage(string imagePath)
        //{
            
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(imagePath))
        //        {
                   
        //                // Nếu tồn tại, hiển thị ảnh trong PictureBox
        //                picaccount.Image = Image.FromFile(imagePath);
        //                picaccount.SizeMode = PictureBoxSizeMode.Zoom; // Để ảnh tự điều chỉnh kích thước theo PictureBox
                    
                   
        //        }
        //        else
        //        {
        //            // Nếu không có đường dẫn ảnh, đặt ảnh mặc định hoặc thông báo lỗi
        //            picaccount.Image = Properties.Resources.amusement_park; // Đặt ảnh mặc định từ Resource
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ nếu có lỗi khi hiển thị ảnh
        //        MessageBox.Show("Lỗi hiển thị ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void guna2ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            AccountMenu.RenderMode = ToolStripRenderMode.System;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();

            FrmLOGIN f = new FrmLOGIN();
            
            f.Show();
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện của FrmLISTPD và truyền tham chiếu đến FrmNHAP
            FrmPROFILE frm = new FrmPROFILE(_manv);

            // Hiển thị FrmLISTPD mà không đóng FrmNHAP
            frm.Show();
           
        }


    

        private void btnncc_Click(object sender, EventArgs e)
        {
            btnncc.FillColor = Color.White;
            btnncc.ForeColor = Color.FromArgb(112, 173, 71);
            btnncc.Image = Properties.Resources.supplier_green;

            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnthongke.FillColor = Color.FromArgb(230, 230, 230);
            btnthongke.ForeColor = Color.FromArgb(127, 127, 127);
            btnthongke.Image = Properties.Resources.thongkegrey;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            FrmNCC frm = new FrmNCC();
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnthongke_Click_1(object sender, EventArgs e)
        {

            btnthongke.FillColor = Color.White;
            btnthongke.ForeColor = Color.FromArgb(112, 173, 71);
            btnthongke.Image = Properties.Resources.thongkegreen;

            // Đặt màu nền, màu chữ, và hình ảnh cho các nút khác
            btncustomer.FillColor = Color.FromArgb(230, 230, 230);
            btncustomer.ForeColor = Color.FromArgb(127, 127, 127);
            btncustomer.Image = Properties.Resources.cusgrey;

            btnproduct.FillColor = Color.FromArgb(230, 230, 230);
            btnproduct.ForeColor = Color.FromArgb(127, 127, 127);
            btnproduct.Image = Properties.Resources.productgrey;

            btnhome.FillColor = Color.FromArgb(230, 230, 230);
            btnhome.ForeColor = Color.FromArgb(127, 127, 127);
            btnhome.Image = Properties.Resources.homegrey;

            btndonhang.FillColor = Color.FromArgb(230, 230, 230);
            btndonhang.ForeColor = Color.FromArgb(127, 127, 127);
            btndonhang.Image = Properties.Resources.donhanggrey1;

            btnemploy.FillColor = Color.FromArgb(230, 230, 230);
            btnemploy.ForeColor = Color.FromArgb(127, 127, 127);
            btnemploy.Image = Properties.Resources.nhanviengrey;

            btnhelp.FillColor = Color.FromArgb(230, 230, 230);
            btnhelp.ForeColor = Color.FromArgb(127, 127, 127);
            btnhelp.Image = Properties.Resources.helpgrey1;

            btncategory.FillColor = Color.FromArgb(230, 230, 230);
            btncategory.ForeColor = Color.FromArgb(127, 127, 127);
            btncategory.Image = Properties.Resources.category_grey;

            btnncc.FillColor = Color.FromArgb(230, 230, 230);
            btnncc.ForeColor = Color.FromArgb(127, 127, 127);
            btnncc.Image = Properties.Resources.supplier_grey;


            FrmWFBAOCAO frm = new FrmWFBAOCAO();
            frm.TopLevel = false;
            panelbody.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void txtChucvu_Click(object sender, EventArgs e)
        {

        }

        private void txtName_Click(object sender, EventArgs e)
        {

        }

        private void btnBAN_Click(object sender, EventArgs e)
        {
            //FrmBAN frm = new FrmBAN();
            //frm.TopLevel = false;
            //panelbody.Controls.Add(frm);
            //frm.BringToFront();
            //frm.Show();
        }
    }
}
