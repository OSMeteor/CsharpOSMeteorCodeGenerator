using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsharpOSMeteorCodeGenerator.Model;
using System.Data.SqlClient;
using System.Data;

namespace CsharpOSMeteorCodeGenerator.Data
{
    public class TableData
    {

        /// <summary>
        /// List the tables of an especific database
        /// </summary>
        /// <param name="conn"> SQL Connection </param>
        /// <returns> tables list </returns>
        public List<Table> ListTables(SqlConnectionStringBuilder conn)
        {
            List<Table> lstTables = new List<Table>();
            ColumnData columnDataLayer = new ColumnData();

            using (SqlConnection dbConnection = new SqlConnection(conn.ConnectionString))
            {
                dbConnection.Open();
                StringBuilder strBuilder = new StringBuilder();
                SqlCommand cmd = new SqlCommand();

                strBuilder.Append("select ");
                strBuilder.Append("a.table_catalog, ");
                strBuilder.Append("a.table_schema, ");
                strBuilder.Append("a.table_name ");
                strBuilder.Append("from information_schema.tables a");

                cmd.CommandType = CommandType.Text;
                cmd.Connection = dbConnection;
                cmd.CommandText = strBuilder.ToString();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Table table = new Table();
                    table.Catalog = reader["table_catalog"].ToString();
                    table.Schema = reader["table_schema"].ToString();
                    table.Name = reader["table_name"].ToString();
                    // Fill the table columns
                    foreach (Column column in columnDataLayer.ListColumnsByTable(table, conn))
                    {
                        table.AddColumn(column);
                    }
                    lstTables.Add(table);
                }
                dbConnection.Close();
            }
            return lstTables;
        }
        public List<Column> ListTablesColumn(string tableName, SqlConnectionStringBuilder conn)
        {
            List<Column> lstColumns = new List<Column>();
            using (SqlConnection dbConnection = new SqlConnection(conn.ConnectionString))
            {
                dbConnection.Open();
                StringBuilder strBuilder = new StringBuilder();
                SqlCommand cmd = new SqlCommand();

                strBuilder.Append("select ");
                strBuilder.Append("a.column_name as column_name, ");
                strBuilder.Append("CASE a.is_nullable ");
                strBuilder.Append("WHEN 'YES' THEN 1 ");
                strBuilder.Append("ELSE 0 ");
                strBuilder.Append("END as nullable, ");
                strBuilder.Append("a.data_type as type, ");
                strBuilder.Append("a.character_maximum_length as character_maximum_length, ");
                strBuilder.Append("(SELECT count([sc].[name]) as have ");
                strBuilder.Append("FROM sys.indexes [si] ");
                strBuilder.Append("JOIN ");
                strBuilder.Append("sys.index_columns [sic] ");
                strBuilder.Append("ON ");
                strBuilder.Append("[si].[object_id] = [sic].[object_id] AND ");
                strBuilder.Append("[si].[index_id] = [sic].[index_id] ");
                strBuilder.Append("JOIN ");
                strBuilder.Append("sys.columns [sc] ");
                strBuilder.Append("ON ");
                strBuilder.Append("[sic].[object_id] = [sc].[object_id] AND ");
                strBuilder.Append("[sic].[column_id] = [sc].[column_id] ");
                strBuilder.Append("WHERE [si].[is_primary_Key] = 1 ");
                strBuilder.Append("and [sc].[name] = a.column_name ");
                strBuilder.Append("and OBJECT_NAME([si].[object_id]) = a.table_name ");
                strBuilder.Append(") as primary_key, ");
                strBuilder.Append("( ");
                strBuilder.Append("select ");
                strBuilder.Append("count(ccu.column_name) as have ");
                strBuilder.Append("from information_schema.constraint_column_usage ccu ");
                strBuilder.Append("inner join information_schema.table_constraints tc ");
                strBuilder.Append("on (ccu.constraint_name = tc.constraint_name) ");
                strBuilder.Append("where tc.Constraint_Type = 'FOREIGN KEY' ");
                strBuilder.Append("and ccu.table_name = a.table_name ");
                strBuilder.Append("and ccu.column_name = a.column_name ");
                strBuilder.Append(") as foreign_key ");
                strBuilder.Append("from information_schema.columns a ");
                strBuilder.Append("inner join  information_schema.tables b ");
                strBuilder.Append("on (a.table_name = b.table_name) ");
                strBuilder.Append("where a.table_name = @tableName ");

                SqlParameter data = new SqlParameter("@tableName", SqlDbType.NChar);
                data.Value = tableName;

                cmd.CommandText = strBuilder.ToString();
                cmd.Parameters.Add(data);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = dbConnection;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Column column = new Column();
                    column.ForeignKey = Convert.ToBoolean(reader["foreign_key"]);
                    column.PrimaryKey = Convert.ToBoolean(reader["primary_key"]);
                    if (reader["character_maximum_length"] != DBNull.Value)
                    {
                        column.CharacterMaximumLength = Convert.ToInt32(reader["character_maximum_length"]);
                    }
                    column.Name = reader["column_name"].ToString();
                    column.Nullable = Convert.ToBoolean(reader["nullable"]);
                    column.Type = reader["type"].ToString();
                    //column.Table = table;
                    lstColumns.Add(column);
                }
                dbConnection.Close();
            }

