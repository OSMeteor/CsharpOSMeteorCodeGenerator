using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsharpOSMeteorCodeGenerator.Model;
using System.IO;
using System.Threading;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;

namespace CsharpOSMeteorCodeGenerator
{
    public partial class F_CodeGenerator : Form
    {       
        public F_CodeGenerator()
        {
            InitializeComponent();          
        }
        //创建一个委托，是为访问TextBox控件服务的。
        public delegate void UpdateTxt();
        //定义一个委托变量
        public UpdateTxt updateTxt;
        public object selectnode { get; set; }
        string Pidnode_dbname = "";
        List<Table> list_tab;
        List<Table> list_tab2;

        public void BFrom_load() {
            Thread.Sleep(50);
            this.BeginInvoke(updateTxt);            
        }
     
        public void f_code_load() {
            this.btn_browse.Enabled=false;
            this.llab_checkAllorNot.Enabled = false;
            this.btn_CodeGenerator.Enabled = false;
            this.btn_Close.Enabled = false;
            OSMyNode node = (OSMyNode)this.selectnode;
            if (node != null)
                if (node.Parent == null)
                {
                    Pidnode_dbname = node.Text;
                    this.pBar.Value = 1;
                    Data.GetListTables glt = new Data.GetListTables();
                     list_tab = glt.ListTables(Model.EModel.connBuilder, node.Text);
                     list_tab2 = glt.ListViews(Model.EModel.connBuilder, node.Text);
                    int list_count=list_tab.Count + list_tab2.Count;
                    this.pBar.Maximum = list_count+1;
                   
                    if (list_count <= 0) {
                        MessageBox.Show("this db is null");
                        this.Close();
                    }
                    this.lab_pBar.Text = "this table and views load start";
                    foreach (Table table in list_tab)
                    {                       
                        node = new OSMyNode();
                        node.Text = table.Name;
                        node.ToolTipText = table.Name;
                        node.Value = table;
                        node.ImageIndex = 5;
                        node.SelectedImageIndex = 5;
                        node.Checked = true;                   
                        treeView1.Nodes.Add(node);
                        this.pBar.Value++;
                    }
                    foreach (Table table in list_tab2)
                    {
                        node = new OSMyNode();
                        node.Text = table.Name;
                        node.Value = table;
                        node.ToolTipText = table.Name;
                        node.ImageIndex = 6;
                        node.SelectedImageIndex = 6;
                        node.Checked = true;
                        treeView1.Nodes.Add(node);
                        this.pBar.Value++;
                    }
                    this.lab_pBar.Text = "this table and views load end";
                    //checkedListBox_CheckListBoxALl(this.checkedListBox1,true);                  
                    this.btn_browse.Enabled = true;
                    this.llab_checkAllorNot.Enabled = true;
                    this.btn_CodeGenerator.Enabled = true;
                    this.btn_Close.Enabled = true;
                }
        }
        public void checkedListBox_CheckListBoxALl(System.Windows.Forms.CheckedListBox checkedListBox,Boolean boo )
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, boo);//true就是全选
            }
        }
        private void F_CodeGenerator_Shown(object sender, EventArgs e)
        {          
            updateTxt = new UpdateTxt(f_code_load);
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                //ThreadMethodTxt();
                BFrom_load();
            }));
            objThread.Start();   
        }
        private void F_CodeGenerator_Load(object sender, EventArgs e)
        {
            this.chk_isEntity.Checked = true;
            this.chk_ADO.Checked = true;
            this.chk_Linq.Checked = true;
           
            
        }
        private void btn_browse_Click(object sender, EventArgs e)
        {
             string foldPath = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            this.pBar.Value = 0;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               
                foldPath = dialog.SelectedPath + @"\";
                this.txt_foldPath.Text = foldPath;
                if (!String.IsNullOrEmpty(this.txt_foldPath.Text.Trim()))
                {
                    this.chk_isEntity.Checked = true;
                    this.chk_ADO.Checked = true;
                    this.chk_Linq.Checked = true;
                }
                //if (foldPath.Substring(0, 1).ToUpper() == "C")
                //{
                //    //MessageBox.Show("备份文件不能放在系统盘！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    if (foldPath != "")
                //    {
                //        //SetTime(0, 50);
                //        //Boolean rs = DBHelper.BackUpDB(@foldPath);

                //        //if (rs)
                //        //{
                //        //    this.pBar.Value = this.pBar.Maximum;
                //        //    Core.User.Insert(Core.User.LoginedUserCode, string.Format(Entity.Const.dbBack, Core.User.LoginedUserCode, "成功"));
                //        //    MessageBox.Show("备份成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        //}
                //        //else
                //        //{
                //        //    Core.User.Insert(Core.User.LoginedUserCode, string.Format(Entity.Const.dbBack, Core.User.LoginedUserCode, "失败"));
                //        //    MessageBox.Show("备份失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        //}
                //    }
                //}
            }
        }
     
        private void btn_CodeGenerator_Click(object sender, EventArgs e)
        {
             string foldPath= this.txt_foldPath.Text.Trim();
            if (String.IsNullOrEmpty(foldPath)) {
               this.lab_pBar.Text=" this SavePath is null";
                return;
            }
            string iswindowsPath = "^[a-zA-Z]{1}:(\\.+)*";
            System.Text.RegularExpressions.Regex  regex=new System.Text.RegularExpressions.Regex(iswindowsPath);
            if (!regex.Match(foldPath).Success)
            {
               this.lab_pBar.Text=" this SavePath is illegal";
                return;
            }
            if (String.IsNullOrEmpty(this.txt_namespace.Text.Trim()))
            {
                this.lab_pBar.Text = " this Namespace is null";
                return;
            }
            foldPath += "MeteorCode\\";
            if (!Directory.Exists(foldPath))
            {
                Directory.CreateDirectory(foldPath);
              
            }
            //else {
            //    Directory.Delete(foldPath,true);
            //    Directory.CreateDirectory(foldPath);
            //    System.Threading.Thread.Sleep(100);
            //}            
            if ((this.chk_isEntity.Checked == false && this.chk_ADO.Checked == false && this.chk_Linq.Checked == false))
            {
                this.lab_pBar.Text = " Please select a type";
            }
            else
            {
              
                GeneratorCode(foldPath);
                if (!String.IsNullOrEmpty(StrEntity))
                {
                  String str=  StrEntity.Replace("MeteorCode", "MeteorEntityCode");

                  Codebianyi(str);
                }
            }
        }
        string StrEntity = String.Empty;
        string StrADO= String.Empty;
        string StrLinq = String.Empty;
        public void GeneratorCode(string filePath)
        {
              string Namespace_str = this.txt_namespace.Text.Trim();
            string Filepath_entity = filePath+"MeteorEntity.cs";
            //string Filepath_sql = @"..\..\config\tempsql.cs";
            string Filepath_adoDAL = filePath + "MeteorADODAL.cs";
            string Filepath_adoBll = filePath + "MeteorADOBLL.cs";
            string Filepath_linq = filePath+"MeteorLinq.cs";
            int countli = 0;
            if (this.chk_isEntity.Checked)
                countli = countli + 1;
            if (this.chk_ADO.Checked)
                countli = countli + 2;
            if (this.chk_Linq.Checked)
                countli = countli + 1;
            this.pBar.Maximum = treeView1.Nodes.Count * countli;
            this.pBar.Value = 0;           
            #region  生成实体类
            if (this.chk_isEntity.Checked)
            {
                using (StreamWriter sw = File.CreateText(Filepath_entity))
                {
                    string Type = "Entity";
                    string Str_stat = String.Format(Model.BaseClass.str_stat, Namespace_str, Type);
                    string Str_end = Model.BaseClass.str_end;
                    sw.WriteLine(Str_stat);
                    StrEntity += Str_stat;
                    foreach (OSMyNode node in treeView1.Nodes)
                    {
                        if (node.Checked == true)
                        {
                            sw.WriteLine("        #region " + node.Text);
                            //StrEntity += "        #region " + node.Text;
                            string tablename = node.Text;
                            DataTable dt = Model.EModel.getDatable(Pidnode_dbname, tablename);
                            string Str_entity = GetrTableAttribute(dt);
                            string WStr_ent = String.Format(Model.BaseClass.strtable_eneity, tablename, Str_entity);
                            sw.WriteLine(WStr_ent);
                            StrEntity += WStr_ent;
                            sw.WriteLine("        #endregion ");
                            //StrEntity += "        #endregion ";
                        }
                        this.pBar.Value++;
                    }
                    //foreach (OSMyNode node in treeView1.Nodes)
                    //{
                    //    if (node.Checked == true)
                    //    {
                    //        //MessageBox.Show(node.Text);
                    //        //}
                    //        //foreach (Table table in list_tab)
                    //        //{
                    //        string tablename = node.Text;
                    //        DataTable dt = Model.EModel.getDatable(Pidnode_dbname, tablename);
                    //        sw.WriteLine("       public class " + tablename);
                    //        sw.WriteLine("       {");
                    //        foreach (DataColumn cloumn in dt.Columns)
                    //        {
                    //            string rs = "";
                    //            rs += "           public " + cloumn.DataType.ToString() + " " + cloumn.ColumnName + "{set;get;}\n";
                    //            sw.WriteLine(rs);
                    //        }
                    //        sw.WriteLine("       }");
                    //    }
                    //}

                    sw.WriteLine(Str_end);
                    StrEntity += Str_end;
                    sw.Close();
                }
            }
            #endregion
            #region ADO            
            if (this.chk_ADO.Checked)
            {
                #region 生成SQLHerper
                Model.BaseClass.CreateSqlHelper(filePath, Namespace_str);
                StrADO += Model.BaseClass.GetStrSQLHelper(Namespace_str);
                #endregion
                #region ADODAL
                using (StreamWriter sw = File.CreateText(Filepath_adoDAL))
                {
                    string Type = "ADODAL";
                    string Str_stat = String.Format(Model.BaseClass.str_stat, Namespace_str, Type);
                    string Str_end = Model.BaseClass.str_end;
                    sw.WriteLine(Str_stat);
                    foreach (OSMyNode node in treeView1.Nodes)
                    {
                        if (node.Checked == true)
                        {
                            string StrTableName = node.Text;
                            sw.WriteLine("        #region " + StrTableName);
                            string StrTablefieldInsertsql = GetrTablefieldInsertSQL(node); 
                            string StrTablefieldSelectSQL = GetrTablefieldSelectSQL(node);                          
                            string StrTablefieldUpdateSQL = GetrTablefieldUpdateSQL(node);
                            string StrTablefieldDeleteSQL = GetrTablefieldDeleteSQL(node);
                            string StrTablefieldSqlParameterSQL = GetrTablefieldSqlParameterSQL(node);                           
                            string StrTableInsert = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADOInsert, StrTableName, StrTablefieldInsertsql, StrTablefieldSqlParameterSQL);
                            string StrTableDelete = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADODelete, StrTableName, StrTablefieldDeleteSQL, StrTablefieldSqlParameterSQL);
                            string StrTableSelect = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADOSelect, StrTableName, StrTablefieldSelectSQL);
                            string StrTableUpdate = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADOUpdate, StrTableName, StrTablefieldUpdateSQL, StrTablefieldSqlParameterSQL);
                            sw.WriteLine(StrTableInsert);
                            Data.TableData tabdata = new Data.TableData();
                            DataTable dt = Model.EModel.getDatable(Pidnode_dbname, StrTableName);
                            Table tab = (Table)node.Value;     
                            foreach (Column item in tab.Columns)
                            {
                                if (item.PrimaryKey)
                                {
                                    foreach (DataColumn dcloumn in dt.Columns)
                                    {
                                        if (item.Name == dcloumn.ColumnName)
                                        {
                                            string StrTablefieldSqlParameterSQLL_PrimaryKey = GetrTablefieldSqlParameterSQL_PrimaryKey(node); 
                                            string StrTableDeleteFromPrimary = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADODeleteFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName, StrTablefieldDeleteSQL, StrTablefieldSqlParameterSQLL_PrimaryKey);
                                            sw.WriteLine(StrTableDeleteFromPrimary);
                                            string StrTableSelectFromPrimary = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADOSelectFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName, StrTablefieldSelectSQL, StrTablefieldSqlParameterSQLL_PrimaryKey);
                                            string StrTableSelectFromPrimary_bool = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADOSelectFromPrimaryKey_bool, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName, StrTablefieldSelectSQL, StrTablefieldSqlParameterSQLL_PrimaryKey);                                            
                                            sw.WriteLine(StrTableSelectFromPrimary);
                                            sw.WriteLine(StrTableSelectFromPrimary_bool);
                                            string StrTablefieldUpdateSQL_PrimaryKey = GetrTablefieldUpdateSQL_PrimaryKey(node);
                                            string StrTablefieldSqlParameterSQL_PrimaryKeyUpdate = GetrTablefieldSqlParameterSQL_PrimaryKeyUpdate(node);
                                            string StrTableUpdateFromPrimary = String.Format(Model.BaseClass.BaseTableADODALClass.strtableADOUpdateFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName, StrTablefieldUpdateSQL_PrimaryKey, StrTablefieldSqlParameterSQL_PrimaryKeyUpdate);
                                            sw.WriteLine(StrTableUpdateFromPrimary);
                                        }
                                    }
                                }                               
                            }                 
                            sw.WriteLine(StrTableDelete);
                            sw.WriteLine(StrTableSelect);
                            sw.WriteLine(StrTableUpdate);
                            sw.WriteLine("        #endregion");
                        }
                        this.pBar.Value++;
                    }
                    sw.WriteLine(Str_end);
                    sw.Close();
                }
