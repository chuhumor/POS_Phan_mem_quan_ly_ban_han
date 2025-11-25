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
    public partial class FrmEMPLOY : Form
    {

        

        // FrmLOGIN f;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
     //   public bool ShouldOpenFrmADDNV { get; private set; } = false;
       
        int currentMANV = 1;
        string sql, constr;
        int i;
        Boolean addnewflag = false;
        public bool ShouldOpenFrmCTEMPLOY { get; private set; } = false;

        public FrmEMPLOY()/*FrmLOGIN frm*/
        {
            InitializeComponent();
          //  grdData.CellClick += grdData_CellClick;
           // f = frm;
            // Di chuyển cột "colDel" vào cuối danh sách cột hiển thị trong DataGridView
         //  grdData.Columns["colDel"].DisplayIndex = grdData.Columns.Count - 1;

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
                    string sql = "SELECT MANV, TENNV,CHUCVU, SDT FROM DMNV " +
                    "WHERE DMNV.TRANGTHAI =1 " +
                    "order by MANV";
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
       
        private void FrmEMPLOY_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
               // grdData.Columns["colDel"].DisplayIndex = grdData.Columns.Count - 1;

                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();


                // Lấy lastMANV từ dữ liệu không có điều kiện TRANGTHAI
                string lastMANVQuery = "SELECT TOP 1 MANV FROM DMNV ORDER BY MANV DESC";
                SqlCommand lastMANVCmd = new SqlCommand(lastMANVQuery, conn);
                object lastMANVResult = lastMANVCmd.ExecuteScalar();

                if (lastMANVResult != null)
                {
                    string lastMANV = lastMANVResult.ToString();
                    currentMANV = int.Parse(lastMANV.Substring(2)) + 1;
                }
                else
                {
                    currentMANV = 1; // Nếu không có dữ liệu thì giá trị khởi tạo là 1
                }

                sql = "SELECT MANV, TENNV, CHUCVU, SDT FROM DMNV" +
                    " where TRANGTHAI =1" +

                     " ORDER BY MANV ASC";
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

                    grdData.CurrentCell = grdData[0, 3];

                }
                //if (dt.Rows.Count > 0)
                //{
                //    // string lastMANV = dt.Rows[0]["MANV"].ToString();
                //    string lastMANV = dt.Rows[dt.Rows.Count - 1]["MANV"].ToString();

                //    currentMANV = int.Parse(lastMANV.Substring(2)) + 1;
                //}
                //else
                //{
                //    currentMANV = 1; // Nếu không có hóa đơn nào thì giá trị khởi tạo là 1
                //}
                conn.Close(); 
            }
             
         catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }}
           
        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comFilter.SelectedItem != null)
            {
                if (comFilter.SelectedItem.ToString() == "Tất cả")
                {
                    try
                    {

                        string sql = "SELECT MANV, TENNV, CHUCVU, SDT FROM DMNV" +
                            " WHERE TRANGTHAI =1" +
                     " ORDER BY MANV ASC";
                        //string sql = "SELECT MANV,ANHNV, TENNV,CHUCVU,SDT, EMAIL, DIACHI FROM DMNV ";


                        da = new SqlDataAdapter(sql, conn);
                    //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                    dt.Clear();
                    //xoa hết dữ liệu cũ đi    
                    da.Fill(dt);
                    // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
                    //  grdDataEMP.DataSource = dt;
                    //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                    //    grdDataEMP.Refresh();
                    //làm mới lại ô lưới
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

       
        private void btnAdd_Click(object sender, EventArgs e)
        {


           
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
        
        }

     

        private void FilterData()
        {
            //try
            //{
            //    string filter = comFilter.SelectedItem?.ToString();
            //    string keyword = txtSearch.Text;

            //    string sql = $"SELECT MANV, TENNV, CHUCVU, SDT FROM DMNV";


            //    if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(keyword))
            //    {
            //        sql += $" WHERE {filter} LIKE N'%{keyword}%' AND WHERE TRANGTHAI =1";
            //    }
            //    sql += " ORDER BY MANV ASC";


            //   // conn.Open();
            //    da.SelectCommand = new SqlCommand(sql, conn);
            //    dt.Clear();
            //    da.Fill(dt);
            //    // Cập nhật giá trị cho cột STT
            //    if (!dt.Columns.Contains("STT"))
            //    {
            //        dt.Columns.Add("STT", typeof(int));
            //    }
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        dt.Rows[i]["STT"] = i + 1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex.Message);
            //}
            ////finally
            ////{
            ////    conn.Close();
            ////}
            ///
            try
            {
                string filter = comFilter.SelectedItem?.ToString();
                string keyword = txtSearch.Text;
                string filterColumnName = ""; // Biến này để lưu tên cột dựa trên SelectedItem

                // Dựa vào giá trị của filter để xác định cột cần tìm kiếm
                switch (filter)
                {
                    case "Mã nhân viên":
                        filterColumnName = "MANV";
                        break;
                    case "Tên nhân viên":
                        filterColumnName = "TENNV";
                        break;
                    case "Chức vụ":
                        filterColumnName = "CHUCVU";
                        break;
                    case "Số điện thoại":
                        filterColumnName = "SDT";
                        break;

                    default:
                        break;
                }

                string sql = $"SELECT MANV, TENNV, CHUCVU, SDT FROM DMNV";


                if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filterColumnName} LIKE N'%{keyword}%' AND WHERE TRANGTHAI =1";
                }
                sql += " ORDER BY MANV ASC";

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
       // private bool isEditMode = false;

        private void picNV_Click(object sender, EventArgs e)
        {
         
        }
       

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnback_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmCTEMPLOY frmCTEMPLOY = new FrmCTEMPLOY(null, this);//

            frmCTEMPLOY.txtMANV.Focus();
            frmCTEMPLOY.txtMANV.ReadOnly = true;
            frmCTEMPLOY.txtTENNV.ReadOnly = false;
            frmCTEMPLOY.txtSDT.ReadOnly = false;
            frmCTEMPLOY.txtEMAIL.ReadOnly = false;
            frmCTEMPLOY.comNhom.Enabled = true;
            frmCTEMPLOY.txtDIACHI.ReadOnly = false;
            frmCTEMPLOY.txtUSERNAME.ReadOnly = false;
            frmCTEMPLOY.txtPASSWORD.ReadOnly = false;

            frmCTEMPLOY.txtMANV.Enabled = false;
            frmCTEMPLOY.txtTENNV.Enabled = true;
            frmCTEMPLOY.txtSDT.Enabled = true;
            frmCTEMPLOY.txtEMAIL.Enabled = true;
            frmCTEMPLOY.txtDIACHI.Enabled = true;
            frmCTEMPLOY.txtUSERNAME.Enabled = true;
            frmCTEMPLOY.txtPASSWORD.Enabled = true;



            frmCTEMPLOY.btncapnhat.Visible = false;
            frmCTEMPLOY.btnluu.Visible = true;
            frmCTEMPLOY.changepic.Visible = true;
   


          //  FrmADDNV frmADDNV = new FrmADDNV(f,this);




           frmCTEMPLOY.MANV = "NV" + currentMANV.ToString("D2");//  $"HD{currentMAHDN:000}"; // Truyền giá trị của currentMAHDN tăng lên 1 cho FrmNHAP
                                                                //  frmCTEMPLOY.txtMANV.Text = "NV" + currentMANV.ToString("D2");

          frmCTEMPLOY.txtMANV.Text = frmCTEMPLOY.MANV;
            ShouldOpenFrmCTEMPLOY = true;
              
            frmCTEMPLOY.ShowDialog();
          //  RefreshDataGrid();
          //  UpdateCurrentMANV();

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

        private void grdData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEDIT_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();



                // Tạo câu truy vấn SQL để lấy dữ liệu
                string queryNV = "SELECT MANV, TENNV, CHUCVU, SDT, EMAIL, DIACHI FROM DMNV" +
                    " where TRANGTHAI =1" +

                     " ORDER BY MANV ASC";

                using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            // Đổ dữ liệu từ CSDL vào DataSet
                            da.Fill(ds, "dtDSNV");

                            // Tạo đối tượng báo cáo
                            rptDSNV r = new rptDSNV();

                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                            // Hiển thị báo cáo
                            FrmRPTDSNV f = new FrmRPTDSNV();
                            f.crystalReportViewer1.ReportSource = r;
                            f.ShowDialog();
                        }
                    }
                }

            }
        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdData.Columns["colDel"].Index && e.RowIndex >= 0)
            {
                string maNV = grdData.Rows[e.RowIndex].Cells["MANV"].Value.ToString();

                if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa bản ghi tương ứng trong CSDL
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string deleteQuery1 = "UPDATE DMTK SET TRANGTHAI = 0 WHERE MANV = @MaNV";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery1, conn))
                        {
                            deleteCommand.Parameters.AddWithValue("@MaNV", maNV);
                            deleteCommand.ExecuteNonQuery();
                        }
                        string deleteQuery2 = "UPDATE DMNV SET TRANGTHAI = 0 WHERE MANV = @MaNV";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery2, conn))
                        {
                            deleteCommand.Parameters.AddWithValue("@MaNV", maNV);
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
                    string maNV = selectedRow.Cells["MANV"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTEMPLOY f = new FrmCTEMPLOY(maNV, this);




                    string initialComNhomValue = f.comNhom.Text;

                    f.txtMANV.Focus();
                    f.txtMANV.ReadOnly = true;
                    f.txtTENNV.ReadOnly = false;
                    f.txtSDT.ReadOnly = false;
                    f.txtEMAIL.ReadOnly = false;
                    f.txtDIACHI.ReadOnly = false;
                    f.txtUSERNAME.ReadOnly = false;
                    f.txtPASSWORD.ReadOnly = false;

                    //    frmCTCUS.txtTONGCHITIEU.ReadOnly = false;
                    //   frmCTCUS.txtTONGSLDH.ReadOnly = false;
                    // frmCTCUS.Hide();
                    f.comNhom.Enabled = true;



                    f.txtMANV.Enabled = true;
                    f.txtTENNV.Enabled = true;
                    f.txtSDT.Enabled = true;
                    f.txtEMAIL.Enabled = true;
                    f.txtDIACHI.Enabled = true;
                    //   frmCTCUS.txtTONGCHITIEU.Enabled = true;
                    //frmCTCUS.txtTONGSLDH.Enabled = true;
                    f.txtUSERNAME.Enabled = true;
                    f.txtPASSWORD.Enabled = true;


                    f.btncapnhat.Visible = true;
                    f.btnluu.Visible = false;

                    f.comNhom.Text = initialComNhomValue;
                    //foreach (Control control in f.guna2Panel1.Controls)
                    //{
                    //    if (control is Label)
                    //    {
                    //        control.Hide();
                    //    }
                    //}
                    //f.Size = new Size(632, 433);
                    f.ShowDialog();

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
                    string maNV = selectedRow.Cells["MANV"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTEMPLOY f = new FrmCTEMPLOY(maNV, this);
                    f.btncapnhat.Visible = false;
                    f.btnluu.Visible = false;


                    f.txtMANV.Focus();
                    f.txtMANV.ReadOnly = true;
                    f.txtTENNV.ReadOnly = true;
                    f.txtSDT.ReadOnly = true;
                    f.txtEMAIL.ReadOnly = true;
                    f.txtDIACHI.ReadOnly = true;
                    f.txtUSERNAME.ReadOnly = true; 
                    f.txtPASSWORD.ReadOnly = true;
                    //   frmCTCUS.lblTONGCHITIEU.ReadOnly = true;
                    // frmCTCUS.lblTONGSLDH.ReadOnly = true;

                    f.comNhom.Enabled = false;



                    f.txtMANV.Enabled = false;
                    f.txtTENNV.Enabled = false;
                    f.txtSDT.Enabled = false;
                    f.txtEMAIL.Enabled = false;
                    f.txtDIACHI.Enabled = false;
                    f.txtUSERNAME.Enabled = false;
                    f.txtPASSWORD.Enabled = false;


                    //sql = "SELECT CHUCVU FROM DMNV where MANV = ";
                    //da = new SqlDataAdapter(sql, conn);
                    //com2dt.Clear();
                    //da.Fill(com2dt);
                    //frmCTEMPLOY.comNhom.DataSource = com2dt;
                    //frmCTEMPLOY.comNhom.DisplayMember = "CHUCVU";
                    //frmCTEMPLOY.comNhom.ValueMember = "MANV";
                    f.ShowDialog();

                }
            }
        }
  
      
    }
}