            return lstColumns;
        }
        public List<Table> ListViews(SqlConnectionStringBuilder conn)
        {
            List<Table> lstTables = new List<Table>();
            ColumnData columnDataLayer = new ColumnData();

            using (SqlConnection dbConnection = new SqlConnection(conn.ConnectionString))
            {
                dbConnection.Open();
                StringBuilder strBuilder = new StringBuilder();
                SqlCommand cmd = new SqlCommand();

                strBuilder.Append("select ");
                strBuilder.Append("a.table_catalog, ");
                strBuilder.Append("a.table_schema, ");
                strBuilder.Append("a.table_name ");
                strBuilder.Append("from information_schema.views a");

                cmd.CommandType = CommandType.Text;
                cmd.Connection = dbConnection;
                cmd.CommandText = strBuilder.ToString();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Table table = new Table();
                    table.Catalog = reader["table_catalog"].ToString();
                    table.Schema = reader["table_schema"].ToString();
                    table.Name = reader["table_name"].ToString();
                    // Fill the table columns
                    foreach (Column column in columnDataLayer.ListColumnsByTable(table, conn))
                    {
                        table.AddColumn(column);
                    }
                    lstTables.Add(table);
                }
                dbConnection.Close();
            }
            return lstTables;
        }
        public List<Table> ListProc(SqlConnectionStringBuilder conn)
        {
            List<Table> lstTables = new List<Table>();
            ColumnData columnDataLayer = new ColumnData();

            using (SqlConnection dbConnection = new SqlConnection(conn.ConnectionString))
            {
                dbConnection.Open();
                StringBuilder strBuilder = new StringBuilder();
                SqlCommand cmd = new SqlCommand();

                strBuilder.Append("select ");
                strBuilder.Append("a.SPECIFIC_CATALOG as dbname ,");
                strBuilder.Append(" a.ROUTINE_NAME as procname ,");               
                strBuilder.Append("a.ROUTINE_DEFINITION as proccontext,");
                strBuilder.Append("a.CREATED as createdate, ");
                strBuilder.Append("a.LAST_ALTERED as updatetime");
                strBuilder.Append(" from  information_schema.ROUTINES a  where  a.ROUTINE_TYPE='PROCEDURE'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = dbConnection;
                cmd.CommandText = strBuilder.ToString();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Table table = new Table();
                    table.Catalog = reader["updatetime"].ToString();
                    table.Schema = reader["proccontext"].ToString();
                    table.Name = reader["procname"].ToString();
                     //Fill the table columns
                    foreach (Column column in columnDataLayer.ListColumnsByTable(table, conn))
                    {
                        table.AddColumn(column);
                    }
                    lstTables.Add(table);
                }
                dbConnection.Close();
            }
            return lstTables;
        }
        public List<Table> ListFunction(SqlConnectionStringBuilder conn)
        {
            List<Table> lstTables = new List<Table>();
            ColumnData columnDataLayer = new ColumnData();

            using (SqlConnection dbConnection = new SqlConnection(conn.ConnectionString))
            {
                dbConnection.Open();
                StringBuilder strBuilder = new StringBuilder();
                SqlCommand cmd = new SqlCommand();

                strBuilder.Append("select");
                strBuilder.Append("a.SPECIFIC_CATALOG as dbname ,");
                strBuilder.Append(" a.ROUTINE_NAME as procname ,");
                strBuilder.Append("a.ROUTINE_DEFINITION as proccontext,");
                strBuilder.Append("a.CREATED as createdate, ");
                strBuilder.Append("a.LAST_ALTERED as updatetime");
                strBuilder.Append("from  information_schema.ROUTINES a  where  a.ROUTINE_TYPE='FUNCTION'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = dbConnection;
                cmd.CommandText = strBuilder.ToString();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Table table = new Table();
                    table.Catalog = reader["dbname"].ToString();
                    table.Schema = reader["proccontext"].ToString();
                    table.Name = reader["procname"].ToString();
                    // Fill the table columns
                    //foreach (Column column in columnDataLayer.ListColumnsByTable(table, conn))
                    //{
                    //    table.AddColumn(column);
                    //}
                    lstTables.Add(table);
                }
                dbConnection.Close();
            }
            return lstTables;
        }
    }
}
