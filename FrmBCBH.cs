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
using Excel = Microsoft.Office.Interop.Excel;

namespace QLBDS
{
    public partial class FrmBCBH : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        public FrmBCBH()
        {
            InitializeComponent();
        }

        private void FrmBCBH_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                //using (SqlConnection conn = new SqlConnection(constr))
                //{
                conn.ConnectionString = constr;

                conn.Open();
                // comDay.SelectedItem = "Ngày";
                // comNhom.Text = "Theo nhân viên";
                comNhom.Text = "Theo thời gian";
                
                comDay.Text = "Ngày";
                //LoadChart1();
                // LoadDataForGrid1();
                //LoadDataAndChart();

                // grdDataEMP.Visible = false;
                //chartEMP.Visible = false;
                //  LoadChartEMP();
                // LoadGrdEMP();


            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);

            }
        }
        //private void LoadDataAndChart()
        //{
        //    string sql = string.Empty;
        //    DateTime ngayBanDau = Tungay.Value.Date;
        //    DateTime ngayKetThuc = Denngay.Value.Date;
        //    string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
        //    string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

        //    if (filterType == "day")
        //    {
        //        sql = $"WITH DoanhThu AS(" +
        //            $" SELECT" +
        //            $" CONVERT(date, NGAYBAN) AS NgayBan," +
        //            $" ISNULL(SUM(TONGTIEN), 0) AS DoanhThu" +
        //            $" FROM" +
        //            $" dmhdb" +
        //            $" WHERE" +
        //            $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        //            $"GROUP BY" +
        //            $" CONVERT(date, NGAYBAN)" +
        //            $" )," +
        //            $" ChiPhi AS(" +
        //            $" SELECT" +
        //            $" CONVERT(date, NGAYBAN) AS NgayBan," +
        //            $" ISNULL(SUM(DMCTHDB.SL* GIANHAP), 0) AS ChiPhi," +
        //            $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang" +
        //            $" FROM" +
        //            $" DMCTHDB" +
        //            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        //            $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
        //            $" WHERE" +
        //            $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        //            $"GROUP BY" +
        //            $" CONVERT(date, NGAYBAN)" +
        //            $" )," +
        //            $" sldon as(" +
        //            $" select CONVERT(date, NGAYBAN) as NgayBan, count(mahdb) as sl" +
        //            $" from dmhdb" +
        //            $" where CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
        //            $" group by CONVERT(date, NGAYBAN)" +
        //            $" )" +
        //            $" SELECT" +
        //            $" CAST(DoanhThu.NgayBan AS NVARCHAR(10)) AS NgayBan," +
        //            $" DoanhThu.DoanhThu," +
        //            $" DoanhThu.DoanhThu - ChiPhi.ChiPhi AS LoiNhuanGop," +
        //            $" sldon.sl as sldon," +
        //            $" ChiPhi.TienHang," +
        //            $" CAST(0 AS BIT) AS IsTotal" +
        //            $" FROM" +
        //            $" DoanhThu" +
        //            $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
        //            $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
        //            $" UNION ALL" +
        //            $" SELECT" +
        //            $" N'Tổng cộng' AS NgayBan," +
        //            $" sum(DoanhThu.DoanhThu)," +
        //            $" sum(DoanhThu.DoanhThu - ChiPhi.ChiPhi) AS LoiNhuanGop," +
        //            $" sum(sldon.sl) as sldon," +
        //            $" sum(ChiPhi.TienHang)," +
        //            $" CAST(1 AS BIT) AS IsTotal" +
        //            $" FROM" +
        //            $" DoanhThu" +
        //            $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
        //            $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
        //            $" ORDER BY" +
        //            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
        //            $" NgayBan DESC";


        //        cmd = new SqlCommand(sql, conn);
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        da = new SqlDataAdapter(sql, conn);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "DataAndChart");

        //        // Assign the data to the grid
        //        grdData1.DataSource = ds.Tables["DataAndChart"];
        //        grdData1.Columns["IsTotal"].Visible = false;

        //        // Assign the data to the chart
        //        chartTIME.Series.Clear();

        //        // Add Series for DoanhThu, excluding the total row


        //        //Thêm Series cho DoanhThu
        //        Series series1 = chartTIME.Series.Add("DoanhThu");
        //        series1.ChartType = SeriesChartType.Line;
        //        series1.Points.DataBindXY(ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
        //                                     ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
        //        series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
        //        series1.MarkerSize = 8; // Kích thước của điểm

        //        // Thêm Series cho LoiNhuanGop
        //        Series series2 = chartTIME.Series.Add("LoiNhuanGop");
        //        series2.ChartType = SeriesChartType.Line;
        //        series2.Points.DataBindXY(ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
        //                                         ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
        //        series2.MarkerStyle = MarkerStyle.Circle;
        //        series2.MarkerSize = 8;


        //        //chartTIME.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";

        //        var filteredData = ds.Tables["DataAndChart"].AsEnumerable()
        //            .Where(row => !row.Field<bool>("IsTotal") && row.Field<string>("NgayBan") != "Tổng cộng")
        //            .CopyToDataTable();

        //        chartTIME.DataSource = filteredData;
        //        foreach (var label in chartTIME.ChartAreas[0].AxisX.CustomLabels)
        //        {
        //            label.Text = DateTime.Parse(label.Text).ToString("dd/MM/yyyy");
        //        }
        //        // Cập nhật dữ liệu cho biểu đồ
        //     //   chartTIME.DataSource = ds.Tables["DataAndChart"];
        //    }
        //    else if (filterType == "month")
        //    {
        //        sql = $"WITH DoanhThu AS(" +
        //  $" SELECT" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') AS NgayBan," +
        //  $" ISNULL(SUM(TONGTIEN), 0) AS DoanhThu" +
        //  $" FROM" +
        //  $" dmhdb" +
        //  $" WHERE" +
        //  $"  FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy') " +
        //  $"GROUP BY" +
        //  $"  FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy')" +
        //  $" )," +
        //  $" ChiPhi AS(" +
        //  $" SELECT" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') AS NgayBan," +
        //  $" ISNULL(SUM(DMCTHDB.SL* GIANHAP), 0) AS ChiPhi," +
        //  $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang" +
        //  $" FROM" +
        //  $" DMCTHDB" +
        //  $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        //  $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
        //  $" WHERE" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy') " +
        //  $"GROUP BY" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy')" +
        //  $" )," +
        //  $" sldon as(" +
        //  $" select FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') as NgayBan, count(mahdb) as sl" +
        //  $" from dmhdb" +
        //  $" where FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy')" +
        //  $" group by FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy')" +
        //  $" )" +
        //  $" SELECT" +
        //  $" CAST(DoanhThu.NgayBan AS NVARCHAR(10)) AS NgayBan," +
        //  $" DoanhThu.DoanhThu," +
        //  $" DoanhThu.DoanhThu - ChiPhi.ChiPhi AS LoiNhuanGop," +
        //  $" sldon.sl as sldon," +
        //  $" ChiPhi.TienHang," +
        //  $" CAST(0 AS BIT) AS IsTotal" +

        //  $" FROM" +
        //  $" DoanhThu" +
        //  $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
        //  $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
        //  $" UNION ALL" +
        //  $" SELECT" +
        //  $" N'Tổng cộng' AS NgayBan," +
        //  $" sum(DoanhThu.DoanhThu)," +
        //  $" sum(DoanhThu.DoanhThu - ChiPhi.ChiPhi) AS LoiNhuanGop," +
        //  $" sum(sldon.sl) as sldon," +
        //  $" sum(ChiPhi.TienHang)," +
        //  $" CAST(1 AS BIT) AS IsTotal" +
        //  $" FROM" +
        //  $" DoanhThu" +
        //  $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
        //  $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
        //  $" ORDER BY" +
        //  // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
        //  $" NgayBan DESC";

        //        cmd = new SqlCommand(sql, conn);
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        da = new SqlDataAdapter(sql, conn);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "DataAndChart");

        //        // Assign the data to the grid
        //        grdData1.DataSource = ds.Tables["DataAndChart"];
        //        grdData1.Columns["IsTotal"].Visible = false;
        //        // Assign the data to the chart
        //        chartTIME.Series.Clear();

        //        // Add Series for DoanhThu, excluding the total row
        //        //Thêm Series cho DoanhThu
        //        Series series1 = chartTIME.Series.Add("DoanhThu");
        //        series1.ChartType = SeriesChartType.Column;
        //        series1.Points.DataBindXY(ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
        //                                    ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
        //        //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
        //        //series1.MarkerSize = 8; // Kích thước của điểm

        //        // Thêm Series cho LoiNhuanGop
        //        Series series2 = chartTIME.Series.Add("LoiNhuanGop");
        //        series2.ChartType = SeriesChartType.Column;
        //        series2.Points.DataBindXY(ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
        //                                         ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
        //        //series2.MarkerStyle = MarkerStyle.Circle;
        //        //series2.MarkerSize = 8;


        //        var filteredData = ds.Tables["DataAndChart"].AsEnumerable()
        //            .Where(row => !row.Field<bool>("IsTotal") && row.Field<string>("NgayBan") != "Tổng cộng")
        //            .CopyToDataTable();

        //        chartTIME.DataSource = filteredData;
        //        chartTIME.ChartAreas[0].AxisX.LabelStyle.Format = "MM/yyyy";

        //        // Cập nhật dữ liệu cho biểu đồ
        //    //    chartTIME.DataSource = ds.Tables["DataAndChart"];
        //    }
        //    else if (filterType == "year")
        //    {
        //        sql = $"WITH DoanhThu AS(" +
        //  $" SELECT" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy') AS NgayBan," +
        //  $" ISNULL(SUM(TONGTIEN), 0) AS DoanhThu" +
        //  $" FROM" +
        //  $" dmhdb" +
        //  $" WHERE" +
        //  $"  FORMAT(CONVERT(date, NGAYBAN), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy') " +
        //  $"GROUP BY" +
        //  $"  FORMAT(CONVERT(date, NGAYBAN), 'yyyy')" +
        //  $" )," +
        //  $" ChiPhi AS(" +
        //  $" SELECT" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy') AS NgayBan," +
        //  $" ISNULL(SUM(DMCTHDB.SL* GIANHAP), 0) AS ChiPhi," +
        //  $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang" +
        //  $" FROM" +
        //  $" DMCTHDB" +
        //  $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        //  $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
        //  $" WHERE" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy') " +
        //  $"GROUP BY" +
        //  $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy')" +
        //  $" )," +
        //  $" sldon as(" +
        //  $" select FORMAT(CONVERT(date, NGAYBAN), 'yyyy') as NgayBan, count(mahdb) as sl" +
        //  $" from dmhdb" +
        //  $" where FORMAT(CONVERT(date, NGAYBAN), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy')" +
        //  $" group by FORMAT(CONVERT(date, NGAYBAN), 'yyyy')" +
        //  $" )" +
        //  $" SELECT" +
        //  $" CAST(DoanhThu.NgayBan AS NVARCHAR(10)) AS NgayBan," +
        //  $" DoanhThu.DoanhThu," +
        //  $" DoanhThu.DoanhThu - ChiPhi.ChiPhi AS LoiNhuanGop," +
        //  $" sldon.sl as sldon," +
        //  $" ChiPhi.TienHang," +
        //  $" CAST(0 AS BIT) AS IsTotal" +

        //  $" FROM" +
        //  $" DoanhThu" +
        //  $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
        //  $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
        //  $" UNION ALL" +
        //  $" SELECT" +
        //  $" N'Tổng cộng' AS NgayBan," +
        //  $" sum(DoanhThu.DoanhThu)," +
        //  $" sum(DoanhThu.DoanhThu - ChiPhi.ChiPhi) AS LoiNhuanGop," +
        //  $" sum(sldon.sl) as sldon," +
        //  $" sum(ChiPhi.TienHang)," +
        //  $" CAST(1 AS BIT) AS IsTotal" +
        //  $" FROM" +
        //  $" DoanhThu" +
        //  $" LEFT JOIN ChiPhi ON DoanhThu.NgayBan = ChiPhi.NgayBan" +
        //  $" LEFT JOIN sldon ON DoanhThu.NgayBan = sldon.NgayBan" +
        //  $" ORDER BY" +
        //  // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
        //  $" NgayBan DESC";

        //        cmd = new SqlCommand(sql, conn);
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        da = new SqlDataAdapter(sql, conn);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "DataAndChart");

        //        // Assign the data to the grid
        //        grdData1.DataSource = ds.Tables["DataAndChart"];
        //        grdData1.Columns["IsTotal"].Visible = false;
        //        // Assign the data to the chart
        //        chartTIME.Series.Clear();

        //        // Add Series for DoanhThu, excluding the total row
        //      //  var chartData = ds.Tables["DataAndChart"].AsEnumerable().Where(row => !row.Field<bool>("IsTotal"));
        //        //Thêm Series cho DoanhThu
        //        Series series1 = chartTIME.Series.Add("DoanhThu");
        //        series1.ChartType = SeriesChartType.Column;
        //        series1.Points.DataBindXY(ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
        //                                    ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
        //        //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
        //        //series1.MarkerSize = 8; // Kích thước của điểm

        //        // Thêm Series cho LoiNhuanGop
        //        Series series2 = chartTIME.Series.Add("LoiNhuanGop");
        //        series2.ChartType = SeriesChartType.Column;
        //        series2.Points.DataBindXY(ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
        //                                         ds.Tables["DataAndChart"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
        //        //series2.MarkerStyle = MarkerStyle.Circle;
        //        //series2.MarkerSize = 8;


        //        var filteredData = ds.Tables["DataAndChart"].AsEnumerable()
        //            .Where(row => !row.Field<bool>("IsTotal") && row.Field<string>("NgayBan") != "Tổng cộng")
        //            .CopyToDataTable();

        //        chartTIME.DataSource = filteredData;
        //        chartTIME.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy";

        //        // Cập nhật dữ liệu cho biểu đồ
        //    }
        //}

   
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
            else if (comNhom.SelectedItem.ToString() == "Theo khách hàng")
            {

                LoadChartCUS();
                LoadGrdCUS();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            {

                LoadChartPD();
                LoadGrdPD();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo đơn hàng")
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
            else if (comNhom.SelectedItem.ToString() == "Theo khách hàng")
            {

                LoadChartCUS();
                LoadGrdCUS();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            {

                LoadChartPD();
                LoadGrdPD();

            }
            else if (comNhom.SelectedItem.ToString() == "Theo đơn hàng")
            {

                LoadChartHD();
                LoadGrdHD();

            }

        }

        private void FrmBCBH_Scroll(object sender, ScrollEventArgs e)
        {

        }
        string filterType = "Day";

        private void grdData1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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
            //chartTIME.Series.Clear();
            //grdData1.DataSource = null;
            //grdData1.Rows.Clear();
            if (comNhom.SelectedItem.ToString() == "Theo thời gian")
            {
                LoadDataForGrid1();
                LoadChart1();
                chartTIME.Visible = true;
                grdData1.Visible = true;
                grdDataEMP.Visible = false;
                grdDataPD.Visible = false;
                
                grdDataCUS.Visible = false;
               // guna2DataGridView1.Visible = false;

                comDay.Visible = true;
                lblDay.Visible = true;
                grdDataHD.Visible = false;

            }
            else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
            {
             
                LoadChartEMP();
                LoadGrdEMP();
                grdDataEMP.Visible = true;
                chartTIME.Visible = true;
                grdData1.Visible = false;
                comDay.Visible = false;
                grdDataPD.Visible = false;
                lblDay.Visible = false;
                grdDataCUS.Visible = false;
                grdDataHD.Visible = false;

            }
            else if (comNhom.SelectedItem.ToString() == "Theo đơn hàng")
            {
                LoadChartHD();
                LoadGrdHD();
                grdDataHD.Visible = true;
                grdDataPD.Visible = false;
                chartTIME.Visible = true;
                grdData1.Visible = false;
                grdDataEMP.Visible = false;
                comDay.Visible = false;
                lblDay.Visible = false;
                grdDataCUS.Visible = false;

            }
            else if (comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            {
                LoadChartPD();
                LoadGrdPD();
                grdDataPD.Visible = true;
                chartTIME.Visible = true;
                grdData1.Visible = false;
                grdDataEMP.Visible = false;
                comDay.Visible = false;
                lblDay.Visible = false;
                grdDataCUS.Visible = false;
                grdDataHD.Visible = false;
            }
       
            else if (comNhom.SelectedItem.ToString() == "Theo khách hàng")
            {
                LoadChartCUS();
                LoadGrdCUS();
                grdDataPD.Visible = false;
                chartTIME.Visible = true;
                grdData1.Visible = false;
                grdDataEMP.Visible = false;
                comDay.Visible = false;
                lblDay.Visible = false;
                grdDataCUS.Visible = true;
                grdDataHD.Visible = false;

            }



        }

        private void chartTIME_Click(object sender, EventArgs e)
        {

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
                    $" CONVERT(date, NGAYBAN) AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
                    $" CHIETKHAU," +
                    $" SL" +
                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
                    $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" + 
                    $" )," +
                    $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
                    $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
                    $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +
                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" LEFT JOIN DMHDB ON DMHDB.MAHDB = DMCTHDB.MAHDB" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" CAST(A.NgayBan AS NVARCHAR(10)) AS NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(B.TienHang) AS TienHang," +
                    $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
                    $" SUM(A.DoanhThu - B.TienThue-B.TienVon) AS LoiNhuanGop," +
                    $" SUM(B.TienThue) as TienThue," +



                    $" SUM(B.TienVon) as TienVon," +
                    $" COUNT(A.MAHDB) AS SLdon," +
                    $" SUM(A.SL) as SLhang" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $" CONVERT(date, A.NGAYBAN)" +
                   
                    $" UNION ALL" +
                    $" SELECT" +
                    $" N'Tổng cộng' AS NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(B.TienHang) AS TienHang," +
                    $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
                    $" SUM(A.DoanhThu -B.TienThue- B.TienVon) AS LoiNhuanGop," +
                    $" sum(B.TienThue) as TienThue," +
                    $" SUM(B.TienVon) as TienVon," +
                    $" COUNT(A.MAHDB) AS SLdon," +
                    $" SUM(A.SL) as SLhang" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                  
                    $" ORDER BY" +
                    // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
                    $" NgayBan DESC";

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
                if (grdData1.Columns.Contains("TENKH"))
                {
                    grdData1.Columns["TENKH"].Visible = false;
                }
                if (grdData1.Columns.Contains("MAHDB"))
                {
                    grdData1.Columns["MAHDB"].Visible = false;
                }
            }
                    else if (filterType == "Month")
                    {
                sql = $"WITH A AS(" +
            $" SELECT" +
            $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') AS NgayBan," +
            $" mahdb," +
            $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" dmhdb" +
            $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') BETWEEN FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy') " + $" ),"+
            $" B AS (" +
            $" SELECT" +
            $" dmcthdb.MAHDB," +
            $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
            $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDB" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
            $" LEFT JOIN DMHDB ON DMHDB.MAHDB = DMCTHDB.MAHDB" +
            $" GROUP BY" +
            $" dmcthdb.MAHDB)" +
            $" SELECT" +
            $" CAST(A.NgayBan AS NVARCHAR(10)) AS NgayBan," +
            $" SUM(A.DoanhThu) AS DoanhThu," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
            $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
            $" SUM(B.TienThue) as TienThue," +
            $" SUM(B.TienVon) as TienVon," +
            $" COUNT(A.MAHDB) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
            $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
            $" GROUP BY" +
            $" A.NgayBan" +
            //$" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy')" +

            $" UNION ALL" +
            $" SELECT" +
            $" N'Tổng cộng' AS NgayBan," +
            $" SUM(A.DoanhThu) AS DoanhThu," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
            $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
            $" sum(B.TienThue) as TienThue," +
            $" SUM(B.TienVon) as TienVon," +
            $" COUNT(A.MAHDB) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
            $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +

            $" ORDER BY" +
            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
            $" NgayBan DESC";
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
                if (grdData1.Columns.Contains("TENKH"))
                {
                    grdData1.Columns["TENKH"].Visible = false;
                }
                if (grdData1.Columns.Contains("MAHDB"))
                {
                    grdData1.Columns["MAHDB"].Visible = false;
                }
            }
                    else if (filterType == "Year")
                    {
                        sql = $"WITH A AS(" +
                    $" SELECT" +
                    $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy') AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
                    $" CHIETKHAU," +
                    $" SL" +
                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
  $"  FORMAT(CONVERT(date, NGAYBAN), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy') " + $" )," +
                    $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
                    $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
                    $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +
                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" LEFT JOIN DMHDB ON DMHDB.MAHDB = DMCTHDB.MAHDB" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" CAST(A.NgayBan AS NVARCHAR(20)) AS NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(B.TienHang) AS TienHang," +
                    $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
                    $" SUM(B.TienThue) as TienThue," +
                    $" SUM(B.TienVon) as TienVon," +
                    $" COUNT(A.MAHDB) AS SLdon," +
                    $" SUM(A.SL) as SLhang" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $" A.NgayBan" +
                 // $"  FORMAT(CONVERT(date, A.NGAYBAN), 'yyyy')" +

                    $" UNION ALL" +
                    $" SELECT" +
                    $" N'Tổng cộng' AS NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(B.TienHang) AS TienHang," +
                    $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
                    $" sum(B.TienThue) as TienThue," +
                    $" SUM(B.TienVon) as TienVon," +
                    $" COUNT(A.MAHDB) AS SLdon," +
                    $" SUM(A.SL) as SLhang" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +

                    $" ORDER BY" +
                    // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
                    $" NgayBan DESC";
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
                if (grdData1.Columns.Contains("TENKH"))
                {
                    grdData1.Columns["TENKH"].Visible = false;
                }
                if (grdData1.Columns.Contains("MAHDB"))
                {
                    grdData1.Columns["MAHDB"].Visible = false;
                }
            }

        }
        //   }
        //  }
        public void LoadChart1()
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
                    $" CONVERT(date, NGAYBAN) AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu" +
       
                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
                    $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
                    $" )," +
                    $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +
            
                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" A.NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +        
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop" +  
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $" CONVERT(date, A.NGAYBAN)" +

                    $" ORDER BY" +
                    // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +
        
                $" NgayBan asc";

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
                Series series1 = chartTIME.Series.FirstOrDefault(s => s.Name == "DoanhThu");

                if (series1== null)
                {
                    series1 = new Series("DoanhThu");
                    chartTIME.Series.Add(series1);
                }
                series1.ChartType = SeriesChartType.Line;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<DateTime>("NgayBan")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
                series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
                series1.MarkerSize = 8; // Kích thước của điểm

                // Thêm Series cho LoiNhuanGop
                //    Series series2 = chartTIME.Series.Add("LoiNhuanGop");
                Series series2 = chartTIME.Series.FirstOrDefault(s => s.Name == "LoiNhuanGop");

                if (series2 == null)
                {
                    series2 = new Series("LoiNhuanGop");
                    chartTIME.Series.Add(series2);
                }
                series2.ChartType = SeriesChartType.Line;
                series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<DateTime>("NgayBan")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
                series2.MarkerStyle = MarkerStyle.Circle;
                series2.MarkerSize = 8;


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
                sql = $"WITH A AS(" +
                    $" SELECT" +
                  $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu" +

                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
 $"  FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy') " +
  $" )," +

 $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +

                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" A.NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $"  A.NgayBan" +
                  //$"  FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy')" +

                    $" ORDER BY" +
                // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +

                $" NgayBan asc";

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
                Series series1 = chartTIME.Series.FirstOrDefault(s => s.Name == "DoanhThu");

                if (series1 == null)
                {
                    series1 = new Series("DoanhThu");
                    chartTIME.Series.Add(series1);
                }
                series1.ChartType = SeriesChartType.Column;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());

                // Thêm Series cho LoiNhuanGop
                //Series series2 = chartTIME.Series.Add("LoiNhuanGop");
                Series series2 = chartTIME.Series.FirstOrDefault(s => s.Name == "LoiNhuanGop");

                if (series2 == null)
                {
                    series2 = new Series("LoiNhuanGop");
                    chartTIME.Series.Add(series2);
                }
                series2.ChartType = SeriesChartType.Column;
                series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());

                // Cập nhật dữ liệu cho biểu đồ
                chartTIME.DataSource = ds.Tables["Sales"];
                chartTIME.ChartAreas[0].AxisX.Interval = 1;
                Axis xAxis = chartTIME.ChartAreas[0].AxisX;
                //xAxis.LabelStyle.Angle = 30;
                xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

            }
            else if (filterType == "Year")
            {
                sql = $"WITH A AS(" +
                    $" SELECT" +
                  $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy') AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu" +

                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
   $"  FORMAT(CONVERT(date, NGAYBAN), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy') " +
   $" )," +
                    $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +

                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" A.NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $" A.NgayBan" +
                    //$" FORMAT(CONVERT(date, NGAYBAN), 'yyyy')" +

                    $" ORDER BY" +
                // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +

                $" NgayBan asc";

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
                Series series1 = chartTIME.Series.FirstOrDefault(s => s.Name == "DoanhThu");

                if (series1 == null)
                {
                    series1 = new Series("DoanhThu");
                    chartTIME.Series.Add(series1);
                }
                series1.ChartType = SeriesChartType.Column;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
                //series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
                //series1.MarkerSize = 8; // Kích thước của điểm

                // Thêm Series cho LoiNhuanGop
                // Series series2 = chartTIME.Series.Add("LoiNhuanGop");
                Series series2 = chartTIME.Series.FirstOrDefault(s => s.Name == "LoiNhuanGop");

                if (series2 == null)
                {
                    series2 = new Series("LoiNhuanGop");
                    chartTIME.Series.Add(series2);
                }
                series2.ChartType = SeriesChartType.Column;
                series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("NgayBan")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());



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

            sql = $"WITH A AS (" +
    $" SELECT" +
    $" MANV," +
    $" CONVERT(date, NGAYBAN) AS NgayBan," +
    $" mahdb," +
    $" ISNULL(TONGTIEN, 0) AS DoanhThu" +

    $" FROM" +
    $" dmhdb" +
    $" WHERE" +
    $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
    $" )," +
    $" B AS(" +
    $" SELECT" +
    $" dmcthdb.MAHDB," +
    $" dmhdb.manv," +
    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +


    $" FROM" +
    $" DMCTHDB" +
    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
    $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

    $" GROUP BY" +
    $" dmcthdb.MAHDB, dmhdb.manv" +

    $" )" +
    $" SELECT" +
    $" TENNV," +
    $" SUM(A.DoanhThu) AS DoanhThu," +
    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop" +

    $" FROM" +
    $" A" +
    $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
    $" LEFT JOIN DMNV ON A.MANV = DMNV.MANV" +
    $" GROUP BY A.MANV, TENNV";
       


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
                Series series1 = chartTIME.Series.Add("DoanhThu");
                series1.ChartType = SeriesChartType.Line;
                series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENNV")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
                series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
                series1.MarkerSize = 8; // Kích thước của điểm

                // Thêm Series cho LoiNhuanGop
                Series series2 = chartTIME.Series.Add("LoiNhuanGop");
                series2.ChartType = SeriesChartType.Line;
                series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENNV")).ToArray(),
                                        ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
                series2.MarkerStyle = MarkerStyle.Circle;
                series2.MarkerSize = 8;



            // Cập nhật dữ liệu cho biểu đồ
            chartTIME.DataSource = ds.Tables["Sales"];

            chartTIME.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chartTIME.ChartAreas[0].AxisX;
            //xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;



        }
        private void LoadGrdEMP()
        {
            string sql= string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS (" +
        $" SELECT" +
        $" MANV," +
        $" CONVERT(date, NGAYBAN) AS NgayBan," +
        $" mahdb," +
        $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
        $" CHIETKHAU," +
        $" SL" +
        $" FROM" +
        $" dmhdb" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdb.MAHDB," +
        $" dmhdb.manv," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
        $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +

        $" FROM" +
        $" DMCTHDB" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

        $" GROUP BY" +
        $" dmcthdb.MAHDB, dmhdb.manv" +

        $" )" +
        $" SELECT" +
        $" TENNV," +
        $" SUM(A.DoanhThu) AS DoanhThu," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
        $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
        $" SUM(B.TienThue) as TienThue," +
        $" SUM(B.TienVon) as TienVon," +
        $" COUNT(A.MAHDB) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
        $" LEFT JOIN DMNV ON A.MANV = DMNV.MANV" +
        $" GROUP BY A.MANV, TENNV"+

            $" UNION ALL" +
        $" SELECT" +
        $" N'Tổng cộng' AS TENNV," +
        $" SUM(A.DoanhThu) as DoanhThu," +
        $" SUM(B.TienHang) as TienHang," +
        $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
        $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
        $" sum(B.TienThue) as TienThue," +
        $" sum(B.TienVon) as TienVon," +
        $" COUNT(A.MAHDB) AS SLdon," +
        $" sum(A.SL) as SLhang" +
        $" FROM A" +
        $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
        $" LEFT JOIN DMNV ON A.MANV = DMNV.MANV";

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
            if (grdDataEMP.Columns.Contains("TENKH"))
            {
                grdDataEMP.Columns["TENKH"].Visible = false;
            }
            if (grdDataEMP.Columns.Contains("NgayBan"))
            {
                grdDataEMP.Columns["NgayBan"].Visible = false;
            }
            if (grdDataEMP.Columns.Contains("MAHDB"))
            {
                grdDataEMP.Columns["MAHDB"].Visible = false;
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
        $" DMCTHDb.MAHH," +
        $" ISNULL(sum(THANHTIEN*(1-CHIETKHAU/100)),0) AS DoanhThu," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +
      
        $" FROM" +
        $" dmCThdb" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" 	DMCTHDb.MAhh" +
        $" )" +

        $" SELECT" +
        $" TENHH," +
        $" A.DoanhThu as DoanhThu," +    
        $" (A.DoanhThu - A.TienVon) as LoiNhuanGop" +     
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
            Series series1 = chartTIME.Series.Add("DoanhThu");
            series1.ChartType = SeriesChartType.Line;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENHH")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
            series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            series1.MarkerSize = 8; // Kích thước của điểm

            // Thêm Series cho LoiNhuanGop
            Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            series2.ChartType = SeriesChartType.Line;
            series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENHH")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
            series2.MarkerStyle = MarkerStyle.Circle;
            series2.MarkerSize = 8;



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
        $" DMCTHDb.MAHH," +
        $" ISNULL(sum(THANHTIEN*(1-CHIETKHAU/100)),0) AS DoanhThu," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
        $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue," +
        $" count(dmcthdb.mahdb) as SLdon," +
        $" sum(dmcthdb.SL) as SLhang" +
        $" FROM" +
        $" dmCThdb" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" 	DMCTHDb.MAhh" +
        $" )" +

        $" SELECT" +
        $" TENHH," +
        $" A.DoanhThu as DoanhThu," +
        $" A.TienHang as TienHang," +
        $" (A.DoanhThu - A.TienThue) as DoanhThuThuan," +
        $" (A.DoanhThu - A.TienVon) as LoiNhuanGop," +
        $" A.TienThue AS TienThue," +
        $" A.TienVon as TienVon," +
        $" A.SLdon as SLdon," +
        $" A.SLhang as SLhang" +
        $" FROM" +
        $" A" +
        $" LEFT JOIN DMHH ON A.MAHH = DMHH.MAHH" +

        $" UNION ALL" +
        $" SELECT" +
        $" N'Tổng cộng' AS TENHH," +
        $" sum(A.DoanhThu) as DoanhThu," +
        $" sum(A.TienHang) as TienHang," +
        $" sum(A.DoanhThu - A.TienThue) as DoanhThuThuan," +
        $" sum(A.DoanhThu - A.TienVon) as LoiNhuanGop," +
        $" sum(A.TienThue) AS TienThue," +
        $" sum(A.TienVon) as TienVon," +
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
            if (grdDataPD.Columns.Contains("TENKH"))
            {
                grdDataPD.Columns["TENKH"].Visible = false;
            }
            if (grdDataPD.Columns.Contains("NgayBan"))
            {
                grdDataPD.Columns["NgayBan"].Visible = false;
            }
            if (grdDataPD.Columns.Contains("MAHDB"))
            {
                grdDataPD.Columns["MAHDB"].Visible = false;
            }
        }
        public void LoadChartCUS()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS (" +
    $" SELECT" +
    $" MAKH," +
    $" CONVERT(date, NGAYBAN) AS NgayBan," +
    $" mahdb," +
    $" ISNULL(TONGTIEN, 0) AS DoanhThu" +

    $" FROM" +
    $" dmhdb" +
    $" WHERE" +
    $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
    $" )," +
    $" B AS(" +
    $" SELECT" +
    $" dmcthdb.MAHDB," +
    $" dmhdb.maKH," +
    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +


    $" FROM" +
    $" DMCTHDB" +
    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
    $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

    $" GROUP BY" +
    $" dmcthdb.MAHDB, dmhdb.maKH" +

    $" )" +
    $" SELECT" +
    $" TENKH," +
    $" SUM(A.DoanhThu) AS DoanhThu," +
    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop" +

    $" FROM" +
    $" A" +
    $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
    $" LEFT JOIN DMKH ON A.MAKH = DMKH.MAKH" +
    $" GROUP BY A.MAKH, TENKH";


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
            Series series1 = chartTIME.Series.Add("DoanhThu");
            series1.ChartType = SeriesChartType.Line;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENKH")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
            series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            series1.MarkerSize = 8; // Kích thước của điểm

            // Thêm Series cho LoiNhuanGop
            Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            series2.ChartType = SeriesChartType.Line;
            series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("TENKH")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("LoiNhuanGop")).ToArray());
            series2.MarkerStyle = MarkerStyle.Circle;
            series2.MarkerSize = 8;



            // Cập nhật dữ liệu cho biểu đồ
            chartTIME.DataSource = ds.Tables["Sales"];

            //// Lấy đối tượng Axis cho trục hoành
            //Axis xAxis = chartTIME.ChartAreas[0].AxisX;

            //// Xoay chữ ở góc 30 độ
            //xAxis.LabelStyle.Angle = 30;

            //// Loại bỏ chữ trên trục hoành và thêm chữ mới với góc xoay
            //xAxis.CustomLabels.Clear();
            //for (int i = 0; i < ds.Tables["Sales"].Rows.Count; i++)
            //{
            //    string label = ds.Tables["Sales"].Rows[i].Field<string>("TENKH");

            //    // Thêm chữ mới với góc xoay
            //    CustomLabel customLabel = new CustomLabel(i + 0.5, i + 1.5, label, 0, LabelMarkStyle.None);
            //    xAxis.CustomLabels.Add(customLabel);
            //}
            chartTIME.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chartTIME.ChartAreas[0].AxisX;
            //xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;


        }
        private void LoadGrdCUS()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;

            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $"WITH A AS (" +
        $" SELECT" +
        $" MAKH," +
        $" CONVERT(date, NGAYBAN) AS NgayBan," +
        $" mahdb," +
        $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
        $" CHIETKHAU," +
        $" SL" +
        $" FROM" +
        $" dmhdb" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdb.MAHDB," +
        $" dmhdb.maKH," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
        $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +

        $" FROM" +
        $" DMCTHDB" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

        $" GROUP BY" +
        $" dmcthdb.MAHDB, dmhdb.maKH" +

        $" )" +
        $" SELECT" +
        $" TENKH," +
        $" SUM(A.DoanhThu) AS DoanhThu," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
        $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
        $" SUM(B.TienThue) as TienThue," +
        $" SUM(B.TienVon) as TienVon," +
        $" COUNT(A.MAHDB) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
        $" LEFT JOIN DMkh ON A.MAKH = DMKH.MAKH" +
        $" GROUP BY A.MAKH, TENKH" +

            $" UNION ALL" +
        $" SELECT" +
        $" N'Tổng cộng' AS TENKH," +
        $" SUM(A.DoanhThu) as DoanhThu," +
        $" SUM(B.TienHang) as TienHang," +
        $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
        $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
        $" sum(B.TienThue) as TienThue," +
        $" sum(B.TienVon) as TienVon," +
        $" COUNT(A.MAHDB) AS SLdon," +
        $" sum(A.SL) as SLhang" +
        $" FROM A" +
        $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
        $" LEFT JOIN DMKH ON A.MAKH = DMKH.MAKH";


            // $" CASE WHEN NgayBan = N'Tổng cộng' THEN 1 ELSE 0 END," +


            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
          
            grdDataCUS.DataSource = dt;
            if (grdDataCUS.Columns.Contains("TENNV"))
            {
                grdDataCUS.Columns["TENNV"].Visible = false;
            }
            if (grdDataCUS.Columns.Contains("NgayBan"))
            {
                grdDataCUS.Columns["NgayBan"].Visible = false;
            }
            if (grdDataCUS.Columns.Contains("TENHH"))
            {
                grdDataCUS.Columns["TENHH"].Visible = false;
            }
            if (grdDataCUS.Columns.Contains("MAHDB"))
            {
                grdDataCUS.Columns["MAHDB"].Visible = false;
            }
        }
        public void LoadChartHD()
        {
            string sql = string.Empty;
            DateTime ngayBanDau = Tungay.Value.Date;
            DateTime ngayKetThuc = Denngay.Value.Date;
            string formattedTungay = ngayBanDau.ToString("yyyy-MM-dd");
            string formattedDenngay = ngayKetThuc.ToString("yyyy-MM-dd");

            sql = $" WITH A AS(" +
   $" SELECT" +
    $" MAhdb," +

      $" ISNULL(TONGTIEN, 0) AS DoanhThu" +
  $" FROM" +
       $" dmhdb" +
   $" WHERE" +
     $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
$" )," +
$" B AS(" +
   $" SELECT" +
   $" dmcthdb.MAhdb," +
     $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon" +

   $" FROM" +
       $" DMCTHDB" +
   $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
   $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

   $" GROUP BY" +
 $" dmcthdb.MAhdb" +

$" )" +
$" SELECT" +
   $" A.MAHDB," +
   $" isnull(a.DoanhThu,0) as DoanhThu," +
   $" isnull((A.DoanhThu - B.TienVon),0) AS LoiNhuanGop" +


$" FROM" +
  $" A" +
$" LEFT JOIN B ON A.mahdb = B.mahdb";



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
            Series series1 = chartTIME.Series.Add("DoanhThu");
            series1.ChartType = SeriesChartType.Line;
            series1.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("MAHDB")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double>("DoanhThu")).ToArray());
            series1.MarkerStyle = MarkerStyle.Circle; // Chọn kiểu điểm, có thể thay bằng kiểu khác như Diamond, Square, v.v.
            series1.MarkerSize = 8; // Kích thước của điểm

            // Thêm Series cho LoiNhuanGop
            Series series2 = chartTIME.Series.Add("LoiNhuanGop");
            series2.ChartType = SeriesChartType.Line;
            series2.Points.DataBindXY(ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<string>("MAHDB")).ToArray(),
                                    ds.Tables["Sales"].AsEnumerable().Select(row => row.Field<double?>("LoiNhuanGop")).ToArray());
            series2.MarkerStyle = MarkerStyle.Circle;
            series2.MarkerSize = 8;



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

            sql = $" WITH A AS("+
   $" SELECT"+
    $" MAhdb, SL as SLhang,CHIETKHAU," +

      $" ISNULL(TONGTIEN, 0) AS DoanhThu" +
  $" FROM"+
       $" dmhdb"+
   $" WHERE"+
     $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
