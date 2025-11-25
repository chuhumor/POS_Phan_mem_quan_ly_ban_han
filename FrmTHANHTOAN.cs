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
    public partial class FrmTHANHTOAN : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        //private string loggedInUsername;
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        string sql, constr;
        public string MAHDN { get; set; }
        public string maHDN;
        public string maNCC;
        public int conPhaiTra;

        FrmCTHDN f;
        public FrmTHANHTOAN(string maHDN, string maNCC, int conPhaiTra)
        {
            InitializeComponent();
            this.maHDN = maHDN;
            this.maNCC = maNCC;
            this.conPhaiTra = conPhaiTra;
        }

        private void FrmTHANHTOAN_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();


            conn.Close();
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                // Update thông tin trong DMHDN
                sql = $"UPDATE DMHDN SET DATRA = DATRA +  @SoTien where MAHDN = '{maHDN}'";
                using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                {
                    updateHH0Command.Parameters.AddWithValue("@SoTien", Convert.ToInt32(txtSOTIEN.Text));
                    updateHH0Command.Parameters.AddWithValue("@MaHDN", maHDN);

                    //  updateHHCommand.Parameters.AddWithValue("@MaHH", row["MAHH"].ToString());
                    updateHH0Command.ExecuteNonQuery();
                }
                // Update thông tin trong DMHDN
                sql = $"UPDATE DMHDN SET PHAITRA = PHAITRA -  @SoTien where MAHDN = '{maHDN}'";
                using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                {
                    updateHH0Command.Parameters.AddWithValue("@SoTien", Convert.ToInt32(txtSOTIEN.Text));
                    updateHH0Command.Parameters.AddWithValue("@MaHDN", maHDN);

                    updateHH0Command.ExecuteNonQuery();
                }
                int SoTien = Convert.ToInt32(txtSOTIEN.Text);
                if (SoTien == conPhaiTra)
                {


                    // Update thông tin trong DMHDN
                    sql = $"UPDATE DMHDN SET TRANGTHAI = 1 where MAHDN = '{maHDN}'";
                    using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                    {
                        updateHH0Command.Parameters.AddWithValue("@MaHDN", maHDN);


                        updateHH0Command.ExecuteNonQuery();
                    }
                }
                // Update thông tin trong DMNHACC
                sql = $"UPDATE DMNHACC SET CONGNO = CONGNO -  @SoTien where MANCC = '{maNCC}'";
                using (SqlCommand gf = new SqlCommand(sql, conn))
                {
                    gf.Parameters.AddWithValue("@SoTien", Convert.ToInt32(txtSOTIEN.Text));
                    
                    gf.Parameters.AddWithValue("@MaNCC", maNCC);
                    MessageBox.Show(maNCC + Convert.ToInt32(txtSOTIEN.Text));

                    gf.ExecuteNonQuery();
                }

                // Update thông tin trong TTDONNHAP
                DateTime ngayTTHienTai = DateTime.Now;

                string formattedNgayTT = ngayTTHienTai.ToString("yyyy-MM-dd HH:mm:ss");

                sql = $"INSERT INTO TTDONNHAP VALUES (@MaHDN, @HTTT,@SoTien, @ngayTT)";
                using (SqlCommand updateHH0Command = new SqlCommand(sql, conn))
                {
                    if (comNhom.SelectedItem == null)
                    {
                        updateHH0Command.Parameters.AddWithValue("@HTTT", 0);

                    }
                    else if (comNhom.SelectedItem != null)
                    {
                        if (comNhom.SelectedItem.ToString() == "Tiền mặt")
                        {
                            updateHH0Command.Parameters.AddWithValue("@HTTT", 0);
                        }
                        else if (comNhom.SelectedItem.ToString() == "Quẹt thẻ")
                        {
                            updateHH0Command.Parameters.AddWithValue("@HTTT", 1);

                        }
                        else if (comNhom.SelectedItem.ToString() == "Chuyển khoản")
                        {
                            updateHH0Command.Parameters.AddWithValue("@HTTT", 2);

                        }

                    }
                    updateHH0Command.Parameters.AddWithValue("@ngayTT", formattedNgayTT);

                    updateHH0Command.Parameters.AddWithValue("@SoTien", Convert.ToInt32(txtSOTIEN.Text));
                    updateHH0Command.Parameters.AddWithValue("@maHDN", maHDN);

                    updateHH0Command.ExecuteNonQuery();
                }

                MessageBox.Show("Thanh toán thành công !");

                conn.Close();

            }
        }
    }
}