#endregion
                #region ADO_BLL            
                    using (StreamWriter sw = File.CreateText(Filepath_adoBll))
                    {
                        string Type = "ADOBLL";
                        string Str_stat = String.Format(Model.BaseClass.str_stat, Namespace_str, Type);
                        string Str_end = Model.BaseClass.str_end;
                        sw.WriteLine(Str_stat);
                        foreach (OSMyNode node in treeView1.Nodes)
                        {
                            if (node.Checked == true)
                            {                               
                                string StrTableName = node.Text;
                                sw.WriteLine("        #region " + StrTableName);
                                string StrTableInsert = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOInsert, StrTableName);                               
                                string StrTableDelete = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADODelete, StrTableName);
                                string StrTableSelect = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOSelect, StrTableName);
                                string StrTableSelectALL = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOSelectALL, StrTableName);
                                string StrTableUpdate = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOUpdate, StrTableName);
                                sw.WriteLine(StrTableInsert);
                                Data.TableData tabdata = new Data.TableData();
                                DataTable dt = Model.EModel.getDatable(Pidnode_dbname, StrTableName);
                                Table tab = (Table)node.Value;
                                foreach (Column item in tab.Columns)
                                {
                                    if (item.PrimaryKey)
                                    {
                                        foreach (DataColumn dcloumn in dt.Columns)
                                        {
                                            if (item.Name == dcloumn.ColumnName)
                                            {
                                                string strtableADOInsertFromPrimaryKey = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOInsertFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName);
                                                sw.WriteLine(strtableADOInsertFromPrimaryKey);
                                                string StrTablefieldSqlParameterSQLL_PrimaryKey = GetrTablefieldSqlParameterSQL_PrimaryKey(node);
                                                string StrTableDeleteFromPrimary = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADODeleteFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName);
                                                sw.WriteLine(StrTableDeleteFromPrimary);
                                                string StrTableSelectFromPrimary = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOSelectFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName);
                                                string StrTableSelectFromPrimary_bool = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOSelectFromPrimaryKey_bool, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName);
                                                sw.WriteLine(StrTableSelectFromPrimary);
                                                sw.WriteLine(StrTableSelectFromPrimary_bool);
                                                string StrTableUpdateFromPrimary = String.Format(Model.BaseClass.BaseTableADOBLLClass.strtableADOUpdateFromPrimaryKey, StrTableName, dcloumn.DataType.ToString(), dcloumn.ColumnName);
                                                sw.WriteLine(StrTableUpdateFromPrimary);
                                            }
                                        }
                                    }
                                }   
                                sw.WriteLine(StrTableDelete);
                                sw.WriteLine(StrTableSelectALL);
                                sw.WriteLine(StrTableSelect);
                                sw.WriteLine(StrTableUpdate);
                                sw.WriteLine("        #endregion");
                            }
                            this.pBar.Value++;
                        }
                        sw.WriteLine(Str_end);
                        sw.Close();
                    }
                #endregion
            }
            #endregion
           
            #region linq
            if (this.chk_Linq.Checked)
            {
                using (StreamWriter sw = File.CreateText(Filepath_linq))
                {
                    string Type = "Linq";
                    string Str_stat = String.Format(Model.BaseClass.str_stat, Namespace_str, Type);
                    string Str_end = Model.BaseClass.str_end;
                    sw.WriteLine(Str_stat);
                    foreach (OSMyNode node in treeView1.Nodes)
                    {
                        if (node.Checked == true)
                        {
                            string StrTableName = node.Text;
                            sw.WriteLine("        #region " + StrTableName);
                            string linqadd_entity = GetrTableAttribute_linqadd_entity(node);
                            string linqadd = String.Format(Model.BaseClass.BaseTableLinqClass.strtablelinqadd, StrTableName, StrTableName, StrTableName, linqadd_entity, StrTableName);
                            string strtablelinqdelete = String.Format(Model.BaseClass.BaseTableLinqClass.strtablelinqdelete, StrTableName, StrTableName, StrTableName, StrTableName);
                            string linqupdate_entity = GetrTableAttribute_linqupdate_entity(node);
                            string strtablelinqupdate = String.Format(Model.BaseClass.BaseTableLinqClass.strtablelinqupdate, StrTableName, StrTableName, StrTableName, StrTableName, linqupdate_entity);
                            string strtablelinqselect = String.Format(Model.BaseClass.BaseTableLinqClass.strtablelinqselect, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, linqupdate_entity);
                            string strtablelinqselectToIEnumerable = String.Format(Model.BaseClass.BaseTableLinqClass.strtablelinqselectToIEnumerable, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName, StrTableName);
                            sw.WriteLine(strtablelinqselect);
                            sw.WriteLine(strtablelinqselectToIEnumerable);
                            sw.WriteLine(linqadd);
                            sw.WriteLine(strtablelinqdelete);
                            sw.WriteLine(strtablelinqupdate);
                            sw.WriteLine("        #endregion ");
                        }
                        this.pBar.Value++;
                    }
                    sw.WriteLine(Str_end);
                    sw.Close();
                }
            }
            #endregion
            this.lab_pBar.Text = " Code Generator successful !!!";
        }
        #region  生成属性
        public string GetrTableAttribute(DataTable dt)
        {
            string rs = "\r\n";
            foreach (DataColumn cloumn in dt.Columns)
            {
                rs += "            public " + cloumn.DataType.ToString() + " " + cloumn.ColumnName + "{set;get;}\r\n";
            }
            return rs;
        }
        public string GetrTableAttribute(Table tab)
        {
            string rs = "\n";
            foreach (Column item in tab.Columns)
            {
                rs += "     public " + GetdbType(item.Type) + "  " + item.Name + " { get; set ; }\n";
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
        public string GetrTableAttribute_linqadd_entity(OSMyNode node)
        {
            string rs = "\r\n";
            rs += "                    " + node.Text + "  newojb = new " + node.Text + "()\r\n";
            rs += "                    {\r\n";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                //string convetType = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2CsharpTypeString(item.Type).ToString();
                if (tab.Columns.Count == num)
                {
                    rs += "                      " + item.Name + "=" + "obj." + item.Name + "\r\n";
                }
                else
                {
                    rs += "                      " + item.Name + "=" + "obj." + item.Name + ",\r\n";
                }
                num++;
            }
            rs += "                    };";

            return rs;
        }
        public string GetrTableAttribute_linqupdate_entity(OSMyNode node)
        {
            string rs = "\r\n";
            Table tab = (Table)node.Value;
            foreach (Column item in tab.Columns)
            {
                //string convetType = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2CsharpTypeString(item.Type).ToString();
                rs += "                      query." + item.Name + "=" + "obj." + item.Name + ";\r\n";
            }
            return rs;
        }
        public string GetrTablefieldSelectSQL(OSMyNode node)
        {
            string rs = "use " + Pidnode_dbname + " select ";
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
            //rs += " from " + node.Text + " where 1=1";
            rs += " from " + " [" + Pidnode_dbname + "].[dbo].["+node.Text+"] " + "";           
            return rs;

        }
        public string GetrTablefieldInsertSQL(OSMyNode node)
        {
            string rs = "use "+Pidnode_dbname+" insert into " + " [" + Pidnode_dbname + "].[dbo].[" + node.Text + "] " + "(";
            //Insert INTO table(field1,field2,...) values(value1,value2,...)
            Table tab = (Table)node.Value;
            string strvalue = " values(";
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += item.Name;
                    strvalue += "@" + item.Name ;
                }
                else
                {
                    rs += item.Name + ",";
                    strvalue += "@" + item.Name + ",";
                }
                num++;

            }
            rs += ")";
            rs += strvalue + ")";
            return rs;

        }
        public string GetrTablefieldUpdateSQL(OSMyNode node)
        {
            string rs = "use " + Pidnode_dbname + " update " + " [" + Pidnode_dbname + "].[dbo].[" + node.Text + "] " + " set ";
            //string rs = "update " + node.Text + " set ";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    rs += item.Name + "=" + "@"+item.Name;
                }
                else
                {
                    rs += item.Name + "=" + "@"+item.Name + ",";
                }
                num++;

            }
            rs += "";
            return rs;

        }
        public string GetrTablefieldUpdateSQL_PrimaryKey(OSMyNode node)
        {
            string rs = "use " + Pidnode_dbname + " update " + " [" + Pidnode_dbname + "].[dbo].[" + node.Text + "] " + " set ";
            //string rs = "update " + node.Text + " set ";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (item.PrimaryKey)
                {
                
                }else{
                    if (tab.Columns.Count == num)
                    {
                        rs += item.Name + "=" + "@" + item.Name;
                    }
                    else
                    {
                        rs += item.Name + "=" + "@" + item.Name + ",";
                    }
                }
                num++;

            }
            rs += "";
            if (rs.EndsWith(","))
                rs = rs.Remove(rs.Length - 1);
            return rs;

        }
        
        public string GetrTablefieldDeleteSQL(OSMyNode node)
        {
            string rs = "use " + Pidnode_dbname + " delete  " + " [" + Pidnode_dbname + "].[dbo].[" + node.Text + "] " + "";
            return rs;

        }
        public string GetrTablefieldSqlParameterSQL(OSMyNode node)
        {
            string rs = "\r\n            SqlParameter[] spms = {";
            Table tab = (Table)node.Value;       
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                        " " + Sqltypelength.ToString() + ") { Value = obj." + item.Name + " }";
                }
                else
                {
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                     " " + Sqltypelength.ToString() + ") { Value = obj." + item.Name + " },";
                }
                num++;

            }
            rs += "\r\n ";
            rs += "            };";           
            return rs;

        }
        public string GetrTablefieldSqlParameterSQL_PrimaryKey(OSMyNode node)
        {
            string rs = "\r\n            SqlParameter[] spms = {";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (item.PrimaryKey)
                {
                    if (tab.Columns.Count == num)
                    {
                        string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                        string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                        rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                            " " + Sqltypelength.ToString() + ") { Value =" + item.Name + " }";
                    }
                    else
                    {
                        string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                        string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                        rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                         " " + Sqltypelength.ToString() + ") { Value =" + item.Name + " },";
                    }
                }
                num++;

            }
            rs += "\r\n ";
            rs += "            };";
            return rs;

        }
        public string GetrTablefieldSqlParameterSQL_PrimaryKeyUpdate(OSMyNode node)
        {
            string rs = "\r\n            SqlParameter[] spms = {";
            Table tab = (Table)node.Value;
            int num = 1;
            foreach (Column item in tab.Columns)
            {
                if (tab.Columns.Count == num)
                {
                    string value = "obj." + item.Name;
                    if (item.PrimaryKey)
                        value = item.Name;
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                        " " + Sqltypelength.ToString() + ") { Value = " + value + " }";
                }
                else
                {
                    string value = "obj." + item.Name;
                    if (item.PrimaryKey)
                        value = item.Name;
                    string Sqltype = Data.SqlDbTypeAndDbTypeConvert.SqlTypeString2SqlTypers(item.Type);
                    string Sqltypelength = GetrTablefieldSqlParameterSQLLength(item.Type);
                    rs += "\r\n               new SqlParameter(\"@" + item.Name + "\", " + Sqltype +
                     " " + Sqltypelength.ToString() + ") { Value =" + value + " },";
                }
                num++;

            }
            rs += "\r\n ";
            rs += "            };";
            return rs;

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
        #endregion
        public void NodecheckAllorNot(Boolean  checkstate)
        {

            foreach (OSMyNode node in treeView1.Nodes)
            {
                node.Checked = checkstate;
            }
        
        }
            
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int checkT = 1;
        private void llab_checkAllorNot_LinkClicked(object sender, EventArgs e)
        {
            if (checkT == 1)
            {

                NodecheckAllorNot(false);
                checkT = 0;
            }
            else
            {
                NodecheckAllorNot(true);
                checkT = 1;
            }
        }

        private void llab_checkAllorNot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void btn_CodeGenerator_DLL_Click(object sender, EventArgs e)
        {

            //if (!String.IsNullOrEmpty(StrEntity))
            //{

            //Codebianyi(StrEntity);
            //}
        }
        public void Codebianyi(string StrEntity)
        {
            string CodeStr = StrEntity;
            // 创建编译器对象
            CSharpCodeProvider p = new CSharpCodeProvider();
            ICodeCompiler cc = p.CreateCompiler();

            // 设置编译参数
            CompilerParameters options = new CompilerParameters();
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Xml.dll");
            options.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            options.ReferencedAssemblies.Add("System.Data.dll");
            options.ReferencedAssemblies.Add("System.Core.dll");
            options.OutputAssembly = @"C:\Users\Administrator\Desktop\MeteorCode\" + "MeteorEntityCode.dll";           
            // 2. 直接指定源码字符串
            string code = CodeStr;
//            string code = @"
//                using System;
//                namespace Samples
//                {
//                    public class Class1
//                    {
//                        static void Main(string[] args)
//                        {
//                            Console.WriteLine(""Hello, World!"");
//                            Console.WriteLine(DateTime.Now.ToString());
//                        }
//                    }
//                }
//            ";
//            string code2 = @"
//                using System;
//                namespace Samples32
//                {
//                    public class Class1
//                    {
//                        static void Main(string[] args)
//                        {
//                            Console.WriteLine(""Hello, World!"");
//                            Console.WriteLine(DateTime.Now.ToString());
//                        }
//                    }
//                }
//            ";
//            string code = @"
//                using System;
//                namespace Samples
//                {
//                  
//        public class sysdiagrams
//        { 
//            public System.String name{set;get;}
//            public System.Int32 principal_id{set;get;}
//            public System.Int32 diagram_id{set;get;}
//            public System.Int32 version{set;get;}
//            public System.Byte[] definition{set;get;}
//                    
//        }
//                }
//            ";
            CodeSnippetCompileUnit cu = new CodeSnippetCompileUnit(code);
            //CodeSnippetCompileUnit cu2 = new CodeSnippetCompileUnit(code2);
            //CodeCompileUnit[] codecomUnit=new CodeCompileUnit[2];
            //codecomUnit[0] = cu;
            //codecomUnit[1] = cu2;


            // 开始编译
            CompilerResults cr = cc.CompileAssemblyFromDom(options, cu);
            //CompilerResults cr = cc.CompileAssemblyFromDomBatch(options, codecomUnit);
            

            string er = "";
            // 显示编译信息
            if (cr.Errors.Count == 0)
                Console.WriteLine("\"{0}\" compiled ok!", cr.CompiledAssembly.Location);
            else
            {
                Console.WriteLine("Complie Error:");
                foreach (CompilerError error in cr.Errors)
                {
                    Console.WriteLine("  {0}", error);
                    er += error;
                }
            }
            if (!String.IsNullOrEmpty(er))
                MessageBox.Show(er);
            Console.WriteLine("Press Enter key to exit...");
            Console.ReadLine();
        }
      
       
       
    }
}
