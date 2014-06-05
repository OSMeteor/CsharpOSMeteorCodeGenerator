public DataTable  Table_1Select_Custom666(System.String column1,System.Int32 column2,System.DateTime column3,System.Guid id)
{
            string sql="use test select column1,column2,column3,id from  [test].[dbo].[Table_1]  where column1=@column1,column2=@column2,column3=@column3,id=@id"

            SqlParameter[] spms = {
               new SqlParameter("@column1", SqlDbType.VarChar , -1 ) { Value =column1 },
               new SqlParameter("@column2", SqlDbType.Int ) { Value =column2 },
               new SqlParameter("@column3", SqlDbType.DateTime ) { Value =column3 },
               new SqlParameter("@id", SqlDbType.UniqueIdentifier ) { Value =id }
             };
            return SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql, spms);
}

