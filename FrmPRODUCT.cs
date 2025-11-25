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
    public partial class FrmPRODUCT : Form

    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();

        //   DBconnect dbcon = new DBconnect();

        //  SqlDataReader dr;

        string sql, constr;
        int i;
        Boolean addnewflag = false;
        public FrmPRODUCT()
        {
            InitializeComponent();
           // grdData1.CellClick += grdData1_CellClick;

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
                    string sql = "SELECT MAHH, TENHH,TENNHOM, SL,GIABAN FROM DMHH" +
                    " LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM " +
                    "WHERE DMHH.TRANGTHAI =1 " +
                    "order by MAHH ASC";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    // DataTable dt = new DataTable();


                    if (!dt.Columns.Contains("STT"))
                    {
                        dt.Columns.Add("STT", typeof(int));
                        grdData1.Columns["STT"].Visible = false;

                    }
                    dt.Clear();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }

                   // Gán dữ liệu mới vào grdData
                    grdData1.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
     
        private void FrmPRODUCT_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
                sql = "SELECT MAHH, TENHH,TENNHOM, SL,GIABAN FROM DMHH" +
                    " LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM " +
                     "WHERE DMHH.TRANGTHAI =1 " +

                    "order by MAHH"; 
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

                // NapCT();
                grdData1.DataSource = dt;
                if (grdData1.Columns.Contains("STT"))
                {
                    grdData1.Columns["STT"].Visible = false;
                }
                if (dt.Rows.Count > 0)

                { //grdData1.Columns["STT"].DisplayIndex = 0;

                    grdData1.CurrentCell = grdData1[0, 9];
                  
                }
                //    conn.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }
     
   
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                    case "Mã hàng hóa":
                        filterColumnName = "MAHH";
                        break;
                    case "Tên hàng hóa":
                        filterColumnName = "TENHH";
                        break;
                    case "Tên nhóm":
                        filterColumnName = "TENNHOM";
                        break;


                    default:
                        break;
                }

                string sql = $"SELECT MAHH, TENHH, TENNHOM,SL ,GIABAN FROM DMHH" +
                    $" LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM";

                if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filterColumnName} LIKE N'%{keyword}%' AND DMHH.TRANGTHAI = 1";
                }

             

                da.SelectCommand = new SqlCommand(sql, conn);
                dt.Clear();
                da.Fill(dt);

                if (!dt.Columns.Contains("STT"))
                {
                    dt.Columns.Add("STT", typeof(int));
                    grdData1.Columns["STT"].Visible = false;

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
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

    
          private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
       {
            if (comFilter.SelectedItem != null)
            {
                if (comFilter.SelectedItem.ToString() == "Tất cả")
                {


                    string sql = "SELECT MAHH, TENHH, TENNHOM, SL, GIABAN FROM DMHH " +
                        "LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM " +
                    "WHERE DMHH.TRANGTHAI =1 ";


da = new SqlDataAdapter(sql, conn);
                    //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                    dt.Clear();
                    //xoa hết dữ liệu cũ đi    
                    da.Fill(dt);
                    // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                    if (!dt.Columns.Contains("STT"))
                    {
                        dt.Columns.Add("STT", typeof(int));
                        grdData1.Columns["STT"].Visible = false;

                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }
                    grdData1.DataSource = dt;
               
                }
                else
                {
                    FilterData();
                }
            }
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void comNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          //  NapCT();
        }
        private void grdData1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdData1.Columns["colDel"].Index && e.RowIndex >= 0)
            {
                string maHH = grdData1.Rows[e.RowIndex].Cells["MAHH"].Value.ToString();

                if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa bản ghi tương ứng trong CSDL
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string deleteQuery1 = "UPDATE DMHH SET TRANGTHAI = 0 WHERE MAHH = @MaHH";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery1, conn))
                        {
                            deleteCommand.Parameters.AddWithValue("@MaHH", maHH);
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
                    grdData1.DataSource = dt;
                    //grdData.Refresh();  // Cập nhật hiển thị

                    //UpdateCurrentMANV();
                }
            }

            else if (e.ColumnIndex == grdData1.Columns["colEdit"].Index && e.RowIndex >= 0)
            {

                // Lấy thông tin dòng được chọn
                int selectedRowIndex = e.RowIndex;
                if (selectedRowIndex >= 0)
                {
                    DataGridViewRow selectedRow = grdData1.Rows[selectedRowIndex];

                    // Lấy giá trị của cột MAHH từ dòng được chọn
                    string maHH = selectedRow.Cells["MAHH"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTPRODUCT f = new FrmCTPRODUCT(maHH, this);




                    string initialComNhomValue = f.comNhom.Text;
                    string initialComTHUEVAOValue = f.comNhom.Text;
                    string initialComTHUERAValue = f.comNhom.Text;

                    f.txtTENHH.Focus();
                    f.txtMAHH.ReadOnly = true;
                    f.txtTENHH.ReadOnly = false;
                    f.txtGIANHAP.ReadOnly = false;
                    f.txtGIABAN.ReadOnly = false;
                    f.txtDVT.ReadOnly = false;
                    f.txtBC.ReadOnly = false;

                    //    frmCTCUS.txtTONGCHITIEU.ReadOnly = false;
                    //   frmCTCUS.txtTONGSLDH.ReadOnly = false;
                    // frmCTCUS.Hide();
                    f.comNhom.Enabled = true;
                    f.comTHUEVAO.Enabled = true;
                    f.comTHUERA.Enabled = true;

                    f.txtSL.Enabled = true;
                    f.txtBC.Enabled = true;


                    f.txtMAHH.Enabled = true;
                    f.txtTENHH.Enabled = true;
                    f.txtGIANHAP.Enabled = true;
                    f.txtGIABAN.Enabled = true;
                    f.txtDVT.Enabled = true;
                    //   frmCTCUS.txtTONGCHITIEU.Enabled = true;
                    //frmCTCUS.txtTONGSLDH.Enabled = true;
              


                    f.btncapnhat.Visible = true;
                    f.btnluu.Visible = false;

                    f.comNhom.Text = initialComNhomValue;
                    f.comTHUEVAO.Text = initialComTHUEVAOValue;
                    f.comTHUERA.Text = initialComTHUERAValue;

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
            else if (e.RowIndex >= 0 && e.RowIndex < grdData1.Rows.Count - 1)
            {


                // Lấy thông tin dòng được chọn
                int selectedRowIndex = e.RowIndex;
                if (selectedRowIndex >= 0)
                {
                    DataGridViewRow selectedRow = grdData1.Rows[selectedRowIndex];

                    // Lấy giá trị của cột MAHH từ dòng được chọn
                    string maHH = selectedRow.Cells["MAHH"].Value.ToString();

                    // Tạo một instance của FrmCTPRODUCT và hiển thị thông tin chi tiết
                    FrmCTPRODUCT f = new FrmCTPRODUCT(maHH, this);
                    f.btncapnhat.Visible = false;
                    f.btnluu.Visible = false;


                   // f.txtMAHH.Focus();
                    f.txtMAHH.ReadOnly = true;
                    f.txtTENHH.ReadOnly = true;
                    f.txtGIANHAP.ReadOnly = true;
                    f.txtGIABAN.ReadOnly = true;
                    f.txtDVT.ReadOnly = true;
                    f.txtBC.ReadOnly = true;
                    //   frmCTCUS.lblTONGCHITIEU.ReadOnly = true;
                    // frmCTCUS.lblTONGSLDH.ReadOnly = true;

                    f.comNhom.Enabled = false;
                    f.comTHUEVAO.Enabled = false;
                    f.comTHUERA.Enabled = false;
                    f.txtSL.Enabled = false;
                    f.txtBC.Enabled = false;
                    f.txtMAHH.Enabled = false;
                    f.txtTENHH.Enabled = false;
                    f.txtDVT.Enabled = false;
                    f.txtGIANHAP.Enabled = false;
                    f.txtGIABAN.Enabled = false;
          


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


        private void txtMAHH_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void grdData1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Đổi màu nền của dòng khi con trỏ chuột vào
                grdData1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(226, 240, 217);
            }
        }

        private void grdData1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Khôi phục màu nền gốc khi con trỏ chuột rời khỏi dòng
                grdData1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void grdData1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEDIT_Click(object sender, EventArgs e)
        {
            FrmXPRODUCT f = new FrmXPRODUCT();
            f.btnXUAT.Enabled = true;
            f.ShowDialog();
            
        }

        private void grdData1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmCTPRODUCT frmCTPRODUCT = new FrmCTPRODUCT(null,this);//
           
            frmCTPRODUCT.txtMAHH.Focus();
            frmCTPRODUCT.txtMAHH.ReadOnly = false;
            frmCTPRODUCT.txtGIANHAP.ReadOnly = false;
            frmCTPRODUCT.txtGIABAN.ReadOnly = false;
            frmCTPRODUCT.txtDVT.ReadOnly = false;
            frmCTPRODUCT.txtBC.ReadOnly = false;
            frmCTPRODUCT.comNhom.Enabled = true;
            frmCTPRODUCT.comTHUEVAO.Enabled = true;
            frmCTPRODUCT.comTHUERA.Enabled = true;
            frmCTPRODUCT.txtBC.Enabled = true;
            frmCTPRODUCT.txtSL.Enabled = true;
            frmCTPRODUCT.txtTENHH.ReadOnly = false;

            frmCTPRODUCT.txtMAHH.Enabled = true;
            frmCTPRODUCT.txtGIANHAP.Enabled = true;
            frmCTPRODUCT.txtGIABAN.Enabled = true;
            frmCTPRODUCT.txtDVT.Enabled = true;
            frmCTPRODUCT.txtTENHH.Enabled = true;

            //frmCTPRODUCT.changepic.Enabled = true;
            //frmCTPRODUCT.btncapnhat.Enabled = false;
            //frmCTPRODUCT.btnluu.Enabled = true;

            frmCTPRODUCT.btncapnhat.Visible = false;
            frmCTPRODUCT.btnluu.Visible = true;
            frmCTPRODUCT.changepic.Visible = true;
            frmCTPRODUCT.btnTHUE.Checked = false;
            //FrmCTPRODUCT frmCTPRODUCT = new FrmCTPRODUCT();
            //frmCTPRODUCT.Show();

            // Thay đổi cách bạn nạp dữ liệu cho comNhom sau khi nhấp vào nút "Sửa"
            //string sql = "SELECT MANHOM,TENNHOM FROM DMNHOMHH";
            //da = new SqlDataAdapter(sql, conn);
            //com2dt.Clear();
            //da.Fill(com2dt);
            //frmCTPRODUCT.comNhom.DataSource = com2dt;
            //frmCTPRODUCT.comNhom.DisplayMember = "TENNHOM";
            //frmCTPRODUCT.comNhom.ValueMember = "MANHOM";
            frmCTPRODUCT.ShowDialog();
        }
     

    }
}






