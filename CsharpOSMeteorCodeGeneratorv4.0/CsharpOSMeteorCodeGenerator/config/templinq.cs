//
/************linqselect****************/
public static List<Entity.tb_module_type> getALLuserlist()
{
    List<Entity.tb_module_type>  rslist=new List<Entity.tb_module_type>();
    using (DataObjectDataContext db = new DataObjectDataContext())
    {
        var querylist = from s in db.tb_module_type orderby s.id select s ;
        if(querylist==null)
        {
             return rslist;
        }
        foreach (var obj in querylist)
        {
              Entity.tb_module_type  query=new Entity.tb_module_type ();
              query.id=(Object)obj.id;
              query.pid=(Object)obj.pid;
              query.mtype_name=(String)obj.mtype_name;
              query.mtype_code=(String)obj.mtype_code;
              query.loop=(Int32)obj.loop;
              query.pic_name=(String)obj.pic_name;
              query.memo=(String)obj.memo;
              query.stauts=(Int32)obj.stauts;

              rslist.Add(query);
        }
    }
    return rslist;
    }
/****************************/
//
/************linqselectToIEnumerable****************/
public static IEnumerable<Entity.tb_module_type> getALLtb_module_typeToIEnumerable(?Expression<Func<Entity.tb_module_type, bool>> expression,int isall)
{
    using (DataObjectDataContext db = new DataObjectDataContext())
    {
        if (isall == 1)
        {
            return db.tb_module_type.Rows.Cast<Entity.tb_module_type>();
        }
        else
        {
            return db.tb_module_type.Rows.Cast<Entity.tb_module_type>().AsQueryable<Entity.tb_module_type>().Where(expression);
        }
    }
}
/****************************/

/************linqAdd****************/
public static int tb_module_typeAdd(Entity.tb_module_type obj)
{
    int rs = 0;//0  >>add failure
    //1  >>Has been in existence can't repeat to add
    //2  >>add success
    using (DataObjectDataContext dt = new DataObjectDataContext())
    {
        var query = from s in dt.tb_module_type
                    where (your conditions)
                    select  s ;
        if (query.ToList().Count > 0)
        {
            rs = 1;
        }
        else
        {              
            tb_module_type  newojb = new tb_module_type()
            {
              id=(Object)obj.id,
              pid=(Object)obj.pid,
              mtype_name=(String)obj.mtype_name,
              mtype_code=(String)obj.mtype_code,
              loop=(Int32)obj.loop,
              pic_name=(String)obj.pic_name,
              memo=(String)obj.memo,
              stauts=(Int32)obj.stauts
            };
            dt.tb_module_type.InsertOnSubmit(newojb);
            dt.SubmitChanges();
            rs = 2;
        }
    }
    return rs;
}
/****************************/

/************linqDelete****************/
public static int tb_module_typeDelete(string objid)
{
      int rs = 0; // 0 failure
                 //1  Delete the object does not exist
                //2 success
    using (DataObjectDataContext db = new DataObjectDataContext())
    {
        var query = db.tb_module_type.SingleOrDefault<tb_module_type>(s => s.id == System.Guid.Parse(objid));
        if (query == null)
        {
            rs = 1;
            return rs;
        }
        db.tb_module_type.DeleteOnSubmit(query);
        db.SubmitChanges();
        rs=2;
    }
    return rs;
}
/****************************/

/************linqUpdate****************/
 public static int tb_module_typeUpdate(Entity.tb_module_type obj)
        {
            int rs = 0;// 0 failure
                      //1  update the object does not exist
                     //2 success
        using (DataObjectDataContext db = new DataObjectDataContext())
        {
            var query = db.tb_module_type.SingleOrDefault<tb_module_type>(s => s.id == System.Guid.Parse(obj.id));
            if (query == null)
            {
                rs = 1;
                return rs;
            }
              query.id=(Object)obj.id;
              query.pid=(Object)obj.pid;
              query.mtype_name=(String)obj.mtype_name;
              query.mtype_code=(String)obj.mtype_code;
              query.loop=(Int32)obj.loop;
              query.pic_name=(String)obj.pic_name;
              query.memo=(String)obj.memo;
              query.stauts=(Int32)obj.stauts;
                
                db.SubmitChanges();
                rs = 2;

            }
            return rs;
        }
/****************************/
