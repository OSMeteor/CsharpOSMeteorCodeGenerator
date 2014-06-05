using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsharpOSMeteorCodeGenerator.Model;
using ICSharpCode.TextEditor;
using CsharpOSMeteorCodeGenerator.Data;
using System.IO;

namespace CsharpOSMeteorCodeGenerator
{
    public partial class F_ADO_CodeGenerator : Form
    {
        public F_ADO_CodeGenerator()
        {
            InitializeComponent();
        }
        public object selectnode { get; set; }
        public string SQLType { get; set; }
        public string Pidnode_dbname { get; set; }
        string Pidnode_TBname = "";
        //List<Table> list_tab;
        //List<Table> list_tab2;
        int type = 0;
      
        private void F_ADO_CodeGenerator_Load(object sender, EventArgs e)
        {
            F_AD0_Load();
            if (SQLType == "Select") {
                type = 1;
               
            }
            if (SQLType == "Update")
            {
                type = 2;
            }
            if (SQLType == "Insert")
            {
                type = 3;
                this.groupBox2.Hide();
                //this.tvw_field.Enabled = false;                   
                //this.llab_checkAllorNot.Hide();
                //NodecheckAllorNot(true, this.tvw_field);
                setIsnull_insert(this.tvw_field);
            }
            if (SQLType == "Delete")
            {
                type = 4;
                this.groupBox1.Hide();
            }
            this.label1.Text = SQLType;
            this.txt_methodsname.Text = Pidnode_TBname + SQLType + "_Custom";
            
        }
        void F_AD0_Load() {
            this.btn_CodeGenerator.Enabled = false;
            this.bun_back.Enabled = false;
            this.llab_checkAllorNot.Enabled = false;
            this.llab_checkAllorNot_Click1.Enabled = false;
            
            OSMyNode node = (OSMyNode)this.selectnode;
            Pidnode_TBname = node.Text;
            Data.GetListTables glt = new Data.GetListTables();
            Data.ColumnData dcd = new Data.ColumnData();
            Data.TableData tabdata = new Data.TableData();
            List<Column> list_column = tabdata.ListTablesColumn(node.Text, Model.EModel.connBuilder);
            OSMyNode Tnode;
            foreach (Column column in list_column)
            {
                Tnode = new OSMyNode();
                Tnode.Value = column;
                //Tnode.Text = column.Name;
                Tnode.Text = column.Name + " \n(" + ListarAtributos(column) + ")";
                Tnode.ToolTipText = column.Name + " \n(" + ListarAtributos(column) + ")";
                Tnode.ImageIndex = GetImageIndex(column);
                Tnode.SelectedImageIndex = GetImageIndex(column);
                this.tvw_field.Nodes.Add(Tnode);
                //OSMyNode Tnode2 = new OSMyNode();
                //Tnode2 = Tnode;
                Tnode = new OSMyNode();
                Tnode.Value = column;
                //Tnode.Text = column.Name;
                Tnode.Text = column.Name + " \n(" + ListarAtributos(column) + ")";
                Tnode.ToolTipText = column.Name + " \n(" + ListarAtributos(column) + ")";
                Tnode.ImageIndex = GetImageIndex(column);
                Tnode.SelectedImageIndex = GetImageIndex(column);
                this.tvw_field_where.Nodes.Add(Tnode);
            }
            this.btn_CodeGenerator.Enabled = true;
            this.bun_back.Enabled = true;
            this.llab_checkAllorNot.Enabled = true;
            this.llab_checkAllorNot_Click1.Enabled = true;
        }
        private int GetImageIndex(Column column)
        {
            if (column.PrimaryKey)
            {
                return (int)DBImage.PRIMARY_KEY;
            }
            else if (column.ForeignKey)
            {
                return (int)DBImage.FOREIGN_KEY;
            }
            else
            {
                return (int)DBImage.FIELD;
            }
        }
        /// <summary>
        ///  Database's objects image
        /// </summary>
        public enum DBImage : int
        {
            TABLE = 0,
            FIELD = 1,
            PRIMARY_KEY = 2,
            FOREIGN_KEY = 3
        }
        private string ListarAtributos(Column column)
        {
            string attributes = String.Empty;

            if (column.PrimaryKey)
            {
                attributes += "PK, ";
            }

            if (column.ForeignKey)
            {
                attributes += "FK, ";
            }

            if (column.CharacterMaximumLength != null)
            {
                attributes += column.Type + "(" + column.CharacterMaximumLength + "), ";
            }
            else
            {
                attributes += column.Type + ", ";
            }

            attributes += (column.Nullable) ? "null" : "not null";

            return attributes;
        }
        public void NodecheckAllorNot(Boolean checkstate,System.Windows.Forms.TreeView Trv )
        {

            foreach (OSMyNode node in Trv.Nodes)
            {
                node.Checked = checkstate;
            }

        }
        int checkT = 1;
        private void llab_checkAllorNot_Click(object sender, EventArgs e)
        {
            if (checkT == 1)
            {

                NodecheckAllorNot(false,this.tvw_field);
                if (type == 3)
                    setIsnull_insert(this.tvw_field);
                checkT = 0;
            }
            else
            {
                NodecheckAllorNot(true,this.tvw_field);
                checkT = 1;
            }
        }
        int checkT2 = 1;
        private void llab_checkAllorNot_Click1_Click(object sender, EventArgs e)
        {
            if (checkT2 == 1)
            {

                NodecheckAllorNot(false, this.tvw_field_where);             
                checkT2 = 0;
            }
            else
            {             
                NodecheckAllorNot(true, this.tvw_field_where);
               
                checkT2 = 1;
            }
        }
        TextEditorControl txtEditControl;
        //string Filepath_ADOCode = @"..\..\config\tempADO_Code.cs";       
        void loadtxtEdit() {
            //this.panel3.Controls.Clear();
            this.groupBox1.Hide();
            this.groupBox2.Hide();
            if (txtEditControl == null)
            {
                txtEditControl = new TextEditorControl();
                txtEditControl.Dock = DockStyle.Fill;

                txtEditControl.Document.HighlightingStrategy =
              ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                txtEditControl.Encoding = System.Text.Encoding.Default;
                this.panel3.Controls.Add(txtEditControl);
                //txtEditControl.LoadFile(Filepath_ADOCode);
            }
            else {
                this.panel3.Controls.Remove(txtEditControl);
                txtEditControl = new TextEditorControl();
                txtEditControl.Dock = DockStyle.Fill;
                txtEditControl.Document.HighlightingStrategy =
              ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                txtEditControl.Encoding = System.Text.Encoding.Default;
                this.panel3.Controls.Add(txtEditControl);
                //txtEditControl.LoadFile(Filepath_ADOCode);
            }
           
           
        }
      
