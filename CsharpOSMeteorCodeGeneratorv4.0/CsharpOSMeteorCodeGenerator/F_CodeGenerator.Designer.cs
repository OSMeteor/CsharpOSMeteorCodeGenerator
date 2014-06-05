namespace CsharpOSMeteorCodeGenerator
{
    partial class F_CodeGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_CodeGenerator));
            this.lstImage = new System.Windows.Forms.ImageList(this.components);
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btn_browse = new System.Windows.Forms.Button();
            this.txt_foldPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llab_checkAllorNot = new System.Windows.Forms.LinkLabel();
            this.txt_namespace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_CodeGenerator_DLL = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lab_pBar = new System.Windows.Forms.Label();
            this.chk_Linq = new System.Windows.Forms.CheckBox();
            this.chk_ADO = new System.Windows.Forms.CheckBox();
            this.btn_CodeGenerator = new System.Windows.Forms.Button();
            this.chk_isEntity = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstImage
            // 
            this.lstImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstImage.ImageStream")));
            this.lstImage.TransparentColor = System.Drawing.Color.Transparent;
            this.lstImage.Images.SetKeyName(0, "table-icon.png");
            this.lstImage.Images.SetKeyName(1, "field-icon.png");
            this.lstImage.Images.SetKeyName(2, "primary_key-icon.png");
            this.lstImage.Images.SetKeyName(3, "foreign_key-icon.png");
            this.lstImage.Images.SetKeyName(4, "db.png");
            this.lstImage.Images.SetKeyName(5, "00060.png");
            this.lstImage.Images.SetKeyName(6, "views.png");
            this.lstImage.Images.SetKeyName(7, "proc.png");
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(3, 15);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(474, 19);
            this.pBar.TabIndex = 2;
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(378, 12);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(74, 23);
            this.btn_browse.TabIndex = 3;
            this.btn_browse.Text = "browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txt_foldPath
            // 
            this.txt_foldPath.Location = new System.Drawing.Point(91, 12);
            this.txt_foldPath.Name = "txt_foldPath";
            this.txt_foldPath.Size = new System.Drawing.Size(255, 21);
            this.txt_foldPath.TabIndex = 4;
            this.txt_foldPath.Text = "C:\\Users\\Administrator\\Desktop\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Save Path:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llab_checkAllorNot);
            this.panel1.Controls.Add(this.txt_namespace);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txt_foldPath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_browse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 370);
            this.panel1.TabIndex = 6;
            // 
            // llab_checkAllorNot
            // 
            this.llab_checkAllorNot.AutoSize = true;
            this.llab_checkAllorNot.Location = new System.Drawing.Point(367, 51);
            this.llab_checkAllorNot.Name = "llab_checkAllorNot";
            this.llab_checkAllorNot.Size = new System.Drawing.Size(101, 12);
            this.llab_checkAllorNot.TabIndex = 10;
            this.llab_checkAllorNot.TabStop = true;
            this.llab_checkAllorNot.Text = "check all or not";
            this.llab_checkAllorNot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llab_checkAllorNot_LinkClicked);
            this.llab_checkAllorNot.Click += new System.EventHandler(this.llab_checkAllorNot_LinkClicked);
            // 
            // txt_namespace
            // 
            this.txt_namespace.Location = new System.Drawing.Point(91, 41);
            this.txt_namespace.Name = "txt_namespace";
            this.txt_namespace.Size = new System.Drawing.Size(255, 21);
            this.txt_namespace.TabIndex = 9;
            this.txt_namespace.Text = "MeteorCode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "NameSpace";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_CodeGenerator_DLL);
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Controls.Add(this.lab_pBar);
            this.panel2.Controls.Add(this.chk_Linq);
            this.panel2.Controls.Add(this.chk_ADO);
            this.panel2.Controls.Add(this.btn_CodeGenerator);
            this.panel2.Controls.Add(this.chk_isEntity);
            this.panel2.Controls.Add(this.pBar);
            this.panel2.Location = new System.Drawing.Point(3, 298);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(477, 72);
            this.panel2.TabIndex = 7;
            // 
            // btn_CodeGenerator_DLL
            // 
            this.btn_CodeGenerator_DLL.Location = new System.Drawing.Point(192, 40);
            this.btn_CodeGenerator_DLL.Name = "btn_CodeGenerator_DLL";
            this.btn_CodeGenerator_DLL.Size = new System.Drawing.Size(116, 23);
            this.btn_CodeGenerator_DLL.TabIndex = 8;
            this.btn_CodeGenerator_DLL.Text = "CodeGeneratorDLL";
            this.btn_CodeGenerator_DLL.UseVisualStyleBackColor = true;
            this.btn_CodeGenerator_DLL.Visible = false;
            this.btn_CodeGenerator_DLL.Click += new System.EventHandler(this.btn_CodeGenerator_DLL_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(406, 41);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(59, 23);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lab_pBar
            // 
            this.lab_pBar.AutoSize = true;
            this.lab_pBar.ForeColor = System.Drawing.Color.Red;
            this.lab_pBar.Location = new System.Drawing.Point(74, 0);
            this.lab_pBar.Name = "lab_pBar";
            this.lab_pBar.Size = new System.Drawing.Size(29, 12);
            this.lab_pBar.TabIndex = 6;
            this.lab_pBar.Text = "pBar";
            // 
            // chk_Linq
            // 
            this.chk_Linq.AutoSize = true;
            this.chk_Linq.Location = new System.Drawing.Point(124, 45);
            this.chk_Linq.Name = "chk_Linq";
            this.chk_Linq.Size = new System.Drawing.Size(48, 16);
            this.chk_Linq.TabIndex = 5;
            this.chk_Linq.Text = "Linq";
            this.chk_Linq.UseVisualStyleBackColor = true;
            // 
            // chk_ADO
            // 
            this.chk_ADO.AutoSize = true;
            this.chk_ADO.Location = new System.Drawing.Point(76, 45);
            this.chk_ADO.Name = "chk_ADO";
            this.chk_ADO.Size = new System.Drawing.Size(42, 16);
            this.chk_ADO.TabIndex = 5;
            this.chk_ADO.Text = "ADO";
            this.chk_ADO.UseVisualStyleBackColor = true;
            // 
            // btn_CodeGenerator
            // 
            this.btn_CodeGenerator.Location = new System.Drawing.Point(314, 41);
            this.btn_CodeGenerator.Name = "btn_CodeGenerator";
            this.btn_CodeGenerator.Size = new System.Drawing.Size(86, 23);
            this.btn_CodeGenerator.TabIndex = 4;
            this.btn_CodeGenerator.Text = "Generator";
            this.btn_CodeGenerator.UseVisualStyleBackColor = true;
            this.btn_CodeGenerator.Click += new System.EventHandler(this.btn_CodeGenerator_Click);
            // 
            // chk_isEntity
            // 
            this.chk_isEntity.AutoSize = true;
            this.chk_isEntity.Location = new System.Drawing.Point(10, 44);
            this.chk_isEntity.Name = "chk_isEntity";
            this.chk_isEntity.Size = new System.Drawing.Size(60, 16);
            this.chk_isEntity.TabIndex = 3;
            this.chk_isEntity.Text = "Entity";
            this.chk_isEntity.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(0, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 233);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TableAndView";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.lstImage;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(474, 213);
            this.treeView1.TabIndex = 0;
            // 
            // F_CodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 370);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_CodeGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_CodeGenerator";
            this.Load += new System.EventHandler(this.F_CodeGenerator_Load);
            this.Shown += new System.EventHandler(this.F_CodeGenerator_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList lstImage;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox txt_foldPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_CodeGenerator;
        private System.Windows.Forms.CheckBox chk_isEntity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox chk_Linq;
        private System.Windows.Forms.CheckBox chk_ADO;
        private System.Windows.Forms.Label lab_pBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_namespace;
        private System.Windows.Forms.LinkLabel llab_checkAllorNot;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_CodeGenerator_DLL;
    }
}