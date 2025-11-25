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
    public partial class FrmCTPRODUCT : Form
    {

        private string maHH;
        private string originalImage;
        private string selectedFileName = "";
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        FrmPRODUCT f;
        DataTable com2dt = new DataTable();
        string sql, constr;
        //  Boolean addnewflag = false;
        public int id = 0; 
        private bool isEditMode = false;
        private bool addnewflag = true;
        public FrmCTPRODUCT()
        {
            InitializeComponent();

        }
        private string initialComNhomValue; // Biến lưu trữ giá trị ban đầu của comNhom
        private string initialComTHUEVAOValue; // Biến lưu trữ giá trị ban đầu của comNhom
        private string initialComTHUERAValue; // Biến lưu trữ giá trị ban đầu của comNhom

        //public FrmCTPRODUCT(string maHH,FrmPRODUCT frm): this() //
        //{
        //    this.maHH = maHH;
        //    //  MessageBox.Show("Giá trị của maHH: " + maHH);
        //    f = frm;


        //}
        public FrmCTPRODUCT(string maHH, FrmPRODUCT parentForm) : this()
        {
            this.maHH = maHH;
            f = parentForm;

        }

        public void clear()
        {
            txtMAHH.Clear();
            txtTENHH.Clear();
            txtDVT.Clear();
            txtGIABAN.Clear();
            txtGIANHAP.Clear();
            txtMOTA.Clear();
            txtBC.Clear();
            txtMAHH.Focus();
            picproduct.Image = Image.FromFile(@"C:\Users\Admin\Downloads\vegetable.png");
            // Xóa các giá trị đã chọn trong ComboBox comNhom
            comNhom.SelectedIndex = -1;
            comTHUEVAO.SelectedIndex = -1;

            comTHUERA.SelectedIndex = -1;


            // Đặt lại giá trị của NumericUpDown numericUpDown về giá trị mặc định
            txtSL.Value = 1;

        }
        private void FrmCTPRODUCT_Load(object sender, EventArgs e)
        {
            
            try
            {
                guna2ShadowForm1.SetShadowForm(this);
               

                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;

                

                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryHH = $"SELECT MAHH, TENHH, GIANHAP, GIABAN, TENNHOM, DVT, SL, ANHHH, MOTA,THUEVAO, THUERA, BARCODE FROM DMHH, DMNHOMHH" +
                    $" WHERE DMHH.MANHOM = DMNHOMHH.MANHOM " +
                    $"AND MAHH = '{maHH}'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryHH, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    txtMAHH.Text = reader["MAHH"].ToString();
                    txtTENHH.Text = reader["TENHH"].ToString();
                    txtGIANHAP.Text = reader["GIANHAP"].ToString();
                    txtGIABAN.Text = reader["GIABAN"].ToString();
                    comNhom.Text = reader["TENNHOM"].ToString();
                    txtMOTA.Text = reader["MOTA"].ToString();
                    txtBC.Text = reader["BARCODE"].ToString();
                    if (reader["THUEVAO"].ToString() == null)
                    {
                        comTHUEVAO.Visible = false;
                        btnTHUE.Checked = false;

                    }
                    else
                    {
                        string thueVao = reader["THUEVAO"].ToString();
                        switch (thueVao)
                        {
                            case "10":
                                comTHUEVAO.Text = "Thuế suất 10%";
                                break;
                            case "8":
                                comTHUEVAO.Text = "Thuế suất 8%";
                                break;
                            case "5":
                                comTHUEVAO.Text = "Thuế suất 5%";
                                break;
                            case "0":
                                comTHUEVAO.Text = "Thuế suất 0%";
                                break;
                        }
                    }
                    if (reader["THUERA"].ToString() == null)
                    {
                        comTHUERA.Visible = false;

                    }
                    else

                    {
                        string thueRa = reader["THUERA"].ToString();
                        switch (thueRa)
                        {
                            case "10":
                                comTHUERA.Text = "Thuế suất 10%";
                                break;
                            case "8":
                                comTHUERA.Text = "Thuế suất 8%";
                                break;
                            case "5":
                                comTHUERA.Text = "Thuế suất 5%";
                                break;
                            case "0":
                                comTHUERA.Text = "Thuế suất 0%";
                                break;
                        }
                    }
                    initialComNhomValue = comNhom.Text;
                    initialComTHUEVAOValue = comTHUEVAO.Text;
                    initialComTHUERAValue = comTHUERA.Text;

                    txtDVT.Text = reader["DVT"].ToString();
                    txtSL.Text = reader["SL"].ToString();
                    string imagePath = reader["ANHHH"].ToString();
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

        private void GIANHAP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //private string originalComNhomValue; // Biến để lưu trữ giá trị ban đầu của comNhom

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                //  Lấy thông tin từ các textbox và controls
                string maHH = txtMAHH.Text;
                string tenHH = txtTENHH.Text;
                string sl = txtSL.Text;
                string tenNhom = comNhom.Text;
             //   string thueVao = comTHUEVAO.Text;
              //  string thueRa = comTHUERA.Text;

                string giaNhap = txtGIANHAP.Text;
                string giaBan = txtGIABAN.Text;
                string imagePath = picproduct.ImageLocation;
                string dvt = txtDVT.Text;
                string moTa = txtMOTA.Text;
                String barcode = txtBC.Text;
                // Kiểm tra xem chế độ chỉnh sửa đã được bật và có tệp hình ảnh được chọn không
                if (isEditMode && !string.IsNullOrEmpty(selectedFileName))
                {
                    imagePath = selectedFileName;
                }
                else
                {
                    imagePath = originalImage;
                }
                if (string.IsNullOrEmpty(maHH) || string.IsNullOrEmpty(tenHH) || string.IsNullOrEmpty(giaBan) || string.IsNullOrEmpty(tenNhom))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                // Thực hiện thêm mới thông tin vào cơ sở dữ liệu
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                // Lấy MANHOM từ comNhom

                //DataRow[] matchingRows = com2dt.Select("TENNHOM = '" + tenNhom + "'");

                ////DataRow[] matchingRows = com2dt.Select($"TENNHOM = '{tenNhom}'");
                //if (matchingRows.Length > 0)
                //{
                //    string maNhom = matchingRows[0]["MANHOM"].ToString();
                string maNhom = GetMaNhomFromTenNhom(tenNhom);

                string query = "UPDATE DMHH SET TENHH = @tenHH, SL = @sl, DVT = @dvt, GIANHAP = @giaNhap, GIABAN = @giaBan, MOTA =@moTa, THUEVAO =@thueVao, THUERA = @thueRa," +
                        " MANHOM = @maNhom, ANHHH = @selectedImagePath, BARCODE = @barcode" +
                        " WHERE MAHH = @maHH";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenHH", tenHH);
                    cmd.Parameters.AddWithValue("@sl", sl);
                    cmd.Parameters.AddWithValue("@dvt", dvt);
                    cmd.Parameters.AddWithValue("@giaNhap", giaNhap);
                    cmd.Parameters.AddWithValue("@giaBan", giaBan);
                    cmd.Parameters.AddWithValue("@selectedImagePath", imagePath);
                    cmd.Parameters.AddWithValue("@maHH", maHH);
                    cmd.Parameters.AddWithValue("@maNhom", maNhom);
                cmd.Parameters.AddWithValue("@moTa", moTa);
                cmd.Parameters.AddWithValue("@barcode", barcode);

                if (comTHUEVAO.SelectedItem != null)
                {
                    if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 10%")
                    {
                        cmd.Parameters.AddWithValue("@thueVao", 10);
                    }
                    else if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 8%")
                    {
                        cmd.Parameters.AddWithValue("@thueVao", 8);

                    }
                    else if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 5%")
                    {
                        cmd.Parameters.AddWithValue("@thueVao", 5);

                    }
                    else if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 0%")
                    {
                        cmd.Parameters.AddWithValue("@thueVao", 0);

                    }

                }
                if (comTHUERA.SelectedItem != null)
                {
                    if (comTHUERA.SelectedItem.ToString() == "Thuế suất 10%")
                    {
                        cmd.Parameters.AddWithValue("@thueRa", 10);
                    }
                    else if (comTHUERA.SelectedItem.ToString() == "Thuế suất 8%")
                    {
                        cmd.Parameters.AddWithValue("@thueRa", 8);

                    }
                    else if (comTHUERA.SelectedItem.ToString() == "Thuế suất 5%")
                    {
                        cmd.Parameters.AddWithValue("@thueRa", 5);

                    }
                    else if (comTHUERA.SelectedItem.ToString() == "Thuế suất 0%")
                    {
                        cmd.Parameters.AddWithValue("@thueRa", 0);

                    }

                }


                int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //Clear();                    // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                    //f?.RefreshDataGrid();
                //}

                txtMAHH.ReadOnly = true;
                txtGIANHAP.ReadOnly = true;
                txtGIABAN.ReadOnly = true;
                txtDVT.ReadOnly = true;
                txtBC.ReadOnly = true;
                comNhom.Enabled = false;
                comTHUEVAO.Enabled = false;
                comTHUERA.Enabled = false;

                txtSL.Enabled = false;
                txtTENHH.ReadOnly = true;
                txtMOTA.ReadOnly = true;

                txtMAHH.Enabled = false;
                txtGIANHAP.Enabled = false;
                txtGIABAN.Enabled = false;
                txtDVT.Enabled = false;
                txtTENHH.Enabled = false;
                txtMOTA.Enabled = false;
                txtBC.Enabled = false;

                btncapnhat.Enabled = true;
                btnluu.Enabled = false;
               // changepic.Enabled = false;
                // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                conn.Close();
                f?.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
        private string GetMaNhomFromTenNhom(string tenNhom)
        {
            string maNhom = string.Empty;

            // Thực hiện truy vấn SQL để lấy MANHOM từ TENNHOM
            string query = "SELECT MANHOM FROM DMNHOMHH WHERE TENNHOM = @tenNhom";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tenNhom", tenNhom);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        maNhom = result.ToString();
                    }
                }
            }

            return maNhom;
        }
        private void btnluu_Click(object sender, EventArgs e)
        {

            try
            {
                //  Lấy thông tin từ các textbox và controls
                string maHH = txtMAHH.Text;
                string tenHH = txtTENHH.Text;
                string sl = txtSL.Text;
                string tenNhom = comNhom.Text;
               // string thueVao = comTHUEVAO.Text;
              //  string thueRa = comTHUERA.Text;
                string giaNhap = txtGIANHAP.Text;
                string giaBan = txtGIABAN.Text;
                string imagePath = picproduct.ImageLocation;
                string dvt = txtDVT.Text;
                string moTa = txtMOTA.Text;
                string barcode = txtBC.Text;
                // Kiểm tra xem chế độ chỉnh sửa đã được bật và có tệp hình ảnh được chọn không
                //if (isEditMode && !string.IsNullOrEmpty(selectedFileName))
                //{
                //    imagePath = selectedFileName;
                //}
                //else
                //{
                //    imagePath = originalImage;
                //}
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                // Khai báo và khởi tạo SqlCommand để kiểm tra mã hàng hóa
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string checkDuplicateQuery = "SELECT COUNT(*) FROM DMHH WHERE MAHH = @maHH";
                cmd.Parameters.AddWithValue("@maHH", maHH);
                cmd.CommandText = checkDuplicateQuery;

                int duplicateCount = (int)cmd.ExecuteScalar();

                if (duplicateCount > 0)
                {
                    MessageBox.Show("Mã hàng hóa này đã tồn tại!", "Thông báo");
                    txtMAHH.Focus();
                    return;
                }
                // Lấy MANHOM từ comNhom
                string maNhom = GetMaNhomFromTenNhom(tenNhom);

                //  DataRow[] matchingRows = com2dt.Select($"TENNHOM = '{tenNhom}'");
                //  if (matchingRows.Length > 0)
                // {
                //     string maNhom = matchingRows[0]["MANHOM"].ToString();

                string query = "INSERT INTO DMHH (MAHH, TENHH, SL, DVT, GIANHAP, GIABAN, MANHOM, ANHHH, TRANGTHAI, MOTA, THUEVAO, THUERA, BARCODE) " +
                            "VALUES (@maHH, @tenHH, @sl, @dvt, @giaNhap, @giaBan, @maNhom, @selectedImagePath,1, @moTa,@thueVao, @thueRa, @barcode)";

                    SqlCommand cmd1 = new SqlCommand(query, conn);
                    cmd1.Parameters.AddWithValue("@tenHH", tenHH);
                    cmd1.Parameters.AddWithValue("@sl", sl);
                    cmd1.Parameters.AddWithValue("@dvt", dvt);
                    cmd1.Parameters.AddWithValue("@giaNhap", giaNhap);
                    cmd1.Parameters.AddWithValue("@giaBan", giaBan);
                    cmd1.Parameters.AddWithValue("@selectedImagePath", selectedFileName);//imagePath
                    cmd1.Parameters.AddWithValue("@maHH", maHH);
                    cmd1.Parameters.AddWithValue("@maNhom", maNhom);
                cmd1.Parameters.AddWithValue("@moTa", moTa);
                cmd1.Parameters.AddWithValue("@barcode", barcode);

                if (comTHUERA.SelectedItem != null)
                {
                    if (comTHUERA.SelectedItem.ToString() == "Thuế suất 10%")
                    {
                        cmd1.Parameters.AddWithValue("@thueRa", 10);
                    }
                    else if (comTHUERA.SelectedItem.ToString() == "Thuế suất 8%")
                    {
                        cmd1.Parameters.AddWithValue("@thueRa", 8);

                    }
                    else if (comTHUERA.SelectedItem.ToString() == "Thuế suất 5%")
                    {
                        cmd1.Parameters.AddWithValue("@thueRa", 5);

                    }
                    else if (comTHUERA.SelectedItem.ToString() == "Thuế suất 0%")
                    {
                        cmd1.Parameters.AddWithValue("@thueRa", 0);

                    }
                  
                }  else
                    {
                    cmd1.Parameters.AddWithValue("@thueRa", null);

                }

                if (comTHUEVAO.SelectedItem != null)
                {
                    if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 10%")
                    {
                        cmd1.Parameters.AddWithValue("@thueVao", 10);
                    }
                    else if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 8%")
                    {
                        cmd1.Parameters.AddWithValue("@thueVao", 8);

                    }
                    else if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 5%")
                    {
                        cmd1.Parameters.AddWithValue("@thueVao", 5);

                    }
                    else if (comTHUEVAO.SelectedItem.ToString() == "Thuế suất 0%")
                    {
                        cmd1.Parameters.AddWithValue("@thueVao", 0);

                    }

                }
                else
                {
                    cmd1.Parameters.AddWithValue("@thueVao", null);

                }

                int rowsAffected = cmd1.ExecuteNonQuery();

                f?.RefreshDataGrid();

                if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lưu thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    clear();                    // Gọi phương thức RefreshDataGrid trong form FrmPRODUCT
                    //f?.RefreshDataGrid();
                

             

                conn.Close();
                f?.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
       // private bool isEditMode = false;

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            f?.RefreshDataGrid();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            f?.RefreshDataGrid();
        }

        private void picproduct_Click(object sender, EventArgs e)
        {

        }

        private void btnXUATALL_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void fdg_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void changepic_Click(object sender, EventArgs e)
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
