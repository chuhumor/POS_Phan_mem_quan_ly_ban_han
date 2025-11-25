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
    public partial class FrmBCNH : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        public FrmBCNH()
        {
            InitializeComponent();
        }

        private void FrmBCNH_Load(object sender, EventArgs e)
        {
            try
            {
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                //using (SqlConnection conn = new SqlConnection(constr))
                //{
                conn.ConnectionString = constr;

                conn.Open();
               
                comNhom.Text = "Theo thời gian";

                comDay.Text = "Ngày";
              //  LoadDataForGrid1();
             //   LoadChart1();

            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);

            }
        }

        private void Tungay_ValueChanged(object sender, EventArgs e)
        {
            if (comNhom.SelectedItem.ToString() == "Theo thời gian")
            {
                LoadDataForGrid1();
                LoadChart1();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
            {

                LoadChartEMP();
                LoadGrdEMP();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo nhà cung cấp")
            {

                LoadChartNCC();
                LoadGrdNCC();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            {

                LoadChartPD();
                LoadGrdPD();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo đơn nhập")
            {

                LoadChartHD();
                LoadGrdHD();

            }
        }
        private void Denngay_ValueChanged(object sender, EventArgs e)
        {
            if (comNhom.SelectedItem.ToString() == "Theo thời gian")
            {
                LoadDataForGrid1();
                LoadChart1();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
            {

                LoadChartEMP();
                LoadGrdEMP();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo nhà cung cấp")
            {

                LoadChartNCC();
                LoadGrdNCC();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            {

                LoadChartPD();
                LoadGrdPD();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo đơn nhập")
            {

                LoadChartHD();
                LoadGrdHD();

            }
        }
        string filterType = "Day";

        private void comDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comDay.SelectedItem.ToString() == "Ngày")
            {
                filterType = "Day";


            }
            else if (comDay.SelectedItem.ToString() == "Tháng")
            {
                filterType = "Month";


            }
            else if (comDay.SelectedItem.ToString() == "Năm")
            {
                filterType = "Year";

            }
            // Gọi hàm load dữ liệu khi có sự thay đổi trong ComboBox
            LoadDataForGrid1();
            LoadChart1();
            //LoadDataAndChart();
        }
        private void comNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNhom.SelectedItem.ToString() == "Theo thời gian")
            {
                LoadDataForGrid1();
                LoadChart1();
                chartTIME.Visible = true;
                grdData1.Visible = true;
               // chartEMP.Visible = false;
                grdDataEMP.Visible = false;
               // chartPD.Visible = false;
                grdDataPD.Visible = false;

              //  chartCUS.Visible = false;
                grdDataNCC.Visible = false;
             //   guna2DataGridView1.Visible = false;

                comDay.Visible = true;
                lblDay.Visible = true;
             //   chartHD.Visible = false;
                grdDataHD.Visible = false;

            }
            else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
            {

                LoadChartEMP();
                LoadGrdEMP();
            //    chartEMP.Visible = false;
                grdDataEMP.Visible = true;
                chartTIME.Visible = true;
                grdData1.Visible = false;
                comDay.Visible = false;
             //   chartPD.Visible = false;
               grdDataPD.Visible = false;
                lblDay.Visible = false;
                //  chartCUS.Visible = false;
                grdDataNCC.Visible = false;
                //   chartHD.Visible = false;
                //  grdDataHD.Visible = false;

            }
            else if (comNhom.SelectedItem.ToString() == "Theo đơn nhập")
            {
                   LoadChartHD();
                   LoadGrdHD();
                //   chartHD.Visible = false;
                grdDataHD.Visible = true;
               // chartPD.Visible = false;
                grdDataPD.Visible = false;
                chartTIME.Visible = true;
                grdData1.Visible = false;
               // chartEMP.Visible = false;
                grdDataEMP.Visible = false;
                comDay.Visible = false;
                lblDay.Visible = false;
                //chartNCC.Visible = false;
                grdDataNCC.Visible = false;

            }
            else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            {
                LoadChartPD();
                LoadGrdPD();
                //chartPD.Visible = false;
                grdDataPD.Visible = true;
                chartTIME.Visible = true;
                grdData1.Visible = false;
               // chartEMP.Visible = false;
                grdDataEMP.Visible = false;
                comDay.Visible = false;
                lblDay.Visible = false;
              //  chartCUS.Visible = false;
                grdDataNCC.Visible = false;
              //  chartHD.Visible = false;
                grdDataHD.Visible = false;
            }

            else if (comNhom.SelectedItem.ToString() == "Theo nhà cung cấp")
            {
                LoadChartNCC();
                LoadGrdNCC();
                //chartPD.Visible = false;
                grdDataPD.Visible = false;
                chartTIME.Visible = true;
                grdData1.Visible = false;
               // chartEMP.Visible = false;
                grdDataEMP.Visible = false;
                comDay.Visible = false;
                lblDay.Visible = false;
             //   chartCUS.Visible = false;
                grdDataNCC.Visible = true;
                //chartHD.Visible = false;
                grdDataHD.Visible = false;

            }

        }
        private void LoadDataForGrid1()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            if (filterType == "Day")
            {
                sql = $"WITH A AS(" +
            $" SELECT" +
            $" CONVERT(date, NGAYNHAP) AS NgayNhap," +
            $" mahdN," +
            $" ISNULL(TONGTIEN, 0) AS TongTien," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
            $" )," +
            $" B AS(" +
            $" SELECT" +
            $" dmcthdN.MAHDN," +
            $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDN" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
            $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
            $" GROUP BY" +
            $" dmcthdN.MAHDN" +        
            $" )" +
            $" SELECT" +
            $" CAST(A.NGAYNHAP AS NVARCHAR(20)) AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
         
            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" GROUP BY CONVERT(date, A.NgayNhap)" +
            $" UNION ALL" +
            $" SELECT" +
            $" N'Tổng cộng' AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" ORDER BY" +
            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
            $" NgayNhap DESC";

                cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);

                grdData1.DataSource = dt;

                if (grdData1.Columns.Contains("TENNV"))
                {
                    grdData1.Columns["TENNV"].Visible = false;
                }
                if (grdData1.Columns.Contains("TENHH"))
                {
                    grdData1.Columns["TENHH"].Visible = false;
                }
                if (grdData1.Columns.Contains("TENNCC"))
                {
                    grdData1.Columns["TENNCC"].Visible = false;
                }
                if (grdData1.Columns.Contains("MAHDN"))
                {
                    grdData1.Columns["MAHDN"].Visible = false;
                }
            }
            else if (filterType == "Month")
            {
                sql = $"WITH A AS(" +
            $" SELECT" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'MM/yyyy') AS NgayNhap," +
            $" mahdN," +
            $" ISNULL(TONGTIEN, 0) AS TongTien," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'MM/yyyy') BETWEEN FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy')" +
            $" )," +
            $" B AS(" +
            $" SELECT" +
            $" dmcthdN.MAHDN," +
            $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDN" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
            $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
            $" GROUP BY" +
            $" dmcthdN.MAHDN" +
            $" )" +
            $" SELECT" +
            $" CAST(A.NGAYNHAP AS NVARCHAR(20)) AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +

            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" GROUP BY A.NgayNhap" +
            $" UNION ALL" +
            $" SELECT" +
            $" N'Tổng cộng' AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" ORDER BY" +
            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
            $" NgayNhap DESC";
                cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);
                grdData1.DataSource = dt;

                if (grdData1.Columns.Contains("TENNV"))
                {
                    grdData1.Columns["TENNV"].Visible = false;
                }
                if (grdData1.Columns.Contains("TENHH"))
                {
                    grdData1.Columns["TENHH"].Visible = false;
                }
                if (grdData1.Columns.Contains("TENNCC"))
                {
                    grdData1.Columns["TENNCC"].Visible = false;
                }
                if (grdData1.Columns.Contains("MAHDN"))
                {
                    grdData1.Columns["MAHDN"].Visible = false;
                }
            }
            else if (filterType == "Year")
            {
                sql = $"WITH A AS(" +
            $" SELECT" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy') AS NgayNhap," +
            $" mahdN," +
            $" ISNULL(TONGTIEN, 0) AS TongTien," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy')" +
            $" )," +
            $" B AS(" +
            $" SELECT" +
            $" dmcthdN.MAHDN," +
            $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDN" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
            $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
            $" GROUP BY" +
            $" dmcthdN.MAHDN" +
            $" )" +
            $" SELECT" +
            $" CAST(A.NGAYNHAP AS NVARCHAR(20)) AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +

            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" GROUP BY A.NgayNhap" +
            $" UNION ALL" +
            $" SELECT" +
            $" N'Tổng cộng' AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" ORDER BY" +
            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
            $" NgayNhap DESC";
                cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);
                grdData1.DataSource = dt;

                if (grdData1.Columns.Contains("TENNV"))
                {
                    grdData1.Columns["TENNV"].Visible = false;
                }
                if (grdData1.Columns.Contains("TENHH"))
                {
                    grdData1.Columns["TENHH"].Visible = false;
                }
                if (grdData1.Columns.Contains("TENNCC"))
                {
                    grdData1.Columns["TENNCC"].Visible = false;
                }
                if (grdData1.Columns.Contains("MAHDB"))
                {
                    grdData1.Columns["MAHDB"].Visible = false;
                }
            }

        }
        public void LoadChart1()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");
            if (filterType == "Day")
            {
                sql =
            $" SELECT" +
            $" CONVERT(date, NGAYNHAP) AS NGAYNHAP," +
            $" ISNULL(SUM(TONGTIEN), 0) as TONGTIEN" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
            $"GROUP BY" +
            $" CONVERT(date, NGAYNHAP)" +
            $" ORDER BY NGAYNHAP ASC";
    
                cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                DataSet ds = new DataSet();
                da.Fill(ds, "Sales");

                // Xóa dữ liệu cũ trên biểu đồ
                chartTIME.Series.Clear();

                // Thêm Series cho DoanhThu
                // Series series1 = chartTIME.Series.Add("DoanhThu");
                Series series1 = chartTIME.Series.FirstOrDefault(s => s.Name == "TONGTIEN");

                if (series1 == null)
                {
                    series1 = new Series("TONGTIEN");
                    chartTIME.Series.Add(series1);
                }
                series1.ChartType = SeriesChartType.Column;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<DateTime>("NGAYNHAP")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());
                //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
                //series1.MarkerSize = 8; // Kích thước của điểm

                // Thêm Series cho LoiNhuanGop
                //    Series series2 = chartTIME.Series.Add("LoiNhuanGop");
             
                chartTIME.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";

                // Cập nhật dữ liệu cho biểu đồ

                chartTIME.DataSource = ds.Tables["Sales"];

                // // Lấy đối tượng Axis cho trục hoành
                // Axis xAxis = chartTIME.ChartAreas[0].AxisX;

                ////  Xoay chữ ở góc 30 độ
                //  xAxis.LabelStyle.Angle = 30;

                ////  Loại bỏ chữ trên trục hoành và thêm chữ mới với góc xoay
                //  xAxis.CustomLabels.Clear();
                //  for (int i = 0; i < ds.Tables["Sales"].Rows.Count; i++)
                //  {
                //      DateTime label = ds.Tables["Sales"].Rows[i].Field<DateTime>("NgayBan");

                //   //   Thêm chữ mới với góc xoay
                //      CustomLabel customLabel = new CustomLabel(i + 0.5, i + 1.5, label.ToString("yyyy-MM-dd"), 0, LabelMarkStyle.None);
                //      xAxis.CustomLabels.Add(customLabel);
                //  }

                chartTIME.ChartAreas[0].AxisX.Interval = 1;
                Axis xAxis = chartTIME.ChartAreas[0].AxisX;
                //xAxis.LabelStyle.Angle = 30;
                xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            }
            else if (filterType == "Month")
            {


             
                sql = $" SELECT" +
           $" FORMAT(CONVERT(date, NGAYNHAP), 'MM/yyyy') AS NGAYNHAP," +
           $" ISNULL(SUM(TONGTIEN), 0) as TONGTIEN" +
           $" FROM" +
           $" DMHDN" +
           $" WHERE" +
            $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
           $"GROUP BY" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'MM/yyyy')" +
           $" ORDER BY NGAYNHAP ASC";

                cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                DataSet ds = new DataSet();
                da.Fill(ds, "Sales");

                // Xóa dữ liệu cũ trên biểu đồ
                chartTIME.Series.Clear();

                // Thêm Series cho DoanhThu
                // Series series1 = chartTIME.Series.Add("DoanhThu");
                Series series1 = chartTIME.Series.FirstOrDefault(s => s.Name == "TONGTIEN");

                if (series1 == null)
                {
                    series1 = new Series("TONGTIEN");
                    chartTIME.Series.Add(series1);
                }
                series1.ChartType = SeriesChartType.Column;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NGAYNHAP")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());

                // Thêm Series cho LoiNhuanGop
                //Series series2 = chartTIME.Series.Add("LoiNhuanGop");
                //Series series2 = chartTIME.Series.FirstOrDefault(s => s.Name == "LoiNhuanGop");

                //if (series2 == null)
                //{
                //    series2 = new Series("LoiNhuanGop");
                //    chartTIME.Series.Add(series2);
                //}
                //series2.ChartType = SeriesChartType.Column;
                //series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
                //                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());

                // Cập nhật dữ liệu cho biểu đồ
                chartTIME.DataSource = ds.Tables["Sales"];
                chartTIME.ChartAreas[0].AxisX.Interval = 1;
                Axis xAxis = chartTIME.ChartAreas[0].AxisX;
                //xAxis.LabelStyle.Angle = 30;
                xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

            }
            else if (filterType == "Year")
            {
           
                sql =
                $" SELECT" +
           $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy') AS NGAYNHAP," +
           $" ISNULL(SUM(TONGTIEN), 0) as TONGTIEN" +
           $" FROM" +
           $" DMHDN" +
           $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy') " +
           $"GROUP BY" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy')" +
           $" ORDER BY NGAYNHAP ASC";
                cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da = new SqlDataAdapter(sql, conn);
                //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
                DataSet ds = new DataSet();
                da.Fill(ds, "Sales");

                // Xóa dữ liệu cũ trên biểu đồ
                chartTIME.Series.Clear();

                // Thêm Series cho DoanhThu
                //  Series series1 = chartTIME.Series.Add("DoanhThu");
                Series series1 = chartTIME.Series.FirstOrDefault(s => s.Name == "TONGTIEN");

                if (series1 == null)
                {
                    series1 = new Series("TONGTIEN");
                    chartTIME.Series.Add(series1);
                }
                series1.ChartType = SeriesChartType.Column;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NGAYNHAP")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());
                //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
                //series1.MarkerSize = 8; // Kích thước của điểm

                // Thêm Series cho LoiNhuanGop
                // Series series2 = chartTIME.Series.Add("LoiNhuanGop");
              


                chartTIME.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy";

                // Cập nhật dữ liệu cho biểu đồ
                chartTIME.DataSource = ds.Tables["Sales"];
                Axis xAxis = chartTIME.ChartAreas[0].AxisX;
                //xAxis.LabelStyle.Angle = 30;
                xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            }


        }
        public void LoadChartEMP()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS(" +
        $" SELECT" +
        $" 	MANV," +
        $" ISNULL(SUM(TONGTIEN), 0) AS TONGTIEN" +
        $" FROM" +
        $" DMHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" MANV" +
     
        $" )" +
        $" SELECT" +
        $" TENNV," +
        $" A.TONGTIEN" +
   
        $" FROM" +
        $" A" +

        $" LEFT JOIN DMNV ON A.MANV = dmnv.MANV";


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();
            da.Fill(ds, "Sales");

            // Xóa dữ liệu cũ trên biểu đồ
            chartTIME.Series.Clear();

            // Thêm Series cho DoanhThu
            Series series1 = chartTIME.Series.Add("TONGTIEN");
            series1.ChartType = SeriesChartType.RangeColumn;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENNV")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());
            //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            //series1.MarkerSize = 8; // Kích thước của điểm

            //// Thêm Series cho LoiNhuanGop
            //Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            //series2.ChartType = SeriesChartType.Line;
            //series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENNV")).ToArray(),
            //                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
            //series2.MarkerStyle = MarkerStyle.Circle;
            //series2.MarkerSize = 8;



            // Cập nhật dữ liệu cho biểu đồ
            chartTIME.DataSource = ds.Tables["Sales"];

            chartTIME.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chartTIME.ChartAreas[0].AxisX;
            //xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;



        }
        private void LoadGrdEMP()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS(" +
        $" SELECT" +
        $" MANV," +
        $" CONVERT(date, NGAYNHAP) AS NgayNhap," +
        $" mahdn," +
        $" ISNULL(TONGTIEN, 0) AS TongTien," +
        $" CHIETKHAU," +
        $" SL" +
       
        $" FROM" +
        $" DMHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdN.MAHDN," +
        $" dmhdN.manv," +
        $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
        $" FROM" +
        $" DMCTHDN" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
        $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
      
        $" GROUP BY" +
        $" dmcthdN.MAHDN, dmhdN.manv" +

        $" )" +
        $" SELECT" +
        $" TENNV," +
        $" SUM(A.TongTien) AS TongTien," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(B.TienThue) as TienThue," +
        $" COUNT(A.MAHDn) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
     
        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
        $" LEFT JOIN DMNV ON A.MANV = dmnv.MANV" +
        $" GROUP BY A.MANV, TENNV" +
        $" UNION ALL" +
        $" SELECT" +
        $" N'Tổng cộng' AS TENNV," +
        $" SUM(A.TongTien) AS TongTien," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(B.TienThue) as TienThue," +
        $" COUNT(A.MAHDn) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
        $" FROM" +
        $" A" +
       $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
        $" LEFT JOIN DMNV ON A.MANV = dmnv.MANV";

            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);

            grdDataEMP.DataSource = dt;
            if (grdDataEMP.Columns.Contains("TENHH"))
            {
                grdDataEMP.Columns["TENHH"].Visible = false;
            }
            if (grdDataEMP.Columns.Contains("TENNCC"))
            {
                grdDataEMP.Columns["TENNCC"].Visible = false;
            }
            if (grdDataEMP.Columns.Contains("NGAYNHAP"))
            {
                grdDataEMP.Columns["NGAYNHAP"].Visible = false;
            }
            if (grdDataEMP.Columns.Contains("MAHDN"))
            {
                grdDataEMP.Columns["MAHDN"].Visible = false;
            }
        }
        public void LoadChartNCC()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS(" +
        $" SELECT" +
        $" 	MANCC," +
        $" ISNULL(SUM(TONGTIEN), 0) AS TONGTIEN" +
        $" FROM" +
        $" DMHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" MANCC" +

        $" )" +
        $" SELECT" +
        $" TENNCC," +
        $" A.TONGTIEN" +

        $" FROM" +
        $" A" +

        $" LEFT JOIN DMNHACC ON A.MANCC = DMNHACC.MANCC";


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();
            da.Fill(ds, "Sales");

            // Xóa dữ liệu cũ trên biểu đồ
            chartTIME.Series.Clear();

            // Thêm Series cho DoanhThu
            Series series1 = chartTIME.Series.Add("TONGTIEN");
            series1.ChartType = SeriesChartType.RangeColumn;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENNCC")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());
            //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            //series1.MarkerSize = 8; // Kích thước của điểm

            //// Thêm Series cho LoiNhuanGop
            //Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            //series2.ChartType = SeriesChartType.Line;
            //series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENNV")).ToArray(),
            //                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
            //series2.MarkerStyle = MarkerStyle.Circle;
            //series2.MarkerSize = 8;



            // Cập nhật dữ liệu cho biểu đồ
            chartTIME.DataSource = ds.Tables["Sales"];

            chartTIME.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chartTIME.ChartAreas[0].AxisX;
            //xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;



        }
        private void LoadGrdNCC()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS(" +
        $" SELECT" +
        $" MANCC," +
        $" CONVERT(date, NGAYNHAP) AS NgayNhap," +
        $" mahdn," +
        $" ISNULL(TONGTIEN, 0) AS TongTien," +
        $" CHIETKHAU," +
        $" SL" +

        $" FROM" +
        $" DMHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdN.MAHDN," +
        $" dmhdN.maNCC," +
        $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
        $" FROM" +
        $" DMCTHDN" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
        $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +

        $" GROUP BY" +
        $" dmcthdN.MAHDN, dmhdN.maNCC" +

        $" )" +
        $" SELECT" +
        $" TENNCC," +
        $" SUM(A.TongTien) AS TongTien," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(B.TienThue) as TienThue," +
        $" COUNT(A.MAHDn) AS SLdon," +
        $" SUM(A.SL) as SLhang" +

        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
        $" LEFT JOIN DMNHACC ON A.MANCC = dmNHACC.MANCC" +
        $" GROUP BY A.MANCC, TENNCC" +
        $" UNION ALL" +
        $" SELECT" +
        $" N'Tổng cộng' AS TENNCC," +
        $" SUM(A.TongTien) AS TongTien," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(B.TienThue) as TienThue," +
        $" COUNT(A.MAHDn) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
        $" FROM" +
        $" A" +
       $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
        $" LEFT JOIN DMNHACC ON A.MANCC = DMNHACC.MANCC";

            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);

            grdDataNCC.DataSource = dt;
            if (grdDataNCC.Columns.Contains("TENHH"))
            {
                grdDataNCC.Columns["TENHH"].Visible = false;
            }
            if (grdDataNCC.Columns.Contains("TENNV"))
            {
                grdDataNCC.Columns["TENNV"].Visible = false;
            }
            if (grdDataNCC.Columns.Contains("NGAYNHAP"))
            {
                grdDataNCC.Columns["NGAYNHAP"].Visible = false;
            }
            if (grdDataNCC.Columns.Contains("MAHDN"))
            {
                grdDataNCC.Columns["MAHDN"].Visible = false;
            }
        }
        public void LoadChartPD()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS (" +
       $" SELECT" +
       $" DMCTHDN.MAHH," +
       $" ISNULL(sum(THANHTIEN*(1-DMCTHDN.CHIETKHAU/100)),0) AS TONGTIEN" +
       $" FROM" +
       $" DMCTHDN" +
       $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
       $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
       $" WHERE" +
       $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
       $"GROUP BY" +
       $" 	DMCTHDN.MAhh" +
       $" )" +

       $" SELECT" +
       $" TENHH," +
       $" A.TONGTIEN" +
       $" FROM" +
       $" A" +
       $" LEFT JOIN DMHH ON A.MAHH = DMHH.MAHH";

     
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();
            da.Fill(ds, "Sales");

            // Xóa dữ liệu cũ trên biểu đồ
            chartTIME.Series.Clear();

            // Thêm Series cho DoanhThu
            Series series1 = chartTIME.Series.Add("TONGTIEN");
            series1.ChartType = SeriesChartType.Column;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENHH")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());
            //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            //series1.MarkerSize = 8; // Kích thước của điểm

            //// Thêm Series cho LoiNhuanGop
            //Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            //series2.ChartType = SeriesChartType.Line;
            //series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENHH")).ToArray(),
            //                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
            //series2.MarkerStyle = MarkerStyle.Circle;
            //series2.MarkerSize = 8;



            // Cập nhật dữ liệu cho biểu đồ
            chartTIME.DataSource = ds.Tables["Sales"];
            // Lấy đối tượng Axis cho trục hoành
            //Axis xAxis = chartPD.ChartAreas[0].AxisX;

            //// Xoay chữ ở góc 30 độ
            //xAxis.LabelStyle.Angle = 30;

            //// Loại bỏ chữ trên trục hoành và thêm chữ mới với góc xoay
            //xAxis.CustomLabels.Clear();
            //for (int i = 0; i < ds.Tables["Sales"].Rows.Count; i++)
            //{
            //    string label = ds.Tables["Sales"].Rows[i].Field<string>("TENHH");

            //    // Thêm chữ mới với góc xoay
            //    CustomLabel customLabel = new CustomLabel(i + 0.5, i + 1.5, label, 0, LabelMarkStyle.None);
            //    xAxis.CustomLabels.Add(customLabel);
            //}

            chartTIME.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chartTIME.ChartAreas[0].AxisX;
            //xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;


        }
        private void LoadGrdPD()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS (" +
        $" SELECT" +
        $" DMCTHDN.MAHH," +
        $" sum(THANHTIEN*(1-DMHDN.CHIETKHAU/100)) AS TongTien," +
        $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-dmhdn.CHIETKHAU/100)),0) AS TienThue," +
        $" count(dmcthdn.mahdn) as SLdon," +
        $" sum(dmcthdn.SL) as SLhang" +
        $" FROM" +
        $" DMCTHDN" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
        $" LEFT JOIN DMHDN ON DMCTHDN.MAHDN = DMHDN.MAHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" 	DMCTHDN.MAhh" +
        $" )" +

        $" SELECT" +
        $" TENHH," +
        $" A.TongTien as TongTien," +
        $" A.TienHang as TienHang," +
        $" A.TienThue AS TienThue," +
        $" A.SLdon as SLdon," +
        $" A.SLhang as SLhang" +
     
        $" FROM" +
        $" A" +
        $" LEFT JOIN DMHH ON A.MAHH = DMHH.MAHH" +

        $" UNION ALL" +
        $" SELECT" +
        $" N'Tổng cộng' AS TENHH," +
        $" sum(A.TongTien) as TongTien," +
        $" sum(A.TienHang) as TienHang," +
        $" sum(A.TienThue) AS TienThue," +
        $" sum(A.SLdon) as SLdon," +
        $" sum(A.SLhang) as SLhang" +
        $" FROM" +
        $" A" +
        $" LEFT JOIN DMHH ON A.MAHH = DMHH.MAHH";


            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);

            grdDataPD.DataSource = dt;
            if (grdDataPD.Columns.Contains("TENNV"))
            {
                grdDataPD.Columns["TENNV"].Visible = false;
            }
            if (grdDataPD.Columns.Contains("TENNCC"))
            {
                grdDataPD.Columns["TENNCC"].Visible = false;
            }
            if (grdDataPD.Columns.Contains("NGAYNHAP"))
            {
                grdDataPD.Columns["NGAYNHAP"].Visible = false;
            }
            if (grdDataPD.Columns.Contains("MAHDN"))
            {
                grdDataPD.Columns["MAHDN"].Visible = false;
            }
        }
        public void LoadChartHD()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql =
           $" SELECT" +
           $" MAhdN," +
           $" ISNULL(TONGTIEN, 0) AS TONGTIEN" +
           $" FROM" +
           $" dmhdN" +
           $" WHERE" +
           $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' ";

            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();
            da.Fill(ds, "Sales");

            // Xóa dữ liệu cũ trên biểu đồ
            chartTIME.Series.Clear();

            // Thêm Series cho DoanhThu
            Series series1 = chartTIME.Series.Add("TONGTIEN");
            series1.ChartType = SeriesChartType.Column;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("MAHDN")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("TONGTIEN")).ToArray());
            //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            //series1.MarkerSize = 8; // Kích thước của điểm

            //// Thêm Series cho LoiNhuanGop
            //Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            //series2.ChartType = SeriesChartType.Line;
            //series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("MAHDB")).ToArray(),
            //                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double?>("LoiNhuanGop")).ToArray());
            //series2.MarkerStyle = MarkerStyle.Circle;
            //series2.MarkerSize = 8;



            // Cập nhật dữ liệu cho biểu đồ
            chartTIME.DataSource = ds.Tables["Sales"];

            //}
            chartTIME.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chartTIME.ChartAreas[0].AxisX;
            //xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
        }
        private void LoadGrdHD()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $" WITH A AS(" +
   $" SELECT" +
    $" MAhdN," +
    $" SL AS SLhang," +
    $" CHIETKHAU," +
      $" ISNULL(TONGTIEN, 0) AS TongTien" +
  $" FROM" +
       $" dmhdN" +
   $" WHERE" +
     $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
