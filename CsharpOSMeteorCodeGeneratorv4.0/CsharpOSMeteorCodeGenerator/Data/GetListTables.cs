using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsharpOSMeteorCodeGenerator.Model;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace CsharpOSMeteorCodeGenerator.Data
{
  public  class GetListTables
    {
        /// <summary>
        /// List the tables of a specific database
        /// </summary>
        /// <param name="conn"> SQL Connection </param>
        /// <returns> tables list </returns>
        public List<Table> ListTables(SqlConnectionStringBuilder conn, string db)
        {
            conn.InitialCatalog = db;
            return new TableData().ListTables(conn);
        }
        public List<Table> ListViews(SqlConnectionStringBuilder conn, string db)
        {
            conn.InitialCatalog = db;
            return new TableData().ListViews(conn);
        }
        public List<Table> ListProc(SqlConnectionStringBuilder conn, string db)
        {
            conn.InitialCatalog = db;
            return new TableData().ListProc(conn);
        }
        public List<string> GetListDBName(SqlConnectionStringBuilder connBuilder)
        {
            List<string> rs = new List<string>();
            using (SqlConnection dbConnection = new SqlConnection(connBuilder.ConnectionString))
            {
                dbConnection.Open();
                DataTable tempDataTable = dbConnection.GetSchema(SqlClientMetaDataCollectionNames.Databases);
                //string str = ""; ;
                foreach (DataRow dr in tempDataTable.Rows)
                {
                    //数据库名  序号   创建时间
                    //str += dr[0] + "   " + dr[1] + "  " + dr[2] + "\n";
                    rs.Add(dr[0].ToString());
                }
                //MessageBox.Show(str);
                //DataTable tempDataTable = dbConnection.GetSchema("Databases");   //和相同效果                     
                //cmbDatabase.DataSource = tempDataTable;                        
                //cmbDatabase.DisplayMember = tempDataTable.Columns["database_name"].ColumnName;
                //cmbDatabase.ValueMember = tempDataTable.Columns["database_name"].ColumnName;                       
                //MessageBox.Show("Connected successfully!", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                dbConnection.Close();
            }
            return rs;
        
        }
      
        /************linqselect****************/
        //public static IEnumerable<User> getALLuserlist(Expression<Func<User, bool>> expression, int isall)
        //{
        //    using (DataObjectDataContext db = new DataObjectDataContext())
        //    {
        //        if (isall == 1)
        //        {
        //            return db.dt.Rows.Cast<User>();//全查
        //        }
        //        else
        //        {
        //            return db.dt.Rows.Cast<User>().AsQueryable<User>().Where(expression);
        //        }
        //    }
        //}
        /****************************/
        public class User
        {

            public int UserId { get; set; }
            public string UserName { get; set; }
            public string PwdHash { get; set; }
            public int Status { get; set; }
            public DateTime CreateTime { get; set; }

        }
    }
}
