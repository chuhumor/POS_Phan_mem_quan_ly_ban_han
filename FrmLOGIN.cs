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

namespace QLBDS
{
    public partial class FrmLOGIN : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr;
        DataTable dt = new DataTable();
        string sql, constr, username, userright;
        int i = 0;

        public FrmLOGIN()
        {
            InitializeComponent();
        }
  
    


        private void FrmLOGIN_Load(object sender, EventArgs e)
        {
            lblmessage.Text = "";
            float scaleFactor = 1.0f;
            using (Graphics graphics = this.CreateGraphics())
            {
                scaleFactor = graphics.DpiX / 96f; // 96f là DPI mặc định
            }

            this.Scale(new SizeF(scaleFactor, scaleFactor));
            constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;"
                        + "Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string _manv ="", _role = "", _name = "", _anh="";
       // _username = "",
            sql = "select *from DMTK where USERNAME ='" + txtuser.Text +
                "' and PASSWORD = '" + txtpass.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                string query = "SELECT DMNV.MANV,TENNV, CHUCVU, ANHNV FROM DMNV, DMTK WHERE USERNAME = @username AND DMNV.MANV=DMTK.MANV" ;
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", txtuser.Text);
                SqlDataReader reader = command.ExecuteReader();

                //dang nhap thanh cong
                username = dt.Rows[0][0].ToString();
                //username sẽ lấy gtri của cột 0, dòng 0 trong datatable
                userright = dt.Rows[0][3].ToString();
                //dong 0 cot 3
                if (reader.Read())
                {
                   // _username = reader["USERNAME"].ToString();
                   _manv = reader["MANV"].ToString();
                    _role = reader["CHUCVU"].ToString();
                    _name = reader["TENNV"].ToString();
                    _anh = reader["ANHNV"].ToString();
                
                    reader.Close();
                  
                }
          
                    txtuser.Clear();
                    txtpass.Clear();
                    this.Hide();
                if (_role == "Quản lý")
                {
                    FrmMAIN frm = new FrmMAIN(_manv, _anh, _name, _role);
                    frm.txtName.Text = _name;
                    frm.txtChucvu.Text = _role;
                    frm.picaccount.Image = Image.FromFile(_anh);
                    frm.Show();
                    frm.btnhome_Click(sender, e);
                    frm.btnhome.Enabled = true;
                    frm.btncategory.Enabled = true;
                    frm.btncustomer.Enabled = true;
                    frm.btnemploy.Enabled = true;
                    frm.btnhelp.Enabled = true;
                    frm.btnhome.Enabled = true;
                    frm.btnncc.Enabled = true;
                    frm.btnproduct.Enabled = true;
                    frm.btnthongke.Enabled = true;
                }
                else {
                    FrmMAIN frm = new FrmMAIN(_manv, _anh, _name, _role);
                    frm.txtName.Text = _name;
                    frm.txtChucvu.Text = _role;
                    frm.picaccount.Image = Image.FromFile(_anh);
                    frm.Show();
                    frm.btndonhang_Click(sender, e);
                    frm.btnhome.Enabled = false;
                    frm.btncategory.Enabled = false;
                    frm.btncustomer.Enabled = false;
                    frm.btnemploy.Enabled = false;
                    frm.btnhelp.Enabled = false;
                    frm.btnhome.Enabled = false;
                    frm.btnncc.Enabled = false;
                    frm.btnproduct.Enabled = false;
                    frm.btnthongke.Enabled = false;
                }


               

                this.Hide();
            }
            else
            {
                //dang nhap that bai
                i++;
                if (i > 3)
                {
                    MessageBox.Show("Bạn đã đăng nhập quá 3 lần!");
                    Application.Exit();
                }
                else
                {
                    i++;
                    lblmessage.Text = "Thông tin đăng nhập không chính xác, hãy kiểm tra lại";

                    txtuser.Focus();
                    //để con trỏ nhấp nháy chỗ user 
                }
            }
        }

        private void QuenMK_Click(object sender, EventArgs e)
        {
            string _username = txtuser.Text;
            string _email = "";

            sql = "select EMAIL from DMNV WHERE MANV IN(SELECT MANV FROM DMTK WHERE USERNAME = '" + txtuser.Text + "')";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                _email = dt.Rows[0]["EMAIL"].ToString(); // Lấy giá trị EMAIL từ DataTable dt
                                                         // Mở form FrmFORGET1 với _username đã lấy và _email từ dữ liệu
                FrmFORGET1 frm = new FrmFORGET1(_username, _email);
                this.Hide();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Không tìm thấy email!");
            }
        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
