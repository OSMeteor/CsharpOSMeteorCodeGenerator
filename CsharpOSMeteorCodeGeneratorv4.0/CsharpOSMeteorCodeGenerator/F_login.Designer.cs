namespace CsharpOSMeteorCodeGenerator
{
    partial class F_login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_login));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comb_serverName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.combx_servertype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comb_Authentication = new System.Windows.Forms.ComboBox();
            this.comb_UserName = new System.Windows.Forms.ComboBox();
            this.txt_PassWord = new System.Windows.Forms.TextBox();
            this.btn_Connection = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.check_Remember = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ServeName:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(533, 87);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // comb_serverName
            // 
            this.comb_serverName.FormattingEnabled = true;
            this.comb_serverName.Items.AddRange(new object[] {
            ".",
            "(local)"});
            this.comb_serverName.Location = new System.Drawing.Point(191, 133);
            this.comb_serverName.Name = "comb_serverName";
            this.comb_serverName.Size = new System.Drawing.Size(298, 20);
            this.comb_serverName.TabIndex = 3;
            this.comb_serverName.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "ServeType:";
            // 
            // combx_servertype
            // 
            this.combx_servertype.Enabled = false;
            this.combx_servertype.FormattingEnabled = true;
            this.combx_servertype.Items.AddRange(new object[] {
            "database engine"});
            this.combx_servertype.Location = new System.Drawing.Point(191, 105);
            this.combx_servertype.Name = "combx_servertype";
            this.combx_servertype.Size = new System.Drawing.Size(298, 20);
            this.combx_servertype.TabIndex = 4;
            this.combx_servertype.Text = "database engine";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Authentication:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "UserName:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Password:";
            // 
            // comb_Authentication
            // 
            this.comb_Authentication.FormattingEnabled = true;
            this.comb_Authentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "Sql Server Authentication"});
            this.comb_Authentication.Location = new System.Drawing.Point(191, 161);
            this.comb_Authentication.Name = "comb_Authentication";
            this.comb_Authentication.Size = new System.Drawing.Size(298, 20);
            this.comb_Authentication.TabIndex = 3;
            this.comb_Authentication.SelectedIndexChanged += new System.EventHandler(this.comb_Authentication_SelectedIndexChanged);
            // 
            // comb_UserName
            // 
            this.comb_UserName.FormattingEnabled = true;
            this.comb_UserName.Items.AddRange(new object[] {
            "sa"});
            this.comb_UserName.Location = new System.Drawing.Point(206, 189);
            this.comb_UserName.Name = "comb_UserName";
            this.comb_UserName.Size = new System.Drawing.Size(283, 20);
            this.comb_UserName.TabIndex = 3;
            this.comb_UserName.Text = "sa";
            // 
            // txt_PassWord
            // 
            this.txt_PassWord.Location = new System.Drawing.Point(206, 219);
            this.txt_PassWord.Name = "txt_PassWord";
            this.txt_PassWord.Size = new System.Drawing.Size(283, 21);
            this.txt_PassWord.TabIndex = 5;
            // 
            // btn_Connection
            // 
            this.btn_Connection.Location = new System.Drawing.Point(204, 293);
            this.btn_Connection.Name = "btn_Connection";
            this.btn_Connection.Size = new System.Drawing.Size(75, 23);
            this.btn_Connection.TabIndex = 6;
            this.btn_Connection.Text = "Connection";
            this.btn_Connection.UseVisualStyleBackColor = true;
            this.btn_Connection.Click += new System.EventHandler(this.btn_Connection_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(311, 293);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // check_Remember
            // 
            this.check_Remember.AutoSize = true;
            this.check_Remember.Location = new System.Drawing.Point(206, 254);
            this.check_Remember.Name = "check_Remember";
            this.check_Remember.Size = new System.Drawing.Size(150, 16);
            this.check_Remember.TabIndex = 7;
            this.check_Remember.Text = "Remember the password";
            this.check_Remember.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(14, 280);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(489, 4);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // F_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(525, 327);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.check_Remember);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Connection);
            this.Controls.Add(this.txt_PassWord);
            this.Controls.Add(this.combx_servertype);
            this.Controls.Add(this.comb_UserName);
            this.Controls.Add(this.comb_Authentication);
            this.Controls.Add(this.comb_serverName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to the database engine";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comb_serverName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combx_servertype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comb_Authentication;
        private System.Windows.Forms.ComboBox comb_UserName;
        private System.Windows.Forms.TextBox txt_PassWord;
        private System.Windows.Forms.Button btn_Connection;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.CheckBox check_Remember;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}

