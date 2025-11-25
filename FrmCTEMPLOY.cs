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
    public partial class FrmCTEMPLOY : Form

    {
        public string MANV { get; set; }
        //   string imagePath = "";
        private string originalImage;

        public string maNV;
        private string selectedFileName = "";

        private string initialComNhomValue; // Biến lưu trữ giá trị ban đầu của comNhom

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        //private string loggedInUsername;
      //  DataTable comdt = new DataTable();
      //  DataTable com2dt = new DataTable();
        string sql, constr;
        FrmEMPLOY f;
        private bool isEditMode = false;
        private bool addnewflag = true;

        public FrmCTEMPLOY()
        {
            InitializeComponent();

        }
        public FrmCTEMPLOY(string maNV, FrmEMPLOY parentForm) : this()
        {
            this.maNV = maNV;
            f = parentForm;

        }

        public void clear()
        {
            txtMANV.Text = "NV" + (int.Parse(MANV.Substring(2)) + 1).ToString("D2");
            txtMANV.Enabled = false;
         //   txtMANV.Clear();
            txtTENNV.Clear();
            txtSDT.Clear();
            txtEMAIL.Clear();
            txtDIACHI.Clear();
            txtUSERNAME.Clear();
            txtPASSWORD.Clear();
            txtTENNV.Focus(); 
            picaccount.Image = Image.FromFile(@"C:\Users\Admin\Downloads\amusement-park.png");
            // Xóa các giá trị đã chọn trong ComboBox comNhom
            comNhom.SelectedIndex = -1;

         

        }
        private void FrmCTEMPLOY_Load(object sender, EventArgs e)
        {
            try
            {
               // txtMANV.Text = MANV;

                guna2ShadowForm1.SetShadowForm(this);


                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;

                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryNV = $"SELECT DMNV.MANV, TENNV, SDT,EMAIL, DIACHI, CHUCVU, USERNAME, PASSWORD, ANHNV FROM DMNV, DMTK" +
                    $" WHERE DMNV.MANV=DMTK.MANV " +
                    $"AND DMNV.MANV = '{maNV}'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryNV, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMANV.Text = reader["MANV"].ToString();
                    txtTENNV.Text = reader["TENNV"].ToString();
                    txtSDT.Text = reader["SDT"].ToString();
                    txtEMAIL.Text = reader["EMAIL"].ToString();
                    txtDIACHI.Text = reader["DIACHI"].ToString();
                    comNhom.Text = reader["CHUCVU"].ToString();
                    initialComNhomValue = comNhom.Text;

                    txtUSERNAME.Text = reader["USERNAME"].ToString();
                    txtPASSWORD.Text = reader["PASSWORD"].ToString();
                    string imagePath = reader["ANHNV"].ToString();
                    originalImage = imagePath;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        picaccount.Image = Image.FromFile(imagePath);
                    }
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
       


        private void picaccount_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void changepic_Click(object sender, EventArgs e)
        {
            isEditMode = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp;";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                 selectedFileName = openFileDialog.FileName;
                // Thực hiện xử lý với tệp đã chọn ở đây (ví dụ: gán ảnh vào picaccount)
                picaccount.Image = Image.FromFile(selectedFileName);
            }
        }

        private void btndoimk_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin đã sửa từ các textbox
                string maNV = txtMANV.Text;
                string tenNV = txtTENNV.Text;
                string sdt = txtSDT.Text;
                string email = txtEMAIL.Text;
                string diaChi = txtDIACHI.Text;
                string chucVu = comNhom.Text;
                string imagePath = picaccount.ImageLocation;
                string userName = txtUSERNAME.Text;
                string passWord = txtPASSWORD.Text;
                string groupValue = "";

                if (comNhom.Text == "Quản lý")
                {
                    groupValue = "ADMIN";
                }
                else if (comNhom.Text == "Nhân viên")
                {
                    groupValue = "USER";
                }
                if (isEditMode && !string.IsNullOrEmpty(selectedFileName))
                {
                    imagePath = selectedFileName;
                    ;
                }
                else
                {
                    imagePath = originalImage;
                }
                // Kiểm tra tính hợp lệ của thông tin (nếu cần thiết)
                // Ví dụ: Kiểm tra xem các trường bắt buộc đã được nhập hay chưa.
                if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện cập nhật thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                string query = "UPDATE DMNV SET TENNV = @tenNV, SDT = @sdt, EMAIL = @email, DIACHI = @diaChi,"
                             + " CHUCVU = @chucVu, ANHNV = @selectedImagePath"
                             + " WHERE MANV = @maNV";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenNV", tenNV);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
                cmd.Parameters.AddWithValue("@chucVu", chucVu);
                cmd.Parameters.AddWithValue("@selectedImagePath", imagePath);
                cmd.Parameters.AddWithValue("@maNV", maNV);
                int rowsAffected = cmd.ExecuteNonQuery();





                // Cập nhật thông tin trong bảng DMTK
           
                string queryDMTK = "UPDATE DMTK SET USERNAME = @userName, PASSWORD = @passWord, [GROUP] = @Group"
                                 + " WHERE MANV = @maNV";

                SqlCommand cmdDMTK = new SqlCommand(queryDMTK, conn);
                cmdDMTK.Parameters.AddWithValue("@userName", userName);
                cmdDMTK.Parameters.AddWithValue("@passWord", passWord);
                cmdDMTK.Parameters.AddWithValue("@maNV", maNV);
                cmdDMTK.Parameters.AddWithValue("@Group", groupValue);

                int rowsAffectedDMTK = cmdDMTK.ExecuteNonQuery();

                if (rowsAffected > 0 && rowsAffectedDMTK > 0)
                {
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể lưu thông tin. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtMANV.ReadOnly = true;
                txtTENNV.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtEMAIL.ReadOnly = true;
                comNhom.Enabled = false;
                txtUSERNAME.ReadOnly = true;
                txtPASSWORD.ReadOnly = true;
                txtDIACHI.ReadOnly = true;

                txtMANV.Enabled = false;
                txtTENNV.Enabled = false;
                txtSDT.Enabled = false;
                txtEMAIL.Enabled = false;
                txtDIACHI.Enabled = false;
                txtPASSWORD.Enabled = false;
                txtUSERNAME.Enabled = false;


                btncapnhat.Enabled = true;
                btnluu.Enabled = false;
                MANV = txtMANV.Text;
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
                string maNV = txtMANV.Text;
                string tenNV = txtTENNV.Text;
                string sdt = txtSDT.Text;
                string email = txtEMAIL.Text;
                string diaChi = txtDIACHI.Text;
                string chucVu = comNhom.Text;
                string imagePath = picaccount.ImageLocation;
                string userName = txtUSERNAME.Text;
                string passWord = txtPASSWORD.Text;


                if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(sdt))
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

               string query  = "INSERT INTO DMNV VALUES (@MaNV, @TenNV,@ChucVu,@SDT, @Email,@Diachi, @AnhNV,1)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@tenNV", tenNV);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
                cmd.Parameters.AddWithValue("@chucVu", chucVu);
                cmd.Parameters.AddWithValue("@AnhNV", selectedFileName);
                cmd.Parameters.AddWithValue("@maNV", maNV);
                int rowsAffected = cmd.ExecuteNonQuery();
            
                string groupValue = "";

                if (comNhom.Text == "Quản lý")
                {
                    groupValue = "ADMIN";
                }
                else if (comNhom.Text == "Nhân viên")
                {
                    groupValue = "USER";
                }

                 string query2 = "INSERT INTO DMTK VALUES (@Username, @Password, @MaNV, @Group,1)";
                SqlCommand cmdDMTK = new SqlCommand(query2, conn);
                cmdDMTK.Parameters.AddWithValue("@MaNV", maNV);
                cmdDMTK.Parameters.AddWithValue("@Username", userName);
                cmdDMTK.Parameters.AddWithValue("@Password", passWord);
                cmdDMTK.Parameters.AddWithValue("@Group", groupValue);
               // cmdDMTK.ExecuteNonQuery();
                    int rowsAffectedDMTK = cmdDMTK.ExecuteNonQuery();

              

            if (rowsAffected > 0 && rowsAffectedDMTK > 0)
            {
               
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clear();
               
                conn.Close();
                MANV = txtMANV.Text;
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

        private void changepic_Click_1(object sender, EventArgs e)
        {
            isEditMode = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
                // Thực hiện xử lý với tệp đã chọn ở đây (ví dụ: gán ảnh vào picaccount)
                picaccount.Image = Image.FromFile(selectedFileName);
            }
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
