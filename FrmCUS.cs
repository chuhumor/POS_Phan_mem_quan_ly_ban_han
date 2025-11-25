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
    public partial class FrmCUS : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();

       public int currentMAKH = 1;

        string sql, constr;
        int i;
        Boolean addnewflag = false;
        public bool ShouldOpenFrmCTCUS { get; private set; } = false;

        public FrmCUS()
        {
            InitializeComponent();
            //grdData.CellClick += grdData_CellClick;

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
                    string sql = "SELECT MAKH, TENKH,LOAIKH, SDT, TONGCHITIEU, TONGSLDH FROM DMKH " +
                    "WHERE DMKH.TRANGTHAI =1 AND MAKH != 'ANONYMOUS' "+
                    "order by MAKH ASC";
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
        private void grdData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == grdData.Columns["LOAIKH"].Index && e.Value != null)
            {
                int loaiKhachHang = Convert.ToInt32(e.Value);
                if (loaiKhachHang == 0)
                {
                    e.Value = "Bán lẻ";
                }
                else if (loaiKhachHang == 1)
                {
                    e.Value = "Bán buôn";
                }
            }
        }

        private void FrmCUS_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();

                string lastMAKHQuery = "SELECT TOP 1 MAKH FROM DMKH ORDER BY MAKH DESC";
                SqlCommand lastMAKHCmd = new SqlCommand(lastMAKHQuery, conn);
                object lastMAKHResult = lastMAKHCmd.ExecuteScalar();

                if (lastMAKHResult != null)
                {
                    string lastMAKH = lastMAKHResult.ToString();
                    currentMAKH = int.Parse(lastMAKH.Substring(2)) + 1;
                }
                else
                {
                    currentMAKH = 1; // Nếu không có dữ liệu thì giá trị khởi tạo là 1
                }
                sql = "SELECT MAKH, TENKH,LOAIKH, SDT, TONGCHITIEU, TONGSLDH FROM DMKH " +
                    "WHERE DMKH.TRANGTHAI =1 AND MAKH != 'ANONYMOUS' "+
                    "order by MAKH ASC";
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                dt.Clear();

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

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

                { //grdData1.Columns["STT"].DisplayIndex = 0;

                    grdData.CurrentCell = grdData[0, 9];

                }
                grdData.CellFormatting += new DataGridViewCellFormattingEventHandler(grdData_CellFormatting);

                conn.Close();


                //sql = "select LOAIKH from DMKH";
                //da = new SqlDataAdapter(sql, conn);
                //com2dt.Clear();
                //da.Fill(com2dt);
                //comNhom.DataSource = com2dt;
                //comNhom.DisplayMember = "LOAIKH";
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

                string sql = $"SELECT MAKH, TENKH,LOAIKH, SDT, TONGCHITIEU, TONGSLDH FROM DMKH";

                if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filter} LIKE N'%{keyword}%' AND TRANGTHAI =1 AND  MAKH != 'ANONYMOUS' ";
                }
                sql += " ORDER BY MAKH ASC";
              //  conn.Open();
                da.SelectCommand = new SqlCommand(sql, conn);
                dt.Clear();
                da.Fill(dt);
                // Cập nhật giá trị cho cột STT

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
                // MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grdDataCUS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            FrmCTCUS frmCTCUS = new FrmCTCUS(null, this);//

            frmCTCUS.txtTENKH.Focus();
            frmCTCUS.txtMAKH.ReadOnly = true;
            frmCTCUS.txtTENKH.ReadOnly = false;
            frmCTCUS.txtSDT.ReadOnly = false;
            frmCTCUS.txtEMAIL.ReadOnly = false;
            frmCTCUS.comNhom.Enabled = true;
            frmCTCUS.txtDIACHI.ReadOnly = false;
            //   frmCTCUS.txtTONGCHITIEU.ReadOnly = false;
            // frmCTCUS.txtTONGSLDH.ReadOnly = false;

            frmCTCUS.txtMAKH.Enabled = false;
            frmCTCUS.txtTENKH.Enabled = true;
            frmCTCUS.txtSDT.Enabled = true;
            frmCTCUS.txtEMAIL.Enabled = true;
            frmCTCUS.txtDIACHI.Enabled = true;
            // frmCTCUS.txtTONGCHITIEU.Enabled = true;
            //frmCTCUS.txtTONGSLDH.Enabled = true;


            foreach (Control control in frmCTCUS.guna2Panel2.Controls)
            {
                if (control is Label)
                {
                    control.Hide();
                }
            }
              frmCTCUS.Hide();

            frmCTCUS.Size = new Size(632, 433);

            frmCTCUS.btncapnhat.Visible = false;
            frmCTCUS.btnluu.Visible = true;



            //  FrmADDNV frmADDNV = new FrmADDNV(f,this);




            frmCTCUS.MAKH = "KH" + currentMAKH.ToString("D3");//  $"HD{currentMAHDN:000}"; // Truyền giá trị của currentMAHDN tăng lên 1 cho FrmNHAP
                                                              //  frmCTEMPLOY.txtMANV.Text = "NV" + currentMANV.ToString("D2");

            frmCTCUS.txtMAKH.Text = frmCTCUS.MAKH;
            ShouldOpenFrmCTCUS = true;

            frmCTCUS.ShowDialog();
            //  RefreshDataGrid();
            //  UpdateCurrentMANV();
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();



                // Tạo câu truy vấn SQL để lấy dữ liệu
                string queryNV = "SELECT MAKH, TENKH,LOAIKH, SDT,EMAIL, DIACHI, TONGCHITIEU, TONGSLDH FROM DMKH " +
                    "WHERE DMKH.TRANGTHAI =1 AND MAKH != 'ANONYMOUS' " +
                    "order by MAKH ASC";

                using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            // Đổ dữ liệu từ CSDL vào DataSet
                            da.Fill(ds, "dtDSKH");

                            // Tạo đối tượng báo cáo
                            rptDSKH r = new rptDSKH();

                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                            // Hiển thị báo cáo
                            FrmRPTDSKH f = new FrmRPTDSKH();
                            f.crystalReportViewer1.ReportSource = r;
                            f.ShowDialog();
                        }
                    }
                }

            }
        }

        private void grdDataCUS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdData.Columns["colDel"].Index && e.RowIndex >= 0)
            {
                string maKH = grdData.Rows[e.RowIndex].Cells["MAKH"].Value.ToString();

                if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa bản ghi tương ứng trong CSDL
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string deleteQuery1 = "UPDATE DMKH SET TRANGTHAI = 0 WHERE MAKH = @MaKH";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery1, conn))
                        {
                            deleteCommand.Parameters.AddWithValue("@MaKH", maKH);
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

                    //UpdateCurrentMANV();
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
                    string maKH = selectedRow.Cells["MAKH"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTCUS frmCTCUS = new FrmCTCUS(maKH, this);




                    string initialComNhomValue = frmCTCUS.comNhom.Text;

                    frmCTCUS.txtMAKH.Focus();
                    frmCTCUS.txtMAKH.ReadOnly = true;
                    frmCTCUS.txtTENKH.ReadOnly = false;
                    frmCTCUS.txtSDT.ReadOnly = false;
                    frmCTCUS.txtEMAIL.ReadOnly = false;
                    frmCTCUS.txtDIACHI.ReadOnly = false;
                    //    frmCTCUS.txtTONGCHITIEU.ReadOnly = false;
                    //   frmCTCUS.txtTONGSLDH.ReadOnly = false;
                    // frmCTCUS.Hide();
                    frmCTCUS.comNhom.Enabled = true;



                    frmCTCUS.txtMAKH.Enabled = true;
                    frmCTCUS.txtTENKH.Enabled = true;
                    frmCTCUS.txtSDT.Enabled = true;
                    frmCTCUS.txtEMAIL.Enabled = true;
                    frmCTCUS.txtDIACHI.Enabled = true;
                    //   frmCTCUS.txtTONGCHITIEU.Enabled = true;
                    //frmCTCUS.txtTONGSLDH.Enabled = true;


                    frmCTCUS.btncapnhat.Visible = true;
                    frmCTCUS.btnluu.Visible = false;

                    frmCTCUS.comNhom.Text = initialComNhomValue;
                    foreach (Control control in frmCTCUS.guna2Panel2.Controls)
                    {
                        if (control is Label)
                        {
                            control.Hide();
                        }
                    }
                    frmCTCUS.Size = new Size(632, 433);
                    frmCTCUS.ShowDialog();

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
                    string maKH = selectedRow.Cells["MAKH"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTCUS frmCTCUS = new FrmCTCUS(maKH, this);
                    frmCTCUS.btncapnhat.Visible = false;
                    frmCTCUS.btnluu.Visible = false;


                    frmCTCUS.txtMAKH.Focus();
                    frmCTCUS.txtMAKH.ReadOnly = true;
                    frmCTCUS.txtTENKH.ReadOnly = true;
                    frmCTCUS.txtSDT.ReadOnly = true;
                    frmCTCUS.txtEMAIL.ReadOnly = true;
                    frmCTCUS.txtDIACHI.ReadOnly = true;
                    //   frmCTCUS.lblTONGCHITIEU.ReadOnly = true;
                    // frmCTCUS.lblTONGSLDH.ReadOnly = true;

                    frmCTCUS.comNhom.Enabled = false;



                    frmCTCUS.txtMAKH.Enabled = false;
                    frmCTCUS.txtTENKH.Enabled = false;
                    frmCTCUS.txtSDT.Enabled = false;
                    frmCTCUS.txtEMAIL.Enabled = false;
                    frmCTCUS.txtDIACHI.Enabled = false;


                    //sql = "SELECT CHUCVU FROM DMNV where MANV = ";
                    //da = new SqlDataAdapter(sql, conn);
                    //com2dt.Clear();
                    //da.Fill(com2dt);
                    //frmCTEMPLOY.comNhom.DataSource = com2dt;
                    //frmCTEMPLOY.comNhom.DisplayMember = "CHUCVU";
                    //frmCTEMPLOY.comNhom.ValueMember = "MANV";
                    frmCTCUS.ShowDialog();

                }
            }
        }

       
    }
}
