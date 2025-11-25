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
   
    public partial class UCNHOMPD : UserControl
    {
        public string ANHNHOM { get; private set; }
        public string TENNHOM { get; private set; }
        public string MANHOM { get; private set; }
        public int TRANGTHAI { get; private set; }
  
        public event EventHandler UserControlClick;

        private string initialtrangthai;
        public UCNHOMPD(string tenNhom, string imagePath, string maNhom, int trangThai)
        {
   
            InitializeComponent();
            // Thêm UserControl vào Panel
            //Panel1.Controls.Add(this);
            //// Đặt Dock của UserControl là Fill để nó lấp đầy Panel
            //this.Dock = DockStyle.Fill;
            //// Đặt Margin cho UserControl để tạo khoảng cách shadow
            //this.Margin = new Padding(10);

            //// Gọi sự kiện MouseEnter để xử lý hiệu ứng di chuyển
            //this.MouseEnter += UCNHOMPD_MouseEnter;
            //// Gọi sự kiện MouseLeave để xử lý hiệu ứng khi con trỏ chuột ra
            //this.MouseLeave += UCNHOMPD_MouseLeave;


            ANHNHOM = imagePath;
            TENNHOM = tenNhom;
            MANHOM = maNhom;
            TRANGTHAI = trangThai;
          //  this.lblTENNHOM.Click += TENNHOM_Click;

              this.Click += UCNHOMPD_Click;

        }


        private void UCNHOMPD_Click(object sender, EventArgs e)
        {
           
                UserControlClick?.Invoke(this, EventArgs.Empty);
            
        }

        private void TENNHOM_Click(object sender, EventArgs e)
        {

        }
  

        private void UCNHOMPD_Load(object sender, EventArgs e)
        {
            lblTENNHOM.Text = TENNHOM;
         
            try
            {
                picANHNHOM.Image = Image.FromFile(ANHNHOM);
            }
            catch (Exception ex)
            {
                picANHNHOM.Image = Image.FromFile(@"C:\Users\Admin\Downloads\vegetable.png");
            }
            if (TRANGTHAI == 0)
            {
                // Trạng thái là 0 (Dừng bán)
                txtTRANGTHAI.Text = "Dừng bán";
                txtTRANGTHAI.FillColor = Color.FromArgb(251,229,214); // Màu fill
                txtTRANGTHAI.BorderColor = Color.FromArgb(251, 229, 214); // Màu viền
                txtTRANGTHAI.ForeColor = Color.FromArgb(197,90,17); // Màu chữ
                txtTRANGTHAI.Width = 80;
            }
            else if (TRANGTHAI == 1)
            {
                // Trạng thái là 1 (Bán)
                txtTRANGTHAI.Text = "Bán";
                txtTRANGTHAI.FillColor = Color.FromArgb(226,240,217); // Màu fill
                txtTRANGTHAI.BorderColor = Color.FromArgb(226, 240, 217); // Màu viền
                txtTRANGTHAI.Width = 45;
                txtTRANGTHAI.ForeColor = Color.FromArgb(112,173,71); // Màu chữ
            }
            initialtrangthai = txtTRANGTHAI.Text;
           
        }
        private void guna2Panel_Click(object sender, EventArgs e)
        {
         //   UserControlClick?.Invoke(this, EventArgs.Empty);

        }
        private void picANHNHOM_Click(object sender, EventArgs e)
        {

        }


        public event EventHandler EditButtonClicked;
        public event EventHandler DelButtonClicked;

        // Khi nút btnEDIT được nhấn, gọi sự kiện EditButtonClicked

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditButtonClicked?.Invoke(this, EventArgs.Empty);

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DelButtonClicked?.Invoke(this, EventArgs.Empty);

        }

        private void UCNHOMPD_MouseEnter(object sender, EventArgs e)
        {

        }

        private void UCNHOMPD_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTRANGTHAI_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
