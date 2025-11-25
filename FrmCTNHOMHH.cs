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
    public partial class FrmCTNHOMHH : Form
    {
        private string maNhom;
        public string MANHOM { get; set; }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        FrmNHOMPD f;
        //DataTable com2dt = new DataTable();
        string sql, constr;
        //  Boolean addnewflag = false;
        public int id = 0;
        private bool isEditMode = false;
        private bool addnewflag = true;
        private string selectedFileName = "";
        private string originalImage;


        public FrmCTNHOMHH()
        {
            InitializeComponent();
        }
        public FrmCTNHOMHH(string maNhom, FrmNHOMPD parentForm) : this()
        {
            this.maNhom = maNhom;
            f = parentForm;

        }

        private void FrmCTNHOMHH_Load(object sender, EventArgs e)
        {
            try
            {
                guna2ShadowForm1.SetShadowForm(this);


                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;



                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryNHH = $"SELECT MANHOM, TENNHOM, ANHNHOM FROM DMNHOMHH " +
                    $"WHERE MANHOM = '{maNhom}'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryNHH, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    txtMANHOM.Text = reader["MANHOM"].ToString();
                    txtTENNHOM.Text = reader["TENNHOM"].ToString();
                    string imagePath = reader["ANHNHOM"].ToString();
                    originalImage = imagePath;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        picproduct.Image = Image.FromFile(imagePath);
                    }
                }

                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }
          
            private void btncapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                //  Lấy thông tin từ các textbox và controls
                string maNhom = txtMANHOM.Text;
                string tenNhom = txtTENNHOM.Text;
                string imagePath = picproduct.ImageLocation;
                if (isEditMode && !string.IsNullOrEmpty(selectedFileName))
                {
                    imagePath = selectedFileName;
                }
                else
                {
                    imagePath = originalImage;
                }

                if (string.IsNullOrEmpty(maNhom) || string.IsNullOrEmpty(tenNhom))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Thực hiện thêm mới thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

      

                string query = "UPDATE DMNHOMHH SET TENNHOM = @tenNhom," +
                    " ANHNHOM = @selectedImagePath" +
                      
                        " WHERE MANHOM = @maNhom";


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenNhom", tenNhom);

                cmd.Parameters.AddWithValue("@maNhom", maNhom);
                cmd.Parameters.AddWithValue("@selectedImagePath", imagePath);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Clear();                    // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                //f?.RefreshDataGrid();
                //}

                txtMANHOM.ReadOnly = true;
               
                txtTENNHOM.ReadOnly = true;

                txtMANHOM.Enabled = false;

                txtTENNHOM.Enabled = false;


                btncapnhat.Enabled = true;
                btnluu.Enabled = false;
                // changepic.Enabled = false;
                // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                conn.Close();
                f?.RefreshUserControls();
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
                string maNhom = txtMANHOM.Text;
                string tenNhom = txtTENNHOM.Text;
                string imagePath = picproduct.ImageLocation;

                if (string.IsNullOrEmpty(maNhom) || string.IsNullOrEmpty(tenNhom))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện thêm mới thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string checkDuplicateQuery = "SELECT COUNT(*) FROM DMHH WHERE MANHOM = @maNHOM";
                cmd.Parameters.AddWithValue("@maNHOM", maNhom);
                cmd.CommandText = checkDuplicateQuery;

                int duplicateCount = (int)cmd.ExecuteScalar();

                if (duplicateCount > 0)
                {
                    MessageBox.Show("Mã nhóm hàng hóa này đã tồn tại!", "Thông báo");
                    txtMANHOM.Focus();
                    return;
                }
                string query = "INSERT INTO DMNHOMHH (MANHOM, TENNHOM, ANHNHOM, TRANGTHAI) " +
                            "VALUES (@maNhom, @tenNhom,@selectedImagePath,1)";

                SqlCommand cmd1 = new SqlCommand(query, conn);
                cmd1.Parameters.AddWithValue("@tenNhom", tenNhom);
                cmd1.Parameters.AddWithValue("@selectedImagePath", selectedFileName);//imagePath

                cmd1.Parameters.AddWithValue("@maNhom", maNhom);

                int rowsAffected = cmd1.ExecuteNonQuery();

             //   f?.RefreshUserControls();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clear();                    
                conn.Close();
                MANHOM = txtMANHOM.Text;
                f?.RefreshUserControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //f?.RefreshUserControls();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
          //  f?.RefreshUserControls();
        }

        public void clear()
        {
            txtMANHOM.Text = "NHH" + (int.Parse(MANHOM.Substring(3)) + 1).ToString("D2");
            txtMANHOM.Enabled = false;
            txtTENNHOM.Clear();
           
            txtTENNHOM.Focus();
           picproduct.Image = Image.FromFile(@"C:\Users\Admin\Downloads\vegetable.png");
            // Xóa các giá trị đã chọn trong ComboBox comNhom
           // comNhom.SelectedIndex = -1;

            // Đặt lại giá trị của NumericUpDown numericUpDown về giá trị mặc định
            //txtSL.Value = 1;

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
                picproduct.Image = Image.FromFile(selectedFileName);
            }
        }

      

    }
}
