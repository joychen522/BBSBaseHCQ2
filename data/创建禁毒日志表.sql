--����������־��
if exists (select * from sysobjects where name = 'Bane_LogDetail')
drop table Bane_LogDetail;
exec P_CreateTable 'Bane_LogDetail','������־��',
'log_id,����,int,1
|user_id,������ID,int,
|user_name,����������,20,
|log_type,��������,200,
|log_ip,����IP,20,
|log_title,��������,100,
|log_context,����,1000,
|log_date,��¼����,datetime,1
|log_note,��ע,1000,
';
exec P_CreateIndex 'Bane_LogDetail','log_id';
exec P_CreateDefault 'Bane_LogDetail','log_date','getdate()';
go