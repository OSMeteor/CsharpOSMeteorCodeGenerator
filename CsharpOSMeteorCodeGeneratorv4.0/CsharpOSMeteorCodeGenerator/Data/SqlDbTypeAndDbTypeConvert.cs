using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CsharpOSMeteorCodeGenerator.Data
{
    public class SqlDbTypeAndDbTypeConvert
    {
        // SqlDbType转换为C#数据类型
        public static Type SqlType2CsharpType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(Int64);
                case SqlDbType.Binary:
                    return typeof(Object);
                case SqlDbType.Bit:
                    return typeof(Boolean);
                case SqlDbType.Char:
                    return typeof(String);
                case SqlDbType.DateTime:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                    return typeof(Decimal);
                case SqlDbType.Float:
                    return typeof(Double);
                case SqlDbType.Image:
                    return typeof(Object);
                case SqlDbType.Int:
                    return typeof(Int32);
                case SqlDbType.Money:
                    return typeof(Decimal);
                case SqlDbType.NChar:
                    return typeof(String);
                case SqlDbType.NText:
                    return typeof(String);
                case SqlDbType.NVarChar:
                    return typeof(String);
                case SqlDbType.Real:
                    return typeof(Single);
                case SqlDbType.SmallDateTime:
                    return typeof(DateTime);
                case SqlDbType.SmallInt:
                    return typeof(Int16);
                case SqlDbType.SmallMoney:
                    return typeof(Decimal);
                case SqlDbType.Text:
                    return typeof(String);
                case SqlDbType.Timestamp:
                    return typeof(Object);
                case SqlDbType.TinyInt:
                    return typeof(Byte);
                case SqlDbType.Udt://自定义的数据类型
                    return typeof(Object);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Object);
                case SqlDbType.VarBinary:
                    return typeof(Object);
                case SqlDbType.VarChar:
                    return typeof(String);
                case SqlDbType.Variant:
                    return typeof(Object);
                case SqlDbType.Xml:
                    return typeof(Object);
                default:
                    return null;
            }
        }

        // sql server数据类型（如：varchar）
        // 转换为SqlDbType类型
        public static SqlDbType SqlTypeString2SqlType(string sqlTypeString)
        {
            SqlDbType dbType = SqlDbType.Variant;//默认为Object

            switch (sqlTypeString)
            {
                case "int":
                    dbType = SqlDbType.Int;
                    break;
                case "varchar":
                    dbType = SqlDbType.VarChar;
                    break;
                case "bit":
                    dbType = SqlDbType.Bit;
                    break;
                case "datetime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "decimal":
                    dbType = SqlDbType.Decimal;
                    break;
                case "float":
                    dbType = SqlDbType.Float;
                    break;
                case "image":
                    dbType = SqlDbType.Image;
                    break;
                case "money":
                    dbType = SqlDbType.Money;
                    break;
                case "ntext":
                    dbType = SqlDbType.NText;
                    break;
                case "nvarchar":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "smalldatetime":
                    dbType = SqlDbType.SmallDateTime;
                    break;
                case "smallint":
                    dbType = SqlDbType.SmallInt;
                    break;
                case "text":
                    dbType = SqlDbType.Text;
                    break;
                case "bigint":
                    dbType = SqlDbType.BigInt;
                    break;
                case "binary":
                    dbType = SqlDbType.Binary;
                    break;
                case "char":
                    dbType = SqlDbType.Char;
                    break;
                case "nchar":
                    dbType = SqlDbType.NChar;
                    break;
                case "numeric":
                    dbType = SqlDbType.Decimal;
                    break;
                case "real":
                    dbType = SqlDbType.Real;
                    break;
                case "smallmoney":
                    dbType = SqlDbType.SmallMoney;
                    break;
                case "sql_variant":
                    dbType = SqlDbType.Variant;
                    break;
                case "timestamp":
                    dbType = SqlDbType.Timestamp;
                    break;
                case "tinyint":
                    dbType = SqlDbType.TinyInt;
                    break;
                case "uniqueidentifier":
                    dbType = SqlDbType.UniqueIdentifier;
                    break;
                case "varbinary":
                    dbType = SqlDbType.VarBinary;
                    break;
                case "xml":
                    dbType = SqlDbType.Xml;
                    break;
            }
            return dbType;
        }
        // sql server中的数据类型，转换为C#中的类型类型
        public static Type SqlTypeString2CsharpType(string sqlTypeString)
        {
            SqlDbType dbTpe = SqlTypeString2SqlType(sqlTypeString);

            return SqlType2CsharpType(dbTpe);
        }

        // 将sql server中的数据类型，转化为C#中的类型的字符串
        public static string SqlTypeString2CsharpTypeString(string sqlTypeString)
        {
            Type type = SqlTypeString2CsharpType(sqlTypeString);

            return type.Name;
        }
        public static string SqlTypeString2SqlTypers(string sqlTypeString)
        {
            //SqlDbType dbType ="SqlDbType.Variant;//默认为Object
            string rs ="SqlDbType.Variant";
            switch (sqlTypeString)
            {
                case "int":
                    rs ="SqlDbType.Int";
                    break;
                case "varchar":
                    rs ="SqlDbType.VarChar";
                    break;
                case "bit":
                    rs ="SqlDbType.Bit";
                    break;
                case "datetime":
                    rs ="SqlDbType.DateTime";
                    break;
                case "decimal":
                    rs ="SqlDbType.Decimal";
                    break;
                case "float":
                    rs ="SqlDbType.Float";
                    break;
                case "image":
                    rs ="SqlDbType.Image";
                    break;
                case "money":
                    rs ="SqlDbType.Money";
                    break;
                case "ntext":
                    rs ="SqlDbType.NText";
                    break;
                case "nvarchar":
                    rs ="SqlDbType.NVarChar";
                    break;
                case "smalldatetime":
                    rs ="SqlDbType.SmallDateTime";
                    break;
                case "smallint":
                    rs ="SqlDbType.SmallInt";
                    break;
                case "text":
                    rs ="SqlDbType.Text";
                    break;
                case "bigint":
                    rs ="SqlDbType.BigInt";
                    break;
                case "binary":
                    rs ="SqlDbType.Binary";
                    break;
                case "char":
                    rs ="SqlDbType.Char";
                    break;
                case "nchar":
                    rs ="SqlDbType.NChar";
                    break;
                case "numeric":
                    rs ="SqlDbType.Decimal";
                    break;
                case "real":
                    rs ="SqlDbType.Real";
                    break;             
                case "smallmoney":
                    rs = "SqlDbType.SmallMoney";
                    break;
                case "sql_variant":
                    rs = "SqlDbType.Variant";
                    break;
                case "timestamp":
                    rs = "SqlDbType.Timestamp";
                    break;
                case "tinyint":
                    rs = "SqlDbType.TinyInt";
                    break;
                case "uniqueidentifier":
                    rs = "SqlDbType.UniqueIdentifier";
                    break;
                case "varbinary":
                    rs = "SqlDbType.VarBinary";
                    break;
                case "xml":
                    rs = "SqlDbType.Xml";
                    break;
            }
            return rs;
        }
        public static string Str_TypersStringSqlType(string sqlTypeString)
        {
            //SqlDbType dbType ="SqlDbType.Variant;//默认为Object
            //System.Convert.ToDateTime
            string rs = "System.Convert.ToString";
            //System.Convert.
            switch (sqlTypeString)
            {
                case "int":
                    rs = "System.Convert.ToInt32";
                    break;
                case "varchar":
                    rs = "System.Convert.ToString";
                    break;
                case "bit":
                    rs = "System.Convert.ToBoolean";
                    break;
                case "datetime":
                    rs = " DateTime.Parse";
                    break;
                case "decimal":
                    rs = "Convert.ToDecimal";
                    break;
                case "float":
                    rs = "System.Convert.ToDouble";
                    break;
                case "image":
                    rs = "(Object)";
                    break;
                case "money":
                    rs = "Convert.ToDecimal";
                    break;
                case "ntext":
                    rs = "System.Convert.ToString";
                    break;
                case "nvarchar":
                    rs = "System.Convert.ToString";
                    break;                    
                case "smalldatetime":
                    rs = "DateTime.Parse";
                    break;
                case "smallint":
                    rs = "System.Convert.ToInt16";
                    break;
                case "text":
                    rs = "System.Convert.ToString";
                    break;
                case "bigint":
                    rs = "System.Convert.ToInt32";
                    break;
                case "binary":
                    rs = "SqlDbType.Binary";
                    break;
                case "char":
                    rs = "System.Convert.ToString";
                    break;
                case "nchar":
                    rs = "System.Convert.ToString";
                    break;
                case "numeric":
                    rs = "SqlDbType.Decimal";
                    break;
                case "real":
                    rs = "(object)";
                    break;
                case "smallmoney":
                    rs = "(object)";
                    break;
                case "sql_variant":
                    rs = "(object)";
                    break;
                case "timestamp":
                    rs = "(object)";
                    break;
                case "tinyint":
                    rs = "System.Convert.ToInt32";
                    break;
                case "uniqueidentifier":
                    rs = "System.Guid.Parse";
                    break;
                case "varbinary":
                    rs = "(Object)";
                    break;
                case "xml":
                    rs = "(Object)";
                    break;
            }
            return rs;
        }
    }
}
