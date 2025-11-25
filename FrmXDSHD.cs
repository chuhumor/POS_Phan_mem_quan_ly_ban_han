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
    public partial class FrmXDSHD : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();



        string sql, constr;
        public FrmXDSHD()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void btnXUAT_Click(object sender, EventArgs e)
        {

            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                if (btnXUATALL.Checked)
                {
                    Tungay.Enabled = false;
                    Denngay.Enabled = false;

                    // Tạo câu truy vấn SQL để lấy dữ liệu
                    string queryHH = "SELECT MAHDB, TENKH,NGAYBAN,TENNV, TONGTIEN,DMHDB.SL FROM DMHDB " +
                    "LEFT JOIN DMKH ON DMKH.MAKH = DMHDB.MAKH " +
                    "LEFT JOIN DMNV ON DMNV.MANV = DMHDB.MANV" +
                    " ORDER BY MAHDB DESC";

                    using (SqlCommand cmd = new SqlCommand(queryHH, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtDSHDB");

                                // Tạo đối tượng báo cáo
                                rptDSHDBAN r = new rptDSHDBAN();

                                // Gán DataSet vào báo cáo
                                r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTDSHDB f = new FrmRPTDSHDB();
                                f.crystalReportViewer1.ReportSource = r;
                                f.ShowDialog();
                            }
                        }
                    }
                }
                else if (btnXUATLE.Checked)
                { 
                    Tungay.Enabled = true;
                    Denngay.Enabled = true;
                    DateTime ngayBanDau = Tungay.Value.Date;

                    ngayBanDau = ngayBanDau.AddHours(0).AddMinutes(0).AddSeconds(0);

                    DateTime ngayKetThuc = Denngay.Value.Date;

                    ngayKetThuc = ngayKetThuc.AddHours(23).AddMinutes(59).AddSeconds(59);

                    string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd HH:mm:ss");
                    string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd HH:mm:ss");

                  
                    string queryHH = $"SELECT MAHDB, TENKH, NGAYBAN, TENNV, TONGTIEN, DMHDB.SL FROM DMHDB " +
                                    $"LEFT JOIN DMKH ON DMKH.MAKH = DMHDB.MAKH " +
                                    $"LEFT JOIN DMNV ON DMNV.MANV = DMHDB.MANV " +
                                    $"WHERE NGAYBAN BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
                                    $"ORDER BY MAHDB DESC";
               


                    using (SqlCommand cmd = new SqlCommand(queryHH, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtDSHDB");

                                // Tạo đối tượng báo cáo
                                rptDSHDBAN r = new rptDSHDBAN();

                                // Gán DataSet vào báo cáo
                                r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTDSHDB f = new FrmRPTDSHDB();
                                f.crystalReportViewer1.ReportSource = r;
                                f.ShowDialog();
                            }
                        }
                    }
                }

            }

        }

        private void btnXUATN_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                if (btnXUATALL.Checked)
                {
                    Tungay.Enabled = false;
                    Denngay.Enabled = false;

                    // Tạo câu truy vấn SQL để lấy dữ liệu
                    string queryHH = "SELECT MAHDN,NGAYNHAP,TENNCC,TENNV, TONGTIEN, DMHDN.SL FROM DMHDN " +
                    "LEFT JOIN DMNHACC ON DMNHACC.MANCC = DMHDN.MANCC " +
                    "LEFT JOIN DMNV ON DMNV.MANV = DMHDN.MANV" +
                    " ORDER BY MAHDN DESC";

                    using (SqlCommand cmd = new SqlCommand(queryHH, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtDSHDN");

                                // Tạo đối tượng báo cáo
                                rptDSHDNHAP r = new rptDSHDNHAP();

                                // Gán DataSet vào báo cáo
                                r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTDSHDN f = new FrmRPTDSHDN();
                                f.crystalReportViewer1.ReportSource = r;
                                f.ShowDialog();
                            }
                        }
                    }
                }
                else if (btnXUATLE.Checked)
                {
                    Tungay.Enabled = true;
                    Denngay.Enabled = true;
                    DateTime ngayBanDau = Tungay.Value.Date;

                    ngayBanDau = ngayBanDau.AddHours(0).AddMinutes(0).AddSeconds(0);

                    DateTime ngayKetThuc = Denngay.Value.Date;

                    ngayKetThuc = ngayKetThuc.AddHours(23).AddMinutes(59).AddSeconds(59);

                    string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd HH:mm:ss");
                    string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd HH:mm:ss");


                    string queryHH = $"SELECT MAHDN,NGAYNHAP,TENNCC,TENNV, TONGTIEN, DMHDN.SL FROM DMHDN " +
                    $"LEFT JOIN DMNHACC ON DMNHACC.MANCC = DMHDN.MANCC " +
                    $"LEFT JOIN DMNV ON DMNV.MANV = DMHDN.MANV " +
                    $"WHERE NGAYNHAP BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
                   $"ORDER BY MAHDN DESC";


                    using (SqlCommand cmd = new SqlCommand(queryHH, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtDSHDN");

                                // Tạo đối tượng báo cáo
                                rptDSHDNHAP r = new rptDSHDNHAP();

                                // Gán DataSet vào báo cáo
                                r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTDSHDN f = new FrmRPTDSHDN();
                                f.crystalReportViewer1.ReportSource = r;
                                f.ShowDialog();
                            }
                        }
                    }
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
           // f?.RefreshDataGrid();
        }

        private void FrmXDSHDB_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }
    }
}
