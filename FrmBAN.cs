using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.Globalization;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;// thư viện đọc barcode và qrcode
using ZXing.Aztec;
namespace QLBDS
{
    public partial class FrmBAN : Form
    {
        private string maKH; // Biến lưu mã khách hàng
        private string _manv;

        public string MaNhanVien
        {
            get { return _manv; }
            set { _manv = value; }
        }
        public string MAHDB { get; set; }
        public string TenNhanVien { get; set; }
        public string ChucVu { get; set; }
        public FrmCUS f1;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        SqlDataReader dr;
        FrmLOGIN f;
       // FrmMAIN f1;

        string sql, constr;

        private double tongTien = 0;
        private double khachPhaiTra = 0;
        private double chietKhau = 0;
        private double tienThue = 0;
    

        public FrmBAN(FrmLOGIN frm)//, FrmMAIN frm1
        {
            InitializeComponent();
            // Gắn sự kiện TextChanged cho txtKHACHDUA
            txtKHACHDUA.TextChanged += TxtKHACHDUA_TextChanged;
            txtCHIETKHAU.TextChanged += txtCHIETKHAU_TextChanged;

            f = frm;
           
        }
        FilterInfoCollection filterInfoCollection;// biến lưu thông tin các thiết bị video
        VideoCaptureDevice captureDevice;// biến tương tác với thiết bị video
        private void txtCHIETKHAU_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCHIETKHAU.Text))
            {
                chietKhau = 0;
            }
            else
            {
                chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
            }

            UpdatekhachPhaiTra();
            //UpdateTongDG();
            //UpdateTongGG();
          //  Cập nhật lại số tiền thừa dựa trên giá trị trong txtKHACHDUA
            //if (double.TryParse(txtKHACHDUA.Text, out double khachDua))
            //{
            //    double tienThua = khachDua - khachPhaiTra;
            //    lblTIENTHUA.Text = tienThua.ToString("C0", new CultureInfo("vi-VN"));





            //}

            //else
            //{
            //    lblTIENTHUA.Text = "Khách đưa không hợp lệ";
            //}






            bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
            btnTHANHTOAN.Enabled = dt.Rows.Count > 0 && allSLGreaterThanZero;
            //   grdData.Columns["MAHH"].Visible = false;
            bool khachDuaValid = double.TryParse(txtKHACHDUA.Text, out double khachDua);
            if (khachDuaValid && khachDua >= khachPhaiTra)
            {
                btnTHANHTOAN.Enabled = true;
                double tienThua = khachDua - khachPhaiTra;
                lblTIENTHUA.Text = tienThua.ToString("C0", new CultureInfo("vi-VN"));
            }
            else
            {
                btnTHANHTOAN.Enabled = false;
                if (!khachDuaValid || khachDua < khachPhaiTra)
                {
                    lblTIENTHUA.Text = "Khách đưa không hợp lệ";
                }
            }
        }
        private void TxtKHACHDUA_TextChanged(object sender, EventArgs e)
        {// cu : khachPhaiTra=tongTien
          
            bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
            bool khachDuaValid = double.TryParse(txtKHACHDUA.Text, out double khachDua);

            if (khachDuaValid && allSLGreaterThanZero)
            {
                if (comNhom.SelectedItem != null && (comNhom.SelectedItem.ToString() == "Quẹt thẻ" || comNhom.SelectedItem.ToString() == "Chuyển khoản"))
                {
                    if (khachDua == khachPhaiTra)
                    {
                        btnTHANHTOAN.Enabled = true;
                        double tienThua = khachDua - khachPhaiTra;
                        lblTIENTHUA.Text = tienThua.ToString("C0", new CultureInfo("vi-VN"));
                    }
                    else if (khachDua > khachPhaiTra)
                    {
                        btnTHANHTOAN.Enabled = false;
                        lblTIENTHUA.Text = "Số tiền vượt quá";
                    }

                }
                else if (khachDua >= khachPhaiTra && (comNhom.SelectedItem == null || comNhom.SelectedItem.ToString() == "Tiền mặt"))
                {



                    btnTHANHTOAN.Enabled = true;
                    double tienThua = khachDua - khachPhaiTra;
                    lblTIENTHUA.Text = tienThua.ToString("C0", new CultureInfo("vi-VN"));

                }
            }
            else
            {
                btnTHANHTOAN.Enabled = false;
                if (!khachDuaValid || khachDua < khachPhaiTra)
                {
                    
                    lblTIENTHUA.Text = "Khách đưa không hợp lệ";

                }
                else
                {
                    lblTIENTHUA.Text = "Số lượng sản phẩm không hợp lệ";
                }
            }
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            // Đóng FrmDONHANG
            this.Close();

        }

        private void FrmBAN_Load(object sender, EventArgs e)
        {

            //Bay loi
            try
            {
                // lấy ds taastc ả các thiết bị video đang được kết nối với mấy tính 
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                // thêm tên mỗi thiết bị vào droplist
                foreach (FilterInfo filterInfo in filterInfoCollection)
                    comDEVICE.Items.Add(filterInfo.Name);
                comDEVICE.SelectedIndex = 0;// chọn thiết bị có chỉ số đầu tiên trong danh sách
                // Lấy ngày hiện tại
                DateTime ngayBanHienTai = DateTime.Now;

                // Gán ngày hiện tại vào thuộc tính "Text" của Label lblNGAYNHAP
                lblNGAYBAN.Text = ngayBanHienTai.ToString("dd/MM/yyyy HH:mm:ss");

                
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
                //   sql = "SELECT TENNCC, DIACHI FROM DMNHACC";
                //    da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh


                dt.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
                lblMAHDB.Text = MAHDB;// $"HD{currentMAHDN:000}"; 
                lblTENNV.Text = TenNhanVien;
                lblCHUCVU.Text = ChucVu;
                //string maKH = "ANONYMOUS";
               // lblMAKH.Text = maKH;

                grdData.CellValueChanged += new DataGridViewCellEventHandler(grdData_CellValueChanged);
                //grdData.Columns["MAHH"].Visible = false;
                conn.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }

        private void lbluser_Click(object sender, EventArgs e)
        {

        }

     

        private void btnback_Click(object sender, EventArgs e)
        {
            // Đóng FrmBAN
            this.Close();

            // Tạo lại FrmHDBAN
            FrmHDBAN frmHDBAN = new FrmHDBAN(f);
            frmHDBAN.TopLevel = false;
            frmHDBAN.FormBorderStyle = FormBorderStyle.None;
            frmHDBAN.Dock = DockStyle.Fill;

            // Truy cập đến panel 'panelbody' của FrmMAIN và thay thế FrmCTHDN bằng FrmHDNHAP
            FrmMAIN mainForm = Application.OpenForms.OfType<FrmMAIN>().FirstOrDefault();
            if (mainForm != null)
            {
                Guna.UI2.WinForms.Guna2Panel panelBody = mainForm.PanelBody;
                panelBody.Controls.Clear();
                panelBody.Controls.Add(frmHDBAN);
                frmHDBAN.Show();
            }
        }
      


        public void UpdatekhachPhaiTra()
        {
            tienThue = 0;
               tongTien = 0;
            foreach (DataRow row in dt.Rows)
            {
                double thanhTien = Convert.ToDouble(row["THANHTIEN"]);
                tongTien += thanhTien;
                double tienThueNCK = Convert.ToDouble(row["TIENTHUE"]);
       
               tienThue += tienThueNCK;


            }
            lblDONGIAXSL.Text = tongTien.ToString("C0", new CultureInfo("vi-VN"));
           // lblVAT.Text =tienThue.ToString("C0", new CultureInfo("vi-VN"));
            if (string.IsNullOrEmpty(txtCHIETKHAU.Text))
            {
                chietKhau = 0;
            }
            else
            {
                chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
            }

            khachPhaiTra = tongTien- tongTien * chietKhau/100;
            lblVAT.Text =( tienThue * (1-chietKhau / 100)).ToString("C0", new CultureInfo("vi-VN"));
            lblTONGTIEN.Text = khachPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
          //  lblVAT.Text = tienThue.ToString("C0", new CultureInfo("vi-VN"));
            btnTHANHTOAN.Enabled = dt.Rows.Count > 0;
        }
        //public void UpdateTongDG()
        //{
        //    double tongDG = 0;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        double donGia = Convert.ToDouble(row["DONGIA"]);
        //        double soLuong = Convert.ToDouble(row["SL"]);

        //        tongDG += donGia* soLuong;
        //    }

        //    lblDONGIAXSL.Text = tongDG.ToString("C0", new CultureInfo("vi-VN"));
        //    btnTHANHTOAN.Enabled = dt.Rows.Count > 0;
        //}
      
        //public void UpdateTongGG()
        //{
        //    double tongGG = 0;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        double donGia = Convert.ToDouble(row["DONGIA"]);
        //        double soLuong = Convert.ToDouble(row["SL"]);
        //        double giamGia = Convert.ToDouble(row["GIAMGIA"]);

        //        tongGG += donGia * soLuong* giamGia/100;
        //    }

        //    lblSUMGG.Text = tongGG.ToString("C0", new CultureInfo("vi-VN"));
        //    btnTHANHTOAN.Enabled = dt.Rows.Count > 0;
        //}

        private void btnListPD_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện của FrmLISTPD và truyền tham chiếu đến FrmBAN
            FrmLISTPD f = new FrmLISTPD(this);

            // Hiển thị FrmLISTPD mà không đóng FrmBAN
            f.Show();
            if (grdData.Rows.Count > 0)
            {
                if (grdData.Rows.Count > 0)
                {
                    grdData.Visible = true;

                    btnAdd.Visible = false;
                    anh.Visible = false;
                    //grdData.CurrentCell = grdData.Rows[0].Cells["SL"];
                    //grdData.BeginEdit(true);
                }
            }
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xác định chỉ số cột của cột "colDel"
            int colIndex = grdData.Columns["colDel"].Index;

            // Kiểm tra xem sự kiện CellContentClick xảy ra trong cột "colDel" không
            if (e.ColumnIndex == colIndex && e.RowIndex >= 0)
            {
                // Xác định dòng tương ứng với sự kiện CellContentClick
                DataGridViewRow row = grdData.Rows[e.RowIndex];

                // Xóa bản ghi tương ứng
                if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa dòng khỏi DataTable
                    DataRowView rowView = (DataRowView)row.DataBoundItem;
                    dt.Rows.Remove(rowView.Row);

                    // Cập nhật lại số thứ tự (STT) trong bảng dữ liệu
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }
                    if (IsGrdDataEmpty())
                    {
                        btnListPD.Visible = false;
                        grdData.Visible = false;
                        btnAdd.Visible = true;
                        anh.Visible = true;

                    }
                    // UpdateTongTien();
                    UpdatekhachPhaiTra();
                    //UpdateTongDG();
                    //UpdateTongGG();
                    // Cập nhật lại số tiền thừa dựa trên giá trị trong txtKHACHDUA
                    if (double.TryParse(txtKHACHDUA.Text, out double khachDua))
                    {
                        double tienThua = khachDua - khachPhaiTra;
                        lblTIENTHUA.Text = tienThua.ToString("C0", new CultureInfo("vi-VN"));
                    }
                    else
                    {
                        lblTIENTHUA.Text = "Khách đưa không hợp lệ";
                    }
                    // Cập nhật lại DataSource của grdData
                    grdData.DataSource = dt;
                }
            }
            bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
            btnTHANHTOAN.Enabled = dt.Rows.Count > 0 && allSLGreaterThanZero;
        }
        private bool IsGrdDataEmpty()
        {
            return dt.Rows.Count == 0;
        }
        private void grdData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int slColumnIndex = grdData.Columns["SL"].Index;
            int giamGiaColumnIndex = grdData.Columns["GIAMGIA"].Index;
            int thanhTienColumnIndex = grdData.Columns["THANHTIEN"].Index;
            int tienThueColumnIndex = grdData.Columns["TIENTHUE"].Index;

            if (e.RowIndex >= 0 && (e.ColumnIndex == slColumnIndex || e.ColumnIndex == giamGiaColumnIndex))
            {
                DataGridViewRow row = grdData.Rows[e.RowIndex];
                if (string.IsNullOrEmpty(txtCHIETKHAU.Text))
                {
                    chietKhau = 0;
                }
                else
                {
                    chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
                }
                // Lấy giá trị SL và CHIETKHAU từ ô tương ứng
                double soLuong = Convert.ToDouble(row.Cells[slColumnIndex].Value);
                double giamGia = Convert.ToDouble(row.Cells[giamGiaColumnIndex].Value);
               // double thue = Convert.ToDouble(row.Cells[tienThueColumnIndex].Value);

                //  double chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
                // Tính toán thành tiền
                double donGia = Convert.ToDouble(row.Cells["DONGIA"].Value);
                double thue = Convert.ToDouble(row.Cells["THUEVAO"].Value);

                double thanhTien = donGia * soLuong * (1 - giamGia/100);
               
                double tongDG = donGia * soLuong;
                // Cập nhật giá trị của cột THANHTIEN
                row.Cells[thanhTienColumnIndex].Value = thanhTien;
                double tienThueNCK = (thanhTien * thue / 100) / (1 + thue / 100);
                row.Cells[tienThueColumnIndex].Value = tienThueNCK;

                //lblVAT.Text = tienThue.ToString("C0", new CultureInfo("vi-VN"));
                UpdatekhachPhaiTra();
                //UpdateTongDG();
                //UpdateTongGG();
                
                    bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
                btnTHANHTOAN.Enabled = dt.Rows.Count > 0 && allSLGreaterThanZero;
                //   grdData.Columns["MAHH"].Visible = false;
                bool khachDuaValid = double.TryParse(txtKHACHDUA.Text, out double khachDua);
                if (khachDuaValid && khachDua >= khachPhaiTra)
                {
                    btnTHANHTOAN.Enabled = true;
                    double tienThua = khachDua - khachPhaiTra;
                    lblTIENTHUA.Text = tienThua.ToString("C0", new CultureInfo("vi-VN"));
                }
                else
                {
                    btnTHANHTOAN.Enabled = false;
                    if (!khachDuaValid || khachDua < khachPhaiTra)
                    {
                        lblTIENTHUA.Text = "Khách đưa không hợp lệ";
                    }
                }
            }

        }
        private void comKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (comKH.SelectedItem != null)
            //    {
            //        if (comKH.SelectedItem.ToString() == "Khách lẻ")
            //        {

            //            maKH = "ANONYMOUS";
            //            lblMAKH.Text = maKH;


            //        }
            //        else if (comKH.SelectedItem.ToString() == "Khách quen")
            //        {

            //            FrmLISTKH f = new FrmLISTKH(this);
            //            f.ShowDialog();
            //            maKH = f.SelectedMaKH;
            //            lblMAKH.Text = maKH;
            //        }
            //    }


        }
        private void btnTHANHTOAN_Click(object sender, EventArgs e)
        {
            //if (comKH.SelectedItem != null)
            //{
                // Code xử lý thanh toán

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
               
                     }

                double tienThue = 0;
                double tongTien = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        double thanhTien = Convert.ToDouble(row["THANHTIEN"]);
                        tongTien += thanhTien;
                    double tienThueNCK = Convert.ToDouble(row["TIENTHUE"]);

                    tienThue += tienThueNCK;
                    }
                double chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
                if (txtCHIETKHAU.Text == null)
                {
                    chietKhau = 0;
                }
                double khachPhaiTra = tongTien - tongTien * chietKhau / 100;
               double tongThue= (tienThue * (1 - chietKhau / 100));
                // double khachPhaiTra = tongTien - tongTien * Convert.ToDouble(txtCHIETKHAU.Text) / 100;
                int khachdua = Convert.ToInt32(txtKHACHDUA.Text);
                int tienthua = khachdua-Convert.ToInt32(khachPhaiTra);
                    foreach (DataRow row in dt.Rows)
                    {
                        sql = "UPDATE DMKH SET TONGCHITIEU = TONGCHITIEU +  @khachPhaiTra  where MAKH = @MaKH";
                        using (SqlCommand updateKHCommand = new SqlCommand(sql, conn))
                        {
                            updateKHCommand.Parameters.AddWithValue("@khachPhaiTra", khachPhaiTra);
                        updateKHCommand.Parameters.AddWithValue("@MaKH", lblMAKH.Text);
                        updateKHCommand.ExecuteNonQuery();
                        }
                    }

                foreach (DataRow row in dt.Rows)
                {
                    sql = "UPDATE DMKH SET TONGSLDH = TONGSLDH + 1 where MAKH = @MaKH";
                    using (SqlCommand updateKH1Command = new SqlCommand(sql, conn))
                    {
                        updateKH1Command.Parameters.AddWithValue("@MaKH", lblMAKH.Text);
                        updateKH1Command.ExecuteNonQuery();
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    sql = "UPDATE DMHH SET SL = SL -  @SL  where MAHH = @MAHH";
                    using (SqlCommand updateHHCommand = new SqlCommand(sql, conn))
                    {
                        updateHHCommand.Parameters.AddWithValue("@SL", Convert.ToDouble(row["SL"]));
                        updateHHCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());
                        updateHHCommand.ExecuteNonQuery();
                    }
                }
               
                    // Update thông tin trong DMHDb
                    DateTime ngayNhapHienTai = DateTime.Now;

                    string formattedNgayNhap = ngayNhapHienTai.ToString("yyyy-MM-dd HH:mm:ss");

                   string insertsql = "INSERT INTO DMHDB VALUES (@MaHDB, @NgayBan,@khachPhaiTra,@MaKH, @MaNV,@SL,@HTTT,@KHACHDUA, @TIENTHUA,@ChietKhau)";
                    using (SqlCommand updateHDNCommand = new SqlCommand(insertsql, conn))
                    {
                        updateHDNCommand.Parameters.AddWithValue("@NgayBan", formattedNgayNhap);
                    // updateHDNCommand.Parameters.AddWithValue("@TongTien", tongTien);
                     updateHDNCommand.Parameters.AddWithValue("@khachPhaiTra", khachPhaiTra);

                    updateHDNCommand.Parameters.AddWithValue("@SL",0);//Convert.ToInt32(txtSL.Text)
                    updateHDNCommand.Parameters.AddWithValue("@KHACHDUA",khachdua);
                    updateHDNCommand.Parameters.AddWithValue("@TIENTHUA", tienthua);
                    updateHDNCommand.Parameters.AddWithValue("@ChietKhau", chietKhau);//Convert.ToInt32(txtCHIETKHAU.Text));


                    if (lblMAKH.Text == "")
                    {
                   
                        updateHDNCommand.Parameters.AddWithValue("@MaKH", "ANONYMOUS");
                    }
                    else

                    {
                        updateHDNCommand.Parameters.AddWithValue("@MaKH", lblMAKH.Text);
                    }
                        //}
                        //    else
                        //{
                        //    MessageBox.Show("Vui lòng chọn khách hàng", "Thông báo");
                        //        btnTHANHTOAN.Enabled = false;
                        //}
                        if (MaNhanVien != null)
                        {
                            // Sử dụng biến MaNhanVien trong lệnh SQL
                            updateHDNCommand.Parameters.AddWithValue("@MaNV", MaNhanVien);
                        }
                        else
                        {
                            MessageBox.Show("MANV NULL", "Thông báo");
                        }
                    if (comNhom.SelectedItem == null)
                    {
                        updateHDNCommand.Parameters.AddWithValue("@HTTT", 0);

                    }
                    else if (comNhom.SelectedItem != null)
                    { 
                        if (comNhom.SelectedItem.ToString() == "Tiền mặt")
                          {
                            updateHDNCommand.Parameters.AddWithValue("@HTTT", 0);
                          }
                         else if (comNhom.SelectedItem.ToString() == "Quẹt thẻ")
                          {
                            updateHDNCommand.Parameters.AddWithValue("@HTTT", 1);

                        }
                        else if (comNhom.SelectedItem.ToString() == "Chuyển khoản")
                        {
                            updateHDNCommand.Parameters.AddWithValue("@HTTT", 2);

                        }

                    }

                            updateHDNCommand.Parameters.AddWithValue("@MaHDB", lblMAHDB.Text);
                        updateHDNCommand.ExecuteNonQuery();
                    }
                //foreach (DataRow row in dt.Rows)
                //{
                //    sql = "UPDATE DMHDB SET SL = SL +  @SL where MAHDB = @MAHDB";
                //    using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                //    {
                //        if (row["SL"] is int)
                //        {
                //            updateHH0Command.Parameters.AddWithValue("@SL", Convert.ToInt32(row["SL"]));
                //        }
                //        else if (row["SL"] is double)
                //        {
                //            updateHH0Command.Parameters.AddWithValue("@SL", 1);
                //        }
                //        //updateHH0Command.Parameters.AddWithValue("@SL", Convert.ToInt32(row["SL"]));
                //        updateHH0Command.Parameters.AddWithValue("@MaHDB", lblMAHDB.Text);

                //        //  updateHHCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());
                //        updateHH0Command.ExecuteNonQuery();
                //    }
                //}
                foreach (DataRow row in dt.Rows)
                {
                    string maHH = row["MAHH"].ToString();
                    string queryNhom = "SELECT MANHOM FROM DMHH WHERE MAHH = @MaHH";

                    // Sử dụng queryNhom để xác định MANHOM
                    using (SqlCommand queryNhomCommand = new SqlCommand(queryNhom, conn))
                    {
                        queryNhomCommand.Parameters.AddWithValue("@MaHH", maHH);
                        string maNhom = queryNhomCommand.ExecuteScalar().ToString();

                        // Kiểm tra giá trị maNhom để quyết định giá trị cần chèn
                        if (maNhom == "NHH03")
                        {
                            sql = "UPDATE DMHDB SET SL = SL + 1 WHERE MAHDB = @MAHDB";
                        }
                        else
                        {
                            sql = "UPDATE DMHDB SET SL = SL + @SL WHERE MAHDB = @MAHDB";
                        }

                        using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                        {
                            if (maNhom != "NHH03")
                            {
                                updateHH0Command.Parameters.AddWithValue("@SL", Convert.ToInt32(row["SL"]));
                            }

                            updateHH0Command.Parameters.AddWithValue("@MaHDB", lblMAHDB.Text);
                            updateHH0Command.ExecuteNonQuery();
                        }
                    }
                }

                // Update thông tin trong DMCTHDB
                foreach (DataRow row in dt.Rows)
                    {
                        sql = "INSERT INTO DMCTHDB VALUES (@MaHDB,@MaHH,@SL,@DonGia,@ThanhTien,@GiamGia,@tienThue)";
                        using (SqlCommand updateCTHDNCommand = new SqlCommand(sql, conn))
                        {
                            updateCTHDNCommand.Parameters.AddWithValue("@SL", Convert.ToDouble(row["SL"]));
                            updateCTHDNCommand.Parameters.AddWithValue("@DonGia", Convert.ToDouble(row["DONGIA"]));

                            updateCTHDNCommand.Parameters.AddWithValue("@GiamGia", Convert.ToDouble(row["GIAMGIA"]));
                            updateCTHDNCommand.Parameters.AddWithValue("@ThanhTien", Convert.ToDouble(row["THANHTIEN"]));
                        updateCTHDNCommand.Parameters.AddWithValue("@tienThue", Convert.ToDouble(row["TIENTHUE"]));

                        updateCTHDNCommand.Parameters.AddWithValue("@MaHDB", lblMAHDB.Text);
                            updateCTHDNCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());
                            updateCTHDNCommand.ExecuteNonQuery();
                        }
                    }

                //MessageBox.Show("Đã cập nhật đơn nhập hàng thành công!", "Thông báo");
                // string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

                //  using (SqlConnection conn = new SqlConnection(constr))
                // {
                // conn.Open();



                // Tạo câu truy vấn SQL để lấy dữ liệu
                string queryNV = $"SELECT DMHDB.MAHDB, NGAYBAN, TENKH, SDT, TENHH, DMCTHDB.SL AS SL, DVT, DONGIA, THANHTIEN, GIAMGIA, TONGTIEN, DMHDB.SL AS TONGSL, HTTT, KHACHDUA, TIENTHUA, CHIETKHAU FROM DMHDB, DMCTHDB,DMHH, DMKH " +
                    $"WHERE DMHDB.MAHDB = DMCTHDB.MAHDB AND DMCTHDB.MAHH = DMHH.MAHH AND DMHDB.MAKH = DMKH.MAKH" +
                    $" AND DMHDB.MAHDB = '{lblMAHDB.Text}'";
                        

                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtCTHDB");

                                // Tạo đối tượng báo cáo
                                rptCTHDB r = new rptCTHDB();
                           // r.Parameter_KHACHDUA = 
                            //r.SetParameterValue( txtKHACHDUA.Text);
                            //r.SetParameterValue("Parameter_TIENTHUA", lblTIENTHUA.Text);
                            //r.Parameter_KHACHDUA = txtKHACHDUA.Text;
                        
                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTCTHDB f = new FrmRPTCTHDB();
                                f.crystalReportViewer1.ReportSource = r;
                        //    f.crystalReportViewer1.ZoomMode = 30;
                                f.ShowDialog();
                            }
                        }
                    }

               // }
                conn.Close();
                    MAHDB = lblMAHDB.Text; // Cập nhật lại giá trị của MAHDN sau khi tạo mới hóa đơn
                btnsearchKH.Visible = true;
                btnADDKH.Visible = true;
                    ResetForm();
                }
            //}
          //  else
            //{
            //    MessageBox.Show("Vui lòng chọn khách hàng trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
        }
        private bool IsAllSLGreaterThanZero()
        {
            foreach (DataRow row in dt.Rows)
            {
                double sl = Convert.ToDouble(row["SL"]);
                if (sl <= 0)
                {
                    return false; // Nếu có giá trị <= 0 thì trả về false
                }
            }
            return true; // Nếu tất cả giá trị đều > 0 thì trả về true
        }
        private void ResetForm()
        {
            // Xóa dữ liệu trong bảng dt
            dt.Clear();

            // Cập nhật lại trạng thái của các điều khiển
      
            // ...
            // Lấy ngày hiện tại
            DateTime ngayNhapHienTai = DateTime.Now;

            // Gán ngày hiện tại vào thuộc tính "Text" của Label lblNGAYNHAP
            lblNGAYBAN.Text = ngayNhapHienTai.ToString("dd/MM/yyyy HH:mm:ss");


            //doan chuong trinh can bay loi
            //3 dong dau dùng để thiet lap den CSDL QLBDS 
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["STT"] = i + 1;
            }
            lblMAHDB.Text = "HDB" + (int.Parse(MAHDB.Substring(3)) + 1).ToString("D3");
            lblTENNV.Text = TenNhanVien;
            lblCHUCVU.Text = ChucVu;


            grdData.CellValueChanged += new DataGridViewCellEventHandler(grdData_CellValueChanged);

            conn.Close();
            // Cập nhật trạng thái của nút btnNew
            btnTHANHTOAN.Enabled = false; // Vì danh sách SL đang rỗng
            btnListPD.Visible = false;
            btnAdd.Visible = true;
            anh.Visible = true;
            grdData.Visible = false;

           // comKH.SelectedItem = null;
            lblDONGIAXSL.Text = "0";
           // lblSUMGG.Text = "0";
            lblTONGTIEN.Text = "0";
            txtKHACHDUA.Clear();
            txtCHIETKHAU.Clear();
            lblTIENTHUA.Text = "0";
            panelKH.Visible = false;
            lblVAT.Text = "0";
            panelKH.Visible = false;
            btnADDKH.Visible = true;
            btnsearchKH.Visible = true;
         //   comNhom.SelectedItem = null;

            comNhom.SelectedIndex = -1;
            // Cập nhật DataSource của grdData
            grdData.DataSource = dt;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện của FrmLISTPD và truyền tham chiếu đến FrmNHAP
            FrmLISTPD f = new FrmLISTPD(this);

            // Hiển thị FrmLISTPD mà không đóng FrmNHAP
            f.Show();

            if (grdData.Rows.Count > 0)
            {
                grdData.Visible = true;
                btnListPD.Visible = true;
                anh.Visible = false;
                btnAdd.Visible = false;
          
            }
        }

        private void txtKHACHDUA_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void anh_Click(object sender, EventArgs e)
        {

        }

        private void btnDONMOI_Click(object sender, EventArgs e)
        {
            ResetForm();
             lblMAHDB.Text = MAHDB;// $"HD{currentMAHDN:000}"; 

        }


        

        private void btnNEWCUS_Click(object sender, EventArgs e)
        {
           
            FrmCUS frmCUS = new FrmCUS();
            frmCUS.btnNew_Click(sender, e);

        }

        private void lblMAKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnsearchKH_Click(object sender, EventArgs e)
        {
            //FrmLISTKH f = new FrmLISTKH(this);
            //f.ShowDialog();
            //maKH = f.SelectedMaKH;
            //lblMAKH.Text = maKH;
        }

   

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void lblTENKH_Click(object sender, EventArgs e)
        {

        }

        private void btnsearchKH_Click_1(object sender, EventArgs e)
        {
            FrmLISTKH f = new FrmLISTKH(this);
            f.ShowDialog();
            maKH = f.SelectedMaKH;
            lblMAKH.Text = maKH;
            btnADDKH.Visible = false;
            btnsearchKH.Visible = false;

            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string query = "SELECT TENKH, SDT FROM DMKH WHERE MAKH = @MaKH";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTTKH.Text = reader["TENKH"].ToString();
                            lblSDT.Text = reader["SDT"].ToString();
                        }
                    }
                }
            }

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            FrmCUS frmCUS = new FrmCUS();
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            SqlConnection conn = new SqlConnection(constr);

            conn.Open();

            string lastMAKHQuery = "SELECT TOP 1 MAKH FROM DMKH ORDER BY MAKH DESC";
            SqlCommand lastMAKHCmd = new SqlCommand(lastMAKHQuery, conn);
            object lastMAKHResult = lastMAKHCmd.ExecuteScalar();

            if (lastMAKHResult != null)
            {
                string lastMAKH = lastMAKHResult.ToString();
                frmCUS.currentMAKH = int.Parse(lastMAKH.Substring(2)) + 1;
            }

            frmCUS.btnNew_Click(sender, e);


        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            btnADDKH.Visible = true;
            btnsearchKH.Visible = true;
            panelKH.Visible = false;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[comDEVICE.SelectedIndex].MonikerString);// khởi tạo thiết bị vdeo, dựa trên thiết nbị được chọn từ combpbox 
            captureDevice.NewFrame += CaptureDevice_NewFrame;// đăng ký nhận sự kiện frame mới từ camera
            captureDevice.Start();
          //  timer1.Start();
        }



        //private List<string> scannedBarcodes = new List<string>();

        //private void CaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        //{
        //    Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
        //    BarcodeReader reader = new BarcodeReader();
        //    var result = reader.Decode(bitmap);

        //    if (result != null && !scannedBarcodes.Contains(result.Text))
        //    {
        //        if (txtQR.IsHandleCreated)
        //        {
        //            txtQR.BeginInvoke(new MethodInvoker(delegate ()
        //            {
        //                txtQR.Text = result.Text;
        //                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
        //                using (SqlConnection conn = new SqlConnection(constr))
        //                {
        //                    conn.Open();
        //                    sql = $"select MAHH, TENHH, GIABAN, DVT, THUEVAO FROM DMHH " +
        //                           $" WHERE BARCODE = '{txtQR.Text}'";
        //                    SqlCommand cmd = new SqlCommand(sql, conn);
        //                    SqlDataReader reader1 = cmd.ExecuteReader();

        //                    if (reader1.Read())
        //                    {
        //                        string maHH = reader1["MAHH"].ToString();
        //                        string tenHH = reader1["TENHH"].ToString();
        //                        string giaBan = reader1["GIABAN"].ToString();
        //                        string dvt = reader1["DVT"].ToString();
        //                        string thuevaostring = reader1["THUEVAO"].ToString();

        //                        AddRowToGrdData(tenHH, giaBan, maHH, dvt, thuevaostring);

        //                        // Đánh dấu là đã thêm bản ghi
        //                        scannedBarcodes.Add(result.Text);
        //                    }
        //                }
        //            }));
        //        }
        //    }

        //    if (picQR.IsHandleCreated)
        //    {
        //        picQR.BeginInvoke(new MethodInvoker(delegate ()
        //        {
        //            picQR.Image = bitmap;
        //        }));
        //    }
        //}
        public void AddRowToGrdData(string tenHH, string giaBan, string maHH, string dvt, string thuevaostring)
        {
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                sql = "select DMCTHDB.MAHH,TENHH, DMCTHDB.SL,DONGIA,DMHH.DVT,GIAMGIA,THANHTIEN,TIENTHUE, isnull(thuevao,0) as thuevao from DMCTHDB" +
                    " LEFT JOIN DMHH ON DMHH.MAHH = DMCTHDB.MAHH" +
                    " WHERE MAHDB like '" + lblMAHDB + "'";


                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                grdData.DataSource = dt;
                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }

                DataRow newRow = dt.Rows.Add();

                newRow["STT"] = dt.Rows.Count;
                newRow["TENHH"] = tenHH;
                newRow["SL"] = 1;
                newRow["DONGIA"] = giaBan;
                newRow["DVT"] = dvt;

                newRow["GIAMGIA"] = 0;
                // Tính toán thành tiền
                double donGia = Convert.ToDouble(giaBan);
                newRow["THUEVAO"] = thuevaostring;
                newRow["MAHH"] = maHH;

                double thue = Convert.ToDouble(thuevaostring);

                double soLuong = Convert.ToDouble(newRow["SL"]);
                double giamGia = Convert.ToDouble(newRow["GIAMGIA"]);
                double thanhTien = donGia * soLuong * (1 - giamGia / 100);
                newRow["THANHTIEN"] = thanhTien;
                double tienThueNCK = (thanhTien * thue / 100) / (1 + thue / 100);
                newRow["TIENTHUE"] = tienThueNCK;

                //  lblVAT.Text = tienThue.ToString();
                // UpdateTongTien();
                grdData.Columns["TIENTHUE"].Visible = false;
                grdData.Columns["MAHH"].Visible = false;

                grdData.DataSource = dt; // Cập nhật DataSource của GrdData
                                         //  grdData.Columns["MAHH"].Visible = false;
                                         //grdData.Columns["MAHH"].Visible = false;
                if (!IsGrdDataEmpty())
                {
                    btnListPD.Visible = true;
                    grdData.Visible = true;
                    btnAdd.Visible = false;
                    anh.Visible = false;
                }
                UpdatekhachPhaiTra();
                conn.Close();
            }
        }
