/************SqlParameter****************/

 using (SqlConnection conn = new SqlConnection(connectionString))
 {
  conn.Open();
  SqlCommand comm = new SqlCommand();
  comm.Connection = conn;
  string strql_insert="insert into tb_module_type(id,pid,mtype_name,mtype_code,loop,pic_name,memo,stauts) values(@id,@pid,@mtype_name,@mtype_code,@loop,@pic_name,@memo,@stauts)";
  string strql_select=" select id,pid,mtype_name,mtype_code,loop,pic_name,memo,stauts from tb_module_type where 1=1 ";
  string sql_update=" update tb_module_type set id= @id,pid= @pid,mtype_name= @mtype_name,mtype_code= @mtype_code,loop= @loop,pic_name= @pic_name,memo= @memo,stauts= @stauts where 1=1 ";
  comm.CommandText = "";
  comm.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier ) { Value = "v_id" });
  comm.Parameters.Add(new SqlParameter("@pid", SqlDbType.UniqueIdentifier ) { Value = "v_pid" });
  comm.Parameters.Add(new SqlParameter("@mtype_name", SqlDbType.VarChar , -1 ) { Value = "v_mtype_name" });
  comm.Parameters.Add(new SqlParameter("@mtype_code", SqlDbType.VarChar , -1 ) { Value = "v_mtype_code" });
  comm.Parameters.Add(new SqlParameter("@loop", SqlDbType.Int ) { Value = "v_loop" });
  comm.Parameters.Add(new SqlParameter("@pic_name", SqlDbType.VarChar , -1 ) { Value = "v_pic_name" });
  comm.Parameters.Add(new SqlParameter("@memo", SqlDbType.VarChar , -1 ) { Value = "v_memo" });
  comm.Parameters.Add(new SqlParameter("@stauts", SqlDbType.Int ) { Value = "v_stauts" });
  comm.ExecuteNonQuery();
  //SqlDataAdapter adp = new SqlDataAdapter(comm);
  //DataSet result =adp.Fill(result);
  conn.Close();
 }
/****************************/

