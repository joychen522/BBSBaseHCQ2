--创建禁毒日志表
if exists (select * from sysobjects where name = 'Bane_LogDetail')
drop table Bane_LogDetail;
exec P_CreateTable 'Bane_LogDetail','禁毒日志表',
'log_id,主键,int,1
|user_id,操作者ID,int,
|user_name,操作者名称,20,
|log_type,操作类型,200,
|log_ip,操作IP,20,
|log_title,操作标题,100,
|log_context,内容,1000,
|log_date,记录日期,datetime,1
|log_note,备注,1000,
';
exec P_CreateIndex 'Bane_LogDetail','log_id';
exec P_CreateDefault 'Bane_LogDetail','log_date','getdate()';
go