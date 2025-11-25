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
    public partial class FrmCTNCC : Form
    {
        private string maNCC;
        public string MANCC { get; set; }
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        FrmNCC f;
        DataTable com2dt = new DataTable();
        string sql, constr;
        //  Boolean addnewflag = false;
        public int id = 0;
        private bool isEditMode = false;
        private bool addnewflag = true;
        public FrmCTNCC()
        {
            InitializeComponent();
        }
        public FrmCTNCC(string maNCC, FrmNCC parentForm) : this()
        {
            this.maNCC = maNCC;
            f = parentForm;

        }
        private string initialComNhomValue;

        public void clear()
        {
            txtMANCC.Text = "NV" + (int.Parse(MANCC.Substring(3)) + 1).ToString("D2");
            txtMANCC.Enabled = false;
       
            txtTENNCC.Clear();
            txtSDT.Clear();
            txtEMAIL.Clear();
            txtDIACHI.Clear();
            txtMANCC.Focus();
            // Xóa các giá trị đã chọn trong ComboBox comNhom

      

        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {

            try
            {
                //  Lấy thông tin từ các textbox và controls
                string maNCC = txtMANCC.Text;
                string tenNCC = txtTENNCC.Text;
                string sdt = txtSDT.Text;
                string diaChi = txtDIACHI.Text;
                string email = txtEMAIL.Text;
              
              
                if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(sdt) )
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                // Thực hiện thêm mới thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();


                string query = "UPDATE DMNHACC SET TENNCC = @tenNCC, SDT = @sdt, EMAIL = @email, DIACHI = @diaChi" +
                        
                        " WHERE MANCC = @maNCC";


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenNCC", tenNCC);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
               
                cmd.Parameters.AddWithValue("@maNCC", maNCC);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Clear();                    // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                //f?.RefreshDataGrid();
                //}

                txtMANCC.ReadOnly = true;
                txtDIACHI.ReadOnly = true;
                txtEMAIL.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtTENNCC.Enabled = true;


                txtMANCC.Enabled = false;
                txtDIACHI.Enabled = false;
                txtEMAIL.Enabled = false;
                txtSDT.Enabled = false;
                txtTENNCC.Enabled = false;


                btncapnhat.Enabled = true;
                btnluu.Enabled = false;
                // changepic.Enabled = false;
                // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                MANCC = txtMANCC.Text;
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
                string maNCC = txtMANCC.Text;
                string tenNCC = txtTENNCC.Text;
                string sdt = txtSDT.Text;
                string diaChi = txtDIACHI.Text;
                string  email = txtEMAIL.Text;

                if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(sdt) )
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện thêm mới thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();



                string query = "INSERT INTO DMNHACC (MANCC, TENNCC, SDT, EMAIL, DIACHI,TRANGTHAI) " +
                            "VALUES (@maNCC, @tenNCC, @sdt, @email, @diaChi,1)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenNCC", tenNCC);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
                cmd.Parameters.AddWithValue("@maNCC", maNCC);

                int rowsAffected = cmd.ExecuteNonQuery();

                f?.RefreshDataGrid();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clear();
                MANCC = txtMANCC.Text;
                conn.Close();
                f?.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FrmCTNCC_Load(object sender, EventArgs e)
        {

            try
            {
                guna2ShadowForm1.SetShadowForm(this);


                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;



                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryNCC = $"SELECT * FROM DMNHACC" +
                    $" WHERE MANCC = '{maNCC}'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryNCC, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                   
                    txtMANCC.Text = reader["MANCC"].ToString();
                    txtTENNCC.Text = reader["TENNCC"].ToString();
                    txtSDT.Text = reader["SDT"].ToString();
                    txtEMAIL.Text = reader["EMAIL"].ToString();
                    txtDIACHI.Text = reader["DIACHI"].ToString();
                    txtDIACHI.Text = reader["DIACHI"].ToString();
                    lblTONGSLDH.Text = reader["SL"].ToString();

                    int congNo = (int)reader["CONGNO"];
                    lblCONGNO.Text = congNo.ToString("N0") + " VNĐ";

                }

                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }

    }
}