        private void btn_CodeGenerator_Click(object sender, EventArgs e)
        {
            lb_state.Text = "";
            string sql = "";
            string Parameter = "";
            if (type == 1)
            {
                //string selectsql = GetrTablefieldSelectSQL(this.tvw_field, this.tvw_field_where);
                //MessageBox.Show(selectsql);
                //string ParmSelect = GetrTablefieldSqlParameterSQL_select(this.tvw_field_where);
                //MessageBox.Show(ParmSelect);      
                if(!GetMParameterIsCheck(this.tvw_field)){
                 lb_state.Text="Please choose to select the field.";
                 return;
                }
                loadtxtEdit();
                sql = GetrTablefieldSelectSQL(this.tvw_field, this.tvw_field_where);
                Parameter = GetrTablefieldSqlParameterSQL_select(this.tvw_field_where);
                string rs = "public DataTable  " + this.txt_methodsname.Text + "(" + GetMParameter(this.tvw_field_where) + ")\r\n";
                rs += "{\r\n";
                rs += "            string sql=\"" + sql + "\";\r\n";
                rs += Parameter + "\r\n";
                rs += "            return SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql, spms);";
                rs += "\r\n}\r\n";
                this.txtEditControl.Text = rs;
                //using (StreamWriter sw = File.CreateText(Filepath_ADOCode))
                //{
                   
                //    sw.WriteLine(rs);
                //    sw.Close();                 
                //}
              
            }
            if (type == 2) {
                //string updatesql = GetrTablefieldUpdateSQL(this.tvw_field, this.tvw_field_where);
                //MessageBox.Show(updatesql);
                //string ParmUpdate = GetrTablefieldSqlParameterSQL_update(this.tvw_field, this.tvw_field_where);
                //MessageBox.Show(ParmUpdate);
                if(!GetMParameterIsCheck(this.tvw_field)){
                 lb_state.Text="Please choose to update the field.";
                 return;
                }
                if(!GetMParameterIsCheck(this.tvw_field_where)){
                 lb_state.Text="Please choose to update the condition.";
                 return;
                }
                loadtxtEdit();
                   sql =GetrTablefieldUpdateSQL(this.tvw_field, this.tvw_field_where);
                Parameter =GetrTablefieldSqlParameterSQL_update(this.tvw_field , this.tvw_field_where);
                string rs = "public bool  " + this.txt_methodsname.Text + "(" + GetMParameter_update(this.tvw_field, this.tvw_field_where) + ")\r\n";
                rs += "{\r\n";
                rs += "            string sql=\"" + sql + "\";\r\n";
                rs += Parameter + "\r\n";
                rs += @"            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;";
                rs += "\r\n}\r\n";
                this.txtEditControl.Text = rs;
                //using (StreamWriter sw = File.CreateText(Filepath_ADOCode))
                //{
                  
                //    sw.WriteLine(rs);
                //    sw.Close();
                //}
              
            }
            if (type == 3)
            {
                //string insertsql = GetrTablefieldInsertSQL(this.tvw_field);
                //MessageBox.Show(insertsql);
                //string ParmInsert = GetrTablefieldSqlParameterSQL_Insert(this.tvw_field);
                //MessageBox.Show(ParmInsert);
                if(!GetMParameterIsCheck(this.tvw_field)){
                 lb_state.Text="Please choose to insert the field.";
                 return;
                }
                loadtxtEdit();
                sql = GetrTablefieldInsertSQL(this.tvw_field);
                Parameter = GetrTablefieldSqlParameterSQL_Insert(this.tvw_field);
                string rs = "public bool  " + this.txt_methodsname.Text + "(" + GetMParameter(this.tvw_field) + ")\r\n";
                rs += "{\r\n";
                rs += "            string sql=\"" + sql + "\";\r\n";
                rs += Parameter + "\r\n";
                rs += @"             if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;";
                rs += "\r\n}\r\n";
                this.txtEditControl.Text = rs;
                // using (StreamWriter sw = File.CreateText(Filepath_ADOCode))
                //{
                   
                //    sw.WriteLine(rs);
                //    sw.Close();
                //}
            }
            if (type == 4) {
                //string deletesql = GetrTablefieldDeleteSQL(this.tvw_field_where);
                //MessageBox.Show(deletesql);
                //string ParmDelete = GetrTablefieldSqlParameterSQL_select(this.tvw_field_where);
                //MessageBox.Show(ParmDelete);
                if(!GetMParameterIsCheck(this.tvw_field_where)){
                 lb_state.Text="Please choose to delete the condition.";
                 return;
                }
                loadtxtEdit();
                   sql= GetrTablefieldDeleteSQL(this.tvw_field_where);
                Parameter =  GetrTablefieldSqlParameterSQL_select(this.tvw_field_where);
                string rs = "public bool  " + this.txt_methodsname.Text + "(" + GetMParameter(this.tvw_field_where) + ")\r\n";
                rs += "{\r\n";
                rs += "            string sql=\"" + sql + "\";\r\n";
                rs += Parameter + "\r\n";
                rs += @"            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;";
                rs += "\r\n}\r\n";
                this.txtEditControl.Text = rs;
                //using (StreamWriter sw = File.CreateText(Filepath_ADOCode))
                //{
                   
                //    sw.WriteLine(rs);
                //    sw.Close();
                //}
            }

            //loadtxtEdit();
            //txtEditControl.LoadFile(Filepath_ADOCode);
            //txtEditControl.Text = "aa";
        }
    
