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
using ICSharpCode.TextEditor.Document;
using System.IO;
using System.Data.SqlClient;

namespace CsharpOSMeteorCodeGenerator
{
    public partial class F_Main : Form
    {
        TextEditorControl txtEditControl;
        public F_Main()
        {
            InitializeComponent();
            txtEditControl = new TextEditorControl();
            txtEditControl.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(txtEditControl);
        }

        private void F_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
       
        private void F_Main_Load(object sender, EventArgs e)
        {
            rad_isEntity.Checked = true;
            //this.Controls.Clear();
          Data.GetListTables glt = new Data.GetListTables();
          List<string>  list_str=  glt.GetListDBName(Model.EModel.connBuilder);
          OSMyNode node;
          OSMyNode snode;
          foreach (string item in list_str)
          {
              node = new OSMyNode();
              node.Text = item;
              node.ToolTipText = item;
              node.Value = item;
              node.Tag = "fistPid";
              snode = new OSMyNode();
              snode.Text = "snode";            
              snode.ImageIndex = 4;
              snode.SelectedImageIndex = 4;
              node.Nodes.Add(snode);            
              treeView1.Nodes.Add(node);              
          }
         

        }      

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Parent == null)
            {

                this.treeView1.Cursor = Cursors.AppStarting;
                OSMyNode node = (OSMyNode)e.Node;
                node.Nodes.Clear();
                Data.GetListTables glt = new Data.GetListTables();

                OSMyNode snode;
                 OSMyNode cnode;
                List<Table> list_tab = glt.ListTables(Model.EModel.connBuilder, e.Node.Text);
                List<Table> list_tab2 = glt.ListViews(Model.EModel.connBuilder, e.Node.Text);
                List<Table> list_tab3 = glt.ListProc(Model.EModel.connBuilder, e.Node.Text);  
                foreach (Table table in list_tab)
                {
                    snode = new OSMyNode();
                    snode.Text = table.Name;
                    snode.Value = table;
                    snode.ToolTipText = table.Name;
                    snode.ImageIndex = 5;
                    snode.SelectedImageIndex = 5;
                    node.Nodes.Add(snode);
                    //snode.Value = table;
                    //node.Checked = true;
                    //node.SelectedImageIndex = (int)DBImage.TABLE;
                    //node.ImageIndex = (int)DBImage.TABLE;
                    //tree.Nodes.Add(node);
                    cnode = new OSMyNode();
                    cnode.Text = "snode";
                    cnode.Tag = "snode";
                    snode.Nodes.Add(cnode);     
                }
                foreach (Table table in list_tab2)
                {
                    snode = new OSMyNode();
                    snode.Text = table.Name;
                    snode.Value = table;
                    snode.ToolTipText = table.Name;
                    snode.ImageIndex = 6;
                    snode.SelectedImageIndex = 6;
                    node.Nodes.Add(snode);
                    cnode = new OSMyNode();
                    cnode.Text = "snode";
                    cnode.Tag = "snode";
                    snode.Nodes.Add(cnode);
                }
                foreach (Table table in list_tab3)
                {
                    snode = new OSMyNode();
                    snode.Text = table.Name;
                    snode.Value = table;
                    snode.ToolTipText = table.Name;
                    snode.ImageIndex = 7;
                    snode.SelectedImageIndex = 7;
                    node.Nodes.Add(snode);
                    cnode = new OSMyNode();
                    cnode.Text = "snode";
                    cnode.Tag = "snode";
                    snode.Nodes.Add(cnode);
                }
                this.treeView1.Cursor = Cursors.Default ;
            }
            //MessageBox.Show(e.Node.FirstNode.Text);

