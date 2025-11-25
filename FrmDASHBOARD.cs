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
    public partial class FrmDASHBOARD : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
         SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        FrmLOGIN f;
        private double dailysales;
        private int donhang;
        private int sanpham;
        private int slsanpham;
        string sql, constr;
        int i;
        public FrmDASHBOARD(FrmLOGIN frm)
        {

            InitializeComponent();
            //  if(this.IsInDesignMode()) return;
            f = frm;
        }

        private void FrmDASHBOARD_Load(object sender, EventArgs e)
        {
            //Bay loi
            try
            {
              

                 //doan chuong trinh can bay loi
                 //3 dong dau dùng để thiet lap den CSDL QLBDS 
                 constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;
                conn.Open();
               
                lbldailysale.Text = Dailysales().ToString("N0");
                lbldonhang.Text = Donhang().ToString("N0");
                lblproduct.Text = Sanpham().ToString();
                lblslsanpham.Text = Slsanpham().ToString();
                LoadChart1();
                LoadChart2();
                LoadChart3();
                LoadChart4();

                //sql = "SELECT MAHDB, TENKH,NGAYBAN,TENNV, TONGTIEN FROM DMHDB " +
                //    "LEFT JOIN DMKH ON DMKH.MAKH = DMHDB.MAKH " +
                //    "LEFT JOIN DMNV ON DMNV.MANV = DMHDB.MANV" +
                //    " ORDER BY MAHDB DESC";





            }
            catch (Exception err)
            {
                MessageBox.Show("error:" + err.Message);
            }
        }
        public double Dailysales()
        {
            DateTime ngayHienTai = DateTime.Now;
            string NGAYBAN = ngayHienTai.ToShortDateString();
            sql = "SELECT ISNULL(SUM(TONGTIEN), 0) as TONGTIEN FROM DMHDB WHERE NGAYBAN BETWEEN '" + NGAYBAN + " 00:00:00' and '" + NGAYBAN + " 23:59:59'";
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            dailysales = double.Parse(cmd.ExecuteScalar().ToString());

            // Định dạng số tiền với dấu phẩy ngăn cách
            string formattedDailysales = string.Format("{0:n0}", dailysales);

            // Gán giá trị formattedDailysales cho lbldailysale.Text
            lbldailysale.Text = formattedDailysales;

            return dailysales;
        }
        public int Donhang()
        {
            DateTime ngayHienTai = DateTime.Now;
            string NGAYBAN = ngayHienTai.ToShortDateString();
            sql = "SELECT count(*) FROM DMHDB WHERE NGAYBAN BETWEEN '" + NGAYBAN + " 00:00:00' and '" + NGAYBAN + " 23:59:59'";
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            donhang = int.Parse(cmd.ExecuteScalar().ToString());

            // Định dạng số tiền với dấu phẩy ngăn cách
            string formattedDonhang = string.Format("{0:n0}", dailysales);

            // Gán giá trị formattedDailysales cho lbldailysale.Text
            lbldonhang.Text = formattedDonhang;

            return donhang;
        }
        public int Sanpham()
        {
            DateTime ngayHienTai = DateTime.Now;
            string NGAYBAN = ngayHienTai.ToShortDateString();
            sql = "SELECT count(distinct MAHH) FROM DMCTHDB, DMHDB WHERE NGAYBAN BETWEEN '" + NGAYBAN + " 00:00:00' and '" + NGAYBAN + " 23:59:59'" +
                "AND DMCTHDB.MAHDB = DMHDB.MAHDB";
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            sanpham  = int.Parse(cmd.ExecuteScalar().ToString());

            return sanpham;
        }
        public int Slsanpham()
        {
            DateTime ngayHienTai = DateTime.Now;
            string NGAYBAN = ngayHienTai.ToShortDateString();
            sql = "SELECT isnull(sum(SL),0) as SOLUONG FROM  DMHDB WHERE NGAYBAN BETWEEN '" + NGAYBAN + " 00:00:00' and '" + NGAYBAN + " 23:59:59'";
               // "AND DMHDB.MAHDB = DMCTHDB.MAHDB";
            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            slsanpham = int.Parse(cmd.ExecuteScalar().ToString());

            return slsanpham;
        }

        public void LoadChart1()
        {
            sql = "select DAY(NGAYBAN) AS day, isnull(sum(TONGTIEN),0) AS TONGTIEN " +
                "from DMHDB where month(NGAYBAN) = MONTH(GETDATE()) AND YEAR(NGAYBAN) = YEAR(GETDATE())" +
                " GROUP BY day( NGAYBAN)";

            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();

            da.Fill(ds, "Sales");
            // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
            chart1.DataSource = ds.Tables["Sales"];
            Series series1 = chart1.Series["Series1"];
            
            series1.ChartType = SeriesChartType.Column;
            series1.Name = "SALES";

            var chart = chart1;
            chart.Series[series1.Name].XValueMember = "day";
            chart.Series[series1.Name].YValueMembers = "TONGTIEN";
            //  chart.Series[0].IsValueShownAsLabel = true;
            // chart.Series[]

            chart1.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chart1.ChartAreas[0].AxisX;
          //  xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
        }
        public void LoadChart2()
        {
            sql = "select day(NGAYBAN) AS day, isnull(sum(DMHDB.SL),0) AS SOLUONG " +
                "from DMHDB where month(NGAYBAN) = MONTH(GETDATE()) AND YEAR(NGAYBAN) = YEAR(GETDATE())" +
             //   "AND DMHDB.MAHDB = DMCTHDB.MAHDB" +
                " GROUP BY day (NGAYBAN)";

            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();

            da.Fill(ds, "soluong");
            // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
            chart2.DataSource = ds.Tables["soluong"];
            Series series2 = chart2.Series["Series2"];

            series2.ChartType = SeriesChartType.Column;
            series2.Name = "SOLUONG";

            var chart = chart2;
            chart.Series[series2.Name].XValueMember = "day";
            chart.Series[series2.Name].YValueMembers = "SOLUONG";
            chart2.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chart2.ChartAreas[0].AxisX;
         //   xAxis.LabelStyle.Angle = 30;
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

        }

        public void LoadChart3()
        {
            sql = "select day( NGAYBAN) AS day, count(*) AS SL " +
                "from DMHDB where month(NGAYBAN) = MONTH(GETDATE()) AND YEAR(NGAYBAN) = YEAR(GETDATE())" +
                " GROUP BY day(NGAYBAN)";

            cmd = new SqlCommand(sql, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            da = new SqlDataAdapter(sql, conn);
            //cau lenh de data adapter  lay data ve, conn la dia chi, sql la lenh
            DataSet ds = new DataSet();

            da.Fill(ds, "donhang");
            // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
            chart3.DataSource = ds.Tables["donhang"];
            Series series3 = chart3.Series["Series3"];

            series3.ChartType = SeriesChartType.FastLine;
            series3.Name = "DONHANG";

            var chart = chart3;
            chart.Series[series3.Name].XValueMember = "day";
            chart.Series[series3.Name].YValueMembers = "SL";
            chart3.ChartAreas[0].AxisX.Interval = 1;
            Axis xAxis = chart3.ChartAreas[0].AxisX;
       
            xAxis.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

        }

        public void LoadChart4()
        {
            



           sql ="SELECT TOP(10) with ties" +

            " DMCTHDB.MAHH, TENHH, SUM(THANHTIEN) AS DOANHTHU"+

           " FROM DMHH, DMCTHDB, DMHDB"+

          " WHERE DMHH.MAHH = DMCTHDB.MAHH AND DMCTHDB.MAHDB = DMHDB.MAHDB AND month(NGAYBAN) = MONTH(GETDATE()) AND YEAR(NGAYBAN) = YEAR(GETDATE())" +

            " GROUP BY  DMCTHDB.MAHH, TENHH"+
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
            // đổ dữ liệu vừa lấy được phía trên vào bảng du lieu dt
            chart4.DataSource = ds.Tables["top10"];
            Series series4 = chart4.Series["Series4"];

            series4.ChartType = SeriesChartType.Pie;
            series4.Name = "SANPHAM";

            var chart = chart4;
            chart.Series[series4.Name].XValueMember = "TENHH";
            chart.Series[series4.Name].YValueMembers = "DOANHTHU";
           chart.Series[0].IsValueShownAsLabel = true;

        }
        private void chart2_Click(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblslsanpham_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