        public string GetrTablefieldSqlParameterSQLLength(string dbsql)
        {

            string rs = "";
            if (dbsql == "varchar" || dbsql == "nvarchar" || dbsql == "char")
            {
                rs = ", -1 ";
            }
            return rs;
        }
        // 生成数据格式Select
        public string GetrTablefieldSelectSQL(System.Windows.Forms.TreeView tvw_field, System.Windows.Forms.TreeView tvw_field_where)
        {
            string rs = "use " + Pidnode_dbname + " select ";
            string strwhere = " where ";
            foreach (OSMyNode node in tvw_field.Nodes)
             {
                 if (node.Checked == true)
                 {
                     Column item = (Column)node.Value;                   
                         rs += item.Name + ",";     
                 }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            foreach (OSMyNode node in tvw_field_where.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {                   
                        strwhere += "" + item.Name + "=@" + item.Name + ",";
                }  
            }
            if (strwhere.EndsWith(","))
                strwhere = strwhere.Remove(strwhere.Length - 1);
            rs += " from " + " [" + Pidnode_dbname + "].[dbo].[" + Pidnode_TBname + "] " + "";
            rs += strwhere;
            return rs;

        }
        public string GetrTablefieldSqlParameterSQL_select(System.Windows.Forms.TreeView tvw_field_where)
        {
            string rs = "\r\n            SqlParameter[] spms = {";
            foreach (OSMyNode node in tvw_field_where.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                     " " + Sqltypelength.ToString() + ") { Value =" + item.Name + " },";
                }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            rs += "\r\n ";
            rs += "            };";
            return rs;

        }
        //delete 
        public string GetrTablefieldDeleteSQL(System.Windows.Forms.TreeView tvw_field_where)
        {
            string rs = "use " + Pidnode_dbname + " delete  " + " [" + Pidnode_dbname + "].[dbo].[" + Pidnode_TBname + "] " + "";
            string strwhere = " where ";
            foreach (OSMyNode node in tvw_field_where.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {
                    strwhere += "" + item.Name + "=@" + item.Name + ",";
                }
            }
            if (strwhere.EndsWith(","))
                strwhere = strwhere.Remove(strwhere.Length - 1);
            rs += strwhere;
            return rs;
        }
        //  update
        public string GetrTablefieldUpdateSQL(System.Windows.Forms.TreeView tvw_field, System.Windows.Forms.TreeView tvw_field_where)
        {
            string rs = "use " + Pidnode_dbname + " update " + " [" + Pidnode_dbname + "].[dbo].[" + Pidnode_TBname + "] " + " set ";
            string strwhere = " where ";
            foreach (OSMyNode node in tvw_field.Nodes)
            {
                if (node.Checked == true)
                {
                    Column item = (Column)node.Value;
                    rs += "" + item.Name + "=@n_" + item.Name + ",";
                }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            foreach (OSMyNode node in tvw_field_where.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {
                    strwhere += "" + item.Name + "=@o_" + item.Name + ",";
                }
            }
            if (strwhere.EndsWith(","))
                strwhere = strwhere.Remove(strwhere.Length - 1);
            rs += strwhere;
            return rs;
        }
        public string GetrTablefieldSqlParameterSQL_update(System.Windows.Forms.TreeView tvw_field,System.Windows.Forms.TreeView tvw_field_where)
        {
            string rs = "\r\n            SqlParameter[] spms = {";
            foreach (OSMyNode node in tvw_field.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@n_" + item.Name + "\", " + Sqltype +
                     " " + Sqltypelength.ToString() + ") { Value =n_" + item.Name + " },";
                }
            }
            if (!rs.EndsWith(","))
                //rs = rs.Remove(rs.Length - 1);
                rs += ",";
            rs += "\r\n ";
            foreach (OSMyNode node in tvw_field_where.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@o_" + item.Name + "\", " + Sqltype +
                     " " + Sqltypelength.ToString() + ") { Value =o_" + item.Name + " },";
                }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            rs += "\r\n ";

            rs += "            };";
            return rs;

        }
        public string GetMParameter_update(System.Windows.Forms.TreeView tvw_field,System.Windows.Forms.TreeView tvw_field_where)
        {
            string rs = "";
            Data.TableData tabdata = new Data.TableData();
            DataTable dt = Model.EModel.getDatable(Pidnode_dbname, Pidnode_TBname);
            foreach (OSMyNode node in tvw_field.Nodes)
            {
                if (node.Checked == true)
                {
                    Column item = (Column)node.Value;
                    foreach (DataColumn dcloumn in dt.Columns)
                    {
                        if (item.Name == dcloumn.ColumnName)
                        {
                            rs += dcloumn.DataType.ToString() + " n_" + item.Name + ",";
                        }
                    }
                }
            }
            if (!rs.EndsWith(","))
                rs += ",";
            foreach (OSMyNode node in tvw_field_where.Nodes)
            {
                if (node.Checked == true)
                {
                    Column item = (Column)node.Value;
                    foreach (DataColumn dcloumn in dt.Columns)
                    {
                        if (item.Name == dcloumn.ColumnName)
                        {
                            rs += dcloumn.DataType.ToString() + " o_" + item.Name + ",";
                        }
                    }
                }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            return rs;
        }
      //insert
        public void setIsnull_insert(System.Windows.Forms.TreeView tvw_field)
        {

            foreach (OSMyNode node in tvw_field.Nodes)
            {
                Column item = (Column)node.Value;
                if (!item.Nullable)
                {
                    node.Checked = true;
                }
            }
        }
        public string GetrTablefieldInsertSQL(System.Windows.Forms.TreeView tvw_field)
        {
            string rs = "use " + Pidnode_dbname + " insert into " + " [" + Pidnode_dbname + "].[dbo].[" + Pidnode_TBname + "] " + "(";
            string strvalue = " values(";
            foreach (OSMyNode node in tvw_field.Nodes)
            {
                if (node.Checked == true)
                {
                    Column item = (Column)node.Value;
                    rs += item.Name + ",";
                    strvalue += "@" + item.Name + ",";
                }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            if (strvalue.EndsWith(","))
                strvalue = strvalue.Remove(strvalue.Length - 1);
            rs += ")";
            rs += strvalue + ")";
            return rs;

        }
        public string GetrTablefieldSqlParameterSQL_Insert(System.Windows.Forms.TreeView tvw_field)
        {
            string rs = "\r\n            SqlParameter[] spms = {";
            foreach (OSMyNode node in tvw_field.Nodes)
            {
                Column item = (Column)node.Value;
                if (node.Checked == true)
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                     " " + Sqltypelength.ToString() + ") { Value =" + item.Name + " },";
                }
            }
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            rs += "\r\n ";
            rs += "            };";
            return rs;

        }
        //
        public string GetMParameter(System.Windows.Forms.TreeView tvw_field)
        {
            string rs = "";
            Data.TableData tabdata = new Data.TableData();
            DataTable dt = Model.EModel.getDatable(Pidnode_dbname, Pidnode_TBname);
             foreach (OSMyNode node in tvw_field.Nodes)
             {
                 if (node.Checked == true)
                 {
                     Column item = (Column)node.Value;
                     foreach (DataColumn dcloumn in dt.Columns)
                     {
                         if (item.Name == dcloumn.ColumnName)
                         {
                             rs += dcloumn.DataType.ToString() + " " + item.Name + ",";
                         }
                     }
                 }
            }
             if (rs.EndsWith(","))
                rs= rs.Remove(rs.Length - 1);
             return rs;
        }

        private void F_ADO_CodeGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtEditControl != null)
            {
                txtEditControl.Dispose();
            }
            //this.Close();
        }

        private void bun_back_Click(object sender, EventArgs e)
        {
            this.groupBox1.Show();
            this.groupBox2.Show();
            this.panel3.Controls.Remove(txtEditControl);
        }
        public bool GetMParameterIsCheck(System.Windows.Forms.TreeView tvw_field)
        {
            Data.TableData tabdata = new Data.TableData();
            DataTable dt = Model.EModel.getDatable(Pidnode_dbname, Pidnode_TBname);
            bool rs = false;
            foreach (OSMyNode node in tvw_field.Nodes)
            {
                if (node.Checked == true)
                {
                    rs = true;
                }
            }
            return rs;
        }
    }
}
