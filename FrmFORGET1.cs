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
using System.IO;
using System.Net;
using System.Net.Mail;

namespace QLBDS
{
    public partial class FrmFORGET1 : Form
    {
        FrmLOGIN f;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        private string _username ;
        private string _email;

        string sql, constr;
        int i =0;
        string OTPCode;
        public static string to;
        public FrmFORGET1(string username, string email)
        {
            InitializeComponent();
            _username = username;
            _email = email;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmFORGET1_Load(object sender, EventArgs e)
        {


            //constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            //conn.ConnectionString = constr;
            //conn.Open();
        }
        private string fromEmail = "your-email@gmail.com"; // Thay bằng địa chỉ email của bạn
        private string fromPassword = "your-email-password"; // Thay bằng mật khẩu của email
        private Random random = new Random();

        private string GenerateRandomCode()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập Email !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else
            {
                constr = "Data Source=DESKTOP-MN9JIQR\\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
                conn.ConnectionString = constr;

                // Kiểm tra xem kết nối đã mở chưa
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                sql = "SELECT * FROM DMNV WHERE EMAIL = @email";
                da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@email", email);
                dt.Clear();
                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    // Email hợp lệ, tiến hành gửi mã code qua email
                    string code = GenerateRandomCode(); // Hàm tạo mã ngẫu nhiên
                    string subject = "Mã xác thực";
                    string body = "Mã xác thực của bạn là: " + code;

                    try
                    {
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromEmail, fromPassword),
                            EnableSsl = true,
                        };

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(fromEmail);
                        mailMessage.To.Add(email);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;

                        smtpClient.Send(mailMessage);

                        // Mở FrmFORGET2 và tiếp tục xử lý
                        FrmFORGET2 frm = new FrmFORGET2();
                        frm.TopLevel = false;
                        panelbody.Controls.Add(frm);
                        frm.BringToFront();
                        frm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi trong quá trình gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Email không hợp lệ. Vui lòng kiểm tra lại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                }
            }
        }
        private void SendCodeByEmail(string toEmail, string otpCode)
        {
            // Cấu hình thông tin email
            string fromEmail = "your_email@gmail.com";
            string fromPassword = "your_password";
            string subject = "Mã xác nhận";

            // Tạo nội dung email
            string body = $"Mã xác nhận của bạn là: {otpCode}";

            // Tạo đối tượng SmtpClient để gửi email
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            // Gửi email
            using (MailMessage message = new MailMessage(fromEmail, toEmail, subject, body))
            {
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message);
                }
            }
        }

    }
}
