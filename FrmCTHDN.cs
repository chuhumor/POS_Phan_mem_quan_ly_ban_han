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
using System.Globalization;

namespace QLBDS
{
    public partial class FrmCTHDN : Form
    {
        private string maHDN;
        private string maNCC;

        public string TenNV;
         SqlConnection conn = new SqlConnection();
         SqlDataAdapter da = new SqlDataAdapter();
         DataTable dt = new DataTable();
        FrmLOGIN f;
        string sql, constr;
        //  int i;       
 public double tongTien =0;
        //  Boolean addnewflag = false;

        public FrmCTHDN(string maHDN)
        {
            InitializeComponent();
            this.maHDN = maHDN;
            
        }

        private void btnTHANHTOANNOT_Click(object sender, EventArgs e)
        {
            string maHDN = MAHDN.Text;
           // int.TryParse(lblCONPHAITRA.Text, out int conPhaiTra);
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                string ncc = $"select MANCC, PHAITRA FROM DMHDN " +
                  $" WHERE DMHDN.MAHDN = '{maHDN}'";
                
  SqlCommand cmd = new SqlCommand(ncc, conn);

           
          
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string maNCC = reader["MANCC"].ToString();
                   string conPhaiTra1 = reader["PHAITRA"].ToString();

                    int conPhaiTra = Convert.ToInt32(conPhaiTra1);

                    FrmTHANHTOAN f = new FrmTHANHTOAN(maHDN, maNCC, conPhaiTra);//
            f.maNCC = maNCC;
                    f.conPhaiTra = conPhaiTra;
                  f.ShowDialog();  
            } 
            
       
            reader.Close();
            }



        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnback_Click(object sender, EventArgs e)
        {
            // Đóng FrmCTHDN
            this.Close();

            // Tạo lại FrmHDNHAP
            FrmHDNHAP frmHDNHAP = new FrmHDNHAP(f);
            frmHDNHAP.TopLevel = false;
            frmHDNHAP.FormBorderStyle = FormBorderStyle.None;
            frmHDNHAP.Dock = DockStyle.Fill;

            // Truy cập đến panel 'panelbody' của FrmMAIN và thay thế FrmCTHDN bằng FrmHDNHAP
            FrmMAIN mainForm = Application.OpenForms.OfType<FrmMAIN>().FirstOrDefault();
            if (mainForm != null)
            {
                Guna.UI2.WinForms.Guna2Panel panelBody = mainForm.PanelBody;
                panelBody.Controls.Clear();
                panelBody.Controls.Add(frmHDNHAP);
                frmHDNHAP.Show();
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TENNCC_Click(object sender, EventArgs e)
        {

        }
        private void grdData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột hiện tại có phải là cột "NGAYBAN" không
            if (grdData.Columns[e.ColumnIndex].Name == "NGAYNHAP")
            {
                // Kiểm tra xem giá trị của ô hiện tại có thể chuyển đổi thành DateTime không
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime ngayNhap))
                {
                    // Định dạng lại giá trị của ô theo định dạng "dd/MM/yyyy HH:mm:ss"
                    e.Value = ngayNhap.ToString("dd/MM/yyyy HH:mm:ss");
                    e.FormattingApplied = true;
                }
            }
        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();



                string queryNV = $"SELECT DMHDN.MAHDN, NGAYNHAP, TENNCC, SDT, TENHH, DMCTHDN.SL AS SL, DVT, DONGIA, THANHTIEN, DMCTHDN.CHIETKHAU AS CHIETKHAU, TONGTIEN, DMHDN.SL AS TONGSL, DATRA, PHAITRA, DMHDN.CHIETKHAU AS CHIEUKHAUALL FROM DMHDN, DMCTHDN,DMHH, DMNHACC " +
                          $"WHERE DMHDN.MAHDN = DMCTHDN.MAHDN AND DMCTHDN.MAHH = DMHH.MAHH AND DMHDN.MANCC = DMNHACC.MANCC" +
                          $" AND DMHDN.MAHDN = '{MAHDN.Text}'";


                using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            // Đổ dữ liệu từ CSDL vào DataSet
                            da.Fill(ds, "dtCTHDN");

