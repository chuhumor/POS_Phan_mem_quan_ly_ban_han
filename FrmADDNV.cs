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
    public partial class FrmADDNV : Form
    {
        // private FrmEMPLOY frmEMPLOY;
        private string selectedFileName = "";
        public string MANV{ get; set; }
        private FrmEMPLOY employForm;

        private Image originalImage;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        SqlDataReader dr;
        FrmLOGIN f;

        string sql, constr;




        public FrmADDNV(FrmLOGIN frm, FrmEMPLOY employForm)//FrmEMPLOY emForm
        {
            InitializeComponent();
            //frmEMPLOY = emForm;
            f = frm;
            this.employForm = employForm;

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmADDNV_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            txtID.Text = MANV;
            originalImage = picaccount.Image;
            //Bay loi
            try
            {
                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
             
                sql = "select distinct CHUCVU from DMNV";
                da = new SqlDataAdapter(sql, conn);
                com2dt.Clear();
                da.Fill(com2dt);
                txtCHUCVU.DataSource = com2dt;
                txtCHUCVU.DisplayMember = "CHUCVU";

            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCHUCVU.SelectedItem != null)
            {
                // Code xử lý thanh toán

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    sql = "INSERT INTO DMNV VALUES (@MaNV, @TenNV,@ChucVu,@SDT, @Email,@Diachi, @AnhNV)";
                    using (SqlCommand updateHDNCommand = new SqlCommand(sql, conn))
                    {
                        updateHDNCommand.Parameters.AddWithValue("@MaNV", txtID.Text);
                        updateHDNCommand.Parameters.AddWithValue("@TenNV", txtTENNV.Text);
                        updateHDNCommand.Parameters.AddWithValue("@ChucVu", txtCHUCVU.Text);
                        updateHDNCommand.Parameters.AddWithValue("@SDT", txtSDT.Text);
                        updateHDNCommand.Parameters.AddWithValue("@Email", txtEMAIL.Text);

                        updateHDNCommand.Parameters.AddWithValue("@DiaChi", txtDIACHI.Text);
                        updateHDNCommand.Parameters.AddWithValue("@AnhNV", selectedFileName);
                        updateHDNCommand.ExecuteNonQuery();
                    }
                    string groupValue = "";

                    if (txtCHUCVU.Text == "Quản lý")
                    {
                        groupValue = "ADMIN";
                    }
                    else if (txtCHUCVU.Text == "Nhân viên")
                    {
                        groupValue = "USER";
                    }

                    sql = "INSERT INTO DMTK VALUES (@Username, @Password, @MaNV, @Group)";
                    using (SqlCommand updateHDNCommand = new SqlCommand(sql, conn))
                    {
                        updateHDNCommand.Parameters.AddWithValue("@MaNV", txtID.Text);
                        updateHDNCommand.Parameters.AddWithValue("@Username", txtusername.Text);
                        updateHDNCommand.Parameters.AddWithValue("@Password", txtpass.Text);
                        updateHDNCommand.Parameters.AddWithValue("@Group", groupValue);
                        updateHDNCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã thêm nhân viên!", "Thông báo");
                    conn.Close();
                    MANV = txtID.Text; // Cập nhật lại giá trị của MAHDN sau khi tạo mới hóa đơn

                    ResetForm();
                    employForm?.RefreshDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool isEditMode = false;

        private void changepic_Click(object sender, EventArgs e)
        {
            isEditMode = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                 selectedFileName = openFileDialog.FileName;
                // Thực hiện xử lý với tệp đã chọn ở đây (ví dụ: gán ảnh vào picaccount)
                picaccount.Image = Image.FromFile(selectedFileName);
            }
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void ResetForm()
        {
            // Xóa dữ liệu trong bảng dt
            dt.Clear();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Không cần mở kết nối ở đây
                conn.Open();

                txtID.Text = "NV" + (int.Parse(MANV.Substring(2)) + 1).ToString("D2");
                txtTENNV.Clear();
                txtCHUCVU.SelectedItem = null;
                txtDIACHI.Clear();
                txtSDT.Clear();
                txtEMAIL.Clear();
                txtusername.Clear();
                txtpass.Clear();
                picaccount.Image = originalImage;

                // Không cần đóng kết nối ở đây
            }


        }

    }
}
