--用户-人员-区域- 权限表
if exists (select * from sysobjects where name = 'T_UserUnitPersonRelation')
drop table T_UserUnitPersonRelation;
exec P_CreateTable 'T_UserUnitPersonRelation','用户人员区域关系表',
'u_id,主键,int,1
|user_id,登录用户ID,int,1
|person_id,人员用户ID,int,1
|org_id,区域ID,int,1';
exec P_CreateIndex 'T_UserUnitPersonRelation','u_id,user_id';
go