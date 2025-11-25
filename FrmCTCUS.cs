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
    public partial class FrmCTCUS : Form
    {
        public string MAKH { get; set; }
        public string maKH;
        private string initialComNhomValue; // Biến lưu trữ giá trị ban đầu của comNhom

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        //private string loggedInUsername;
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        string sql, constr;
        FrmCUS f;
        private bool isEditMode = false;
        private bool addnewflag = true;
        public FrmCTCUS()
        {
            InitializeComponent();
        }
        public FrmCTCUS(string maKH, FrmCUS parentForm) : this()
        {
            this.maKH = maKH;
            f = parentForm;

        }
        public void clear()
        {
            txtMAKH.Text = "KH" + (int.Parse(MAKH.Substring(3)) + 1).ToString("D3");

            //   txtMANV.Clear();
            txtTENKH.Clear();
            txtSDT.Clear();
            txtEMAIL.Clear();
            txtDIACHI.Clear();
           
            txtMAKH.Focus();
            // Xóa các giá trị đã chọn trong ComboBox comNhom
            comNhom.SelectedIndex = -1;

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin đã sửa từ các textbox
                string maKH = txtMAKH.Text;
                string tenKH = txtTENKH.Text;
                string sdt = txtSDT.Text;
                string email = txtEMAIL.Text;
                string diaChi = txtDIACHI.Text;
                string nhomKH = comNhom.Text;
            //    string tongCT = lblTONGCHITIEU.Text;
              //  string tongSLDH = lblTONGSLDH.Text;
               // int nhomKH;

                if (comNhom.Text == "Bán lẻ")
                {
                    nhomKH = "0";
                }
                else if (comNhom.Text == "Bán buôn")
                {
                    nhomKH = "1";
                }
               
                // Kiểm tra tính hợp lệ của thông tin (nếu cần thiết)
                // Ví dụ: Kiểm tra xem các trường bắt buộc đã được nhập hay chưa.
                if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(comNhom.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện cập nhật thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                string query = "UPDATE DMKH SET TENKH = @tenKH, SDT = @sdt, EMAIL = @email, DIACHI = @diaChi,"
                             + " LOAIKH = @nhomKH" 
                             + " WHERE MAKH = @maKH";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenKH", tenKH);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
                cmd.Parameters.AddWithValue("@nhomKH", nhomKH);
                cmd.Parameters.AddWithValue("@maKH", maKH);
                int rowsAffected = cmd.ExecuteNonQuery();


 

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể lưu thông tin. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtMAKH.ReadOnly = true;
                txtTENKH.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtEMAIL.ReadOnly = true;
                comNhom.Enabled = false;
              
                txtDIACHI.ReadOnly = true;

                txtMAKH.Enabled = false;
                txtTENKH.Enabled = false;
                txtSDT.Enabled = false;
                txtEMAIL.Enabled = false;
                txtDIACHI.Enabled = false;
             

                btncapnhat.Enabled = true;
                btnluu.Enabled = false;
                MAKH = txtMAKH.Text;
                conn.Close();
                f?.RefreshDataGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                //  Lấy thông tin từ các textbox và controls
                string maKH = txtMAKH.Text;
                string tenKH = txtTENKH.Text;
                string sdt = txtSDT.Text;
                string email = txtEMAIL.Text;
                string diaChi = txtDIACHI.Text;
              //  int nhomKH;
                string nhomKH = comNhom.Text;
               // string groupValue = "";

                if (comNhom.Text == "Bán lẻ")
                {
                    nhomKH = "0";
                }
                else if (comNhom.Text == "Bán buôn")
                {
                    nhomKH = "1";
                }

                if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(comNhom.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện thêm mới thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                //  string maNhom = GetMaNhomFromTenNhom(tenNhom);

                //  DataRow[] matchingRows = com2dt.Select($"TENNHOM = '{tenNhom}'");
                //  if (matchingRows.Length > 0)
                // {
                //     string maNhom = matchingRows[0]["MANHOM"].ToString();

                string query = "INSERT INTO DMKH VALUES (@MaKH, @TenKH,@SDT,@Email,@Diachi,1,@nhomKH,0,0)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenKH", tenKH);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Diachi", diaChi);
                cmd.Parameters.AddWithValue("@nhomKH", nhomKH);
              
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                int rowsAffected = cmd.ExecuteNonQuery();

           

                


                if (rowsAffected > 0 )
                {

                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clear();

                conn.Close();
                MAKH = txtMAKH.Text;
                f?.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            f?.RefreshDataGrid();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            f?.RefreshDataGrid();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTENKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void FrmCTCUS_Load(object sender, EventArgs e)
        {
            try
            {

                guna2ShadowForm1.SetShadowForm(this);


                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;

                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryKH = $"SELECT MAKH, TENKH, SDT,EMAIL, DIACHI, LOAIKH, TONGCHITIEU, TONGSLDH FROM DMKH" +
                    $" WHERE MAKH = '{maKH}'";
                   
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryKH, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMAKH.Text = reader["MAKH"].ToString();
                    txtTENKH.Text = reader["TENKH"].ToString();
                    txtSDT.Text = reader["SDT"].ToString();
                    txtEMAIL.Text = reader["EMAIL"].ToString();
                    txtDIACHI.Text = reader["DIACHI"].ToString();
                    // Đọc giá trị LOAIKH từ CSDL
                    int loaiKhachHang = Convert.ToInt32(reader["LOAIKH"]);
                    // Chuyển giá trị LOAIKH thành tên tương ứng
                    comNhom.Text = loaiKhachHang == 0 ? "Bán lẻ" : "Bán buôn";
                    initialComNhomValue = comNhom.Text;
                    //lblTONGCHITIEU.Text = reader["TONGCHITIEU"].ToString();
                    //if (int.TryParse(lblTONGCHITIEU.Text, out int tongTien))
                    //{
                    //    Định dạng giá trị và hiển thị trong TextBox
                    //    lblTONGCHITIEU.Text = tongTien.ToString("N0") + " VND";
                    //}
                    lblTONGSLDH.Text = reader["TONGSLDH"].ToString();

                    int tongChiTieu = (int)reader["TONGCHITIEU"];
                    lblTONGCHITIEU.Text = tongChiTieu.ToString("N0") + " VNĐ";

                }

                reader.Close();



                //sql = "select DISTINCT CHUCVU from DMNV";
                //da = new SqlDataAdapter(sql, conn);
                //com2dt.Clear();
                //da.Fill(com2dt);
                //comNhom.DataSource = com2dt;
                //comNhom.DisplayMember = "CHUCVU";

            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }
    }
}