$" ),"+
$" B AS("+
   $" SELECT"+
   $" dmcthdb.MAhdb," +
     $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
     $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue," +
       $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang"+
   $" FROM"+
       $" DMCTHDB"+
   $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh"+
   $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB"+
  
   $" GROUP BY"+
 $" dmcthdb.MAhdb" +

$" )"+
$" SELECT"+
   $" CAST(A.MAHDB AS NVARCHAR(20)) AS MAHDB," +
   $" isnull(A.SLhang,0) as SLhang," +
   $" isnull(B.TienHang,0) as TienHang," +
   $" isnull(a.DoanhThu,0) as DoanhThu," +
   $" isnull((A.DoanhThu - B.TienVon),0) AS LoiNhuanGop," +
   $" isnull((A.DoanhThu - B.TienThue),0) as DoanhThuThuan," +
   $" isnull(B.TienVon,0) as TienVon," +
   $" isnull(B.TienThue,0) as TienThue" +
   
$" FROM"+
  $" A"+
$" LEFT JOIN B ON A.mahdb = B.mahdb" +
$" UNION ALL"+
$" SELECT"+

    $" N'Tổng cộng' AS mahdb," +
    $" SUM(A.SLhang) AS SLhang," +
    $" SUM(B.TienHang) AS TienHang," +
    $" sum(a.DoanhThu) as DoanhThu," +
    $" sum(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
    $" sum(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
    $" sum(B.TienVon) As TienVon," +
    $" sum(B.TienThue) as TienThue" +
  
