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
    public partial class FrmHDNHAP : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        // SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        //  DataTable comdt = new DataTable();
        //DataTable com2dt = new DataTable();
        public string TenNhanVien { get; set; } = "";
        public string MaNhanVien { get; set; } = "";
        FrmLOGIN f;
        public bool ShouldOpenFrmNHAP { get; private set; } = false;


        string sql, constr;
        int i;
        int currentMAHDN =1;
        Boolean addnewflag = false;
        public FrmHDNHAP(FrmLOGIN frm)
        {
            InitializeComponent();
            grdData.CellClick += grdData_CellClick;
            f = frm;

        }
        private void grdData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột hiện tại có phải là cột "NGAYNHAP" không
            if (grdData.Columns[e.ColumnIndex].Name == "NGAYNHAP")
            {
                // Kiểm tra xem giá trị của ô hiện tại có thể chuyển đổi thành DateTime không
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime ngayNhap))
                {
                    // Định dạng lại giá trị của ô theo định dạng "dd/MM/yyyy HH:mm:ss"
                    e.Value = ngayNhap.ToString("dd/MM/yyyy HH:mm:ss");
                    e.FormattingApplied = true;
                }
            }
            if (grdData.Columns[e.ColumnIndex].Name == "TRANGTHAI")
            {
                // Kiểm tra giá trị của ô hiện tại và thiết lập giá trị và màu sắc tương ứng
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int trangThai))
                {
                    if (trangThai == 0)
                    {
                        e.Value = "Đang giao dịch";
                        e.CellStyle.ForeColor = Color.FromArgb(0225,190,0);
                        e.FormattingApplied = true;
                    }
                    else if (trangThai == 1)
                    {
                        e.Value = "Hoàn thành";
                        e.CellStyle.ForeColor = Color.FromArgb(0,176,80);
                        e.FormattingApplied = true;
                    }
                }
            }

        }

      
                private void FrmHDNHAP_Load(object sender, EventArgs e)
        {                grdData.CellFormatting += new DataGridViewCellFormattingEventHandler(grdData_CellFormatting);

            //Bay loi
            try
            {
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
                sql = "SELECT MAHDN,NGAYNHAP,TENNCC,TENNV, TONGTIEN, DMHDN.SL, DMHDN.TRANGTHAI FROM DMHDN " +
                    "LEFT JOIN DMNHACC ON DMNHACC.MANCC = DMHDN.MANCC " +
                    "LEFT JOIN DMNV ON DMNV.MANV = DMHDN.MANV" +
                    " ORDER BY MAHDN DESC";
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

                dt.Clear();

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                dt.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
                //  NapCT();

                grdData.DataSource = dt;
                if (grdData.Columns.Contains("STT"))
                {
                    grdData.Columns["STT"].Visible = false;
                }
                if (dt.Rows.Count > 0)
                {
                    grdData.CurrentCell = grdData[0, 9];
                    string lastMAHDN = dt.Rows[0]["MAHDN"].ToString();
                    currentMAHDN = int.Parse(lastMAHDN.Substring(3)) + 1;
                }
                else
                {
                    currentMAHDN = 1; // Nếu không có hóa đơn nào thì giá trị khởi tạo là 1
                }



            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterData();

        }
        private void FilterData()
        {
            
            try
            {
                string filter = comFilter.SelectedItem?.ToString();
                string keyword = txtSearch.Text;
                string filterColumnName = ""; // Biến này để lưu tên cột dựa trên SelectedItem

                // Dựa vào giá trị của filter để xác định cột cần tìm kiếm
                switch (filter)
                {
                    case "Mã hóa đơn":
                        filterColumnName = "MAHDN";
                        break;
                    case "Tên nhà cung cấp":
                        filterColumnName = "TENNCC";
                        break;
                    case "Ngày nhập":
                        filterColumnName = "NGAYNHAP";
                        break;
                    case "Nhân viên tạo":
                        filterColumnName = "TENNV";
                        break;

                    default:
                        break;
                }

                string sql = $"SELECT MAHDN,NGAYNHAP,TENNCC,TENNV, TONGTIEN,DMHDN.SL, DMHDN.TRANGTHAI FROM DMHDN " +
                        "LEFT JOIN DMNHACC ON DMNHACC.MANCC = DMHDN.MANCC " +
                        "LEFT JOIN DMNV ON DMNV.MANV = DMHDN.MANV";

                if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filterColumnName} LIKE N'%{keyword}%'";
                }

                sql += " ORDER BY MAHDN DESC";

                da.SelectCommand = new SqlCommand(sql, conn);
                dt.Clear();
                da.Fill(dt);

                if (!dt.Columns.Contains("STT"))
                {
                    dt.Columns.Add("STT", typeof(int));
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comFilter.SelectedItem != null)
            {
                if (comFilter.SelectedItem.ToString() == "Tất cả")
                {

                    try
                    {

                        string sql = "SELECT MAHDN,NGAYNHAP,TENNCC,TENNV, TONGTIEN,DMHDN.SL, DMHDN.TRANGTHAI FROM DMHDN " +
                    "LEFT JOIN DMNHACC ON DMNHACC.MANCC = DMHDN.MANCC " +
                    "LEFT JOIN DMNV ON DMNV.MANV = DMHDN.MANV" +
                    " ORDER BY MAHDN DESC";

                        da = new SqlDataAdapter(sql, conn);
                        //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                        dt.Clear();
                        //xoa hết dữ liệu cũ đi    
                        da.Fill(dt);
                        // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                        // Kiểm tra xem cột 'STT' đã tồn tại trong DataTable dt chưa trước khi thêm
                        if (!dt.Columns.Contains("STT"))
                        {
                            dt.Columns.Add("STT", typeof(int));
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["STT"] = i + 1;
                        }


                        grdData.DataSource = dt;
                        //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }



                }
                else
                {
                    FilterData();
                }
            }
        }
        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdData.Rows.Count - 1)
            {
                //if (grdData.Columns[e.ColumnIndex].Name == "TRANGTHAI")
                //{
                  
                //        if (grdData.Rows[e.RowIndex].Cells["TRANGTHAI"].Value.ToString() == "Đang giao dịch")
                //        {
                          
                //            e.ForeColor = Color.FromArgb(0225, 190, 0);
                //            e.FormattingApplied = true;
                //        }
                //        else if (grdData.Rows[e.RowIndex].Cells["TRANGTHAI"].Value.ToString() == "Hoàn thành")
                //        {
                //            e.CellStyle.ForeColor = Color.FromArgb(0, 176, 80);
                //            e.FormattingApplied = true;
                //        }
                    
                //}

                // Lấy mã hóa đơn nhập từ DataGridView
                string maHDN = grdData.Rows[e.RowIndex].Cells["MAHDN"].Value.ToString();

                // Mở FrmCTHDN và truyền mã hóa đơn nhập
                this.Close();
                FrmCTHDN f = new FrmCTHDN(maHDN);

                f.TopLevel = false;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Dock = DockStyle.Fill;

                // Truy cập đến panel 'panelbody' của FrmMAIN và thay thế FrmHDNHAP bằng FrmCTHDN
                FrmMAIN mainForm = Application.OpenForms.OfType<FrmMAIN>().FirstOrDefault();
                if (mainForm != null)
                {
                    Guna.UI2.WinForms.Guna2Panel panelBody = mainForm.PanelBody;
                    panelBody.Controls.Clear();
                    panelBody.Controls.Add(f);
                    f.Show();
                }

            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               // grdData.CellFormatting += grdData_CellFormatting;

                // Đổi màu nền của dòng khi con trỏ chuột vào
                grdData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(226, 240, 217);
            }
        }

        private void grdData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               // grdData.CellFormatting += grdData_CellFormatting;

                // Khôi phục màu nền gốc khi con trỏ chuột rời khỏi dòng
                grdData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdData_CellMouseEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Đổi màu nền của dòng khi con trỏ chuột vào
                grdData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(226, 240, 217);
            }
        }

        private void grdData_CellMouseLeave_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Khôi phục màu nền gốc khi con trỏ chuột rời khỏi dòng
                grdData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEDIT_Click(object sender, EventArgs e)
        {
            FrmXDSHD f = new FrmXDSHD();
            f.btnXUATN.Enabled = true;
            f.btnXUATB.Visible = false;
            f.ShowDialog();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmMAIN mainForm = Application.OpenForms.OfType<FrmMAIN>().FirstOrDefault();
            if (mainForm != null)
            {
                Guna.UI2.WinForms.Guna2Panel panelBody = mainForm.PanelBody;
                // Bây giờ bạn có thể thao tác với PanelBody như thay đổi các control trong Panel này.
                // Ví dụ:
                panelBody.Controls.Clear(); // Xóa tất cả các control hiện có trong PanelBody
                FrmNHAP frmNhap = new FrmNHAP();
                frmNhap.TenNhanVien = TenNhanVien;
                frmNhap.MaNhanVien = MaNhanVien;

                frmNhap.MAHDN = "HDN" + currentMAHDN.ToString("D3");//  $"HD{currentMAHDN:000}"; // Truyền giá trị của currentMAHDN tăng lên 1 cho FrmNHAP

                ShouldOpenFrmNHAP = true;
              //  frmNhap.lblTENNV = TenNhanVien;
             frmNhap.TopLevel = false;
                frmNhap.FormBorderStyle = FormBorderStyle.None;
                frmNhap.Dock = DockStyle.Fill;
                panelBody.Controls.Add(frmNhap);
                frmNhap.Show();


            }
        }

    }
    }
