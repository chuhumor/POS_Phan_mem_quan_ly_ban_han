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
    public partial class FrmLISTPD : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        SqlDataReader dr;

        string sql, constr;
        int i;
        Boolean addnewflag = false;


        private FrmNHAP frmNHAP;
        private FrmBAN frmBAN;

        public FrmLISTPD(FrmNHAP nhapForm)
        {
            InitializeComponent();

            frmNHAP = nhapForm;
        }
        public FrmLISTPD(FrmBAN banForm)
        {
            InitializeComponent();

            frmBAN = banForm;
        }

        private void FrmLISTPD_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            //Bay loi
            try
            {
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
                sql = "SELECT MAHH, TENHH,GIANHAP,GIABAN, SL, DVT, ISNULL(THUEVAO,0) as THUEVAO, isnull(thuera,0) as THUERA FROM DMHH" +
                    " LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM " +
                    "where DMHH.TRANGTHAI =1 " +
                    "order by MAHH";
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                grdLIST1.DataSource = dt;
                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                dt.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
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

                string sql = $"SELECT MAHH, TENHH, TENNHOM, GIANHAP,GIABAN,SL,DVT,ISNULL(THUEVAO,0) AS THUEVAO, isnull(thuera,0) as THUERA FROM DMHH" +
                    $" LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM";

                if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filter} LIKE N'%{keyword}%' AND where DMHH.TRANGTHAI = 1";
                }

                conn.Open();
                da.SelectCommand = new SqlCommand(sql, conn);
                dt.Clear();
                da.Fill(dt);
                // Cập nhật giá trị cho cột STT

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

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
         
        }
        
        private void grdLIST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private Dictionary<int, bool> clickedRows = new Dictionary<int, bool>();

        private void grdLIST1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdLIST1.Rows.Count && e.ColumnIndex >= 0)
            {
              
                string tenHH = grdLIST1.Rows[e.RowIndex].Cells["TENHH"].Value.ToString();
                
                string maHH = grdLIST1.Rows[e.RowIndex].Cells["MAHH"].Value.ToString();
                string giaBan = grdLIST1.Rows[e.RowIndex].Cells["GIABAN"].Value.ToString();
                string giaNhap = grdLIST1.Rows[e.RowIndex].Cells["GIANHAP"].Value.ToString();
                string dvt = grdLIST1.Rows[e.RowIndex].Cells["DVT"].Value.ToString();
                string thuevaostring = grdLIST1.Rows[e.RowIndex].Cells["THUEVAO"].Value.ToString();
                string thuerastring = grdLIST1.Rows[e.RowIndex].Cells["THUERA"].Value.ToString();

                MessageBox.Show("Đã thêm sản phẩm");
                clickedRows[e.RowIndex] = true;

                FrmNHAP frmNHAP = Application.OpenForms.OfType<FrmNHAP>().FirstOrDefault();
                if (frmNHAP != null)
                {
                    frmNHAP.AddRowToGrdData(tenHH, giaNhap, maHH,dvt, thuerastring);
                }
                FrmBAN frmBAN = Application.OpenForms.OfType<FrmBAN>().FirstOrDefault();
                if (frmBAN != null)
                {
                    frmBAN.AddRowToGrdData(tenHH, giaBan, maHH,dvt,thuevaostring);
                }
               
            }
        }

        private void grdLIST1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comFilter.SelectedItem != null)
            {
                if (comFilter.SelectedItem.ToString() == "Tất cả")
                {


                    string sql = "SELECT MAHH, TENHH, TENNHOM, GIANHAP,GIABAN, SL, DVT,ISNULL(THUEVAO,0) AS THUEVAO, isnull(thuera,0) as THUERA FROM DMHH " +
                        "LEFT JOIN DMNHOMHH ON DMNHOMHH.MANHOM = DMHH.MANHOM "+
                    "where DMHH.TRANGTHAI =1";
                  da = new SqlDataAdapter(sql, conn);
                    //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                    dt.Clear();
                    //xoa hết dữ liệu cũ đi    
                    da.Fill(dt);
                    // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
                    grdLIST1.DataSource = dt;
                    //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                    grdLIST1.Refresh();
                    //làm mới lại ô lưới

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }


                
                }
                else
                {
                    FilterData();
                }
            }
        }
    }
}