$" FROM"+
  $" A"+
$" LEFT JOIN B ON A.mahdb = B.mahdb";


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
            if (grdDataHD.Columns.Contains("NgayBan"))
            {
                grdDataHD.Columns["NgayBan"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("TENHH"))
            {
                grdDataHD.Columns["TENHH"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("TENKH"))
            {
                grdDataHD.Columns["TENKH"].Visible = false;
            }
            if (grdDataHD.Columns.Contains("SLdon"))
            {
                grdDataHD.Columns["SLdon"].Visible = false;
            }
        }
        private void grdDataCUS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chartHD_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void btnEDIT_Click(object sender, EventArgs e)
        {

            //Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            //Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            //app.Visible = true;

            //if (comNhom.SelectedItem.ToString() == "Theo thời gian")
            //{
            //    worksheet = workbook.Sheets["Sheet1"] as Microsoft.Office.Interop.Excel.Worksheet;
            //    if (worksheet == null)
            //    {
            //        worksheet = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Microsoft.Office.Interop.Excel.Worksheet;
            //    }

            //    worksheet = workbook.ActiveSheet;

            //    for (int i = 1; i < grdData1.Columns.Count + 1; i++)
            //    {
            //        worksheet.Cells[1, i] = grdData1.Columns[i - 1].HeaderText;
            //        worksheet.Cells[1, i].Font.Bold = true;
            //    }

            //    for (int i = 0; i < grdData1.Rows.Count; i++)
            //    {
            //        int excelColumn = 1; // Counter for Excel columns

            //        for (int j = 0; j < grdData1.Columns.Count; j++)
            //        {
            //            string headerText = grdData1.Columns[j].HeaderText;

            //            // Check if the column should be excluded
            //            if (headerText != "TENNV" && headerText != "TENHH" && headerText != "TENKH" && headerText != "MAHDB")
            //            {
            //                if (grdData1.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = grdData1.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = "";
            //                }

            //                excelColumn++; // Move to the next Excel column only for included columns
            //            }
            //        }
            //    }
            //}
            //else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
            //{
            //    worksheet = workbook.Sheets["Sheet1"] as Microsoft.Office.Interop.Excel.Worksheet;
            //    if (worksheet == null)
            //    {
            //        worksheet = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Microsoft.Office.Interop.Excel.Worksheet;
            //    }

            //    worksheet = workbook.ActiveSheet;

            //    for (int i = 1; i < grdDataEMP.Columns.Count + 1; i++)
            //    {
            //        worksheet.Cells[1, i] = grdDataEMP.Columns[i - 1].HeaderText;
            //        worksheet.Cells[1, i].Font.Bold = true;
            //    }

            //    for (int i = 0; i < grdDataEMP.Rows.Count; i++)
            //    {
            //        int excelColumn = 1; // Counter for Excel columns

            //        for (int j = 0; j < grdDataEMP.Columns.Count; j++)
            //        {
            //            string headerText = grdDataEMP.Columns[j].HeaderText;

            //            // Check if the column should be excluded
            //            if (headerText != "NgayBan" && headerText != "TENHH" && headerText != "TENKH" && headerText != "MAHDB")
            //            {
            //                if (grdDataEMP.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = grdDataEMP.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = "";
            //                }

            //                excelColumn++; // Move to the next Excel column only for included columns
            //            }
            //        }
            //    }
            //}
            //else if (comNhom.SelectedItem.ToString() == "Theo khách hàng")
            //{
            //    worksheet = workbook.Sheets["Sheet1"] as Microsoft.Office.Interop.Excel.Worksheet;
            //    if (worksheet == null)
            //    {
            //        worksheet = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Microsoft.Office.Interop.Excel.Worksheet;
            //    }

            //    worksheet = workbook.ActiveSheet;

            //    for (int i = 1; i < grdDataCUS.Columns.Count + 1; i++)
            //    {
            //        worksheet.Cells[1, i] = grdDataCUS.Columns[i - 1].HeaderText;
            //        worksheet.Cells[1, i].Font.Bold = true;
            //    }

            //    for (int i = 0; i < grdDataCUS.Rows.Count; i++)
            //    {
            //        int excelColumn = 1; // Counter for Excel columns

            //        for (int j = 0; j < grdDataCUS.Columns.Count; j++)
            //        {
            //            string headerText = grdDataCUS.Columns[j].HeaderText;

            //            // Check if the column should be excluded
            //            if (headerText != "TENNV" && headerText != "TENHH" && headerText != "NgayBan" && headerText != "MAHDB")
            //            {
            //                if (grdDataCUS.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = grdDataCUS.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = "";
            //                }

            //                excelColumn++; // Move to the next Excel column only for included columns
            //            }
            //        }
            //    }
            //}
            //else if(comNhom.SelectedItem.ToString() == "Theo sản phẩm")
            //{
            //    worksheet = workbook.Sheets["Sheet1"] as Microsoft.Office.Interop.Excel.Worksheet;
            //    if (worksheet == null)
            //    {
            //        worksheet = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Microsoft.Office.Interop.Excel.Worksheet;
            //    }

            //    worksheet = workbook.ActiveSheet;

            //    for (int i = 1; i < grdDataPD.Columns.Count + 1; i++)
            //    {
            //        worksheet.Cells[1, i] = grdDataPD.Columns[i - 1].HeaderText;
            //        worksheet.Cells[1, i].Font.Bold = true;
            //    }

            //    for (int i = 0; i < grdDataPD.Rows.Count; i++)
            //    {
            //        int excelColumn = 1; // Counter for Excel columns

            //        for (int j = 0; j < grdDataPD.Columns.Count; j++)
            //        {
            //            string headerText = grdDataPD.Columns[j].HeaderText;

            //            // Check if the column should be excluded
            //            if (headerText != "TENNV" && headerText != "NgayBan" && headerText != "TENKH" && headerText != "MAHDB")
            //            {
            //                if (grdDataPD.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = grdDataPD.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = "";
            //                }

            //                excelColumn++; // Move to the next Excel column only for included columns
            //            }
            //        }
            //    }
            //}
            //else if (comNhom.SelectedItem.ToString() == "Theo đơn hàng")
            //{
            //    worksheet = workbook.Sheets["Sheet1"] as Microsoft.Office.Interop.Excel.Worksheet;
            //    if (worksheet == null)
            //    {
            //        worksheet = workbook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Microsoft.Office.Interop.Excel.Worksheet;
            //    }

            //    worksheet = workbook.ActiveSheet;

            //    for (int i = 1; i < grdDataHD.Columns.Count + 1; i++)
            //    {
            //        worksheet.Cells[1, i] = grdDataHD.Columns[i - 1].HeaderText;
            //        worksheet.Cells[1, i].Font.Bold = true;
            //    }

            //    for (int i = 0; i < grdDataHD.Rows.Count; i++)
            //    {
            //        int excelColumn = 1; // Counter for Excel columns

            //        for (int j = 0; j < grdDataHD.Columns.Count; j++)
            //        {
            //            string headerText = grdDataHD.Columns[j].HeaderText;

            //            // Check if the column should be excluded
            //            if (headerText != "TENNV" && headerText != "TENHH" && headerText != "TENKH" && headerText != "SLdon" && headerText != "NgayBan ")
            //            {
            //                if (grdDataHD.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = grdDataHD.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, excelColumn] = "";
            //                }

            //                excelColumn++; // Move to the next Excel column only for included columns
            //            }
            //        }
            //    }
            //}

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
                    $" CONVERT(date, NGAYBAN) AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
                    $" CHIETKHAU," +
                    $" SL" +
                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
                    $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
                    $" )," +
                    $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
                    $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
                    $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +
                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" LEFT JOIN DMHDB ON DMHDB.MAHDB = DMCTHDB.MAHDB" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" CAST(A.NgayBan AS NVARCHAR(10)) AS NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(B.TienHang) AS TienHang," +
                    $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
                    $" SUM(B.TienThue) as TienThue," +
                    $" SUM(B.TienVon) as TienVon," +
                    $" COUNT(A.MAHDB) AS SLdon," +
                    $" SUM(A.SL) as SLhang" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $" CONVERT(date, A.NGAYBAN)" +


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
                    else if (filterType == "Month")
                    {
                        string queryNV = $"WITH A AS(" +
            $" SELECT" +
            $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') AS NgayBan," +
            $" mahdb," +
            $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
            $" CHIETKHAU," +
            $" SL" +
            $" FROM" +
            $" dmhdb" +
            $" WHERE" +
            $" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy') BETWEEN FORMAT(CONVERT(date, '{formattedTungay}'), 'MM/yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'MM/yyyy') " + $" )," +
            $" B AS (" +
            $" SELECT" +
            $" dmcthdb.MAHDB," +
            $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
            $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
            $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +
            $" FROM" +
            $" DMCTHDB" +
            $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
            $" LEFT JOIN DMHDB ON DMHDB.MAHDB = DMCTHDB.MAHDB" +
            $" GROUP BY" +
            $" dmcthdb.MAHDB)" +
            $" SELECT" +
            $" CAST(A.NgayBan AS NVARCHAR(10)) AS NgayBan," +
            $" SUM(A.DoanhThu) AS DoanhThu," +
            $" SUM(B.TienHang) AS TienHang," +
            $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
            $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
            $" SUM(B.TienThue) as TienThue," +
            $" SUM(B.TienVon) as TienVon," +
            $" COUNT(A.MAHDB) AS SLdon," +
            $" SUM(A.SL) as SLhang" +
            $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
            $" GROUP BY" +
            $" A.NgayBan" +
            //$" FORMAT(CONVERT(date, NGAYBAN), 'MM/yyyy')" +


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
                    else if (filterType == "Year")
                    {
                        string queryNV = $"WITH A AS(" +
                    $" SELECT" +
                    $" FORMAT(CONVERT(date, NGAYBAN), 'yyyy') AS NgayBan," +
                    $" mahdb," +
                    $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
                    $" CHIETKHAU," +
                    $" SL" +
                    $" FROM" +
                    $" dmhdb" +
                    $" WHERE" +
  $"  FORMAT(CONVERT(date, NGAYBAN), 'yyyy') BETWEEN  FORMAT(CONVERT(date, '{formattedTungay}'), 'yyyy') AND  FORMAT(CONVERT(date, '{formattedDenngay}'), 'yyyy') " + $" )," +
                    $" B AS (" +
                    $" SELECT" +
                    $" dmcthdb.MAHDB," +
                    $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
                    $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
                    $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +
                    $" FROM" +
                    $" DMCTHDB" +
                    $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
                    $" LEFT JOIN DMHDB ON DMHDB.MAHDB = DMCTHDB.MAHDB" +
                    $" GROUP BY" +
                    $" dmcthdb.MAHDB)" +
                    $" SELECT" +
                    $" CAST(A.NgayBan AS NVARCHAR(20)) AS NgayBan," +
                    $" SUM(A.DoanhThu) AS DoanhThu," +
                    $" SUM(B.TienHang) AS TienHang," +
                    $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
                    $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
                    $" SUM(B.TienThue) as TienThue," +
                    $" SUM(B.TienVon) as TienVon," +
                    $" COUNT(A.MAHDB) AS SLdon," +
                    $" SUM(A.SL) as SLhang" +
                    $" FROM A LEFT JOIN B ON A.MAHDB = B.MAHDB" +
                    $" GROUP BY" +
                    $" A.NgayBan" +
                    // $"  FORMAT(CONVERT(date, A.NGAYBAN), 'yyyy')" +

                 
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
                else if (comNhom.SelectedItem.ToString() == "Theo nhân viên")
                {
                    string queryNV = $"WITH A AS (" +
        $" SELECT" +
        $" MANV," +
        $" CONVERT(date, NGAYBAN) AS NgayBan," +
        $" mahdb," +
        $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
        $" CHIETKHAU," +
        $" SL" +
        $" FROM" +
        $" dmhdb" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdb.MAHDB," +
        $" dmhdb.manv," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
        $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +

        $" FROM" +
        $" DMCTHDB" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

        $" GROUP BY" +
        $" dmcthdb.MAHDB, dmhdb.manv" +

        $" )" +
        $" SELECT" +
        $" TENNV," +
        $" SUM(A.DoanhThu) AS DoanhThu," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
        $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
        $" SUM(B.TienThue) as TienThue," +
        $" SUM(B.TienVon) as TienVon," +
        $" COUNT(A.MAHDB) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
        $" LEFT JOIN DMNV ON A.MANV = DMNV.MANV" +
        $" GROUP BY A.MANV, TENNV";

      

                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCEMP");

                                // Tạo đối tượng báo cáo
                                rptBCEMP r = new rptBCEMP();

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
        $" DMCTHDb.MAHH," +
        $" ISNULL(sum(THANHTIEN*(1-CHIETKHAU/100)),0) AS DoanhThu," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
        $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue," +
        $" count(dmcthdb.mahdb) as SLdon," +
        $" sum(dmcthdb.SL) as SLhang" +
        $" FROM" +
        $" dmCThdb" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $"GROUP BY" +
        $" 	DMCTHDb.MAhh" +
        $" )" +

        $" SELECT" +
        $" TENHH," +
        $" A.DoanhThu as DoanhThu," +
        $" A.TienHang as TienHang," +
        $" (A.DoanhThu - A.TienThue) as DoanhThuThuan," +
        $" (A.DoanhThu - A.TienVon) as LoiNhuanGop," +
        $" A.TienThue AS TienThue," +
        $" A.TienVon as TienVon," +
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
                                da.Fill(ds, "dtBCPD");

                                // Tạo đối tượng báo cáo
                                rptBCPD r = new rptBCPD();

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
                else if (comNhom.SelectedItem.ToString() == "Theo khách hàng")
                {
                    string queryNV = $"WITH A AS (" +
        $" SELECT" +
        $" MAKH," +
        $" CONVERT(date, NGAYBAN) AS NgayBan," +
        $" mahdb," +
        $" ISNULL(TONGTIEN, 0) AS DoanhThu," +
        $" CHIETKHAU," +
        $" SL" +
        $" FROM" +
        $" dmhdb" +
        $" WHERE" +
        $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}' " +
        $" )," +
        $" B AS(" +
        $" SELECT" +
        $" dmcthdb.MAHDB," +
        $" dmhdb.maKH," +
        $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
        $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang," +
        $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue" +

        $" FROM" +
        $" DMCTHDB" +
        $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
        $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

        $" GROUP BY" +
        $" dmcthdb.MAHDB, dmhdb.maKH" +

        $" )" +
        $" SELECT" +
        $" TENKH," +
        $" SUM(A.DoanhThu) AS DoanhThu," +
        $" SUM(B.TienHang) AS TienHang," +
        $" SUM(A.DoanhThu - B.TienThue) as DoanhThuThuan," +
        $" SUM(A.DoanhThu - B.TienVon) AS LoiNhuanGop," +
        $" SUM(B.TienThue) as TienThue," +
        $" SUM(B.TienVon) as TienVon," +
        $" COUNT(A.MAHDB) AS SLdon," +
        $" SUM(A.SL) as SLhang" +
        $" FROM" +
        $" A" +
        $" LEFT JOIN B ON A.MAHDB = B.MAHDB" +
        $" LEFT JOIN DMkh ON A.MAKH = DMKH.MAKH" +
        $" GROUP BY A.MAKH, TENKH";







                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCCUS");

                                // Tạo đối tượng báo cáo
                                rptBCCUS r = new rptBCCUS();

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
                else if (comNhom.SelectedItem.ToString() == "Theo đơn hàng")
                {
                    string queryNV = $" WITH A AS(" +
   $" SELECT" +
    $" MAhdb, SL as SLhang,CHIETKHAU," +

      $" ISNULL(TONGTIEN, 0) AS DoanhThu" +
  $" FROM" +
       $" dmhdb" +
   $" WHERE" +
     $" CONVERT(date, NGAYBAN) BETWEEN '{formattedTungay}' AND '{formattedDenngay}'" +
$" )," +
$" B AS(" +
   $" SELECT" +
   $" dmcthdb.MAhdb," +
     $" ISNULL(SUM(DMCTHDB.SL * GIANHAP), 0) AS TienVon," +
     $" ISNULL(sum(TIENTHUE*(1-CHIETKHAU/100)),0) AS TienThue," +
       $" ISNULL(SUM(DMCTHDB.SL * DONGIA), 0) AS TienHang" +
   $" FROM" +
       $" DMCTHDB" +
   $" LEFT JOIN dmhh ON dmhh.mahh = DMCTHDB.mahh" +
   $" LEFT JOIN dmhdb ON DMCTHDB.MAHDB = dmhdb.MAHDB" +

   $" GROUP BY" +
 $" dmcthdb.MAhdb" +

$" )" +
$" SELECT" +
   $" CAST(A.MAHDB AS NVARCHAR(20)) AS MAHDB," +
   $" isnull(A.SLhang,0) as SLhang," +
   $" isnull(B.TienHang,0) as TienHang," +
   $" isnull(a.DoanhThu,0) as DoanhThu," +
   $" isnull((A.DoanhThu - B.TienVon),0) AS LoiNhuanGop," +
   $" isnull((A.DoanhThu - B.TienThue),0) as DoanhThuThuan," +
   $" isnull(B.TienVon,0) as TienVon," +
   $" isnull(B.TienThue,0) as TienThue" +

$" FROM" +
  $" A" +
$" LEFT JOIN B ON A.mahdb = B.mahdb";



                    using (SqlCommand cmd = new SqlCommand(queryNV, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                // Đổ dữ liệu từ CSDL vào DataSet
                                da.Fill(ds, "dtBCHD");

                                // Tạo đối tượng báo cáo
                                rptBCHD r = new rptBCHD();

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
