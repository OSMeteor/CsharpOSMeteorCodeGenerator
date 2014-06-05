
/*************EntityClass***************/
    public class tb_module_type
    { 

     public string  id { get; set ; }
     public string  pid { get; set ; }
     public string  mtype_name { get; set ; }
     public string  mtype_code { get; set ; }
     public int  loop { get; set ; }
     public string  pic_name { get; set ; }
     public string  memo { get; set ; }
     public int  stauts { get; set ; }
                    
    }
/***************Entity*************/

     tb_module_type  newojb = new tb_module_type();
     newojb.id=(uniqueidentifier);
     newojb.pid=(uniqueidentifier);
     newojb.mtype_name=(varchar);
     newojb.mtype_code=(varchar);
     newojb.loop=(int);
     newojb.pic_name=(varchar);
     newojb.memo=(varchar);
     newojb.stauts=(int);
/***********Entity*****************/

     tb_module_type  newojb = new tb_module_type()
     {
          id=(uniqueidentifier),
          pid=(uniqueidentifier),
          mtype_name=(varchar),
          mtype_code=(varchar),
          loop=(int),
          pic_name=(varchar),
          memo=(varchar),
          stauts=(int)
     };
/*************JSON***************/
{ "id": v_id(uniqueidentifier), "pid": v_pid(uniqueidentifier), "mtype_name": v_mtype_name(varchar), "mtype_code": v_mtype_code(varchar), "loop": v_loop(int), "pic_name": v_pic_name(varchar), "memo": v_memo(varchar), "stauts": v_stauts(int)}
/****************************/
