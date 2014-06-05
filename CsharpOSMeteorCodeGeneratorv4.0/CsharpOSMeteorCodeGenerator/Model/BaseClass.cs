using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CsharpOSMeteorCodeGenerator.Model
{
    public class BaseClass
    {
       
        public static readonly string str_GeneratorTime = "//===============================================================================\r\n//GeneratorDate:    " +
            System.DateTime.Now + "\r\n//===============================================================================\r\n";
        public static readonly  string str_stat = str_GeneratorTime+@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace {0}
{{
    public class {1}
    {{
";
        public const string str_end = @"
    }   
}
";
        public const string strtable_eneity =
           @"
        public class {0}
        {{ {1}                    
        }}
";
        public class BaseTableLinqClass
        {
            public const string strtablelinq =
        @"
{0}
";
            public const string strtablelinqadd = @"
        public static int {0}Add(Entity.{1} obj)
        {{
            int rs = 0;//0  >>add failure
            //1  >>Has been in existence can't repeat to add
            //2  >>add success
            using (DataObjectDataContext dt = new DataObjectDataContext())
            {{
                var query = from s in dt.{2}
                            where (your conditions)
                            select  s ;
                if (query.ToList().Count > 0)
                {{
                    rs = 1;
                }}
                else
                {{              
                    {3}
                    dt.{4}.InsertOnSubmit(newojb);
                    dt.SubmitChanges();
                    rs = 2;
                }}
            }}
            return rs;
        }}";
            public const string strtablelinqdelete = @"
        public static int {0}Delete(string objid)
        {{
              int rs = 0; // 0 failure
                         //1  Delete the object does not exist
                        //2 success
            using (DataObjectDataContext db = new DataObjectDataContext())
            {{
                var query = db.{1}.SingleOrDefault<{2}>(s => s.id == System.Guid.Parse(objid));
                if (query == null)
                {{
                    rs = 1;
                    return rs;
                }}
                db.{3}.DeleteOnSubmit(query);
                db.SubmitChanges();
                rs=2;
            }}
            return rs;
        }}";
            public const string strtablelinqupdate = @"
            public static int {0}Update(Entity.{1} obj)
            {{
                int rs = 0;// 0 failure
                          //1  update the object does not exist
                         //2 success
                using (DataObjectDataContext db = new DataObjectDataContext())
                {{
                    var query = db.{2}.SingleOrDefault<{3}>(s => s.id == System.Guid.Parse(obj.id));
                    if (query == null)
                    {{
                        rs = 1;
                        return rs;
                    }}{4}                
                        db.SubmitChanges();
                        rs = 2;

                 }}
                return rs;
            }}";
            public const string strtablelinqselect = @"
        public static List<Entity.{0}> getALLuserlist()
        {{
            List<Entity.{1}>  rslist=new List<Entity.{2}>();
            using (DataObjectDataContext db = new DataObjectDataContext())
            {{
                var querylist = from s in db.{3} orderby s.id select s ;
                if(querylist==null)
                {{
                     return rslist;
                }}
                foreach (var obj in querylist)
                {{
                      Entity.{4}  query=new Entity.{5} ();{6}
                      rslist.Add(query);
                }}
            }}
            return rslist;
            }}";
            public const string strtablelinqselectToIEnumerable = @"
        public static IEnumerable<Entity.{0}> getALL{1}ToIEnumerable(?Expression<Func<Entity.{2}, bool>> expression,int isall)
        {{
            using (DataObjectDataContext db = new DataObjectDataContext())
            {{
                if (isall == 1)
                {{
                    return db.{3}.Rows.Cast<Entity.{4}>();
                }}
                else
                {{
                    return db.{5}.Rows.Cast<Entity.{6}>().AsQueryable<Entity.{7}>().Where(expression);
                }}
            }}
        }}";

        }
        #region 创建辅助类
        public static void CreateSqlHelper(string Helperlocation,string namespaceStr)
        {
            Helperlocation += "\\SQLHelper.cs";
            using (StreamWriter sw = File.CreateText(Helperlocation))
            {
                //System.IO.FileStream fs = new System.IO.FileStream(Helperlocation, System.IO.FileMode.CreateNew);
                //System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Configuration;");
                sw.WriteLine("using System.Data;");
                sw.WriteLine("using System.Data.SqlClient;");
                sw.WriteLine("using System.Collections;");
                sw.WriteLine("");
                sw.WriteLine("namespace " + namespaceStr);
                sw.WriteLine("{");
                sw.WriteLine("    public abstract class SqlHelper");
                sw.WriteLine("    {");
                sw.WriteLine("        //DBConnectionString");
                sw.WriteLine("        public static readonly string ConnectionString = ConfigurationManager.AppSettings[\"ConnectionString\"];");
                sw.WriteLine("");
                sw.WriteLine("        // Hashtable to store cached parameters");
                sw.WriteLine("        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string ");
                sw.WriteLine("        /// using the provided parameters.");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <remarks>");
                sw.WriteLine("        /// e.g.:  ");
                sw.WriteLine("        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
                sw.WriteLine("        /// </remarks>");
                sw.WriteLine("        /// <param name=\"connectionString\">a valid connection string for a SqlConnection</param>");
                sw.WriteLine("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
                sw.WriteLine("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
                sw.WriteLine("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
                sw.WriteLine("        /// <returns>an int representing the number of rows affected by the command</returns>");
                sw.WriteLine("        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            using (SqlConnection conn = new SqlConnection(connectionString))");
                sw.WriteLine("            {");
                sw.WriteLine("                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("                int val = cmd.ExecuteNonQuery();");
                sw.WriteLine("                cmd.Parameters.Clear();");
                sw.WriteLine("                return val;");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Execute a SqlCommand (that returns no resultset) against an existing database connection ");
                sw.WriteLine("        /// using the provided parameters.");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <remarks>");
                sw.WriteLine("        /// e.g.:  ");
                sw.WriteLine("        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
                sw.WriteLine("        /// </remarks>");
                sw.WriteLine("        /// <param name=\"conn\">an existing database connection</param>");
                sw.WriteLine("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
                sw.WriteLine("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
                sw.WriteLine("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
                sw.WriteLine("        /// <returns>an int representing the number of rows affected by the command</returns>");
                sw.WriteLine("        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("            int val = cmd.ExecuteNonQuery();");
                sw.WriteLine("            cmd.Parameters.Clear();");
                sw.WriteLine("            return val;");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction ");
                sw.WriteLine("        /// using the provided parameters.");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <remarks>");
                sw.WriteLine("        /// e.g.:  ");
                sw.WriteLine("        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
                sw.WriteLine("        /// </remarks>");
                sw.WriteLine("        /// <param name=\"trans\">an existing sql transaction</param>");
                sw.WriteLine("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
                sw.WriteLine("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
                sw.WriteLine("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
                sw.WriteLine("        /// <returns>an int representing the number of rows affected by the command</returns>");
                sw.WriteLine("        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);");
                sw.WriteLine("            int val = cmd.ExecuteNonQuery();");
                sw.WriteLine("            cmd.Parameters.Clear();");
                sw.WriteLine("            return val;");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string ");
                sw.WriteLine("        /// using the provided parameters.");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <remarks>");
                sw.WriteLine("        /// e.g.:  ");
                sw.WriteLine("        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
                sw.WriteLine("        /// </remarks>");
                sw.WriteLine("        /// <param name=\"connectionString\">a valid connection string for a SqlConnection</param>");
                sw.WriteLine("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
                sw.WriteLine("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
                sw.WriteLine("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
                sw.WriteLine("        /// <returns>A SqlDataReader containing the results</returns>");
                sw.WriteLine("        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            SqlConnection conn = new SqlConnection(connectionString);");
                sw.WriteLine("            // we use a try/catch here because if the method throws an exception we want to ");
                sw.WriteLine("            // close the connection throw code, because no datareader will exist, hence the ");
                sw.WriteLine("            // commandBehaviour.CloseConnection will not work");
                sw.WriteLine("            try");
                sw.WriteLine("            {");
                sw.WriteLine("                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);");
                sw.WriteLine("                cmd.Parameters.Clear();");
                sw.WriteLine("                return rdr;");
                sw.WriteLine("            }");
                sw.WriteLine("            catch");
                sw.WriteLine("            {");
                sw.WriteLine("                conn.Close();");
                sw.WriteLine("                throw;");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string ");
                sw.WriteLine("        /// using the provided parameters.");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <remarks>");
                sw.WriteLine("        /// e.g.:  ");
                sw.WriteLine("        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
                sw.WriteLine("        /// </remarks>");
                sw.WriteLine("        /// <param name=\"connectionString\">a valid connection string for a SqlConnection</param>");
                sw.WriteLine("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
                sw.WriteLine("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
                sw.WriteLine("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
                sw.WriteLine("        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>");
                sw.WriteLine("        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            using (SqlConnection connection = new SqlConnection(connectionString))");
                sw.WriteLine("            {");
                sw.WriteLine("                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("                object val = cmd.ExecuteScalar();");
                sw.WriteLine("                cmd.Parameters.Clear();");
                sw.WriteLine("                return val;");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("        public static object ExecuteScalar(string sql)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            cmd.CommandText = sql;");
                sw.WriteLine("            using (SqlConnection connection = new SqlConnection(ConnectionString))");
                sw.WriteLine("            {");
                sw.WriteLine("                cmd.Connection = connection;");
                sw.WriteLine("                connection.Open();");
                sw.WriteLine("                object val = cmd.ExecuteScalar();");
                sw.WriteLine("                connection.Close();");
                sw.WriteLine("                return val;");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection ");
                sw.WriteLine("        /// using the provided parameters.");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <remarks>");
                sw.WriteLine("        /// e.g.:  ");
                sw.WriteLine("        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
                sw.WriteLine("        /// </remarks>");
                sw.WriteLine("        /// <param name=\"conn\">an existing database connection</param>");
                sw.WriteLine("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
                sw.WriteLine("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
                sw.WriteLine("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
                sw.WriteLine("        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>");
                sw.WriteLine("        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("            object val = cmd.ExecuteScalar();");
                sw.WriteLine("            cmd.Parameters.Clear();");
                sw.WriteLine("            return val;");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// add parameter array to the cache");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <param name=\"cacheKey\">Key to the parameter cache</param>");
                sw.WriteLine("        /// <param name=\"cmdParms\">an array of SqlParamters to be cached</param>");
                sw.WriteLine("        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            parmCache[cacheKey] = commandParameters;");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Retrieve cached parameters");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <param name=\"cacheKey\">key used to lookup parameters</param>");
                sw.WriteLine("        /// <returns>Cached SqlParamters array</returns>");
                sw.WriteLine("        public static SqlParameter[] GetCachedParameters(string cacheKey)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];");
                sw.WriteLine("            if (cachedParms == null)");
                sw.WriteLine("                return null;");
                sw.WriteLine("            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];");
                sw.WriteLine("            for (int i = 0, j = cachedParms.Length; i < j; i++)");
                sw.WriteLine("                    clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();");
                sw.WriteLine("            return clonedParms;");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        /// <summary>");
                sw.WriteLine("        /// Prepare a command for execution");
                sw.WriteLine("        /// </summary>");
                sw.WriteLine("        /// <param name=\"cmd\">SqlCommand object</param>");
                sw.WriteLine("        /// <param name=\"conn\">SqlConnection object</param>");
                sw.WriteLine("        /// <param name=\"trans\">SqlTransaction object</param>");
                sw.WriteLine("        /// <param name=\"cmdType\">Cmd type e.g. stored procedure or text</param>");
                sw.WriteLine("        /// <param name=\"cmdText\">Command text, e.g. Select * from Products</param>");
                sw.WriteLine("        /// <param name=\"cmdParms\">SqlParameters to use in the command</param>");
                sw.WriteLine("        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)");
                sw.WriteLine("        {");
                sw.WriteLine("            if (conn.State != ConnectionState.Open)");
                sw.WriteLine("                conn.Open();");
                sw.WriteLine("            cmd.Connection = conn;");
                sw.WriteLine("            cmd.CommandText = cmdText;");
                sw.WriteLine("            if (trans != null)");
                sw.WriteLine("                cmd.Transaction = trans;");
                sw.WriteLine("            cmd.CommandType = cmdType;");
                sw.WriteLine("            if (cmdParms != null)");
                sw.WriteLine("            {");
                //sw.WriteLine("                foreach (SqlParameter parm in cmdParms)");
                //sw.WriteLine("                    cmd.Parameters.Add(parm);");
                sw.WriteLine("                    cmd.Parameters.AddRange(cmdParms);");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("        public static SqlDataReader ExecuteReader(string sql)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            SqlConnection connection = new SqlConnection(ConnectionString);");
                sw.WriteLine("            connection.Open();");
                sw.WriteLine("            cmd.CommandText = sql;");
                sw.WriteLine("            cmd.Connection = connection;");
                sw.WriteLine("            return cmd.ExecuteReader(CommandBehavior.CloseConnection);");
                sw.WriteLine("        }");
                sw.WriteLine("        public static int ExecuteNonQuery(string sql)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("            using (SqlConnection connection = new SqlConnection(ConnectionString))");
                sw.WriteLine("            {");
                sw.WriteLine("                connection.Open();");
                sw.WriteLine("                cmd.CommandText = sql;");
                sw.WriteLine("                cmd.Connection = connection;");
                sw.WriteLine("                int i = cmd.ExecuteNonQuery();");
                sw.WriteLine("                connection.Close();");
                sw.WriteLine("                return i;");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlConnection connection = new SqlConnection(connectionString);");
                sw.WriteLine("            try");
                sw.WriteLine("            {");
                sw.WriteLine("                connection.Open();");
                sw.WriteLine("                SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("                SqlDataAdapter adp = new SqlDataAdapter(cmd);");
                sw.WriteLine("                DataSet dt = new DataSet();");
                sw.WriteLine("                adp.Fill(dt);");
                sw.WriteLine("                connection.Close();");
                sw.WriteLine("                return dt;");
                sw.WriteLine("            }");
                sw.WriteLine("            catch (Exception ex)");
                sw.WriteLine("            {");
                sw.WriteLine("                throw (ex);");
                sw.WriteLine("            }");
                sw.WriteLine("            finally");
                sw.WriteLine("            {");
                sw.WriteLine("                if (connection.State != ConnectionState.Closed)");
                sw.WriteLine("                {");
                sw.WriteLine("                    connection.Close();");
                sw.WriteLine("                }");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("        public static DataSet ExecuteDataSet(string sql)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlConnection connection = new SqlConnection(ConnectionString);");
                sw.WriteLine("            try");
                sw.WriteLine("            {");
                sw.WriteLine("                connection.Open();");
                sw.WriteLine("                SqlCommand cmd = new SqlCommand(sql, connection);");
                sw.WriteLine("                cmd.CommandType = CommandType.Text;");
                sw.WriteLine("                SqlDataAdapter adp = new SqlDataAdapter(cmd);");
                sw.WriteLine("                DataSet dt = new DataSet();");
                sw.WriteLine("                adp.Fill(dt);");
                sw.WriteLine("                connection.Close();");
                sw.WriteLine("                return dt;");
                sw.WriteLine("            }");
                sw.WriteLine("            catch (Exception ex)");
                sw.WriteLine("            {");
                sw.WriteLine("                throw (ex);");
                sw.WriteLine("            }");
                sw.WriteLine("            finally");
                sw.WriteLine("            {");
                sw.WriteLine("                if (connection.State != ConnectionState.Closed)");
                sw.WriteLine("                {");
                sw.WriteLine("                   connection.Close();");
                sw.WriteLine("                }");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("         /// Get the DataTable object by the query");
                sw.WriteLine("        public static DataTable ExecuteGetDataTableFromTableName(string sql)");
                sw.WriteLine("        {");
                sw.WriteLine("            DataTable dt = new DataTable();");
                sw.WriteLine("            using (SqlConnection conn = new SqlConnection(ConnectionString))");
                sw.WriteLine("            {");
                sw.WriteLine("               SqlCommand scCommand = conn.CreateCommand();");
                sw.WriteLine("               scCommand.CommandText = sql;");
                sw.WriteLine("               SqlDataAdapter sdaAdapter = new SqlDataAdapter(scCommand);");
                sw.WriteLine("               sdaAdapter.Fill(dt);");                
                sw.WriteLine("            }");
                sw.WriteLine("             return dt;");
                sw.WriteLine("        }");
                sw.WriteLine("        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
                sw.WriteLine("        {");
                sw.WriteLine("            SqlConnection connection = new SqlConnection(connectionString);");
                sw.WriteLine("            try");
                sw.WriteLine("            {");
                sw.WriteLine("                connection.Open();");
                sw.WriteLine("                SqlCommand cmd = new SqlCommand();");
                sw.WriteLine("                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
                sw.WriteLine("                SqlDataAdapter adp = new SqlDataAdapter(cmd);");
                sw.WriteLine("                DataTable dt = new DataTable();");
                sw.WriteLine("                adp.Fill(dt);");
                sw.WriteLine("                connection.Close();");
                sw.WriteLine("                return dt;");
                sw.WriteLine("            }");
                sw.WriteLine("            catch (Exception ex)");
                sw.WriteLine("            {");
                sw.WriteLine("                throw (ex);");
                sw.WriteLine("            }");
                sw.WriteLine("            finally");
                sw.WriteLine("            {");
                sw.WriteLine("                if (connection.State != ConnectionState.Closed)");
                sw.WriteLine("                {");
                sw.WriteLine("                    connection.Close();");
                sw.WriteLine("                }");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.Close();
            }
        }
        public static string GetStrSQLHelper(string namespaceStr) {
            StringBuilder sb = new StringBuilder();
               sb.Append("using System;");
               sb.Append("using System.Configuration;");
               sb.Append("using System.Data;");
               sb.Append("using System.Data.SqlClient;");
               sb.Append("using System.Collections;");
               sb.Append("");
               sb.Append("namespace " + namespaceStr);
               sb.Append("{");
               sb.Append("    public abstract class SqlHelper");
               sb.Append("    {");
               sb.Append("        //DBConnectionString");
               sb.Append("        public static readonly string ConnectionString = ConfigurationManager.AppSettings[\"ConnectionString\"];");
               sb.Append("");
               sb.Append("        // Hashtable to store cached parameters");
               sb.Append("        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string ");
               sb.Append("        /// using the provided parameters.");
               sb.Append("        /// </summary>");
               sb.Append("        /// <remarks>");
               sb.Append("        /// e.g.:  ");
               sb.Append("        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
               sb.Append("        /// </remarks>");
               sb.Append("        /// <param name=\"connectionString\">a valid connection string for a SqlConnection</param>");
               sb.Append("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
               sb.Append("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
               sb.Append("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
               sb.Append("        /// <returns>an int representing the number of rows affected by the command</returns>");
               sb.Append("        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            using (SqlConnection conn = new SqlConnection(connectionString))");
               sb.Append("            {");
               sb.Append("                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);");
               sb.Append("                int val = cmd.ExecuteNonQuery();");
               sb.Append("                cmd.Parameters.Clear();");
               sb.Append("                return val;");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Execute a SqlCommand (that returns no resultset) against an existing database connection ");
               sb.Append("        /// using the provided parameters.");
               sb.Append("        /// </summary>");
               sb.Append("        /// <remarks>");
               sb.Append("        /// e.g.:  ");
               sb.Append("        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
               sb.Append("        /// </remarks>");
               sb.Append("        /// <param name=\"conn\">an existing database connection</param>");
               sb.Append("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
               sb.Append("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
               sb.Append("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
               sb.Append("        /// <returns>an int representing the number of rows affected by the command</returns>");
               sb.Append("        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
               sb.Append("            int val = cmd.ExecuteNonQuery();");
               sb.Append("            cmd.Parameters.Clear();");
               sb.Append("            return val;");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction ");
               sb.Append("        /// using the provided parameters.");
               sb.Append("        /// </summary>");
               sb.Append("        /// <remarks>");
               sb.Append("        /// e.g.:  ");
               sb.Append("        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
               sb.Append("        /// </remarks>");
               sb.Append("        /// <param name=\"trans\">an existing sql transaction</param>");
               sb.Append("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
               sb.Append("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
               sb.Append("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
               sb.Append("        /// <returns>an int representing the number of rows affected by the command</returns>");
               sb.Append("        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);");
               sb.Append("            int val = cmd.ExecuteNonQuery();");
               sb.Append("            cmd.Parameters.Clear();");
               sb.Append("            return val;");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string ");
               sb.Append("        /// using the provided parameters.");
               sb.Append("        /// </summary>");
               sb.Append("        /// <remarks>");
               sb.Append("        /// e.g.:  ");
               sb.Append("        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
               sb.Append("        /// </remarks>");
               sb.Append("        /// <param name=\"connectionString\">a valid connection string for a SqlConnection</param>");
               sb.Append("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
               sb.Append("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
               sb.Append("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
               sb.Append("        /// <returns>A SqlDataReader containing the results</returns>");
               sb.Append("        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            SqlConnection conn = new SqlConnection(connectionString);");
               sb.Append("            // we use a try/catch here because if the method throws an exception we want to ");
               sb.Append("            // close the connection throw code, because no datareader will exist, hence the ");
               sb.Append("            // commandBehaviour.CloseConnection will not work");
               sb.Append("            try");
               sb.Append("            {");
               sb.Append("                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);");
               sb.Append("                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);");
               sb.Append("                cmd.Parameters.Clear();");
               sb.Append("                return rdr;");
               sb.Append("            }");
               sb.Append("            catch");
               sb.Append("            {");
               sb.Append("                conn.Close();");
               sb.Append("                throw;");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string ");
               sb.Append("        /// using the provided parameters.");
               sb.Append("        /// </summary>");
               sb.Append("        /// <remarks>");
               sb.Append("        /// e.g.:  ");
               sb.Append("        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
               sb.Append("        /// </remarks>");
               sb.Append("        /// <param name=\"connectionString\">a valid connection string for a SqlConnection</param>");
               sb.Append("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
               sb.Append("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
               sb.Append("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
               sb.Append("        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>");
               sb.Append("        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            using (SqlConnection connection = new SqlConnection(connectionString))");
               sb.Append("            {");
               sb.Append("                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
               sb.Append("                object val = cmd.ExecuteScalar();");
               sb.Append("                cmd.Parameters.Clear();");
               sb.Append("                return val;");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("        public static object ExecuteScalar(string sql)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            cmd.CommandText = sql;");
               sb.Append("            using (SqlConnection connection = new SqlConnection(ConnectionString))");
               sb.Append("            {");
               sb.Append("                cmd.Connection = connection;");
               sb.Append("                connection.Open();");
               sb.Append("                object val = cmd.ExecuteScalar();");
               sb.Append("                connection.Close();");
               sb.Append("                return val;");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection ");
               sb.Append("        /// using the provided parameters.");
               sb.Append("        /// </summary>");
               sb.Append("        /// <remarks>");
               sb.Append("        /// e.g.:  ");
               sb.Append("        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, \"PublishOrders\", new SqlParameter(\"@prodid\", 24));");
               sb.Append("        /// </remarks>");
               sb.Append("        /// <param name=\"conn\">an existing database connection</param>");
               sb.Append("        /// <param name=\"commandType\">the CommandType (stored procedure, text, etc.)</param>");
               sb.Append("        /// <param name=\"commandText\">the stored procedure name or T-SQL command</param>");
               sb.Append("        /// <param name=\"commandParameters\">an array of SqlParamters used to execute the command</param>");
               sb.Append("        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>");
               sb.Append("        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
               sb.Append("            object val = cmd.ExecuteScalar();");
               sb.Append("            cmd.Parameters.Clear();");
               sb.Append("            return val;");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// add parameter array to the cache");
               sb.Append("        /// </summary>");
               sb.Append("        /// <param name=\"cacheKey\">Key to the parameter cache</param>");
               sb.Append("        /// <param name=\"cmdParms\">an array of SqlParamters to be cached</param>");
               sb.Append("        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            parmCache[cacheKey] = commandParameters;");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Retrieve cached parameters");
               sb.Append("        /// </summary>");
               sb.Append("        /// <param name=\"cacheKey\">key used to lookup parameters</param>");
               sb.Append("        /// <returns>Cached SqlParamters array</returns>");
               sb.Append("        public static SqlParameter[] GetCachedParameters(string cacheKey)");
               sb.Append("        {");
               sb.Append("            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];");
               sb.Append("            if (cachedParms == null)");
               sb.Append("                return null;");
               sb.Append("            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];");
               sb.Append("            for (int i = 0, j = cachedParms.Length; i < j; i++)");
               sb.Append("                    clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();");
               sb.Append("            return clonedParms;");
               sb.Append("        }");
               sb.Append("");
               sb.Append("        /// <summary>");
               sb.Append("        /// Prepare a command for execution");
               sb.Append("        /// </summary>");
               sb.Append("        /// <param name=\"cmd\">SqlCommand object</param>");
               sb.Append("        /// <param name=\"conn\">SqlConnection object</param>");
               sb.Append("        /// <param name=\"trans\">SqlTransaction object</param>");
               sb.Append("        /// <param name=\"cmdType\">Cmd type e.g. stored procedure or text</param>");
               sb.Append("        /// <param name=\"cmdText\">Command text, e.g. Select * from Products</param>");
               sb.Append("        /// <param name=\"cmdParms\">SqlParameters to use in the command</param>");
               sb.Append("        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)");
               sb.Append("        {");
               sb.Append("            if (conn.State != ConnectionState.Open)");
               sb.Append("                conn.Open();");
               sb.Append("            cmd.Connection = conn;");
               sb.Append("            cmd.CommandText = cmdText;");
               sb.Append("            if (trans != null)");
               sb.Append("                cmd.Transaction = trans;");
               sb.Append("            cmd.CommandType = cmdType;");
               sb.Append("            if (cmdParms != null)");
               sb.Append("            {");
                //sw.WriteLine("                foreach (SqlParameter parm in cmdParms)");
                //sw.WriteLine("                    cmd.Parameters.Add(parm);");
               sb.Append("                    cmd.Parameters.AddRange(cmdParms);");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("        public static SqlDataReader ExecuteReader(string sql)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            SqlConnection connection = new SqlConnection(ConnectionString);");
               sb.Append("            connection.Open();");
               sb.Append("            cmd.CommandText = sql;");
               sb.Append("            cmd.Connection = connection;");
               sb.Append("            return cmd.ExecuteReader(CommandBehavior.CloseConnection);");
               sb.Append("        }");
               sb.Append("        public static int ExecuteNonQuery(string sql)");
               sb.Append("        {");
               sb.Append("            SqlCommand cmd = new SqlCommand();");
               sb.Append("            using (SqlConnection connection = new SqlConnection(ConnectionString))");
               sb.Append("            {");
               sb.Append("                connection.Open();");
               sb.Append("                cmd.CommandText = sql;");
               sb.Append("                cmd.Connection = connection;");
               sb.Append("                int i = cmd.ExecuteNonQuery();");
               sb.Append("                connection.Close();");
               sb.Append("                return i;");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlConnection connection = new SqlConnection(connectionString);");
               sb.Append("            try");
               sb.Append("            {");
               sb.Append("                connection.Open();");
               sb.Append("                SqlCommand cmd = new SqlCommand();");
               sb.Append("                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
               sb.Append("                SqlDataAdapter adp = new SqlDataAdapter(cmd);");
               sb.Append("                DataSet dt = new DataSet();");
               sb.Append("                adp.Fill(dt);");
               sb.Append("                connection.Close();");
               sb.Append("                return dt;");
               sb.Append("            }");
               sb.Append("            catch (Exception ex)");
               sb.Append("            {");
               sb.Append("                throw (ex);");
               sb.Append("            }");
               sb.Append("            finally");
               sb.Append("            {");
               sb.Append("                if (connection.State != ConnectionState.Closed)");
               sb.Append("                {");
               sb.Append("                    connection.Close();");
               sb.Append("                }");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("        public static DataSet ExecuteDataSet(string sql)");
               sb.Append("        {");
               sb.Append("            SqlConnection connection = new SqlConnection(ConnectionString);");
               sb.Append("            try");
               sb.Append("            {");
               sb.Append("                connection.Open();");
               sb.Append("                SqlCommand cmd = new SqlCommand(sql, connection);");
               sb.Append("                cmd.CommandType = CommandType.Text;");
               sb.Append("                SqlDataAdapter adp = new SqlDataAdapter(cmd);");
               sb.Append("                DataSet dt = new DataSet();");
               sb.Append("                adp.Fill(dt);");
               sb.Append("                connection.Close();");
               sb.Append("                return dt;");
               sb.Append("            }");
               sb.Append("            catch (Exception ex)");
               sb.Append("            {");
               sb.Append("                throw (ex);");
               sb.Append("            }");
               sb.Append("            finally");
               sb.Append("            {");
               sb.Append("                if (connection.State != ConnectionState.Closed)");
               sb.Append("                {");
               sb.Append("                   connection.Close();");
               sb.Append("                }");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("         /// Get the DataTable object by the query");
               sb.Append("        public static DataTable ExecuteGetDataTableFromTableName(string sql)");
               sb.Append("        {");
               sb.Append("            DataTable dt = new DataTable();");
               sb.Append("            using (SqlConnection conn = new SqlConnection(ConnectionString))");
               sb.Append("            {");
               sb.Append("               SqlCommand scCommand = conn.CreateCommand();");
               sb.Append("               scCommand.CommandText = sql;");
               sb.Append("               SqlDataAdapter sdaAdapter = new SqlDataAdapter(scCommand);");
               sb.Append("               sdaAdapter.Fill(dt);");
               sb.Append("            }");
               sb.Append("             return dt;");
               sb.Append("        }");
               sb.Append("        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)");
               sb.Append("        {");
               sb.Append("            SqlConnection connection = new SqlConnection(connectionString);");
               sb.Append("            try");
               sb.Append("            {");
               sb.Append("                connection.Open();");
               sb.Append("                SqlCommand cmd = new SqlCommand();");
               sb.Append("                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);");
               sb.Append("                SqlDataAdapter adp = new SqlDataAdapter(cmd);");
               sb.Append("                DataTable dt = new DataTable();");
               sb.Append("                adp.Fill(dt);");
               sb.Append("                connection.Close();");
               sb.Append("                return dt;");
               sb.Append("            }");
               sb.Append("            catch (Exception ex)");
               sb.Append("            {");
               sb.Append("                throw (ex);");
               sb.Append("            }");
               sb.Append("            finally");
               sb.Append("            {");
               sb.Append("                if (connection.State != ConnectionState.Closed)");
               sb.Append("                {");
               sb.Append("                    connection.Close();");
               sb.Append("                }");
               sb.Append("            }");
               sb.Append("        }");
               sb.Append("    }");
               sb.Append("}");      
            return sb.ToString();        
        }
        #endregion 创建辅助类
        public class BaseTableADODALClass {
            public const string strtableADOInsert = @" 
        public static bool {0}Insert(Entity.{0} obj)
        {{
            string sql = ""{1}"";
            {2}
             if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;
        }}
";

            public const string strtableADODeleteFromPrimaryKey = @" 
        public static bool {0}DeleteFromPrimaryKey({1} {2})
        {{           
            string sql = ""{3}"";           
              sql+="" where {2}=@{2}"";
            {4}
             if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;
        }}
";
            public const string strtableADODelete = @" 
        public static bool {0}Delete(string deleteWhere)
        {{            
            string sql = ""{1}"";
            if(!String.IsNullOrEmpty(deleteWhere.Trim()))
            {{
                sql+="" where ""+deleteWhere;
            }} else{{
              return false;
              }}
             if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, null) > 0)
                return true;
            else
                return false;
        }}
";
            public const string strtableADOSelectFromPrimaryKey = @" 
        public static DataTable {0}SelectFromPrimaryKey({1} {2})
        {{           
            string sql = ""{3}"";           
              sql+="" where {2}=@{2}"";
            {4}
              return SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql, spms);
        }}
";
            public const string strtableADOSelectFromPrimaryKey_bool = @" 
        public static bool {0}SelectFromPrimaryKey_bool({1} {2})
        {{           
            string sql = ""{3}"";           
              sql+="" where {2}=@{2}"";
            {4}
              IDataReader dr= SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, sql, spms);
               if (dr.Read())
                   return true;
                else
                   return false;
        }}
";
            public const string strtableADOSelect = @" 
        public static DataTable  {0}Select(string selectWhere)
        {{            
            string sql = ""{1}"";
            if(!String.IsNullOrEmpty(selectWhere.Trim()))
            {{
                sql+="" where ""+selectWhere;
            }}
             return SqlHelper.ExecuteGetDataTableFromTableName(sql);
        }}
";
            public const string strtableADOUpdateFromPrimaryKey = @" 
        public static bool {0}UpdateFromPrimaryKey(Entity.{0} obj ,{1} {2})
        {{           
            string sql = ""{3}"";           
              sql+="" where {2}=@{2}"";
            {4}
             if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;
        }}
";
            public const string strtableADOUpdate = @" 
        public static bool   {0}Update(Entity.{0} obj , string updateWhere)
        {{            
            string sql = ""{1}"";
            if(!String.IsNullOrEmpty(updateWhere.Trim()))
            {{
                sql+="" where ""+updateWhere;
            }} else{{
              return false;
              }}
            {2}
            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, sql, spms) > 0)
                return true;
            else
                return false;
        }}
";         
        }
        public class BaseTableADOBLLClass
        {
            public const string strtableADOInsert = @" 
        public static bool {0}Insert(Entity.{0} obj)
        {{
           return ADODAL.{0}Insert(obj);
        }}
";
           public const string strtableADOInsertFromPrimaryKey = @" 
        public static bool {0}InsertFromPrimaryKey(Entity.{0} obj,{1} {2})
        {{
            if(ADOBLL.{0}SelectFromPrimaryKey_bool({2}))
                return ADODAL.{0}Insert(obj);
            else
                return false;      
        }}
";
           
            public const string strtableADODelete = @" 
        public static bool {0}Delete(string deleteWhere)
        {{            
             return ADODAL.{0}Delete(deleteWhere);
        }}
";
            public const string strtableADODeleteFromPrimaryKey = @" 
        public static bool {0}DeleteFromPrimaryKey({1} {2})
        {{         
            return ADODAL.{0}DeleteFromPrimaryKey({2});
        }}
";
            public const string strtableADOSelect = @" 
        public static DataTable  {0}Select(string selectWhere)
        {{            
            return ADODAL.{0}Select(selectWhere);
        }}
";
            public const string strtableADOSelectFromPrimaryKey = @" 
        public static bool {0}SelectFromPrimaryKey({1} {2})
        {{         
            return ADODAL.{0}SelectFromPrimaryKey({2});
        }}
";
            public const string strtableADOSelectFromPrimaryKey_bool = @" 
        public static bool {0}SelectFromPrimaryKey_bool({1} {2})
        {{         
            return ADODAL.{0}SelectFromPrimaryKey_bool({2});
        }}
";
            public const string strtableADOSelectALL = @" 
        public static DataTable  {0}SelectALL(string selectWhere)
        {{            
            return ADODAL.{0}Select("""");
        }}
";
            public const string strtableADOUpdateFromPrimaryKey = @" 
        public static bool {0}UpdateFromPrimaryKey(Entity.Table_1 obj ,{1} {2})
        {{         
            return ADODAL.{0}UpdateFromPrimaryKey(obj , {2});
        }}
";
            public const string strtableADOUpdate = @" 
        public static bool  {0}Update(Entity.{0} obj , string updateWhere)
        {{            
             return ADODAL.{0}Update(obj , updateWhere);
        }}
";
        }
    }
    public class BaseTableENtityClass {
        public const string strtable =
            @"
/*************EntityClass***************/
    public class {0}
    {{ 
{1}                    
    }}
/***************Entity*************/
{2}/***********Entity*****************/
{3}
/*************JSON***************/
{4}
/****************************/";
    }
    public class BaseTableSQLClass {
        public const string strtablesql =
    @"
/************Select****************/
{0}
/************Insert****************/
{1}
/************Update****************/
{2}
/************Delete****************/
{3}
/****************************/
";

    }
    public class BaseTableAdoClass
    {
        public const string strtableado =
    @"/************SqlParameter****************/
{0}
/****************************/
";
    }
    public class BaseTableLinqClass
    {
        public const string strtablelinq =
    @"/************add****************/
{0}
/****************************/
";
        public const string strtablelinqadd = @"
/************linqAdd****************/
public static int {0}Add(Entity.{1} obj)
{{
    int rs = 0;//0  >>add failure
    //1  >>Has been in existence can't repeat to add
    //2  >>add success
    using (DataObjectDataContext dt = new DataObjectDataContext())
    {{
        var query = from s in dt.{2}
                    where (your conditions)
                    select  s ;
        if (query.ToList().Count > 0)
        {{
            rs = 1;
        }}
        else
        {{              {3}
            dt.{4}.InsertOnSubmit(newojb);
            dt.SubmitChanges();
            rs = 2;
        }}
    }}
    return rs;
}}
/****************************/";
        public const string strtablelinqdelete = @"
/************linqDelete****************/
public static int {0}Delete(string objid)
{{
      int rs = 0; // 0 failure
                 //1  Delete the object does not exist
                //2 success
    using (DataObjectDataContext db = new DataObjectDataContext())
    {{
        var query = db.{1}.SingleOrDefault<{2}>(s => s.id == System.Guid.Parse(objid));
        if (query == null)
        {{
            rs = 1;
            return rs;
        }}
        db.{3}.DeleteOnSubmit(query);
        db.SubmitChanges();
        rs=2;
    }}
    return rs;
}}
/****************************/";
        public const string strtablelinqupdate = @"
/************linqUpdate****************/
 public static int {0}Update(Entity.{1} obj)
        {{
            int rs = 0;// 0 failure
                      //1  update the object does not exist
                     //2 success
        using (DataObjectDataContext db = new DataObjectDataContext())
        {{
            var query = db.{2}.SingleOrDefault<{3}>(s => s.id == System.Guid.Parse(obj.id));
            if (query == null)
            {{
                rs = 1;
                return rs;
            }}{4}                
                db.SubmitChanges();
                rs = 2;

            }}
            return rs;
        }}
/****************************/";
        public const string strtablelinqselect = @"//
/************linqselect****************/
public static List<Entity.{0}> getALLuserlist()
{{
    List<Entity.{1}>  rslist=new List<Entity.{2}>();
    using (DataObjectDataContext db = new DataObjectDataContext())
    {{
        var querylist = from s in db.{3} orderby s.id select s ;
        if(querylist==null)
        {{
             return rslist;
        }}
        foreach (var obj in querylist)
        {{
              Entity.{4}  query=new Entity.{5} ();{6}
              rslist.Add(query);
        }}
    }}
    return rslist;
    }}
/****************************/";
        public const string strtablelinqselectToIEnumerable = @"//
/************linqselectToIEnumerable****************/
public static IEnumerable<Entity.{0}> getALL{1}ToIEnumerable(?Expression<Func<Entity.{2}, bool>> expression,int isall)
{{
    using (DataObjectDataContext db = new DataObjectDataContext())
    {{
        if (isall == 1)
        {{
            return db.{3}.Rows.Cast<Entity.{4}>();
        }}
        else
        {{
            return db.{5}.Rows.Cast<Entity.{6}>().AsQueryable<Entity.{7}>().Where(expression);
        }}
    }}
}}
/****************************/";
        
    }    
}
