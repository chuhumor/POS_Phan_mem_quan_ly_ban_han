using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.Globalization;

namespace QLBDS
{
    public partial class FrmCTHDB : Form
    {

        private string maHDB;
        public string TenNV;
        public double tongTien =0;

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        FrmLOGIN f;
        string sql, constr;
        //  int i;
        //  Boolean addnewflag = false;
        public FrmCTHDB(string maHDB)
        {
            InitializeComponent();
            this.maHDB = maHDB;

        }
        private void btnback_Click(object sender, EventArgs e)
        {
            // Đóng FrmCTHDB
            this.Close();

            // Tạo lại FrmHDBAN
            FrmHDBAN frmHDBAN = new FrmHDBAN(f);
            frmHDBAN.TopLevel = false;
            frmHDBAN.FormBorderStyle = FormBorderStyle.None;
            frmHDBAN.Dock = DockStyle.Fill;

            // Truy cập đến panel 'panelbody' của FrmMAIN và thay thế FrmCTHDB bằng FrmHDBAN
            FrmMAIN mainForm = Application.OpenForms.OfType<FrmMAIN>().FirstOrDefault();
            if (mainForm != null)
            {
                Guna.UI2.WinForms.Guna2Panel panelBody = mainForm.PanelBody;
                panelBody.Controls.Clear();
                panelBody.Controls.Add(frmHDBAN);
                frmHDBAN.Show();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
              using (SqlConnection conn = new SqlConnection(constr))
             {
             conn.Open();



                string queryNV = $"SELECT DMHDB.MAHDB, NGAYBAN, TENKH, SDT, TENHH, DMCTHDB.SL AS SL, DVT, DONGIA, THANHTIEN, GIAMGIA, TONGTIEN, DMHDB.SL AS TONGSL, HTTT, KHACHDUA, TIENTHUA, CHIETKHAU FROM DMHDB, DMCTHDB,DMHH, DMKH " +
                          $"WHERE DMHDB.MAHDB = DMCTHDB.MAHDB AND DMCTHDB.MAHH = DMHH.MAHH AND DMHDB.MAKH = DMKH.MAKH" +
                          $" AND DMHDB.MAHDB = '{MAHDB.Text}'";


                using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            // Đổ dữ liệu từ CSDL vào DataSet
                            da.Fill(ds, "dtCTHDB");

                            // Tạo đối tượng báo cáo
                            rptCTHDB r = new rptCTHDB();
                            // r.Parameter_KHACHDUA = 
                            //r.SetParameterValue( txtKHACHDUA.Text);
                            //r.SetParameterValue("Parameter_TIENTHUA", lblTIENTHUA.Text);
                            //r.Parameter_KHACHDUA = txtKHACHDUA.Text;

                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                            // Hiển thị báo cáo
                            FrmRPTCTHDB f = new FrmRPTCTHDB();
                            f.crystalReportViewer1.ReportSource = r;
                            //    f.crystalReportViewer1.ZoomMode = 30;
                            f.ShowDialog();
                        }
                    }
                }

            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTONGTIEN_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmCTHDB_Load(object sender, EventArgs e)
        {
            //try
            //{

                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;

                // Hiển thị thông tin hóa đơn bAN từ CSDL lên các label tương ứng
                string queryHoaDonBan = $"WITH A as(" +
                    $" SELECT MAHDB," +
                    $" NGAYBAN," +
                    $" TENKH," +
                    $" TENNV," +
                    $" DMKH.SDT," +
                    $" TONGTIEN," +
                    $" CHIETKHAU," +
                    $" HTTT," +
                    $" DMHDB.SL" +
                    $" FROM DMHDB" +
                    $" LEFT JOIN DMKH ON DMKH.MAKH=DMHDB.MAKH" +
                    $" LEFT JOIN DMNV ON DMHDB.MANV=DMNV.MANV" +
                    $" )," +
                    $" B as(" +
                    $" select dmhdb.mahdb," +
                    $" SUM(THANHTIEN) AS THANHTIEN," +
                    $" ISNULL(sum(TIENTHUE),0) AS TIENTHUE" +
                    $" from DMCTHDB left join dmhdb on dmcthdb.mahdb=dmhdb.mahdb" +
                    $" GROUP BY  DMhdb.MAHDB)" +
                    $" select A.MAHDB," +
                    $" A.NGAYBAN," +
                    $" A.TENKH," +
                    $" A.TENNV," +
                    $" A.SDT," +
                    $" A.TONGTIEN," +
                    $" A.CHIETKHAU," +
                    $" A.HTTT," +
                    $" A.SL," +
                    $" B.THANHTIEN," +
                    $" B.TIENTHUE * (1-A.CHIETKHAU/100) AS TONGTIENTHUE" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" WHERE A.MAHDB = '{maHDB}'";
               

                conn.Open();
                SqlCommand cmd = new SqlCommand(queryHoaDonBan, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    MAHDB.Text = reader["MAHDB"].ToString();

                    // Chuyển đổi ngày tháng thành đối tượng DateTime
                    DateTime ngayBan = Convert.ToDateTime(reader["NGAYBAN"]);
                    NGAYBAN.Text = ngayBan.ToString("dd/MM/yyyy HH:mm:ss");
                    TENKH.Text = reader["TENKH"].ToString();
                    TENNV.Text = reader["TENNV"].ToString();
                    SDT.Text = reader["SDT"].ToString();
                    KHACHPHAITRA.Text = reader["TONGTIEN"].ToString();
                    lblHTTT.Text = reader["HTTT"].ToString();
                    lblSL.Text = reader["SL"].ToString();

                    //  lblCHIETKHAU.Text = reader["CHIETKHAU"].ToString();
                    

                    string chietKhau = reader["CHIETKHAU"].ToString();
                 string tongTien = reader["THANHTIEN"].ToString();
                    string tienThue = reader["TONGTIENTHUE"].ToString();
                    lblVAT.Text = (Convert.ToDouble(tienThue)).ToString("C0", new CultureInfo("vi-VN"));
                    lblCHIETKHAU.Text = (Convert.ToInt32(chietKhau) * Convert.ToInt32(tongTien) /-100).ToString("C0", new CultureInfo("vi-VN"));
                    lblTONGTIEN.Text = (Convert.ToInt32(tongTien)).ToString("C0", new CultureInfo("vi-VN"));
                    switch (lblHTTT.Text)
                    {
                        case "0":
                            lblHTTT.Text = "(Thanh toán bằng Tiền mặt)";
                            break;
                        case "1":
                            lblHTTT.Text = "(Thanh toán bằng Quẹt thẻ)";
                            break;
                        case "2":
                            lblHTTT.Text = "(Thanh toán bằng Chuyển khoản)";
                            break;
                       
                    }

                 

                 
                }

                reader.Close();


                // Hiển thị danh sách chi tiết hóa đơn từ CSDL lên DataGridView
                string queryChiTiet = $"SELECT TENHH,DMCTHDB.SL, DVT, DONGIA, GIAMGIA, THANHTIEN FROM DMCTHDB" +//MAHDB,
                    $" LEFT JOIN DMHH ON DMHH.MAHH = DMCTHDB.MAHH" +
                    $" WHERE MAHDB = '{maHDB}'";
                da.SelectCommand = new SqlCommand(queryChiTiet, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                dt.Clear();
                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                dt.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
                grdData.DataSource = dt;


                if (int.TryParse(KHACHPHAITRA.Text, out int khachPhaiTra))
                {
                    // Định dạng giá trị và hiển thị trong TextBox
                    KHACHPHAITRA.Text = khachPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
                }



                //  tongTien = 0;
                //foreach (DataRow row in dt.Rows)
                //{
                //    int thanhTien = Convert.ToInt32(row["THANHTIEN"]);
                //    tongTien += thanhTien;
                //}

                //lblTONGTIEN.Text = tongTien.ToString("C0", new CultureInfo("vi-VN"));
               

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("error:" + err.Message);
            //}
        }
    }

    
    }

