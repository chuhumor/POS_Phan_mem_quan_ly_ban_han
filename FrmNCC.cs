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
using System.Globalization;

namespace QLBDS
{
    public partial class FrmNCC : Form
    {
        public int currentMANCC = 1;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();


        string sql, constr;
        int i;
        Boolean addnewflag = false;

        public FrmNCC()
        {
            InitializeComponent();
            grdData.CellClick += grdData_CellClick;

        }
        public void RefreshDataGrid()
        {
            // Đây là một ví dụ về cách cập nhật lại dữ liệu trong grdData
            try
            {
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = "SELECT MANCC, TENNCC, SDT, DIACHI FROM DMNHACC" +
                   
                    " WHERE TRANGTHAI =1 " +
                    "order by MANCC";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    // DataTable dt = new DataTable();


                    if (!dt.Columns.Contains("STT"))
                    {
                        dt.Columns.Add("STT", typeof(int));
                    }
                    dt.Clear();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }

                    // Gán dữ liệu mới vào grdData
                    grdData.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }
        private void FrmNCC_Load(object sender, EventArgs e)
        {
            try
            {
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
                string lastMANCCQuery = "SELECT TOP 1 MANCC FROM DMNHACC ORDER BY MANCC DESC";
                SqlCommand lastMANCCCmd = new SqlCommand(lastMANCCQuery, conn);
                object lastMANCCResult = lastMANCCCmd.ExecuteScalar();

                if (lastMANCCResult != null)
                {
                    string lastMANCC = lastMANCCResult.ToString();
                    currentMANCC = int.Parse(lastMANCC.Substring(3)) + 1;
                }
                else
                {
                    currentMANCC = 1; // Nếu không có dữ liệu thì giá trị khởi tạo là 1
                }
                sql = "SELECT MANCC, TENNCC, SDT, DIACHI FROM DMNHACC" +

                    " WHERE TRANGTHAI =1 " +
                    "order by MANCC";
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt


                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                dt.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
                grdData.DataSource = dt;
                if (grdData.Columns.Contains("STT"))
                {
                    grdData.Columns["STT"].Visible = false;
                }
                if (dt.Rows.Count > 0)

                {
                    grdData.CurrentCell = grdData[0, 7];
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

        private void label6_Click(object sender, EventArgs e)
        {

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
                    case "Mã nhà cung cấp":
                        filterColumnName = "MANCC";
                        break;
                    case "Tên nhà cung cấp":
                        filterColumnName = "TENNCC";
                        break;
                    case "Số điện thoại":
                        filterColumnName = "SDT";
                        break;
                    case "Địa chỉ":
                        filterColumnName = "DIACHI";
                        break;
                    default:
                        break;
                }

                string sql = $"SELECT MANCC, TENNCC, SDT, DAICHI FROM DMNHACC";


                if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filterColumnName} LIKE N'%{keyword}%' AND TRANGTHAI =1";
                }

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


                    string sql = "SELECT MANCC, TENNCC,SDT, DIACHI FROM DMNHACC" +

                    " WHERE TRANGTHAI =1 " +
                    "order by MANCC";


                    da = new SqlDataAdapter(sql, conn);
                    //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                    dt.Clear();
                    //xoa hết dữ liệu cũ đi    
                    da.Fill(dt);
                    // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                    if (!dt.Columns.Contains("STT"))
                    {
                        dt.Columns.Add("STT", typeof(int));
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }
                    grdData.DataSource = dt;

                }
                else
                {
                    FilterData();
                }
            }

        }
        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  NapCT();
        }
        public void btnNew_Click(object sender, EventArgs e)
        {
            FrmCTNCC frmCTNCC = new FrmCTNCC(null, this);//


            frmCTNCC.txtTENNCC.Focus();
            frmCTNCC.txtMANCC.ReadOnly = false;
            frmCTNCC.txtTENNCC.ReadOnly = false;
            frmCTNCC.txtSDT.ReadOnly = false;
            frmCTNCC.txtEMAIL.ReadOnly = false;
            frmCTNCC.txtDIACHI.ReadOnly = false;

            frmCTNCC.txtMANCC.Enabled = false;
            frmCTNCC.txtTENNCC.Enabled = true;
            frmCTNCC.txtSDT.Enabled = true;
            frmCTNCC.txtEMAIL.Enabled = true;
            frmCTNCC.txtDIACHI.Enabled = true;

        

            frmCTNCC.btncapnhat.Visible = false;
            frmCTNCC.btnluu.Visible = true;
            frmCTNCC.MANCC = "NCC" + currentMANCC.ToString("D2");//  $"HD{currentMAHDN:000}"; // Truyền giá trị của currentMAHDN tăng lên 1 cho FrmNHAP
                                                                 //  frmCTEMPLOY.txtMANV.Text = "NV" + currentMANV.ToString("D2");

            frmCTNCC.txtMANCC.Text = frmCTNCC.MANCC;
           // ShouldOpenFrmCTEMPLOY = true;
            frmCTNCC.ShowDialog();
        }

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

            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

              

                // Tạo câu truy vấn SQL để lấy dữ liệu
                string queryNCC = "SELECT MANCC, TENNCC, SDT,EMAIL, DIACHI FROM DMNHACC" +

                    " WHERE TRANGTHAI =1 " +
                    "order by MANCC";

                using (SqlCommand cmd = new SqlCommand(queryNCC, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            // Đổ dữ liệu từ CSDL vào DataSet
                            da.Fill(ds, "dtNCC");

                            // Tạo đối tượng báo cáo
                            rptDSNCC r = new rptDSNCC();

                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                            // Hiển thị báo cáo
                            FrmRPTDSNCC f = new FrmRPTDSNCC();
                            f.crystalReportViewer1.ReportSource = r;
                            f.ShowDialog();
                        }
                    }
                }

            }
        }
        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdData.Columns["colDel"].Index && e.RowIndex >= 0)
            {
                string maNCC = grdData.Rows[e.RowIndex].Cells["MANCC"].Value.ToString();

                if (MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa bản ghi tương ứng trong CSDL
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string deleteQuery1 = "UPDATE DMNHACC SET TRANGTHAI = 0 WHERE MANCC = @MaNCC";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery1, conn))
                        {
                            deleteCommand.Parameters.AddWithValue("@MaNCC", maNCC);
                            deleteCommand.ExecuteNonQuery();
                        }

                    }

                    // Xóa dòng khỏi DataTable
                    DataRow rowToDelete = dt.Rows[e.RowIndex];
                    dt.Rows.Remove(rowToDelete);

                    // Cập nhật lại số thứ tự (STT) trong bảng dữ liệu
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }

                    // Gán lại DataSource cho grdData
                    grdData.DataSource = dt;
                    //grdData.Refresh();  // Cập nhật hiển thị
                }
            }
            else if (e.ColumnIndex == grdData.Columns["colEdit"].Index && e.RowIndex >= 0)
            {

                // Lấy thông tin dòng được chọn
                int selectedRowIndex = e.RowIndex;
                if (selectedRowIndex >= 0)
                {
                    DataGridViewRow selectedRow = grdData.Rows[selectedRowIndex];

                    // Lấy giá trị của cột MAHH từ dòng được chọn
                    string maNCC = selectedRow.Cells["MANCC"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTNCC frmCTNCC = new FrmCTNCC(maNCC, this);






                    frmCTNCC.txtTENNCC.Focus();

                    frmCTNCC.txtMANCC.ReadOnly = false;
                    frmCTNCC.txtTENNCC.ReadOnly = false;
                    frmCTNCC.txtSDT.ReadOnly = false;
                    frmCTNCC.txtEMAIL.ReadOnly = false;
                    frmCTNCC.txtDIACHI.ReadOnly = false;

                    frmCTNCC.txtMANCC.Enabled = false;
                    frmCTNCC.txtTENNCC.Enabled = true;
                    frmCTNCC.txtEMAIL.Enabled = true;
                    frmCTNCC.txtSDT.Enabled = true;
                    frmCTNCC.txtDIACHI.Enabled = true;

                    frmCTNCC.btncapnhat.Visible = true;
                    frmCTNCC.btnluu.Visible = false;


                    frmCTNCC.ShowDialog();

                }
            }
            else if (e.RowIndex >= 0 && e.RowIndex < grdData.Rows.Count - 1)
            {


                // Lấy thông tin dòng được chọn
                int selectedRowIndex = e.RowIndex;
                if (selectedRowIndex >= 0)
                {
                    DataGridViewRow selectedRow = grdData.Rows[selectedRowIndex];

                    // Lấy giá trị của cột MAHH từ dòng được chọn
                    string maNCC = selectedRow.Cells["MANCC"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTNCC frmCTNCC = new FrmCTNCC(maNCC, this);
                    frmCTNCC.btncapnhat.Visible = false;
                    frmCTNCC.btnluu.Visible = false;

                    frmCTNCC.txtMANCC.ReadOnly = true;
                    frmCTNCC.txtTENNCC.ReadOnly = true;
                    frmCTNCC.txtSDT.ReadOnly = true;
                    frmCTNCC.txtEMAIL.ReadOnly = true;
                    frmCTNCC.txtDIACHI.ReadOnly = true;



                    frmCTNCC.txtMANCC.Enabled = false;
                    frmCTNCC.txtTENNCC.Enabled = false;
                    frmCTNCC.txtSDT.Enabled = false;
                    frmCTNCC.txtEMAIL.Enabled = false;
                    frmCTNCC.txtDIACHI.Enabled = false;

                    frmCTNCC.ShowDialog();

                }
            }
        }

        
    }
}
