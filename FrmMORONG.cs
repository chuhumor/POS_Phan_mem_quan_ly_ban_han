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
using System.Windows.Forms.DataVisualization.Charting;

namespace QLBDS
{
    public partial class FrmMORONG : Form
    {


        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        public FrmMORONG()
        {
            InitializeComponent();
        }

        private void FrmMORONG_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {


                //doan chuong trinh can bay loi
                //3 dong dau dùng để thiet lap den CSDL QLBDS 
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();

             comNhom.Text = "Theo doanh thu";

               // LoadDT();



            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }

        private void comNhom_SelectedIndexChanged(object sender, EventArgs e)
        { chart4.Series.Clear();
            grdTOP.DataSource = null;
            grdTOP.Rows.Clear();
            if (comNhom.SelectedItem.ToString() == "Theo doanh thu")
            {
               

                LoadDT();
            }
            else if (comNhom.SelectedItem.ToString() == "Theo doanh số")
            {
             
                LoadDS();
            }
          
        }

        private void LoadDT()
        {
            sql = "SELECT TOP(10) with ties" +

         " DMCTHDB.MAHH, TENHH, SUM(THANHTIEN) AS DOANHTHU" +

        " FROM DMHH, DMCTHDB, DMHDB" +

       " WHERE DMHH.MAHH = DMCTHDB.MAHH AND DMCTHDB.MAHDB = DMHDB.MAHDB AND month(NGAYBAN) = MONTH(GETDATE()) AND YEAR(NGAYBAN) = YEAR(GETDATE())" +

         " GROUP BY  DMCTHDB.MAHH, TENHH" +
        " ORDER BY DOANHTHU DESC";
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();

            da.Fill(ds, "top10");

            // Assign the data to the grid
            grdTOP.Columns.Clear();
            //Tạo các cột mới cho "Theo doanh số"
            DataGridViewColumn d = new DataGridViewTextBoxColumn();
            d.DataPropertyName = "col";
            d.HeaderText = "";
            d.Width = 50;
            grdTOP.Columns.Add(d);

            DataGridViewColumn maHHColumn = new DataGridViewTextBoxColumn();
            maHHColumn.DataPropertyName = "MAHH";
            maHHColumn.HeaderText = "Mã Hàng hóa";
            maHHColumn.Width = 120;
            grdTOP.Columns.Add(maHHColumn);

            DataGridViewColumn tenHHColumn = new DataGridViewTextBoxColumn();
            tenHHColumn.DataPropertyName = "TENHH";
            tenHHColumn.HeaderText = "Tên hàng hóa";
            tenHHColumn.Width = 180;

            grdTOP.Columns.Add(tenHHColumn);

            DataGridViewColumn doanhSoColumn = new DataGridViewTextBoxColumn();
            doanhSoColumn.DataPropertyName = "DOANHTHU";
            doanhSoColumn.HeaderText = "Doanh thu";
            doanhSoColumn.Width = 120;
            grdTOP.Columns.Add(doanhSoColumn);

            DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
            d1.DataPropertyName = "col1";
            d1.HeaderText = "";
            d1.Width = 23;
            grdTOP.Columns.Add(d1);

            // Assign the data to the grid
            grdTOP.DataSource = ds.Tables["top10"];
           // grdTOP.Columns["DOANHSO"].Visible = false;
          //  grdTOP.Columns["DVT"].Visible = false;

            // grdTOP.Columns["IsTotal"].Visible = false;

            // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
            chart4.DataSource = ds.Tables["top10"];
            Series series4 = chart4.Series.FirstOrDefault(s => s.Name == "Series4");

            if (series4 == null)
            {
                series4 = new Series("Series4");
                chart4.Series.Add(series4);
            }
           // Series series4 = chart4.Series["Series4"];
            series4.ChartType = SeriesChartType.Pie;
            series4.Palette = ChartColorPalette.Pastel;
            series4.Name = "SANPHAM";

            var chart = chart4;
            chart.Series[series4.Name].XValueMember = "TENHH";
            chart.Series[series4.Name].YValueMembers = "DOANHTHU";
            chart.Series[0].IsValueShownAsLabel = true;
        }

        private void time_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();



                // Tạo câu truy vấn SQL để lấy dữ liệu
                string queryNV = $"WITH DoanhThu AS(" +
                    $" SELECT" +
                    $" CONVERT(date, NGAYBAN) AS NgayBan," +
                    $" ISNULL(SUM(TONGTIEN), 0) AS DoanhThu" +
                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
                    $" CONVERT(date, NGAYBAN) BETWEEN '2023-10-15' AND '2023-11-15' " +
                    $"GROUP BY" +
                    $" CONVERT(date, NGAYBAN)" +
                    $" )," +
                    $" ChiPhi AS(" +
                    $" SELECT" +
                    $" CONVERT(date, NGAYBAN) AS NgayBan," +
                    $" ISNULL(SUM(DMCTHDB.SL* GIANHAP), 0) AS ChiPhi," +
                    $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang" +
                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
                    $" WHERE" +
                    $" CONVERT(date, NGAYBAN) BETWEEN '2023-10-15' AND '2023-11-15' " +
                    $"GROUP BY" +
                    $" CONVERT(date, NGAYBAN)" +
                    $" )," +
                    $" sldon as(" +
                    $" select CONVERT(date, NGAYBAN) as NgayBan, count(mahdb) as sl" +
                    $" from dmhdb" +
                    $" where CONVERT(date, NGAYBAN) BETWEEN '2023-10-15' AND '2023-11-15' " +

