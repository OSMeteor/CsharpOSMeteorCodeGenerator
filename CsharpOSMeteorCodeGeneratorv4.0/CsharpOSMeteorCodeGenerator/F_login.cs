using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Data.SqlClient;


namespace CsharpOSMeteorCodeGenerator
{
    public partial class F_login : Form
    {
        private SqlConnectionStringBuilder connBuilder;
        public F_login()
        {
            InitializeComponent();
            comb_Authentication.SelectedIndex = 0;
            connBuilder = new SqlConnectionStringBuilder();
        }
        public bool AuthenticationIsWindows = false;
        private bool sucess = false;
        F_Main fr = null;
        private void btn_Connection_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(comb_serverName.Text))
            {
                connBuilder.DataSource = comb_serverName.Text;
                connBuilder.PersistSecurityInfo = true;
                if (!AuthenticationIsWindows)
                {
                    connBuilder.UserID = comb_UserName.Text;
                    connBuilder.Password = txt_PassWord.Text;
                }
                else
                {
                    connBuilder.IntegratedSecurity = true;
                }
                try
                {
                    //两种连接方式
                    //Data Source=.;Integrated Security=True;Persist Security Info=True
                    //Data Source=.;Persist Security Info=True;User ID=sa;Password=
                    using (SqlConnection dbConnection = new SqlConnection(connBuilder.ConnectionString))
                    {
                        dbConnection.Open();
                       DataTable tempDataTable = dbConnection.GetSchema(SqlClientMetaDataCollectionNames.Databases);
                        //DataTable tempDataTable = dbConnection.GetSchema("Databases");   //和相同效果                     
                        //cmbDatabase.DataSource = tempDataTable;                        
                        //cmbDatabase.DisplayMember = tempDataTable.Columns["database_name"].ColumnName;
                        //cmbDatabase.ValueMember = tempDataTable.Columns["database_name"].ColumnName;                       
                        //MessageBox.Show("Connected successfully!", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        dbConnection.Close();
                    }
                    Model.EModel.connBuilder = connBuilder;
                    if (fr == null)
                    {
                        fr = new F_Main();
                        this.Hide();
                        fr.Show();                     
                    }
                    else {
                        fr.Close();
                    }
                    sucess = true;
                    //cmbDatabase.Enabled = sucess;
                }
                catch (SqlException)
                {
                    txt_PassWord.Text = String.Empty;
                    connBuilder.Clear();
                    MessageBox.Show("Connection failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                connBuilder.Clear();
                MessageBox.Show("Please select a server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {           
            System.Environment.Exit(0);
        }

        private void comb_Authentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.comb_Authentication.Text))
            {
                return;
            }
            if (this.comb_Authentication.Text == "Windows Authentication")
            {
                this.comb_UserName.Enabled = false;
                this.txt_PassWord.Enabled = false;
                this.check_Remember.Checked = false;
                this.check_Remember.Enabled = false;
                AuthenticationIsWindows = true;
            }
            else if (this.comb_Authentication.Text == "Sql Server Authentication")
            {

                this.comb_UserName.Enabled = true;
                this.txt_PassWord.Enabled = true;
                this.check_Remember.Enabled = true;
                AuthenticationIsWindows = false;
            }
            else {
                return;
            }
        }
    }
}
