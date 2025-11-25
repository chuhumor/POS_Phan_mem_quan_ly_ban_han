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
namespace QLBDS
{
    public partial class FrmNHAP : Form
    {
        private string _manv;

        public string MaNhanVien
        {
            get { return _manv; }
            set { _manv = value; }
        }
        public string MAHDN { get; set; }
        public string TenNhanVien { get; set; }
        //public string MaNhanVien { get; set; }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        SqlDataReader dr;
        FrmLOGIN f;

        //  public Guna.UI2.WinForms.Guna2DataGridView GrdData { get ; set; } 

        private double tongTien = 0;
        private double phaiTra = 0;
        private double chietKhau = 0;
        private double tienThue = 0;


        string sql, constr;
        int i;
        Boolean addnewflag = false;
        public FrmNHAP()
        {
            InitializeComponent();
            txtDATRA.TextChanged += txtDATRA_TextChanged;
            txtCHIETKHAU.TextChanged += txtCHIETKHAU_TextChanged;


        }
       

        private void TENNV_Click(object sender, EventArgs e)
        {

        }

        private void NGAYNHAP_Click(object sender, EventArgs e)
        {

        }

        private void MAHDN_Click(object sender, EventArgs e)
        {

        }
      
    

       


        private void comNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNCC.SelectedIndex != -1)
            {
                string selectedTenNCC = comNCC.SelectedItem.ToString();

                DataRowView selectedRow = (DataRowView)comNCC.SelectedItem;
                string diaChi = selectedRow["DIACHI"].ToString();

                lblDIACHI.Text = diaChi;
            }
        }


        private void lblDIACHI_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
        public void AddRowToGrdData(string tenHH, string giaNhap, string maHH, string dvt, string thuerastring)
        {
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                sql = "select DMCTHDN.MAHH,TENHH, DMCTHDN.SL,DMHH.DVT, DONGIA,CHIETKHAU ,THANHTIEN,TIENTHUE, isnull(thuera,0) as thuera from DMCTHDN" +
                    " LEFT JOIN DMHH ON DMHH.MAHH = DMCTHDN.MAHH" +
                    " WHERE MAHDN like '" + lblMAHDN.Text + "'";


                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                grdData.DataSource = dt;
                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
              //  grdData.Columns["MAHH"].Visible = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }

                DataRow newRow = dt.Rows.Add();

                newRow["STT"] = dt.Rows.Count;
                newRow["MAHH"] = maHH;
                newRow["TENHH"] = tenHH;

                newRow["SL"] = 0;
                newRow["DVT"] = dvt;
                newRow["DONGIA"] = giaNhap;
                newRow["CHIETKHAU"] = 0;
                // Tính toán thành tiền
                double donGia = Convert.ToDouble(giaNhap);
                newRow["THUERA"] = thuerastring;
                double thue = Convert.ToDouble(thuerastring);

                double soLuong = Convert.ToDouble(newRow["SL"]);
                double chietKhau = Convert.ToDouble(newRow["CHIETKHAU"]);
                double thanhTien = donGia * soLuong * (1 - chietKhau/100);
                newRow["THANHTIEN"] = thanhTien;
                double tienThueNCK = (thanhTien * thue / 100) / (1 + thue / 100);
                newRow["TIENTHUE"] = tienThueNCK;
                grdData.Columns["TIENTHUE"].Visible = false;
                grdData.Columns["MAHH"].Visible = false;

                // UpdateTongTien();
                grdData.DataSource = dt; // Cập nhật DataSource của GrdData
                                         //grdData.Columns["MAHH"].Visible = false;
                if (!IsGrdDataEmpty())
                {
                    btnListPD.Visible = true;
                    grdData.Visible = true;
                    btnAdd.Visible = false;
                    anh.Visible = false;
                }
                    conn.Close();
            }
        }
        public void UpdateTongTien()
        {
            tienThue = 0;

            double tongTien = 0;
            foreach (DataRow row in dt.Rows)
            {
                double thanhTien = Convert.ToDouble(row["THANHTIEN"]);
                tongTien += thanhTien;
                double tienThueNCK = Convert.ToDouble(row["TIENTHUE"]);

                tienThue += tienThueNCK;

            }

            lblDONGIAXSL.Text = tongTien.ToString("C0", new CultureInfo("vi-VN"));
            if (string.IsNullOrEmpty(txtCHIETKHAU.Text))
            {
                chietKhau = 0;
            }
            else
            {
                chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
            }

            phaiTra = tongTien - tongTien * chietKhau / 100;
            lblVAT.Text = (tienThue * (1 - chietKhau / 100)).ToString("C0", new CultureInfo("vi-VN"));

            TONGTIEN.Text = phaiTra.ToString("C0", new CultureInfo("vi-VN"));
            btnNew.Enabled = dt.Rows.Count > 0;
        }
        //public void UpdatephaiTra()
        //{
        //    tongTien = 0;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        double thanhTien = Convert.ToDouble(row["THANHTIEN"]);
        //        tongTien += thanhTien;
        //    }

        //    if (string.IsNullOrEmpty(txtCHIETKHAU.Text))
        //    {
        //        chietKhau = 0;
        //    }
        //    else
        //    {
        //        chietKhau = Convert.ToDouble(txtCHIETKHAU.Text);
        //    }

        //    phaiTra = tongTien - tongTien * chietKhau / 100;
        //    TONGTIEN.Text = phaiTra.ToString("C0", new CultureInfo("vi-VN"));
        //    btnNew.Enabled = dt.Rows.Count > 0;
        //}

        //public void UpdateTongDG()
        //{
        //    double tongDG = 0;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        double donGia = Convert.ToDouble(row["DONGIA"]);
        //        double soLuong = Convert.ToDouble(row["SL"]);

        //        tongDG += donGia * soLuong;
        //    }

        //    lblDONGIAXSL.Text = tongDG.ToString("C0", new CultureInfo("vi-VN"));
        //    btnNew.Enabled = dt.Rows.Count > 0;
        //}

        //public void UpdateTongGG()
        //{
        //    double tongGG = 0;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        double donGia = Convert.ToDouble(row["DONGIA"]);
        //        double soLuong = Convert.ToDouble(row["SL"]);
        //        double giamGia = Convert.ToDouble(row["CHIETKHAU"]);

        //        tongGG += donGia * soLuong * giamGia / 100;
        //    }

        //  //  lblSUMGG.Text = tongGG.ToString("C0", new CultureInfo("vi-VN"));
        //    btnNew.Enabled = dt.Rows.Count > 0;
        //}
        private void btnListPD_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện của FrmLISTPD và truyền tham chiếu đến FrmNHAP
            FrmLISTPD f = new FrmLISTPD(this);

            // Hiển thị FrmLISTPD mà không đóng FrmNHAP
            f.Show();
            if (grdData.Rows.Count > 0)
            {
                grdData.Visible = true;
                
                btnAdd.Visible = false;
                anh.Visible = false;
                //grdData.CurrentCell = grdData.Rows[0].Cells["SL"];
                //grdData.BeginEdit(true);
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
                    UpdateTongTien();
                   // UpdateTongDG();
                    //  UpdateTongGG();
                    if (double.TryParse(txtDATRA.Text, out double daTra))
                    {
                        double conPhaiTra = daTra - phaiTra;
                        lblCONPHAITRA.Text = conPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
                    }
                    else
                    {
                        lblCONPHAITRA.Text = "Giá trị nhập không hợp lệ";
                    }
                    // Cập nhật lại DataSource của grdData
                    grdData.DataSource = dt;
                }
            }
 
            bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
            btnNew.Enabled = dt.Rows.Count > 0 && allSLGreaterThanZero;
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

                // Tính toán thành tiền
                double donGia = Convert.ToDouble(row.Cells["DONGIA"].Value);
                double thue = Convert.ToDouble(row.Cells["THUERA"].Value);

                double thanhTien = donGia * soLuong * (1 - giamGia / 100);

                // Cập nhật giá trị của cột THANHTIEN
                row.Cells[thanhTienColumnIndex].Value = thanhTien;
                double tienThueNCK = (thanhTien * thue / 100) / (1 + thue / 100);
                row.Cells[tienThueColumnIndex].Value = tienThueNCK;
                UpdateTongTien();
                // UpdatekhachPhaiTra();
                // UpdateTongDG();

                bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
                btnNew.Enabled = dt.Rows.Count > 0 && allSLGreaterThanZero;
                //   grdData.Columns["MAHH"].Visible = false;
                bool daTraValid = double.TryParse(txtDATRA.Text, out double daTra);
                if (daTraValid)
                {
                    btnNew.Enabled = true;
                    double conPhaiTra = phaiTra - daTra;
                    lblCONPHAITRA.Text = conPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
                }
                else
                {
                    btnNew.Enabled = false;
                    if (!daTraValid)
                    {
                        lblCONPHAITRA.Text = "Giá trị nhập không hợp lệ";
                    }
                }




            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
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
            double phaiTra = tongTien - tongTien * chietKhau / 100;
                double tongThue = (tienThue * (1 - chietKhau / 100));

                // double khachPhaiTra = tongTien - tongTien * Convert.ToDouble(txtCHIETKHAU.Text) / 100;
                int daTra = Convert.ToInt32(txtDATRA.Text);
                int conPhaiTra = Convert.ToInt32(phaiTra) - daTra; 
                foreach (DataRow row in dt.Rows)
                {
                    sql = "UPDATE DMHH SET SL = SL +  @SL  where MAHH = @MAHH";
                    using (SqlCommand updateHHCommand = new SqlCommand(sql, conn))
                    {
                        updateHHCommand.Parameters.AddWithValue("@SL", Convert.ToInt32(row["SL"]));
                        updateHHCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());

                        updateHHCommand.ExecuteNonQuery();
                    }
                }



                // Update thông tin trong DMHDN
                DateTime ngayNhapHienTai = DateTime.Now;

                string formattedNgayNhap = ngayNhapHienTai.ToString("yyyy-MM-dd HH:mm:ss");

                sql = "INSERT INTO DMHDN VALUES (@MaHDN, @NgayNhap,@TongTien,@MaNCC, @MaNV,@SL,@ChietKhau,@DaTra, @PhaiTra,@TrangThai)";
                using (SqlCommand updateHDNCommand = new SqlCommand(sql, conn))
                {
                    
                    updateHDNCommand.Parameters.AddWithValue("@NgayNhap", formattedNgayNhap);
                    updateHDNCommand.Parameters.AddWithValue("@TongTien", phaiTra);
                    updateHDNCommand.Parameters.AddWithValue("@MaNCC", com2dt.Rows[comNCC.SelectedIndex]["MANCC"].ToString());
                    updateHDNCommand.Parameters.AddWithValue("@ChietKhau", chietKhau);
                    updateHDNCommand.Parameters.AddWithValue("@DaTra", daTra); 
                    updateHDNCommand.Parameters.AddWithValue("@PhaiTra", conPhaiTra);
                    if(conPhaiTra > 0)
                    {
                        updateHDNCommand.Parameters.AddWithValue("@TrangThai", 0);

                    }
                    else
                    {
                        updateHDNCommand.Parameters.AddWithValue("@TrangThai", 1);

                    }

                    if (MaNhanVien != null)
                    {
                        // Sử dụng biến MaNhanVien trong lệnh SQL
                        updateHDNCommand.Parameters.AddWithValue("@MaNV", MaNhanVien);
                    }else
                    {
                        MessageBox.Show("MANV NULL", "Thông báo");
                    }    
                    updateHDNCommand.Parameters.AddWithValue("@SL", 0);
               
                    updateHDNCommand.Parameters.AddWithValue("@MaHDN", lblMAHDN.Text);
                    updateHDNCommand.ExecuteNonQuery();
                }

                foreach (DataRow row in dt.Rows)
                {
                    sql = "UPDATE DMHDN SET SL = SL +  @SL where MAHDN = @MAHDN";
                    using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                    {
                        updateHH0Command.Parameters.AddWithValue("@SL", Convert.ToInt32(row["SL"]));
                        updateHH0Command.Parameters.AddWithValue("@MaHDN", lblMAHDN.Text);

                        //  updateHHCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());
                        updateHH0Command.ExecuteNonQuery();
                    }
                }
                // Update thông tin trong DMNHACC
                sql = "UPDATE DMNHACC SET CONGNO = CONGNO + @PhaiTra WHERE MANCC = @MANCC";
                using (SqlCommand updateNCCCommand = new SqlCommand(sql, conn))
                {

                    updateNCCCommand.Parameters.AddWithValue("@PhaiTra", conPhaiTra);
                    updateNCCCommand.Parameters.AddWithValue("@MANCC", com2dt.Rows[comNCC.SelectedIndex]["MANCC"].ToString());
                    updateNCCCommand.ExecuteNonQuery();
                }
                // Update thông tin trong DMNHACC
                sql = "UPDATE DMNHACC SET SL = SL + 1 WHERE MANCC = @MANCC";
                using (SqlCommand updateNCCCommand = new SqlCommand(sql, conn))
                {    
                    updateNCCCommand.Parameters.AddWithValue("@MANCC", com2dt.Rows[comNCC.SelectedIndex]["MANCC"].ToString());
                    updateNCCCommand.ExecuteNonQuery();
                }
                // Update thông tin trong TTDONNHAP
                sql = "INSERT INTO TTDONNHAP VALUES (@MaHDN,@HTTT,@SoTien,@ngayTT)";
                using (SqlCommand updateTTDONNHAPCommand = new SqlCommand(sql, conn))
                {
                    updateTTDONNHAPCommand.Parameters.AddWithValue("@MaHDN", lblMAHDN.Text);
                    if (comNhom.SelectedItem == null)
                    {
                        updateTTDONNHAPCommand.Parameters.AddWithValue("@HTTT", 0);

                    }
                    else if (comNhom.SelectedItem != null)
                    {
                        if (comNhom.SelectedItem.ToString() == "Tiền mặt")
                        {
                            updateTTDONNHAPCommand.Parameters.AddWithValue("@HTTT", 0);
                        }
                        else if (comNhom.SelectedItem.ToString() == "Quẹt thẻ")
                        {
                            updateTTDONNHAPCommand.Parameters.AddWithValue("@HTTT", 1);

                        }
                        else if (comNhom.SelectedItem.ToString() == "Chuyển khoản")
                        {
                            updateTTDONNHAPCommand.Parameters.AddWithValue("@HTTT", 2);

                        }

                    }
                    updateTTDONNHAPCommand.Parameters.AddWithValue("@SoTien", daTra);
                    updateTTDONNHAPCommand.Parameters.AddWithValue("@ngayTT", formattedNgayNhap);


                    updateTTDONNHAPCommand.ExecuteNonQuery();
                }
                // Update thông tin trong DMCTHDN
                foreach (DataRow row in dt.Rows)
                {
                    sql = "INSERT INTO DMCTHDN VALUES (@MaHDN,@MaHH,@SL,@DonGia,@ChietKhau,@ThanhTien,@TienThue)";
                    using (SqlCommand updateCTHDNCommand = new SqlCommand(sql, conn))
                    {
                        updateCTHDNCommand.Parameters.AddWithValue("@SL", Convert.ToInt32(row["SL"]));
                        updateCTHDNCommand.Parameters.AddWithValue("@DonGia", Convert.ToDouble(row["DONGIA"]));

                        updateCTHDNCommand.Parameters.AddWithValue("@ChietKhau", Convert.ToDouble(row["CHIETKHAU"]));
                        updateCTHDNCommand.Parameters.AddWithValue("@ThanhTien", Convert.ToDouble(row["THANHTIEN"]));
                        updateCTHDNCommand.Parameters.AddWithValue("@TienThue", Convert.ToDouble(row["TIENTHUE"]));

                        updateCTHDNCommand.Parameters.AddWithValue("@MaHDN", lblMAHDN.Text);
                        updateCTHDNCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());
                        updateCTHDNCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã cập nhật đơn nhập hàng thành công!", "Thông báo");
                conn.Close();
                MAHDN = lblMAHDN.Text; // Cập nhật lại giá trị của MAHDN sau khi tạo mới hóa đơn

                ResetForm();
            }
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
            // ví dụ: gán lại giá trị cho lblMAHDN, lblNGAYNHAP, ...
            // ...
            // Lấy ngày hiện tại
            DateTime ngayNhapHienTai = DateTime.Now;

            // Gán ngày hiện tại vào thuộc tính "Text" của Label lblNGAYNHAP
            lblNGAYNHAP.Text = ngayNhapHienTai.ToString("dd/MM/yyyy HH:mm:ss");


            //doan chuong trinh can bay loi
            //3 dong dau dùng để thiet lap den CSDL QLBDS 
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
        


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["STT"] = i + 1;
            }
            lblMAHDN.Text =  "HDN" + (int.Parse(MAHDN.Substring(3)) + 1).ToString("D3");
            lblTENNV.Text = TenNhanVien;

            comNCC.SelectedIndexChanged += new EventHandler(comNCC_SelectedIndexChanged);

            sql = "select MANCC,TENNCC, DIACHI from DMNHACC";
            da = new SqlDataAdapter(sql, conn);
            com2dt.Clear();
            da.Fill(com2dt);
            comNCC.DataSource = com2dt;
            comNCC.DisplayMember = "TENNCC";

            grdData.CellValueChanged += new DataGridViewCellEventHandler(grdData_CellValueChanged);

            conn.Close();
            // Cập nhật trạng thái của nút btnNew


            lblDONGIAXSL.Text = "0";
            txtDATRA.Clear();
            txtCHIETKHAU.Clear();
            lblCONPHAITRA.Text = "0";
            TONGTIEN.Text = "0";
            btnNew.Enabled = false; // Vì danh sách SL đang rỗng
            btnListPD.Visible = false;
            btnAdd.Visible = true;
            anh.Visible = true;
            grdData.Visible = false;
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
                //grdData.CurrentCell = grdData.Rows[0].Cells["SL"];
                //grdData.BeginEdit(true);
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnback_Click_1(object sender, EventArgs e)
        {
        
            this.Close();

            // Tạo lại FrmHDBAN
            FrmHDNHAP frmHDNHAP = new FrmHDNHAP(f);
            frmHDNHAP.TopLevel = false;
            frmHDNHAP.FormBorderStyle = FormBorderStyle.None;
            frmHDNHAP.Dock = DockStyle.Fill;

            // Truy cập đến panel 'panelbody' của FrmMAIN và thay thế FrmCTHDN bằng FrmHDNHAP
            FrmMAIN mainForm = Application.OpenForms.OfType<FrmMAIN>().FirstOrDefault();
            if (mainForm != null)
            {
                Guna.UI2.WinForms.Guna2Panel panelBody = mainForm.PanelBody;
                panelBody.Controls.Clear();
                panelBody.Controls.Add(frmHDNHAP);
                frmHDNHAP.Show();
            }
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnADDNCC_Click(object sender, EventArgs e)
        {
            FrmNCC frmNCC = new FrmNCC();
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            SqlConnection conn = new SqlConnection(constr);

            conn.Open();

            string lastMANCCQuery = "SELECT TOP 1 MANCC FROM DMNCC ORDER BY MANCC DESC";
            SqlCommand lastMANCCCmd = new SqlCommand(lastMANCCQuery, conn);
            object lastMANCCResult = lastMANCCCmd.ExecuteScalar();

            if (lastMANCCResult != null)
            {
                string lastMANCC = lastMANCCResult.ToString();
                frmNCC.currentMANCC = int.Parse(lastMANCC.Substring(3)) + 1;
            }

            frmNCC.btnNew_Click(sender, e);
        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

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

            UpdateTongTien();
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
            btnNew.Enabled = dt.Rows.Count > 0 && allSLGreaterThanZero;
            //   grdData.Columns["MAHH"].Visible = false;
            bool daTraValid = double.TryParse(txtDATRA.Text, out double daTra);
            if (daTraValid )
            {
                btnNew.Enabled = true;
                double conPhaiTra = phaiTra-daTra;
                lblCONPHAITRA.Text = conPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
            }
            else
            {
                btnNew.Enabled = false;
                if (!daTraValid )
                {
                    lblCONPHAITRA.Text = "Gía trị nhập không hợp lệ";
                }
            }
        }

        private void txtDATRA_TextChanged(object sender, EventArgs e)
        {

            bool allSLGreaterThanZero = IsAllSLGreaterThanZero();
            bool daTraValid = double.TryParse(txtDATRA.Text, out double daTra);

            if (daTraValid && allSLGreaterThanZero)
            {
       
                 if (daTra > phaiTra)
                {

                    btnNew.Enabled = false;

                    lblCONPHAITRA.Text = "Số tiền vượt quá";



                }
                else
                {                 btnNew.Enabled = true;

                       double conPhaiTra = phaiTra- daTra;
                    lblCONPHAITRA.Text = conPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
                }
            }
            else
            {
                btnNew.Enabled = false;
                if (!daTraValid)
                {

                    lblCONPHAITRA.Text = "Giá trị nhập không hợp lệ";

                }
                else
                {
                    lblCONPHAITRA.Text = "Số lượng sản phẩm không hợp lệ";
                }
            }
        }

        private void comNhom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void FrmNHAP_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
                // Lấy ngày hiện tại
                DateTime ngayNhapHienTai = DateTime.Now;

                // Gán ngày hiện tại vào thuộc tính "Text" của Label lblNGAYNHAP
                lblNGAYNHAP.Text = ngayNhapHienTai.ToString("dd/MM/yyyy HH:mm:ss");


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
                lblMAHDN.Text = MAHDN;// $"HD{currentMAHDN:000}"; 
               lblTENNV.Text = TenNhanVien;
               
                comNCC.SelectedIndexChanged += new EventHandler(comNCC_SelectedIndexChanged);

                sql = "select MANCC,TENNCC, DIACHI from DMNHACC";
                da = new SqlDataAdapter(sql, conn);
                com2dt.Clear();
                da.Fill(com2dt);
                comNCC.DataSource = com2dt;
                comNCC.DisplayMember = "TENNCC";

                grdData.CellValueChanged += new DataGridViewCellEventHandler(grdData_CellValueChanged);
               // grdData.Columns["MAHH"].Visible = false;
                conn.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }
    }
}
