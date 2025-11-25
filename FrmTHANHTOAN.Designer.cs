
namespace QLBDS
{
    partial class FrmTHANHTOAN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comNhom = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSOTIEN = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnluu = new Guna.UI2.WinForms.Guna2Button();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.guna2Panel1.BorderColor = System.Drawing.Color.Silver;
            this.guna2Panel1.Controls.Add(this.btnClose);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(485, 42);
            this.guna2Panel1.TabIndex = 43;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.HoverState.IconColor = System.Drawing.Color.White;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(444, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(41, 41);
            this.btnClose.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(30, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 21);
            this.label10.TabIndex = 77;
            this.label10.Text = "Hình thức thanh toán :";
            // 
            // comNhom
            // 
            this.comNhom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comNhom.BackColor = System.Drawing.Color.Transparent;
            this.comNhom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comNhom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comNhom.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comNhom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comNhom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comNhom.ForeColor = System.Drawing.Color.Black;
            this.comNhom.ItemHeight = 30;
            this.comNhom.Items.AddRange(new object[] {
            "Tiền mặt",
            "Quẹt thẻ",
            "Chuyển khoản"});
            this.comNhom.Location = new System.Drawing.Point(192, 66);
            this.comNhom.Name = "comNhom";
            this.comNhom.Size = new System.Drawing.Size(199, 36);
            this.comNhom.TabIndex = 78;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 21);
            this.label1.TabIndex = 79;
            this.label1.Text = "Số tiền thanh toán       :";
            // 
            // txtSOTIEN
            // 
            this.txtSOTIEN.BorderColor = System.Drawing.Color.Silver;
            this.txtSOTIEN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSOTIEN.DefaultText = "";
            this.txtSOTIEN.DisabledState.BorderColor = System.Drawing.Color.Silver;
            this.txtSOTIEN.DisabledState.FillColor = System.Drawing.Color.White;
            this.txtSOTIEN.DisabledState.ForeColor = System.Drawing.Color.Black;
            this.txtSOTIEN.DisabledState.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtSOTIEN.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.txtSOTIEN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSOTIEN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSOTIEN.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.txtSOTIEN.Location = new System.Drawing.Point(194, 130);
            this.txtSOTIEN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSOTIEN.Name = "txtSOTIEN";
            this.txtSOTIEN.PasswordChar = '\0';
            this.txtSOTIEN.PlaceholderText = "";
            this.txtSOTIEN.SelectedText = "";
            this.txtSOTIEN.Size = new System.Drawing.Size(199, 36);
            this.txtSOTIEN.TabIndex = 96;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.guna2Panel1;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.BorderRadius = 16;
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BorderColor = System.Drawing.Color.Silver;
            this.guna2Panel3.BorderThickness = 1;
            this.guna2Panel3.Controls.Add(this.btnluu);
            this.guna2Panel3.Controls.Add(this.btnThoat);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel3.Location = new System.Drawing.Point(0, 194);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(485, 81);
            this.guna2Panel3.TabIndex = 97;
            // 
            // btnluu
            // 
            this.btnluu.AccessibleName = "btnluu";
            this.btnluu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnluu.BorderRadius = 6;
            this.btnluu.BorderThickness = 1;
            this.btnluu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnluu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnluu.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnluu.DisabledState.ForeColor = System.Drawing.Color.DarkGray;
            this.btnluu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnluu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnluu.ForeColor = System.Drawing.Color.White;
            this.btnluu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnluu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnluu.HoverState.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnluu.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnluu.Location = new System.Drawing.Point(259, 22);
            this.btnluu.Name = "btnluu";
            this.btnluu.PressedColor = System.Drawing.Color.White;
            this.btnluu.Size = new System.Drawing.Size(86, 37);
            this.btnluu.TabIndex = 74;
            this.btnluu.Text = "Lưu";
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnThoat.BorderRadius = 6;
            this.btnThoat.BorderThickness = 1;
            this.btnThoat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThoat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThoat.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnThoat.DisabledState.ForeColor = System.Drawing.Color.DarkGray;
            this.btnThoat.FillColor = System.Drawing.Color.White;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnThoat.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnThoat.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(71)))));
            this.btnThoat.HoverState.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(365, 22);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(86, 37);
            this.btnThoat.TabIndex = 75;
            this.btnThoat.Text = "Thoát";
            // 
            // FrmTHANHTOAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(485, 275);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.txtSOTIEN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comNhom);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTHANHTOAN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTHANHTOAN";
            this.Load += new System.EventHandler(this.FrmTHANHTOAN_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox btnClose;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2ComboBox comNhom;
        private System.Windows.Forms.Label label1;
        public Guna.UI2.WinForms.Guna2TextBox txtSOTIEN;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        public Guna.UI2.WinForms.Guna2Button btnluu;
        public Guna.UI2.WinForms.Guna2Button btnThoat;
    }
}