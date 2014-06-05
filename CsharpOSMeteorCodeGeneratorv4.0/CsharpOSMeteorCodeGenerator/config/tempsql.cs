
/************Select****************/
select id,pid,mtype_name,mtype_code,loop,pic_name,memo,stauts from tb_module_type where 1=1
/************Insert****************/
insert into tb_module_type(id,pid,mtype_name,mtype_code,loop,pic_name,memo,stauts) values(v_id(string),v_pid(string),v_mtype_name(string),v_mtype_code(string),v_loop(int),v_pic_name(string),v_memo(string),v_stauts(int))
/************Update****************/
update tb_module_type set id=(uniqueidentifier),pid=(uniqueidentifier),mtype_name=(varchar),mtype_code=(varchar),loop=(int),pic_name=(varchar),memo=(varchar),stauts=(int)where 1=1
/************Delete****************/
delete  from tb_module_type where 1=1
/****************************/

