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
    public partial class FrmHDBAN : Form

    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
       // SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        //  DataTable comdt = new DataTable();
        //DataTable com2dt = new DataTable();
        public string TenNhanVien { get; set; } = "";
        public string MaNhanVien { get; set; } = "";
        public string ChucVu { get; set; } = "";

        FrmLOGIN f;
        public bool ShouldOpenFrmBAN { get; private set; } = false;
        string sql, constr;
        int i;
        Boolean addnewflag = false;
        int currentMAHDB = 1;
        public FrmHDBAN(FrmLOGIN frm)
        {
            InitializeComponent();
            grdData.CellClick += grdData_CellClick;
            f = frm;
        }
        private void grdData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột hiện tại có phải là cột "NGAYBAN" không
            if (grdData.Columns[e.ColumnIndex].Name == "NGAYBAN")
            {
                // Kiểm tra xem giá trị của ô hiện tại có thể chuyển đổi thành DateTime không
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime ngayBan))
                {
                    // Định dạng lại giá trị của ô theo định dạng "dd/MM/yyyy HH:mm:ss"
                    e.Value = ngayBan.ToString("dd/MM/yyyy HH:mm:ss");
                    e.FormattingApplied = true;
                }
            }
        }

        private void FrmHDBAN_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
              
                grdData.CellFormatting += new DataGridViewCellFormattingEventHandler(grdData_CellFormatting);

                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
                sql = "SELECT MAHDB, TENKH,NGAYBAN,TENNV, TONGTIEN,DMHDB.SL FROM DMHDB " +
                    "LEFT JOIN DMKH ON DMKH.MAKH = DMHDB.MAKH " +
                    "LEFT JOIN DMNV ON DMNV.MANV = DMHDB.MANV" +
                    " ORDER BY MAHDB DESC";
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
                //    grdData.Rows[0].Selected = false;
                //    grdData.ClearSelection();
                   // grdData.CurrentCell = null;
                    string lastMAHDB = dt.Rows[0]["MAHDB"].ToString();
                    currentMAHDB = int.Parse(lastMAHDB.Substring(3)) + 1;
                }
                else
                {
                    currentMAHDB = 1; // Nếu không có hóa đơn nào thì giá trị khởi tạo là 1
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
                        filterColumnName = "MAHDB";
                        break;
                    case "Tên khách hàng":
                        filterColumnName = "TENKH";
                        break;
                    case "Ngày bán":
                        filterColumnName = "NGAYBAN";
                        break;
                    case "Tên nhân viên":
                        filterColumnName = "TENNV";
                        break;
                   
                    default:
                        break;
                }

                string sql = $"SELECT MAHDB, TENKH, NGAYBAN, TENNV, TONGTIEN, DMHDB.SL FROM DMHDB " +
                                    "LEFT JOIN DMKH ON DMKH.MAKH = DMHDB.MAKH " +
                                    "LEFT JOIN DMNV ON DMNV.MANV = DMHDB.MANV";

                if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filterColumnName} LIKE N'%{keyword}%'";
                }

                sql += " ORDER BY MAHDB DESC";

                da.SelectCommand = new SqlCommand(sql, conn);
                dt.Clear();
                da.Fill(dt);

                if (!dt.Columns.Contains("STT"))
                {
                    dt.Columns.Add("STT", typeof(int));
                    grdData.Columns["STT"].Visible = false;
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

                    //try
                    //{

                        string sql = "SELECT MAHDB, TENKH, NGAYBAN,TENNV, TONGTIEN,DMHDB.SL FROM DMHDB " +
                        "LEFT JOIN DMKH ON DMKH.MAKH = DMHDB.MAKH " +
                        "LEFT JOIN DMNV ON DMNV.MANV = DMHDB.MANV" +
                        " ORDER BY MAHDB DESC";

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
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Error: " + ex.Message);
                    //}
         
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
                // Lấy mã hóa đơn bán từ DataGridView
                string maHDB = grdData.Rows[e.RowIndex].Cells["MAHDB"].Value.ToString();

                // Mở FrmCTHDB và truyền mã hóa đơn bán
                this.Close();
                FrmCTHDB f = new FrmCTHDB(maHDB);

                f.TopLevel = false;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Dock = DockStyle.Fill;

                // Truy cập đến panel 'panelbody' của FrmMAIN và thay thế FrmHDBAN bằng FrmCTHDB
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
      //  private Color originalRowColor; // Biến lưu màu gốc của dòng

        private void grdData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Đổi màu nền của dòng khi con trỏ chuột vào
                grdData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(226, 240, 217);
            }
        }

        private void grdData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Khôi phục màu nền gốc khi con trỏ chuột rời khỏi dòng
                grdData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnEDIT_Click(object sender, EventArgs e)
        {


            FrmXDSHD f = new FrmXDSHD();
            f.btnXUATB.Enabled = true;
            f.btnXUATN.Visible = false;
            f.ShowDialog();
           
        }

        private void btnTAODON_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmBAN frmBan = new FrmBAN(f);
            frmBan.TenNhanVien = TenNhanVien;
            frmBan.MaNhanVien = MaNhanVien;
            frmBan.ChucVu = ChucVu;
            frmBan.MAHDB = "HDB" + currentMAHDB.ToString("D3");//  $"HD{currentMAHDN:000}"; // Truyền giá trị của currentMAHDN tăng lên 1 cho FrmNHAP

            ShouldOpenFrmBAN = true;
            //  frmNhap.lblTENNV = TenNhanVien;
           
          
           
           
            frmBan.Show();
            this.Hide();

        }

       
    }
}
