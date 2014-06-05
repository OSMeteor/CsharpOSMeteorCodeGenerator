namespace CsharpOSMeteorCodeGenerator
{
    partial class F_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lstImage = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.rad_linq = new System.Windows.Forms.RadioButton();
            this.rad_ado = new System.Windows.Forms.RadioButton();
            this.rad_sql = new System.Windows.Forms.RadioButton();
            this.rad_isEntity = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 515);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 4;
            this.treeView1.ImageList = this.lstImage;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 4;
            this.treeView1.Size = new System.Drawing.Size(224, 495);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
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
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 465);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(230, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(724, 515);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Code";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.rad_linq);
            this.panel2.Controls.Add(this.rad_ado);
            this.panel2.Controls.Add(this.rad_sql);
            this.panel2.Controls.Add(this.rad_isEntity);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 30);
            this.panel2.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(575, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(77, 12);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "About author";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // rad_linq
            // 
            this.rad_linq.AutoSize = true;
            this.rad_linq.Location = new System.Drawing.Point(235, 8);
            this.rad_linq.Name = "rad_linq";
            this.rad_linq.Size = new System.Drawing.Size(47, 16);
            this.rad_linq.TabIndex = 7;
            this.rad_linq.TabStop = true;
            this.rad_linq.Text = "linq";
            this.rad_linq.UseVisualStyleBackColor = true;
            this.rad_linq.CheckedChanged += new System.EventHandler(this.rad_linq_CheckedChanged);
            // 
            // rad_ado
            // 
            this.rad_ado.AutoSize = true;
            this.rad_ado.Location = new System.Drawing.Point(188, 9);
            this.rad_ado.Name = "rad_ado";
            this.rad_ado.Size = new System.Drawing.Size(41, 16);
            this.rad_ado.TabIndex = 8;
            this.rad_ado.TabStop = true;
            this.rad_ado.Text = "ADO";
            this.rad_ado.UseVisualStyleBackColor = true;
            this.rad_ado.CheckedChanged += new System.EventHandler(this.rad_ado_CheckedChanged);
            // 
            // rad_sql
            // 
            this.rad_sql.AutoSize = true;
            this.rad_sql.Location = new System.Drawing.Point(140, 8);
            this.rad_sql.Name = "rad_sql";
            this.rad_sql.Size = new System.Drawing.Size(41, 16);
            this.rad_sql.TabIndex = 6;
            this.rad_sql.TabStop = true;
            this.rad_sql.Text = "sql";
            this.rad_sql.UseVisualStyleBackColor = true;
            this.rad_sql.CheckedChanged += new System.EventHandler(this.rad_sql_CheckedChanged);
            // 
            // rad_isEntity
            // 
            this.rad_isEntity.AutoSize = true;
            this.rad_isEntity.Location = new System.Drawing.Point(76, 8);
            this.rad_isEntity.Name = "rad_isEntity";
            this.rad_isEntity.Size = new System.Drawing.Size(59, 16);
            this.rad_isEntity.TabIndex = 4;
            this.rad_isEntity.TabStop = true;
            this.rad_isEntity.Text = "Entity";
            this.rad_isEntity.UseVisualStyleBackColor = true;
            this.rad_isEntity.CheckedChanged += new System.EventHandler(this.rad_isEntity_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose:";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "CodeGenerator";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // contextMenu2
            // 
            this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7});
            this.menuItem2.Text = "ADO";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Select-field";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "Insert-field";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 2;
            this.menuItem6.Text = "Update-field";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "Delete-field";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Linq";
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 515);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CsharpOSMeteorCodeGenerator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F_Main_FormClosed);
            this.Load += new System.EventHandler(this.F_Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList lstImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rad_linq;
        private System.Windows.Forms.RadioButton rad_ado;
        private System.Windows.Forms.RadioButton rad_sql;
        private System.Windows.Forms.RadioButton rad_isEntity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenu contextMenu2;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
    }
}