private List<string> scannedBarcodes = new List<string>();
        private DateTime lastScanTime = DateTime.MinValue;
        private readonly TimeSpan scanInterval = TimeSpan.FromSeconds(0.8); // Thời gian chờ giữa các lần quét

        private void CaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            DateTime now = DateTime.Now;

            // Kiểm tra xem đã đến thời điểm có thể thực hiện quét tiếp theo chưa
            if (now - lastScanTime < scanInterval)
            {
                return;
            }

            lastScanTime = now;
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);

            if (result != null)
            {
                if (txtQR.IsHandleCreated)
                {
                    txtQR.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        txtQR.Text = result.Text;
                        constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                        using (SqlConnection conn = new SqlConnection(constr))
                        {
                            conn.Open();
                            sql = $"select MAHH, TENHH, GIABAN, DVT, THUEVAO FROM DMHH " +
                                   $" WHERE BARCODE = '{txtQR.Text}'";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            SqlDataReader reader1 = cmd.ExecuteReader();

                            if (reader1.Read())
                            {
                                string maHH = reader1["MAHH"].ToString();
                                string tenHH = reader1["TENHH"].ToString();
                                string giaBan = reader1["GIABAN"].ToString();
                                string dvt = reader1["DVT"].ToString();
                                string thuevaostring = reader1["THUEVAO"].ToString();

                                if (scannedBarcodes.Contains(result.Text))
                                {
                                    // Nếu đã quét trước đó, tăng SL lên 1
                                    UpdateRowSL(maHH);
                                }
                                else
                                {
                                    // Nếu chưa quét trước đó, thêm mới bản ghi
                                    AddRowToGrdData(tenHH, giaBan, maHH, dvt, thuevaostring);

                                    // Đánh dấu là đã thêm bản ghi
                                    scannedBarcodes.Add(result.Text);
                                }
                            }
                        }
                    }));
                }
            }

            if (picQR.IsHandleCreated)
            {
                picQR.BeginInvoke(new MethodInvoker(delegate ()
                {
                    picQR.Image = bitmap;
                }));
            }
        }

        private void UpdateRowSL(string maHH)
        {
            // Tìm và cập nhật SL cho mã barcode
            foreach (DataGridViewRow row in grdData.Rows)
            {
                if (row.Cells["MAHH"].Value.ToString() == maHH)
                {
                    int currentSL = Convert.ToInt32(row.Cells["SL"].Value);
                    row.Cells["SL"].Value = currentSL + 1;
                    break;
                }
            }
        }
        private void FrmBAN_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (captureDevice.IsRunning)
            //    captureDevice.Stop();
            if(captureDevice!= null)
            {
                if(captureDevice.IsRunning)
                    captureDevice.Stop();
                picQR.Image = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if(picQR.Image != null)
            //{
            //    BarcodeReader barcodeReader = new BarcodeReader();
            //    Result result = barcodeReader.Decode((Bitmap)picQR.Image);
            //    if(result != null)
            //    {
            //        txtQR.Text = result.ToString();
            //        timer1.Stop();
            //        if (captureDevice.IsRunning)
            //            captureDevice.Stop();
            //    }    
            //}
        }

        private void picQR_Click(object sender, EventArgs e)
        {

        }

        private void txtQR_TextChanged(object sender, EventArgs e)
        {

        }

        private void comDEVICE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
