--�û�-��Ա-����- Ȩ�ޱ�
if exists (select * from sysobjects where name = 'T_UserUnitPersonRelation')
drop table T_UserUnitPersonRelation;
exec P_CreateTable 'T_UserUnitPersonRelation','�û���Ա�����ϵ��',
'u_id,����,int,1
|user_id,��¼�û�ID,int,1
|person_id,��Ա�û�ID,int,1
|org_id,����ID,int,1';
exec P_CreateIndex 'T_UserUnitPersonRelation','u_id,user_id';
go