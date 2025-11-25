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

    public partial class FrmNHOMPD : Form
    {
        int currentMANHOM = 1;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();

        string sql, constr;
        int i;
        Boolean addnewflag = false;




        public void RefreshUserControls()
        {
            try
            {
                string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = "SELECT MANHOM,ANHNHOM, TENNHOM, TRANGTHAI FROM DMNHOMHH ORDER BY MANHOM ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Xóa tất cả các UserControl hiện có trên flowLayoutPanel1
                            flowLayoutPanel1.Controls.Clear();

                            while (reader.Read())
                            {
                                // Tạo và hiển thị UserControl dựa trên dữ liệu
                                string tenNhom = reader["TENNHOM"].ToString();
                                string imagePath = reader["ANHNHOM"].ToString();
                                string maNhom = reader["MANHOM"].ToString();
                                int trangThai = Convert.ToInt32(reader["TRANGTHAI"]);
                                UCNHOMPD userControl = new UCNHOMPD(tenNhom, imagePath, maNhom, trangThai);

                               
                               flowLayoutPanel1.Controls.Add(userControl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }
        private void FrmNHOMPD_Load(object sender, EventArgs e)
        {
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                // Lấy lastMANV từ dữ liệu không có điều kiện TRANGTHAI
                string lastMANHOMQuery = "SELECT TOP 1 MANHOM FROM DMNHOMHH ORDER BY MANHOM DESC";
                SqlCommand lastMANHOMCmd = new SqlCommand(lastMANHOMQuery, conn);
                object lastMANHOMResult = lastMANHOMCmd.ExecuteScalar();

                if (lastMANHOMResult != null)
                {
                    string lastMANHOM = lastMANHOMResult.ToString();
                    currentMANHOM = int.Parse(lastMANHOM.Substring(3)) + 1;
                }
                else
                {
                    currentMANHOM = 1; // Nếu không có dữ liệu thì giá trị khởi tạo là 1
                }
                string sql = "SELECT MANHOM, ANHNHOM, TENNHOM, TRANGTHAI FROM DMNHOMHH ORDER BY MANHOM ASC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 4. Tạo và hiển thị UserControl dựa trên dữ liệu
                            string maNhom = reader["MANHOM"].ToString();
                            string tenNhom = reader["TENNHOM"].ToString();
                            string imagePath = reader["ANHNHOM"].ToString();
                            int trangThai = Convert.ToInt32(reader["TRANGTHAI"]);
                            //originalImage = imagePath;
                            //if (!string.IsNullOrEmpty(imagePath))
                            //{
                            //    picproduct.Image = Image.FromFile(imagePath);
                            //}

                            UCNHOMPD userControl = new UCNHOMPD(tenNhom, imagePath, maNhom, trangThai);

                            // Tạo đối tượng UserControlInfo để lưu trữ thông tin
                           

                            userControl.UserControlClick += UserControl_Click; // Xử lý sự kiện khi UserControl được nhấp vào

                            // Đặt sự kiện cho nút btnEDIT trên UserControl
                            userControl.EditButtonClicked += Uc_EditButtonClicked;
                            userControl.DelButtonClicked += Uc_DelButtonClicked;
                          

                            flowLayoutPanel1.Controls.Add(userControl);


                        }
                    }
                }
            }
        }
      

        private void Uc_EditButtonClicked(object sender, EventArgs e)
        {
          
            // Kiểm tra xem sender có phải là một UserControl không
            if (sender is UCNHOMPD)
            {
                // Ép kiểu sender thành UCNHOMPD để có thể truy cập các thuộc tính của UserControl đó
                UCNHOMPD uc = (UCNHOMPD)sender;

                string maNhom = uc.MANHOM;
                // Tạo một instance của FrmCTNHOMHH và truyền thông tin chi tiết
                FrmCTNHOMHH frmCTNHOMHH = new FrmCTNHOMHH(maNhom, this);
                //frmCTNHOMHH.picproduct = anhNhom;
                // Truyền thêm thông tin khác nếu cần
                // ...
                //  string initialComNhomValue = frmCTPRODUCT.comNhom.Text;

                frmCTNHOMHH.txtMANHOM.Focus();
                frmCTNHOMHH.txtMANHOM.ReadOnly = false;
                frmCTNHOMHH.txtTENNHOM.ReadOnly = false;


                frmCTNHOMHH.txtMANHOM.Enabled = true;
                frmCTNHOMHH.txtTENNHOM.Enabled = true;

                frmCTNHOMHH.btncapnhat.Visible = true;
                frmCTNHOMHH.btnluu.Visible = false;
                frmCTNHOMHH.ShowDialog();
            }
        }
 

        private void Uc_DelButtonClicked(object sender, EventArgs e)
        {
            if (sender is UCNHOMPD )
            {

                UCNHOMPD uc = (UCNHOMPD)sender;
                string maNhom = uc.MANHOM;

                if (MessageBox.Show("Bạn có chắc muốn xóa nhóm sản phẩm này?", "Xác nhận xóa",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Xóa bản ghi tương ứng trong CSDL
                        using (SqlConnection conn = new SqlConnection(constr))
                        {
                            conn.Open();
                            string deleteQuery1 = "UPDATE DMNHOMHH SET TRANGTHAI = 0 WHERE MANHOM = @MaNhom";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery1, conn))
                            {
                                deleteCommand.Parameters.AddWithValue("@MaNhom", maNhom);
                                deleteCommand.ExecuteNonQuery();
                            }
                        }

                        // Xóa UserControl khỏi flowLayoutPanel1
                        flowLayoutPanel1.Controls.Remove(uc);

                        // Giải phóng tài nguyên của UserControl
                        uc.Dispose();

                        // Thực hiện các công việc khác sau khi xóa UserControl
                    }
                }
            }
        

        private void UserControl_Click(object sender, EventArgs e)
        {
            // Lấy UserControl đã được nhấp vào
            UCNHOMPD clickedControl = (UCNHOMPD)sender;

            // Lấy thông tin từ UserControl
            string clickedMaNhom = clickedControl.MANHOM;

            // Tạo FrmCTNHOMHH và hiển thị thông tin chi tiết
            FrmCTNHOMHH frmCTNHOMHH = new FrmCTNHOMHH(clickedMaNhom, this);

            // Tùy chỉnh hiển thị và ReadOnly tại đây (nếu cần)
            frmCTNHOMHH.btncapnhat.Visible = false;
            frmCTNHOMHH.btnluu.Visible = false;
            frmCTNHOMHH.txtMANHOM.ReadOnly = true;
            frmCTNHOMHH.txtTENNHOM.ReadOnly = true;
            frmCTNHOMHH.txtMANHOM.Enabled = false;
            frmCTNHOMHH.txtTENNHOM.Enabled = false;

            frmCTNHOMHH.ShowDialog();
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
                    case "Mã nhóm":
                        filterColumnName = "MANHOM";
                        break;
                    case "Tên nhóm":
                        filterColumnName = "TENNHOM";
                        break;


                    default:
                        break;
                }
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = $"SELECT MANHOM, TENNHOM, ANHNHOM, TRANGTHAI FROM DMNHOMHH";


                    if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(keyword))
                    {
                        sql += $" WHERE {filterColumnName} LIKE N'%{keyword}%'";
                    }
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            flowLayoutPanel1.Controls.Clear();

                            while (reader.Read())
                            {
                                // 4. Tạo và hiển thị UserControl dựa trên dữ liệu
                                string maNhom = reader["MANHOM"].ToString();
                                string tenNhom = reader["TENNHOM"].ToString();
                                string imagePath = reader["ANHNHOM"].ToString();
                                int trangThai = Convert.ToInt32(reader["TRANGTHAI"]);
                                //originalImage = imagePath;
                                //if (!string.IsNullOrEmpty(imagePath))
                                //{
                                //    picproduct.Image = Image.FromFile(imagePath);
                                //}

                                UCNHOMPD userControl = new UCNHOMPD(tenNhom, imagePath, maNhom, trangThai);

                                // Tạo đối tượng UserControlInfo để lưu trữ thông tin


                                userControl.UserControlClick += UserControl_Click; // Xử lý sự kiện khi UserControl được nhấp vào

                                // Đặt sự kiện cho nút btnEDIT trên UserControl
                                userControl.EditButtonClicked += Uc_EditButtonClicked;
                                userControl.DelButtonClicked += Uc_DelButtonClicked;


                                flowLayoutPanel1.Controls.Add(userControl);


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comFilter.SelectedItem != null)
            {
                if (comFilter.SelectedItem.ToString() == "Tất cả")
                {

                    constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string sql = "SELECT MANHOM, TENNHOM,ANHNHOM, TRANGTHAI FROM DMNHOMHH " +
                    "order by MANHOM";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // 4. Tạo và hiển thị UserControl dựa trên dữ liệu
                                    string maNhom = reader["MANHOM"].ToString();
                                    string tenNhom = reader["TENNHOM"].ToString();
                                    string imagePath = reader["ANHNHOM"].ToString();
                                    int trangThai = Convert.ToInt32(reader["TRANGTHAI"]);
                                    //originalImage = imagePath;
                                    //if (!string.IsNullOrEmpty(imagePath))
                                    //{
                                    //    picproduct.Image = Image.FromFile(imagePath);
                                    //}

                                    UCNHOMPD userControl = new UCNHOMPD(tenNhom, imagePath, maNhom, trangThai);

                                    // Tạo đối tượng UserControlInfo để lưu trữ thông tin


                                    userControl.UserControlClick += UserControl_Click; // Xử lý sự kiện khi UserControl được nhấp vào

                                    // Đặt sự kiện cho nút btnEDIT trên UserControl
                                    userControl.EditButtonClicked += Uc_EditButtonClicked;
                                    userControl.DelButtonClicked += Uc_DelButtonClicked;


                                    flowLayoutPanel1.Controls.Add(userControl);


                                }
                            }
                        }
                    }
                }
                else
                {
                    FilterData();
                }
            }
        }

        
       

        

        public FrmNHOMPD()
        {
            InitializeComponent();
           // grdData.CellClick += grdData_CellClick;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmCTNHOMHH frmCTNHOMHH = new FrmCTNHOMHH(null, this);//

            frmCTNHOMHH.txtTENNHOM.Focus();
      
            frmCTNHOMHH.txtTENNHOM.ReadOnly = false;

            frmCTNHOMHH.txtMANHOM.Enabled = false;
         
            frmCTNHOMHH.txtTENNHOM.Enabled = true;



            frmCTNHOMHH.btncapnhat.Visible = false;
            frmCTNHOMHH.btnluu.Visible = true;
            frmCTNHOMHH.changepic.Visible = true;

            frmCTNHOMHH.MANHOM = "NHH" + currentMANHOM.ToString("D2");
            frmCTNHOMHH.txtMANHOM.Text = frmCTNHOMHH.MANHOM;
            frmCTNHOMHH.ShowDialog();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
