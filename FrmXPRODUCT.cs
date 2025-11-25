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
    public partial class FrmXPRODUCT : Form
    {


        //private string selectedNhom;

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

     

        string sql, constr;
     

        public FrmXPRODUCT()
        {
            InitializeComponent();
        }

        private void FrmXPRODUCT_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void btnXUATALL_CheckedChanged(object sender, EventArgs e)
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
                    comNhom.Enabled = false;
                    // Tạo câu truy vấn SQL để lấy dữ liệu
                    string queryHH = "SELECT MAHH, TENHH, GIANHAP, GIABAN, TENNHOM, DVT, SL " +
                                     "FROM DMHH " +
                                     "LEFT JOIN DMNHOMHH ON DMHH.MANHOM = DMNHOMHH.MANHOM " +
                                     "where DMHH.TRANGTHAI =1";

                    using (SqlCommand cmd = new SqlCommand(queryHH, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtPRODUCT");

                                // Tạo đối tượng báo cáo
                                rptDSPRODUCT r = new rptDSPRODUCT();

                                // Gán DataSet vào báo cáo
                                r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTPRODUCT f = new FrmRPTPRODUCT();
                                f.crystalReportViewer1.ReportSource = r;
                                f.ShowDialog();
                            }
                        }
                    }
                }
                else if (btnXUATLE.Checked)
                {
                    string selectedNhom = comNhom.Text;

                    comNhom.Enabled = true;
                    // Tạo câu truy vấn SQL để lấy dữ liệu
                    string queryHH = $"SELECT MAHH, TENHH, GIANHAP, GIABAN, TENNHOM, DVT, SL " +
                                     $"FROM DMHH " +
                                     $"LEFT JOIN DMNHOMHH ON DMHH.MANHOM = DMNHOMHH.MANHOM " +
                                     $"where DMHH.TRANGTHAI =1 AND DMNHOMHH.TENNHOM = N'{selectedNhom}'";

                    using (SqlCommand cmd = new SqlCommand(queryHH, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtPRODUCT");

                                // Tạo đối tượng báo cáo
                                rptDSNHOMPD r = new rptDSNHOMPD();

                                // Gán DataSet vào báo cáo
                                r.SetDataSource(ds);

                                // Hiển thị báo cáo
                                FrmRPTNHOMPD f = new FrmRPTNHOMPD();
                                f.crystalReportViewer2.ReportSource = r;
                                f.ShowDialog();
                            }
                        }
                    }
                }    

            }


        }
    }
}
