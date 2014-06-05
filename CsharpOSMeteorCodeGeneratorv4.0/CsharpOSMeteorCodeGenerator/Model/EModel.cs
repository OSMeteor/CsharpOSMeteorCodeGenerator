using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CsharpOSMeteorCodeGenerator.Model
{
  public  class EModel
    {
      public static SqlConnectionStringBuilder connBuilder;
        //use pubs select top 0 * from  KFUSER
      /// <summary>
      ///  通过查询语句获得DataTable 对象
      /// </summary>
      /// <param name="sql">sql查询语句</param>
      /// <returns></returns>       
      public static DataTable ExecuteGetDataTableFromTableName(string sql)
      {
          //DataSet dsSet = new DataSet();
          DataTable dt = new DataTable();
          using (SqlConnection conn = new SqlConnection(connBuilder.ConnectionString))
          {
              //建立Command
              SqlCommand scCommand = conn.CreateCommand();
              //scCommand.CommandText = "select * from " + strTableName;
              scCommand.CommandText = sql;
              //建立Adapter
              SqlDataAdapter sdaAdapter = new SqlDataAdapter(scCommand);
              sdaAdapter.Fill(dt);
          }
          return dt;
      }


    
      public static DataTable getDatable(string dbname,string tablename)
      {
          string sql = string.Format("use {0} select top 0 * from  [{1}].[dbo].[{2}]", dbname, dbname, tablename);
          return ExecuteGetDataTableFromTableName(sql);
      }
    }
  /// <summary> 
  /// 将DataTable转换成泛型集合IList<>助手类 
  /// </summary> 
  public class ConvertHelper
  {
      /// <summary> 
      /// 单表查询结果转换成泛型集合 
      /// </summary> 
      /// <typeparam name="T">泛型集合类型</typeparam> 
      /// <param name="dt">查询结果DataTable</param> 
      /// <returns>以实体类为元素的泛型集合</returns> 
      public static IList<T> convertToList<T>(DataTable dt) where T : new()
      {
          // 定义集合 
          List<T> ts = new List<T>();

          // 获得此模型的类型 
          Type type = typeof(T);
          //定义一个临时变量 
          string tempName = string.Empty;
          //遍历DataTable中所有的数据行  
          foreach (DataRow dr in dt.Rows)
          {
              T t = new T();
              // 获得此模型的公共属性 
              PropertyInfo[] propertys = t.GetType().GetProperties();
              //遍历该对象的所有属性 
              foreach (PropertyInfo pi in propertys)
              {
                  tempName = pi.Name;//将属性名称赋值给临时变量   
                  //检查DataTable是否包含此列（列名==对象的属性名）     
                  if (dt.Columns.Contains(tempName))
                  {
                      // 判断此属性是否有Setter   
                      if (!pi.CanWrite) continue;//该属性不可写，直接跳出   
                      //取值   
                      object value = dr[tempName];
                      //如果非空，则赋给对象的属性   
                      if (value != DBNull.Value)
                          pi.SetValue(t, value, null);
                  }
              }
              //对象添加到泛型集合中 
              ts.Add(t);
          }

          return ts;
      }
  } 
}
