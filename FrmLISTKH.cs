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
    public partial class FrmLISTKH : Form
    {

        public string SelectedMaKH { get; private set; }
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
        private FrmBAN frmBAN;

        private void FrmLISTKH_Load(object sender, EventArgs e)
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
                sql = "SELECT MAKH, TENKH,SDT,EMAIL,DIACHI FROM DMKH" +
                   " WHERE TRANGTHAI =1 AND MAKH != 'ANONYMOUS'" +
                    " order by MAKH";
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                grdLISTKH.DataSource = dt;
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
      //  private Dictionary<int, bool> clickedRows = new Dictionary<int, bool>();

        private void grdLISTKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdLISTKH.Rows.Count && e.ColumnIndex >= 0)
            {


                SelectedMaKH = grdLISTKH.Rows[e.RowIndex].Cells["MAKH"].Value.ToString();
               
                MessageBox.Show("Đã thêm khách hàng " + grdLISTKH.Rows[e.RowIndex].Cells["TENKH"].Value.ToString());
                this.Close();
                frmBAN.panelKH.Visible = true;
                // clickedRows[e.RowIndex] = true;

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

                string sql = $"SELECT MAKH, TENKH, SDT, EMAIL, DIACHI FROM DMKH" +
                   " WHERE TRANGTHAI =1 AND MAKH != 'ANONYMOUS'" +
                    " order by MAKH";
                if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(keyword))
                {
                    sql += $" WHERE {filter} LIKE N'%{keyword}%'";
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

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comFilter.SelectedItem != null)
            {
                if (comFilter.SelectedItem.ToString() == "Tất cả")
                {


                    string sql = "SELECT MAKH, TENKH,SDT,EMAIL,DIACHI FROM DMKH" +
                   " WHERE TRANGTHAI =1 AND MAKH != 'ANONYMOUS'" +
                    " order by MAKH";

                    da = new SqlDataAdapter(sql, conn);
                    //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                    dt.Clear();
                    //xoa hết dữ liệu cũ đi    
                    da.Fill(dt);
                    // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
                    grdLISTKH.DataSource = dt;
                    //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                    grdLISTKH.Refresh();
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

        public FrmLISTKH(FrmBAN banForm)
        {
            InitializeComponent();
            frmBAN = banForm;
        }
    }
}