            if ((e.Node.Parent != null) && (e.Node.FirstNode.Text == "snode"))
            {
                this.treeView1.Cursor = Cursors.AppStarting;
                OSMyNode node = (OSMyNode)e.Node;
                node.Nodes.Clear();
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
                    Tnode.ImageIndex=GetImageIndex(column);
                    Tnode.SelectedImageIndex = GetImageIndex(column);
                    
                    //nodeSon.Checked = true;
                    //nodeSon.Text = column.Name + " (" + ListarAtributos(column) + ")";
                    //nodeSon.Value = column;
                    //nodeSon.SelectedImageIndex = GetImageIndex(column);
                    //nodeSon.ImageIndex = GetImageIndex(column);
                    //node.Nodes.Add(nodeSon);
                    node.Nodes.Add(Tnode);
                }
                this.treeView1.Cursor = Cursors.Default;
            }
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
        string Filepath_entity= @"..\..\config\tempentity.cs";
        string Filepath_sql = @"..\..\config\tempsql.cs";
        string Filepath_ado = @"..\..\config\tempado.cs";
        string Filepath_linq = @"..\..\config\templinq.cs";
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //OSMyNode node = (OSMyNode)treeView1.SelectedNode;
            OSMyNode node = (OSMyNode)e.Node;
            if (node == null)
            {
                return;
            }

            if (node.Parent == null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    treeView1.ContextMenu = this.contextMenu1;
                }
            }
            else if (node.ImageIndex==5)
            {
                if (e.Button == MouseButtons.Right)
                {
                    treeView1.ContextMenu = this.contextMenu2;
                }
            }
            else
            {
                treeView1.ContextMenu = null;

            }
         
            if (node.ImageIndex == 5 || node.ImageIndex==6)
            {
                string StrTableName = node.Text;
                this.treeView1.Cursor = Cursors.AppStarting;
                #region entity             
                //string StrNameSpace = this.txt_namespce.Text;                
                string StrTableAttribute = GetrTableAttribute(node);
                string StrtableAttribute2 = GetrTableAttribute2(node);
                string StrtableAttribute3 = GetrTableAttribute3(node);
                string StrTablefieldJson = GetrTablefieldJson(node);
                using (StreamWriter sw = File.CreateText(Filepath_entity))
                {
                    sw.WriteLine(String.Format(Model.BaseTableENtityClass.strtable, StrTableName, StrTableAttribute, StrtableAttribute2, StrtableAttribute3, StrTablefieldJson));
                    sw.Close();
                }
                #endregion
                #region sql
             
                //string StrTablefield = GetrTablefield(node);
                string StrTablefieldInsertsql = GetrTablefieldInsertSQL(node);
                string StrTablefieldSelectSQL = GetrTablefieldSelectSQL(node);
                string StrTablefieldUpdateSQL = GetrTablefieldUpdateSQL(node);
                string StrTablefieldDeleteSQL = GetrTablefieldDeleteSQL(node);
                //string StrTablefieldSqlParameterSQL = GetrTablefieldSqlParameterSQL(node);
                using (StreamWriter sw = File.CreateText(Filepath_sql))
                {
                    sw.WriteLine(String.Format(Model.BaseTableSQLClass.strtablesql, StrTablefieldSelectSQL, StrTablefieldInsertsql,
                        StrTablefieldUpdateSQL, StrTablefieldDeleteSQL
                        ));
                    sw.Close();
                }
                #endregion
                #region ado
                string StrTablefieldSqlParameterSQL = GetrTablefieldSqlParameterSQL(node);
                 using (StreamWriter sw = File.CreateText(Filepath_ado))
                {
                    sw.WriteLine(String.Format(Model.BaseTableAdoClass.strtableado, StrTablefieldSqlParameterSQL
                        ));
                    sw.Close();
                }
                #endregion
                #region linq
                 string linqadd_entity = GetrTableAttribute_linqadd_entity(node);
                 string linqadd = String.Format(Model.BaseTableLinqClass.strtablelinqadd, StrTableName, StrTableName, StrTableName,linqadd_entity,StrTableName);
                 string strtablelinqdelete = String.Format(Model.BaseTableLinqClass.strtablelinqdelete, StrTableName, StrTableName, StrTableName, StrTableName);
                 string linqupdate_entity = GetrTableAttribute_linqupdate_entity(node);
                 string strtablelinqupdate = String.Format(Model.BaseTableLinqClass.strtablelinqupdate, StrTableName, StrTableName, StrTableName, StrTableName, linqupdate_entity);
                 string strtablelinqselect = String.Format(Model.BaseTableLinqClass.strtablelinqselect, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, linqupdate_entity);
                 string strtablelinqselectToIEnumerable = String.Format(Model.BaseTableLinqClass.strtablelinqselectToIEnumerable, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName);
                 using (StreamWriter sw = File.CreateText(Filepath_linq))
                {
                    sw.WriteLine(strtablelinqselect);
                    sw.WriteLine(strtablelinqselectToIEnumerable);                     
                    sw.WriteLine(linqadd);
                    sw.WriteLine(strtablelinqdelete);
                    sw.WriteLine(strtablelinqupdate);
                    sw.Close();
                }

                #endregion
                 this.treeView1.Cursor = Cursors.Default;
                if (rad_isEntity.Checked)
                {

                    txtEditControl.LoadFile(Filepath_entity);
                }
                if (rad_sql.Checked)
                {

                    txtEditControl.LoadFile(Filepath_sql);
                }
                if (rad_ado.Checked)
                {
                    txtEditControl.LoadFile(Filepath_ado);
                }
                if (rad_linq.Checked)
                {
                    txtEditControl.LoadFile(Filepath_linq);
                }
            }
        }
        public string GetrTableAttribute(OSMyNode node) {
            string rs = "\n";            
            Table tab = (Table)node.Value;            
            foreach (Column item in tab.Columns)
             {
                 rs += "     public " + GetdbType(item.Type) + "  " + item.Name + " { get; set ; }\n";                
             }
            return rs;        
        }
        public string GetrTableAttribute2(OSMyNode node)
        {
            string rs = "\n";
            rs += "     " + node.Text + "  newojb = new " + node.Text + "();\n";
            Table tab = (Table)node.Value;
            foreach (Column item in tab.Columns)
            {
                rs += "     newojb." + item.Name + "="+"("+item.Type+");"+"\n";
            }
            return rs;
        }
        public string GetrTableAttribute3(OSMyNode node)
        {
            string rs = "\n";
            rs += "     " + node.Text + "  newojb = new " + node.Text + "()\n";
            rs += "     {\n";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += "          " + item.Name + "=" + "(" + item.Type + ")" + "\n";
                }
                else
                {
                    rs += "          " + item.Name + "=" + "(" + item.Type + ")," + "\n";
                }
                num++;
               
            }
            rs += "     };";
            return rs;
        }
        public string GetrTablefieldJson(OSMyNode node)
        {
            string rs = "{";
            //Insert INTO table(field1,field2,...) values(value1,value2,...)
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += " \"" + item.Name + "\": " + "v_" + item.Name + "(" + item.Type + ")" + "";
                }
                else
                {
                    rs += " \"" + item.Name + "\": " + "v_" + item.Name + "(" + item.Type + ")" + ",";
                }
                num++;


            }
            rs += "}";
            return rs;

        }
        public string GetrTablefield(OSMyNode node)
        {
            string rs = "\n";
            
            Table tab = (Table)node.Value;
            
            foreach (Column item in tab.Columns)
             {
                 rs += "  " + item.Name + "  ";           
             }
            return rs;
        
        }        
        public string GetdbType(string type)
        {
           string rs = "";
            switch (type)
            {
                case "int": rs = "int"; break;
                case "tinyint": rs = "int"; break;
                case "smallint": rs = "int"; break;
                case "bigint": rs = "int"; break;
                case "datetime": rs = "DateTime"; break;
                case "smalldatetime": rs = "DateTime"; break;
                case "date": rs = "DateTime"; break;
                case "float": rs = "int"; break;
                case "decimal": rs = "decimal"; break;
                case "numeric": rs = "decimal"; break;
                case "money": rs = "decimal"; break;
                case "real": rs = "decimal"; break;
                case "smallmoney": rs = "decimal"; break;
                case "bit": rs = "bool"; break; 
                default:
                    rs = "string";
                    break;
            }
            return rs;
        }
        public string GetrTablefieldSelectSQL(OSMyNode node)
        {
            string rs = "select ";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += item.Name;
                }
                else
                {
                    rs += item.Name + ",";
                }
                num++;
                
            }
            rs += " from "+node.Text+" where 1=1";
            return rs;

        }     
        public string GetrTablefieldInsertSQL(OSMyNode node)
        {
            string rs = "insert into "+node.Text+"(";
            //Insert INTO table(field1,field2,...) values(value1,value2,...)
            Table tab = (Table)node.Value;
            string strvalue = " values(";
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += item.Name;
                    strvalue += "v_" + item.Name + "(" + item.Type+ ")";
                }
                else
                {
                    rs += item.Name + ",";
                    strvalue += "v_" + item.Name + "(" + GetdbType(item.Type) + ")" + ",";
                }
                num++;
              
            }
            rs += ")";
            rs += strvalue+")";
            return rs;

        }
        public string GetrTablefieldUpdateSQL(OSMyNode node)
        {
            string rs = "update " + node.Text +" set ";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += item.Name + "=" + "(" + item.Type + ")";
                }
                else
                {
                    rs += item.Name + "=" + "(" + item.Type + ")" + ",";
                }
                num++;

            }
            rs += "where 1=1";
            return rs;

        }
        public string GetrTablefieldDeleteSQL(OSMyNode node)
        {
            string rs = "delete  from " + node.Text + " where 1=1";           
            return rs;

        }
        public string GetrTablefieldSqlParameterSQL(OSMyNode node)
        {
            string rs = "\n using (SqlConnection conn = new SqlConnection(connectionString))"
            + "\n {"
               + "\n  conn.Open();"
               + "\n  SqlCommand comm = new SqlCommand();"
                + "\n  comm.Connection = conn;";
            Table tab = (Table)node.Value;
             int number1= 1;
             string strsql_update = "\" update " + node.Text + " set ";
             string strql_select = "\" select ";
            //select column1,column2,column3,id from Table_1 where 1=1
             string strql_insert = "\"insert into " + node.Text + "(";
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == number1)
                {
                    strql_insert += item.Name;
                    strsql_update += item.Name + "= @" + item.Name + "";
                    strql_select += item.Name;
                }else{
                    strql_insert += item.Name + ",";
                strsql_update += item.Name + "= @" + item.Name+",";
                strql_select += item.Name + ",";
                }
                number1++;
            }
            strql_insert += ") values(";
               int number2= 1;
              foreach (Column item in tab.Columns)
              {
                if (tab.Columns.Count == number2)
                {
                    strql_insert += "@" + item.Name + "";
                 
                }else{
                    strql_insert += "@" + item.Name + ",";               
                }
                number2++;             
            }
            strql_insert += ")\";\n";           
            strsql_update += " where 1=1 \"";
            strql_select += " from " + node.Text; 
            strql_select += " where 1=1 \"";
            rs += "\n";
            rs += "  string strql_insert=" + strql_insert+"";
            rs += "  string strql_select=" + strql_select + ";";
            rs += "\n";
            rs += "  string sql_update=" + strsql_update + ";";
            rs += "\n  comm.CommandText = \"\";";
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {                    
                    string Sqltype=Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    //string isnull = "false";
                    //rs += "\n   new SqlParameter(\"@" + item.Name + "\", " + Sqltype + ", " + item.CharacterMaximumLength +
                    //    ",ParameterDirection.Input,"+isnull+" , 0, 0, \""+item.Name+"\""+
                    //    ", DataRowVersion.Current,"+"\""+"v_"+item.Name+"\""
                    //    +")";
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\n  comm.Parameters.Add(new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                        " "+Sqltypelength.ToString()+") { Value = \"v_"+item.Name+"\" });";
                }
                else
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    //string isnull = "false";
                    //rs += "\n   new SqlParameter(\"@" + item.Name + "\", " + Sqltype + ", " + item.CharacterMaximumLength +
                    //    ",ParameterDirection.Input," + isnull + " , 0, 0, \"" + item.Name + "\"" +
                    //    ", DataRowVersion.Current," + "\"" + "v_" + item.Name + "\""
                    //    + "),";
                       string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                       rs += "\n  comm.Parameters.Add(new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                        " "+Sqltypelength.ToString()+") { Value = \"v_"+item.Name+"\" });";
                }
                num++;
                
            }
            rs += "\n  ";
            rs += @"comm.ExecuteNonQuery();";
            rs += "\n  //SqlDataAdapter adp = new SqlDataAdapter(comm);";
            rs += "\n  //DataSet result =adp.Fill(result);";
            rs += "\n  conn.Close();";
              rs+="\n }";
            return rs;

        }
        public string  GetrTablefieldSqlParameterSQLLength(string dbsql) {

            string rs = "";
            if (dbsql == "varchar" || dbsql == "nvarchar" || dbsql == "char")
            {
                rs = ", -1 ";
            }
            return rs;        
        }

        public string GetrTableAttribute_linqadd_entity(OSMyNode node)
        {
            string rs = "\n";
            rs += "            " + node.Text + "  newojb = new " + node.Text + "()\n";
            rs += "            {\n";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                string convetType=Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2CsharpTypeString(item.Type).ToString();
                if (tab.Columns.Count == num)
                {
                    rs += "              " + item.Name + "=" + "(" + convetType + ")" + "obj." + item.Name + "\n";
                }
                else
                {
                    rs += "              " + item.Name + "=" + "(" + convetType + ")" + "obj." + item.Name + ",\n";
                }
                num++;
            }
            rs += "            };";
          
            return rs;
        }

        public string GetrTableAttribute_linqupdate_entity(OSMyNode node)
        {
            string rs = "\n";
            Table tab = (Table)node.Value;
            foreach (Column item in tab.Columns)
            {
                string convetType = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2CsharpTypeString(item.Type).ToString();
                    rs += "              query." + item.Name + "=" + "(" + convetType + ")" + "obj." + item.Name + ";\n";
            }
            return rs;
        }

        private void rad_isEntity_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_isEntity.Checked)
            {
                txtEditControl.LoadFile(Filepath_entity);
            }
        }

        private void rad_sql_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_sql.Checked) {
                txtEditControl.LoadFile(Filepath_sql);
            }
        }

        private void rad_ado_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_ado.Checked)
            {
                txtEditControl.LoadFile(Filepath_ado);
            }
        }

        private void rad_linq_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_linq.Checked)
            {
                txtEditControl.LoadFile(Filepath_linq);
            }
        }
        about a = null;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (a == null)
            {
                a = new about();
                a.Show();
            }
            else {
                a.Close();
                a = new about();
                a.Show();
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.treeView1.ContextMenu = null;
            //OSMyNode node = (OSMyNode)treeView1.SelectedNode;
            //if (node == null)
            //{
            //    return;
            //}

            //if (node.Parent == null)
            //{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        treeView1.ContextMenu = this.contextMenu1;
            //    }
            //}
            //else
            //{
            //    treeView1.ContextMenu = null;

            //}
        }
        F_CodeGenerator fcode = null;
        private void menuItem1_Click(object sender, EventArgs e)
        {           
            OSMyNode node = (OSMyNode)treeView1.SelectedNode;
            if ( node.Tag.ToString()== "fistPid")
            {
                if(fcode==null)
                {                    
               fcode= new F_CodeGenerator();
                fcode.Owner = this;
                fcode.selectnode = this.selectnode;
                //fcode.Show();
                fcode.ShowDialog(this);
                }else{
                    fcode.Close();
                  fcode= new F_CodeGenerator();
                fcode.Owner = this;
                fcode.selectnode = this.selectnode;
                //fcode.Show();
                fcode.ShowDialog(this);
                }
            }
        }
        public object selectnode{
            get {
                OSMyNode node = (OSMyNode)treeView1.SelectedNode;
                if (node.Parent != null)
                {
                    //pubs
                   string rs= EModel.connBuilder.ConnectionString;
                       rs=rs.Remove(30);
                       rs += node.Parent.Text + ";Integrated Security=True;Persist Security Info=True";
                       EModel.connBuilder.ConnectionString = rs;
                }
                return node; }
        }
        F_ADO_CodeGenerator fadocode = null;
        private void menuItem4_Click(object sender, EventArgs e)
        {
            string type = "Select";
            if (fadocode == null)
            {
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)selectnode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
            else {
                fadocode.Close();
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            string type = "Insert";
            if (fadocode == null)
            {
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
            else
            {
                fadocode.Close();
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            string type = "Update";
            if (fadocode == null)
            {
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
            else
            {
                fadocode.Close();
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            string type = "Delete";
            if (fadocode == null)
            {
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
            else
            {
                fadocode.Close();
                fadocode = new F_ADO_CodeGenerator();
                fadocode.Owner = this;
                fadocode.selectnode = this.selectnode;
                fadocode.Pidnode_dbname = ((OSMyNode)treeView1.SelectedNode).Parent.Text.Trim().ToString();
                fadocode.SQLType = type;
                fadocode.ShowDialog(this);
            }
        }
        
             //using (SqlConnection conn = new SqlConnection(EModel.connBuilder.ConnectionString))
             //{
             // conn.Open();
             // SqlCommand comm = new SqlCommand();
             // comm.Connection = conn;
             // string strql_insert = "insert into [test].[dbo].[Table_1] (column1,column2,column3,id) values(@column1,@column2,@column3,@id)";
             // comm.CommandText = strql_insert;
             // //comm.Parameters.Add(new SqlParameter("@column1", SqlDbType.VarChar , -1 ) { Value = "v_column1" });
             // //comm.Parameters.Add(new SqlParameter("@column2", SqlDbType.Int ) { Value = 1 });
             // //comm.Parameters.Add(new SqlParameter("@column3", SqlDbType.DateTime) { Value = "2013-11-22 15:39:24.383" });
             // //comm.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value =System.Guid.NewGuid() });
             // SqlParameter[] pm = {
             //                           new SqlParameter("@column1", SqlDbType.VarChar , -1 ) { Value = "v_column1" },
             //                           new SqlParameter("@column2", SqlDbType.Int ) { Value = 1 },
             //                           new SqlParameter("@column3", SqlDbType.DateTime) { Value = "2013-11-22 15:39:24.383" },
             //                           new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value =System.Guid.NewGuid() }
             //                       };
             // comm.Parameters.AddRange(pm);
             // comm.ExecuteNonQuery();
             // //SqlDataAdapter adp = new SqlDataAdapter(comm);
             // //DataSet result =adp.Fill(result);
             // conn.Close();
             //}
       
    }
}
