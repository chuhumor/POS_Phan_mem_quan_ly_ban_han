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
    public partial class FrmPROFILE : Form
    {

        private string _maNhanVien;

        string sql, constr;


        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataTable com2dt = new DataTable();
        SqlDataReader dr;
        FrmLOGIN f;
        // FrmMAIN f1;

        public FrmPROFILE(string maNhanVien)//
        {
            InitializeComponent();
            _maNhanVien = maNhanVien;
        }

        private void FrmPROFILE_Load(object sender, EventArgs e)
        {
            try
            {
                guna2ShadowForm1.SetShadowForm(this);
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;

                // Hiển thị thông tin hóa đơn nhập từ CSDL lên các label tương ứng
                string queryNV = $"SELECT TENNV, SDT,EMAIL, DIACHI, ANHNV FROM DMNV" +
                    $" WHERE MANV= '{_maNhanVien}'";


                conn.Open();

                SqlCommand cmd = new SqlCommand(queryNV, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblTENNV.Text = reader["TENNV"].ToString();
                    lblSDT.Text = reader["SDT"].ToString();
                    lblEMAIL.Text = reader["EMAIL"].ToString();
                    lblDIACHI.Text = reader["DIACHI"].ToString();

                    string imagePath = reader["ANHNV"].ToString();
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        picaccount.Image = Image.FromFile(imagePath);
                    }
                }

                reader.Close();
          
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi: " + err.Message);
            }
        }
    }
}