                    $" group by CONVERT(date, NGAYBAN)" +
                    $" )" +
                    $" SELECT" +
                    $" CAST(DoanhThu.NgayBan AS NVARCHAR(10)) AS NgayBan," +
                    $" DoanhThu.DoanhThu," +
                    $" DoanhThu.DoanhThu - ChiPhi.ChiPhi AS LoiNhuanGop," +
                    $" sldon.sl as sldon," +
                    $" ChiPhi.TienHang " +
                    $" FROM" +
                    $" DoanhThu" +
                    $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
                    $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
                  
                  
                    $" ORDER BY" +
                    // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
                    $" NgayBan DESC";

                using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            // Đổ dữ liệu từ CSDL vào DataSet
                            da.Fill(ds, "dtBCTIME");

                            // Tạo đối tượng báo cáo
                            rptBCTIME r = new rptBCTIME();

                            // Gán DataSet vào báo cáo
                            r.SetDataSource(ds);

                            // Hiển thị báo cáo
                            FrmRPTBCTIME f = new FrmRPTBCTIME();
                            f.crystalReportViewer1.ReportSource = r;
                            f.ShowDialog();
                        }
                    }
                }

            }
        }

        private void LoadDS()
        {
            sql = "SELECT TOP(10) with ties" +

         " DMCTHDB.MAHH, TENHH, SUM(DMCTHDB.SL) AS DOANHSO, DVT" +

        " FROM DMHH, DMCTHDB, DMHDB" +

       " WHERE DMHH.MAHH = DMCTHDB.MAHH AND DMCTHDB.MAHDB = DMHDB.MAHDB AND month(NGAYBAN) = MONTH(GETDATE()) AND YEAR(NGAYBAN) = YEAR(GETDATE())" +

         " GROUP BY  DMCTHDB.MAHH, TENHH, DVT" +
        " ORDER BY DOANHSO DESC";
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();

            da.Fill(ds, "top10");
            // Assign the data to the grid
            grdTOP.Columns.Clear();
            //Tạo các cột mới cho "Theo doanh số"
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
            d2.DataPropertyName = "col";
            d2.HeaderText = "";
            d2.Width = 50;
            grdTOP.Columns.Add(d2);

            DataGridViewColumn maHHColumn = new DataGridViewTextBoxColumn();
            maHHColumn.DataPropertyName = "MAHH";
            maHHColumn.HeaderText = "Mã Hàng hóa";
            maHHColumn.Width = 100;

            grdTOP.Columns.Add(maHHColumn);

            DataGridViewColumn tenHHColumn = new DataGridViewTextBoxColumn();
            tenHHColumn.DataPropertyName = "TENHH";
            tenHHColumn.HeaderText = "Tên hàng hóa";
            tenHHColumn.Width = 180;

            grdTOP.Columns.Add(tenHHColumn);

            DataGridViewColumn doanhSoColumn = new DataGridViewTextBoxColumn();
            doanhSoColumn.DataPropertyName = "DOANHSO";
            doanhSoColumn.HeaderText = "Doanh số";
            doanhSoColumn.Width = 100;
            grdTOP.Columns.Add(doanhSoColumn);

            DataGridViewColumn dvtColumn = new DataGridViewTextBoxColumn();
            dvtColumn.DataPropertyName = "DVT";
            dvtColumn.HeaderText = "ĐVT";
            dvtColumn.Width = 70;

            grdTOP.Columns.Add(dvtColumn);

            DataGridViewColumn d3 = new DataGridViewTextBoxColumn();
            d3.DataPropertyName = "col1";
            d3.HeaderText = "";
            d3.Width = 20;
            grdTOP.Columns.Add(d3);
            //// Tạo các cột mới cho "Theo doanh số"
            //grdTOP.Columns.Add("MAHH", "Mã hàng hóa");
            //grdTOP.Columns.Add("TENHH", "Tên hàng hóa");
            //grdTOP.Columns.Add("DOANHSO", "Doanh số");
            //grdTOP.Columns.Add("DVT", "ĐVT");

            // Ẩn cột DOANHTHU (nếu có)
            if (grdTOP.Columns.Contains("DOANHTHU"))
            {
                grdTOP.Columns["DOANHTHU"].Visible = false;
            }

            // Đổ dữ liệu vào bảng
            grdTOP.DataSource = ds.Tables["top10"];

            // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
            chart4.DataSource = ds.Tables["top10"];
            //Series series4 = chart4.Series["Series4"];
            Series series4 = chart4.Series.FirstOrDefault(s => s.Name == "Series4");

            if (series4 == null)
            {
                series4 = new Series("Series4");
                chart4.Series.Add(series4);
            }
            series4.ChartType = SeriesChartType.Pie;
            series4.Palette = ChartColorPalette.Pastel;
            series4.Name = "SANPHAM";

            var chart = chart4;
            chart.Series[series4.Name].XValueMember = "TENHH";
            chart.Series[series4.Name].YValueMembers = "DOANHSO";
            chart.Series[0].IsValueShownAsLabel = true;
        }
    }
}