                            // Tạo đối tượng báo cáo
                            rptCTHDN r = new rptCTHDN();
                          

                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                            // Hiển thị báo cáo
                            FrmRPTCTHDN f = new FrmRPTCTHDN();
                            f.crystalReportViewer1.ReportSource = r;
                            //    f.crystalReportViewer1.ZoomMode = 30;
                            f.ShowDialog();
                        }
                    }
                }

            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void FrmCTHDN_Load(object sender, EventArgs e)
        {
            //try
            //{ grdData.CellFormatting += new DataGridViewCellFormattingEventHandler(grdData_CellFormatting);
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            conn.ConnectionString = constr;

                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryHoaDonNhap = $"WITH A as(" +
                    $" SELECT MAHDN," +
                    $" NGAYNHAP," +
                    $" TENNCC," +
                    $" TENNV," +
                    $" DMNHACC.DIACHI," +
                    $" TONGTIEN," +
                    $" DATRA," +
                    $" PHAITRA," +
                    $" CHIETKHAU," +
                    $" DMHDN.SL" +
                    $" FROM DMHDN" +
                    $" LEFT JOIN DMNHACC ON DMNHACC.MANCC=DMHDN.MANCC" +
                    $" LEFT JOIN DMNV ON DMHDN.MANV=DMNV.MANV" +
                    $" )," +
                    $" B as(" +
                    $" select dmhdN.mahdN," +
                    $" SUM(THANHTIEN) AS THANHTIEN," +
                    $" ISNULL(sum(TIENTHUE),0) AS TIENTHUE" +
                    $" from DMCTHDN left join dmhdN on dmcthdN.mahdN=dmhdN.mahdN" +
                    $" GROUP BY  DMhdN.MAHDN)" +
                    $" select A.MAHDN," +
                    $" A.DATRA," +
                    $" A.PHAITRA," +
                    $" A.NGAYNHAP," +
                    $" A.TENNCC," +
                    $" A.TENNV," +
                    $" A.DIACHI," +
                    $" A.TONGTIEN," +
                    $" A.CHIETKHAU," +
                    $" A.SL," +
                    $" B.THANHTIEN," +
                    $" B.TIENTHUE * (1-A.CHIETKHAU/100) AS TONGTIENTHUE" +
                    $" FROM A LEFT JOIN B ON A.MAHDN = B.MAHDN" +
                    $" WHERE A.MAHDN = '{maHDN}'";



            //$"SELECT DMHDN.MAHDN, NGAYNHAP, TENNCC, TENNV, DMNHACC.DIACHI, TONGTIEN, DMHDN.CHIETKHAU ,SUM(THANHTIEN) AS THANHTIEN, DATRA,PHAITRA,DMHDN.SL FROM DMHDN" +
            //    $" LEFT JOIN DMNHACC ON DMNHACC.MANCC=DMHDN.MANCC" +
            //    $" LEFT JOIN DMNV ON DMHDN.MANV=DMNV.MANV" +
            //  $" LEFT JOIN DMCTHDN ON DMHDN.MAHDN = DMCTHDN.MAHDN" +
            //    $" WHERE DMHDN.MAHDN = '{maHDN}'" +
            //    $" GROUP BY DMHDN.MAHDN, NGAYNHAP, TENNCC, TENNV, DMNHACC.DIACHI, TONGTIEN, DMHDN.CHIETKHAU, DATRA, PHAITRA,DMHDN.SL";
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryHoaDonNhap, conn);
            SqlDataReader reader = cmd.ExecuteReader();
                
             
                if (reader.Read())
                {
                    MAHDN.Text = reader["MAHDN"].ToString();
                    DateTime ngayNhap = Convert.ToDateTime(reader["NGAYNHAP"]);
                    NGAYNHAP.Text = ngayNhap.ToString("dd/MM/yyyy HH:mm:ss");
                    TENNCC.Text = reader["TENNCC"].ToString();
                    TENNV.Text = reader["TENNV"].ToString();
                    DIACHI.Text = reader["DIACHI"].ToString();
                    TIENPHAITRA.Text = reader["TONGTIEN"].ToString();
                    lblSL.Text = reader["SL"].ToString();

                    // lblDATRA.Text = reader["DATRA"].ToString();
                    // lblCONPHAITRA.Text = reader["PHAITRA"].ToString();
                    string chietKhau = reader["CHIETKHAU"].ToString();
                    string tongTien = reader["THANHTIEN"].ToString();
                    string datra = reader["DATRA"].ToString();
                    string conphaitra = reader["PHAITRA"].ToString();
                string tienThue = reader["TONGTIENTHUE"].ToString();
                lblVAT.Text = (Convert.ToDouble(tienThue)).ToString("C0", new CultureInfo("vi-VN"));
                lblCHIETKHAU.Text = (Convert.ToInt32(chietKhau) * Convert.ToInt32(tongTien) / -100).ToString("C0", new CultureInfo("vi-VN"));
                    lblDATRA.Text = (Convert.ToInt32(datra)).ToString("C0", new CultureInfo("vi-VN"));
                    //  lblCONPHAITRA.Text = (Convert.ToInt32(conphaitra)).ToString("C0", new CultureInfo("vi-VN"));
                    lblCONPHAITRA.Text = conphaitra;

                    if (conphaitra == "0")
                         {
                            btnTHANHTOANNOT.Visible = false;
                            label18.Text = "Đơn nhập hàng thanh toán toàn bộ";

                         }
                    else if(conphaitra != "0")
                        {
                        if (datra == "0")
                        {
                            btnTHANHTOANNOT.Visible = true;
                            label18.Text = "Đơn nhập hàng chưa thanh toán";
                            lblHTTT.Visible = false;

                        }
                        else if(datra != "0") { 
                        btnTHANHTOANNOT.Visible = true;
                            label18.Text = "Đơn nhập hàng thanh toán một phần";
                           

                        }
                        }



                   
                    //if (int.TryParse(TIENPHAITRA.Text, out int tienPhaiTra))
                    //{
                    //    // Định dạng giá trị và hiển thị trong TextBox
                    //    TIENPHAITRA.Text = tienPhaiTra.ToString("C0", new CultureInfo("vi-VN"));
                    //}
                }

                reader.Close();
               
                string queryhttt = $"SELECT MAHDN, httt, SOTIEN, NGAYTT FROM TTDONNHAP" +
                   $" WHERE MAHDN = '{maHDN}'";
                // conn.Open();
                SqlCommand cmd1 = new SqlCommand(queryhttt, conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                StringBuilder resultStringBuilder = new StringBuilder();

                while (reader1.Read())
                {
                    string httt = reader1["httt"].ToString();
                    string sotien = reader1["SOTIEN"].ToString();    
                DateTime ngayTT = Convert.ToDateTime(reader1["NGAYTT"]);

                   // NGAYNHAP.Text = ngayNhap.ToString("dd/MM/yyyy HH:mm:ss");
                    //if (lblCONPHAITRA.Text == "0")
                    //{
                    //    switch (httt)
                    //    {
                    //        case "0":
                    //            resultStringBuilder.AppendLine($"Thanh toán bằng Tiền mặt");
                    //            break;
                    //        case "1":
                    //            resultStringBuilder.AppendLine($"Thanh toán bằng Quẹt thẻ");
                    //            break;
                    //        case "2":
                    //            resultStringBuilder.AppendLine($"Thanh toán bằng Chuyển khoản");
                    //            break;
                    //    }
                    //}
                    //else
                    // {
                    // Ánh xạ giá trị HTTT sang văn bản tương ứng
                    switch (httt)
                        {
                            case "0":
                                resultStringBuilder.AppendLine($"{ngayTT.ToString("dd/MM/yyyy HH:mm:ss")} - Tiền mặt {sotien}");
                                break;
                            case "1":
                                resultStringBuilder.AppendLine($"{ngayTT.ToString("dd/MM/yyyy HH:mm:ss")} - Quẹt thẻ {sotien}");
                                break;
                            case "2":
                                resultStringBuilder.AppendLine($"{ngayTT.ToString("dd/MM/yyyy HH:mm:ss")} - Chuyển khoản {sotien}");
                                break;
                        }
                   // }
                }

                // Gán chuỗi kết quả cho lblHTTT.Text
                lblHTTT.Text = resultStringBuilder.ToString();
                reader1.Close();

                // Hiển thị danh sách chi tiết hóa đơn từ CSDL lên DataGridView
                string queryChiTiet = $"SELECT TENHH, DMCTHDN.SL, DONGIA, CHIETKHAU, THANHTIEN FROM DMCTHDN" +
                $" LEFT JOIN DMHH ON DMHH.MAHH = DMCTHDN.MAHH" +
                $" WHERE MAHDN = '{maHDN}'";
        da.SelectCommand = new SqlCommand(queryChiTiet, conn);
        //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh

        dt.Clear();

                da.Fill(dt);
                // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt

                //câu lệnh này có nghĩa :ô lưới này hãy hiển thị dữ liệu đang có trong bảng dữ liệu dt
                dt.Columns.Add("STT", typeof(int));
                for (int i = 0; i<dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }


                grdData.DataSource = dt;
                if (int.TryParse(TIENPHAITRA.Text, out int phaiTra))
                {
                    // Định dạng giá trị và hiển thị trong TextBox
                    TIENPHAITRA.Text = phaiTra.ToString("C0", new CultureInfo("vi-VN"));
                }



                //  tongTien = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int thanhTien = Convert.ToInt32(row["THANHTIEN"]);
                    tongTien += thanhTien;
                }

                lblTONGTIEN.Text = tongTien.ToString("C0", new CultureInfo("vi-VN"));

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("error:" + err.Message);
            //}
        }
    }
}