$" )," +
$" B AS(" +
   $" SELECT" +
   $" dmcthdN.MAhdN," +
    
       $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
       $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
   $" FROM" +
       $" dmcthdN" +
   $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
   $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
   $" GROUP BY dmcthdN.MAhdN" +
  
$" )" +
$" SELECT" +
   $" CAST(A.MAHDN AS NVARCHAR(20)) AS MAHDN," +
   $" isnull(A.SLhang,0) as SLhang," +
   $" isnull(B.TienHang,0) as TienHang," +
   $" isnull(a.TongTien,0) as TongTien," +
   $" isnull(B.TienThue,0) as TienThue" +
  
$" FROM" +
  $" A" +
$" LEFT JOIN B ON A.mahdN = B.MAHDN" +
$" UNION ALL" +
$" SELECT" +

    $" N'Tổng cộng' AS mahdN," +
    $" sum(A.SLhang) as SLhang," +
   $" sum(B.TienHang) as TienHang," +
   $" sum(a.TongTien) as TongTien," +
   $" sum(B.TienThue) as TienThue" +
$" FROM" +
  $" A" +
$" LEFT JOIN B ON A.mahdN = B.MAHDN";


            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);

            grdDataHD.DataSource = dt;
            if (grdDataHD.Columns.Contains("TENNV"))
            {
                grdDataHD.Columns["TENNV"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("NGAYNHAP"))
            {
                grdDataHD.Columns["NGAYNHAP"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("TENHH"))
            {
                grdDataHD.Columns["TENHH"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("TENNCC"))
            {
                grdDataHD.Columns["TENNCC"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("SLdon"))
            {
                grdDataHD.Columns["SLdon"].Visible = false;
            }
        }

     

        private void btnEDIT_Click(object sender, EventArgs e)
        {
            //   string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");
            string constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                if (comNhom.SelectedItem.ToString() == "Theo thời gian")
                {
                    if (filterType == "Day")
                    {
                        // Tạo câu truy vấn SQL để lấy dữ liệu
                        string queryNV = $"WITH A AS(" +
            $" SELECT" +
            $" CONVERT(date, NGAYNHAP) AS NgayNhap," +
            $" mahdN," +
            $" ISNULL(TONGTIEN, 0) AS TongTien," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
            $" )," +
            $" B AS(" +
            $" SELECT" +
            $" dmcthdN.MAHDN," +
            $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDN" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
            $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
            $" GROUP BY" +
            $" dmcthdN.MAHDN" +
            $" )" +
            $" SELECT" +
            $" CAST(A.NGAYNHAP AS NVARCHAR(20)) AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +

            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" GROUP BY CONVERT(date, A.NgayNhap)";


                        using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    // Đổ dữ liệu từ CSDL vào DataSet
                                    da.Fill(ds, "dtBCTIME2");

                                    // Tạo đối tượng báo cáo
                                    rptBCTIME2 r = new rptBCTIME2();

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
                    else if (filterType == "Month")
                    {
                        string queryNV = $"WITH A AS(" +
            $" SELECT" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'MM/yyyy') AS NgayNhap," +
            $" mahdN," +
            $" ISNULL(TONGTIEN, 0) AS TongTien," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'MM/yyyy') BETWEEN FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy')" +
            $" )," +
            $" B AS(" +
            $" SELECT" +
            $" dmcthdN.MAHDN," +
            $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDN" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
            $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
            $" GROUP BY" +
            $" dmcthdN.MAHDN" +
            $" )" +
            $" SELECT" +
            $" CAST(A.NGAYNHAP AS NVARCHAR(20)) AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +

            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" GROUP BY A.NgayNhap";
                        using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    // Đổ dữ liệu từ CSDL vào DataSet
                                    da.Fill(ds, "dtBCTIME2");

                                    // Tạo đối tượng báo cáo
                                    rptBCTIME2 r = new rptBCTIME2();

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
                    else if (filterType == "Year")
                    {
                        string queryNV = $"WITH A AS(" +
            $" SELECT" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy') AS NgayNhap," +
            $" mahdN," +
            $" ISNULL(TONGTIEN, 0) AS TongTien," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" DMHDN" +
            $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYNHAP), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy')" +
            $" )," +
            $" B AS(" +
            $" SELECT" +
            $" dmcthdN.MAHDN," +
            $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDN" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
            $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
            $" GROUP BY" +
            $" dmcthdN.MAHDN" +
            $" )" +
            $" SELECT" +
            $" CAST(A.NGAYNHAP AS NVARCHAR(20)) AS NgayNhap," +
            $" SUM(A.TongTien) AS TongTien," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(B.TienThue) as TienThue," +
            $" COUNT(A.MAHDn) AS SLdon," +
            $" SUM(A.SL) as SLhang" +

            $" FROM" +
            $" A" +
            $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
            $" GROUP BY A.NgayNhap";
                        using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    // Đổ dữ liệu từ CSDL vào DataSet
                                    da.Fill(ds, "dtBCTIME2");

                                    // Tạo đối tượng báo cáo
                                    rptBCTIME2 r = new rptBCTIME2();

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
                else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
                {
                    string queryNV = $"WITH A AS(" +
        $" SELECT" +
        $" MANV," +
        $" CONVERT(date, NGAYNHAP) AS NgayNhap," +
        $" mahdn," +
        $" ISNULL(TONGTIEN, 0) AS TongTien," +
        $" CHIETKHAU," +
        $" SL" +

        $" FROM" +
        $" DMHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdN.MAHDN," +
        $" dmhdN.manv," +
        $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
        $" FROM" +
        $" DMCTHDN" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
        $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +

        $" GROUP BY" +
        $" dmcthdN.MAHDN, dmhdN.manv" +

        $" )" +
        $" SELECT" +
        $" TENNV," +
        $" SUM(A.TongTien) AS TongTien," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(B.TienThue) as TienThue," +
        $" COUNT(A.MAHDn) AS SLdon," +
        $" SUM(A.SL) as SLhang" +

        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
        $" LEFT JOIN DMNV ON A.MANV = dmnv.MANV" +
        $" GROUP BY A.MANV, TENNV";
                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCEMP2");

                                // Tạo đối tượng báo cáo
                                rptBCEMP2 r = new rptBCEMP2();

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
                else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
                {
                    string queryNV = $"WITH A AS (" +
        $" SELECT" +
        $" DMCTHDN.MAHH," +
        $" sum(THANHTIEN*(1-DMHDN.CHIETKHAU/100)) AS TongTien," +
        $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-dmhdn.CHIETKHAU/100)),0) AS TienThue," +
        $" count(dmcthdn.mahdn) as SLdon," +
        $" sum(dmcthdn.SL) as SLhang" +
        $" FROM" +
        $" DMCTHDN" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
        $" LEFT JOIN DMHDN ON DMCTHDN.MAHDN = DMHDN.MAHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" 	DMCTHDN.MAhh" +
        $" )" +

        $" SELECT" +
        $" TENHH," +
        $" A.TongTien as TongTien," +
        $" A.TienHang as TienHang," +
        $" A.TienThue AS TienThue," +
        $" A.SLdon as SLdon," +
        $" A.SLhang as SLhang" +

        $" FROM" +
        $" A" +
        $" LEFT JOIN DMHH ON A.MAHH = DMHH.MAHH";




                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCPD2");

                                // Tạo đối tượng báo cáo
                                rptBCPD2 r = new rptBCPD2();

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
                else if (comNhom.SelectedItem.ToString() == "Theo nhà cung cấp")
                {
                    string queryNV = $"WITH A AS(" +
        $" SELECT" +
        $" MANCC," +
        $" CONVERT(date, NGAYNHAP) AS NgayNhap," +
        $" mahdn," +
        $" ISNULL(TONGTIEN, 0) AS TongTien," +
        $" CHIETKHAU," +
        $" SL" +

        $" FROM" +
        $" DMHDN" +
        $" WHERE" +
        $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdN.MAHDN," +
        $" dmhdN.maNCC," +
        $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
        $" FROM" +
        $" DMCTHDN" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
        $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +

        $" GROUP BY" +
        $" dmcthdN.MAHDN, dmhdN.maNCC" +

        $" )" +
        $" SELECT" +
        $" TENNCC," +
        $" SUM(A.TongTien) AS TongTien," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(B.TienThue) as TienThue," +
        $" COUNT(A.MAHDn) AS SLdon," +
        $" SUM(A.SL) as SLhang" +

        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDn = B.MAHDn" +
        $" LEFT JOIN DMNHACC ON A.MANCC = dmNHACC.MANCC" +
        $" GROUP BY A.MANCC, TENNCC";

                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCNCC");

                                // Tạo đối tượng báo cáo
                                rptBCNCC r = new rptBCNCC();

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
                else if (comNhom.SelectedItem.ToString() == "Theo đơn nhập")
                {
                    string queryNV = $" WITH A AS(" +
   $" SELECT" +
    $" MAhdN," +
    $" SL AS SLhang," +
    $" CHIETKHAU," +
      $" ISNULL(TONGTIEN, 0) AS TongTien" +
  $" FROM" +
       $" dmhdN" +
   $" WHERE" +
     $" CONVERT(date, NGAYNHAP) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
$" )," +
$" B AS(" +
   $" SELECT" +
   $" dmcthdN.MAhdN," +

       $" ISNULL(SUM(DMCTHDN.SL * DONGIA), 0) AS TienHang," +
       $" ISNULL(sum(TIENTHUE*(1-DMHDN.CHIETKHAU/100)),0) AS TienThue" +
   $" FROM" +
       $" dmcthdN" +
   $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDN.mahh" +
   $" LEFT JOIN dmhdN ON DMCTHDN.MAHDN = dmhdN.MAHDN" +
   $" GROUP BY dmcthdN.MAhdN" +

$" )" +
$" SELECT" +
   $" CAST(A.MAHDN AS NVARCHAR(20)) AS MAHDN," +
   $" isnull(A.SLhang,0) as SLhang," +
   $" isnull(B.TienHang,0) as TienHang," +
   $" isnull(a.TongTien,0) as TongTien," +
   $" isnull(B.TienThue,0) as TienThue" +

$" FROM" +
  $" A" +
$" LEFT JOIN B ON A.mahdN = B.MAHDN";


                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCHD2");

                                // Tạo đối tượng báo cáo
                                rptBCHD2 r = new rptBCHD2();

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
        }

    }

}
