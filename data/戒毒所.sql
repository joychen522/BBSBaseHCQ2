if exists (select * from sysobjects where name = 'Bane_User')
drop table Bane_User;
--user_type����Ա���:�����䶾����������
--user_edu���Ļ��̶�:��ä��Сѧ�����У����У�����ר����У������ѧ������ר�����ƣ����о�������
--job_status����ҵ���:��ҵ������ҵְ�������ֳ�����ҵ������Ա�����徭Ӫ��ũ����У����������Ա������
--bane_type����Ʒ����:������ѻƬ�����飬��ȣ�����������Ƭ��������򣬰��ƿ����Ȱ�ͪ�����߼������䶡���ɿ���ҡͷ�裬�����������඾Ʒ�����зȣ�������Ʒ
--marital_status������״��:δ�飬�ѻ飬����
--user_status��Ŀǰ״̬:��ҵ��������ʱ�Թ������й̶�����
EXEC P_CreateTable 'Bane_User','�䶾��Ա������Ϣ��',
'user_id,����,int,1
|user_name,����,50,1
|alias_name,����,50, 
|user_sex,�Ա�,10,
|user_birth,��������,date,
|user_height,���,20,
|user_identify,���֤��,20,1
|user_edu,�Ļ��̶�,20,
|job_status,��ҵ���,20,
|bane_type,��Ʒ����,20,
|birth_url,�������ڵ�,200,
|family_phone,��ͥ�绰,20,
|live_url,�־�ס��,200,
|move_phone,�ƶ��绰,20,
|attn_name,��Ҫ��ϵ��,50,
|attn_url,��Ҫ��ϵ�˵�ַ,200,
|attn_relation,��Ҫ��ϵ�˹�ϵ,50,
|attn_phone,��Ҫ��ϵ�˵绰,20,
|marital_status,����״��,20,
|is_live_parent,�Ƿ�͸�ĸ��ס,bit,1
|user_status,Ŀǰ״̬,50,
|is_pro_train,�Ƿ�μ�ְҵ��ѵ,bit,1
|user_skill,�����س�,500,
|user_type,��Ա���,100,
|user_phone,��ϵ�绰,20,
|ur_next_date,�´����ʱ��,date,1
|user_photo,��Ƭ,500,
|user_note,��ע,1000,
|org_id,��֯����,int,1
|is_send,�Ƿ��·�,bit,1
|update_date,����ʱ��,date,1
|iris_data1,��һ�κ�Ĥ,4000,
|iris_data2,�ڶ��κ�Ĥ,4000,
|user_resume,���˼���,2000,
|control_date,�ܿص���ʱ��,date,
|user_guid,guidΨһ��ʶ,50,1
|user_pwd,����,100,1
|user_mobile,�ֻ�����,20,
|user_total,�ҵĻ���,int,1
|last_score,�ϴδ������,int,1
|the_score,���δ������,int,1
|the_num,��ʷ�������,int,1',1;
exec P_CreateIndex 'Bane_User','user_id',1,1;
exec P_CreateDefault 'Bane_User','user_sex','��';
exec P_CreateDefault 'Bane_User','is_send','0';
exec P_CreateDefault 'Bane_User','update_date','getdate()';--����ʱ��
exec P_CreateDefault 'Bane_User','is_live_parent','0';
--exec P_CreateDefault 'Bane_User','user_guid',NEWID();
exec P_CreateDefault 'Bane_User','is_pro_train','0';
exec P_CreateDefault 'Bane_User','user_pwd','';
exec P_CreateDefault 'Bane_User','user_total','0';
exec P_CreateDefault 'Bane_User','last_score','0';
exec P_CreateDefault 'Bane_User','the_score','0';
exec P_CreateDefault 'Bane_User','the_num','0';
exec P_CreateDefault 'Bane_User','control_date','getdate()';
GO
--����guid
--ALTER TABLE Bane_User
--ADD user_guid NVARCHAR(50) NOT NULL DEFAULT(NEWID())
--����
--ALTER TABLE Bane_User
--ADD user_pwd NVARCHAR(100) NOT NULL DEFAULT('') 
--�ֻ���
--ALTER TABLE Bane_User
--ADD user_mobile NVARCHAR(20) NULL
--�ҵĻ���
--ALTER TABLE Bane_User
--ADD user_total INT NOT NULL DEFAULT(0)
----�ϴδ������
--ALTER TABLE Bane_User
--ADD last_score INT NOT NULL DEFAULT(0)
----���δ������
--ALTER TABLE Bane_User
--ADD the_score INT NOT NULL DEFAULT(0)
----������ʷ����
--ALTER TABLE Bane_User
--ADD the_num INT NOT NULL DEFAULT(0)
----�ܿص���ʱ��
--ALTER TABLE Bane_User
--ADD control_date date NULL
----�Ƿ񱻹ܿ�
--ALTER TABLE Bane_User
--ADD is_control BIT NOT NULL DEFAULT(0)

----���š�֪ͨ�������
--ALTER TABLE T_MessageNotice
--ADD browse_num int NOT NULL DEFAULT(0)
----��������ͼƬ
--ALTER TABLE T_MessageNotice
--ADD focus_imgage NVARCHAR(200) NULL
----�б�����ͼƬ
--ALTER TABLE T_MessageNotice
--ADD messList_imgage NVARCHAR(200) NULL
----��������ͼƬ
--ALTER TABLE T_MessageNotice
--ADD messDetail_imgage NVARCHAR(200) NULL 

--�����䶾/�����������
--end_reason:����ԭ��:����䶾�����ص㣬�������������Υ���䶾����Э�飬���´�����������תǿ�Ƹ���䶾
if exists (select * from sysobjects where name = 'Bane_RecoveryInfo')
drop table Bane_RecoveryInfo;
EXEC P_CreateTable 'Bane_RecoveryInfo','�����䶾/�������������',
'ri_id,����,int,1
|user_identify,���֤��,20,1
|exec_area,ִ�е���,200,
|exec_unit,ִ�е�λ���,200,
|order_unit,���λ���,200,
|is_aids,�Ƿ��Ⱦ���̲�,bit,1
|isolation_url,���볡��,200,
|isolation_out_date,���볡����������,date,
|cure_ups,�μ�ҩ����������,100,
|in_recovery,�Ƿ���뿵������,bit,
|start_date,��������,date,
|end_date,��������,date,
|end_reason,����ԭ��,100,',1;
exec P_CreateIndex 'Bane_RecoveryInfo','ri_id',1,1;
exec P_CreateDefault 'Bane_RecoveryInfo','is_aids','0';
exec P_CreateDefault 'Bane_RecoveryInfo','isolation_out_date','getdate()';
exec P_CreateDefault 'Bane_RecoveryInfo','in_recovery','0';
exec P_CreateDefault 'Bane_RecoveryInfo','start_date','getdate()';
GO

--����Υ�������¼
if exists (select * from sysobjects where name = 'Bane_CriminalRecord')
drop table Bane_CriminalRecord;
EXEC P_CreateTable 'Bane_CriminalRecord','����Υ�������¼��',
'cr_id,����,int,1
|user_identify,���֤��,20,1
|start_drug_date,��ʼ��������,date,
|drug_year,����ʷ(��),10,
|force_time,ǿ�ƽ䶾����,int,
|force_insulate,ǿ�Ƹ������,int,
|other_record,����Υ�������¼,200,',1;
exec P_CreateIndex 'Bane_CriminalRecord','cr_id',1,1;
exec P_CreateDefault 'Bane_CriminalRecord','start_drug_date','getdate()';
exec P_CreateDefault 'Bane_CriminalRecord','force_time',1;
exec P_CreateDefault 'Bane_CriminalRecord','force_insulate',0;
GO


--��ͥ��Ա/����ϵ��
--��ϵ���:0:��ͥ��Ա��1������ϵ��Ա
if exists (select * from sysobjects where name = 'Bane_FamilyRecord')
drop table Bane_FamilyRecord;
EXEC P_CreateTable 'Bane_FamilyRecord','��ͥ��Ա',
'fr_id,����,int,1
|user_identify,���֤��,20,1
|fr_name,����,50,1
|fr_sex,�Ա�,10,
|fr_birth,��ͥ��Ա��������,date,
|fr_edu,�Ļ��̶�,20,
|fr_family_url,��ͥסַ,200,
|fr_job,ְҵ,100,
|fr_unit,������λ,200,
|fr_relation,�໥��ϵ,50,
|fr_phone,��ϵ�绰,20,
|fr_type,��ϵ���,int,1',1;
exec P_CreateIndex 'Bane_FamilyRecord','fr_id',1,1;
exec P_CreateDefault 'Bane_FamilyRecord','fr_type',0;
GO


--��������¼��
if exists (select * from sysobjects where name = 'Bane_UrinalysisRecord')
drop table Bane_UrinalysisRecord;
EXEC P_CreateTable 'Bane_UrinalysisRecord','��������¼��',
'ur_id,����,int,1
|ur_code,���,100,
|user_identify,���֤��,20,1
|ur_should_date,����Ӧ�����ʱ��,datetime,1
|ur_reality_date,ʵ�����ʱ��,datetime,1
|ur_site,�ֳ����ص�,200,
|ur_method,�ֳ���ⷽ��,200,
|ur_manager,���ල��Ա,50,
|ur_result,�����,1000,
|ur_result_show,�������,50,
|ur_file_name,��������,100,
|ur_attach,��츽���ļ�·��,2000,
|ur_input_date,¼��ʱ��,date,
|ur_note,��ע,1000,
|approve_status,�������״̬,int,1',1;
exec P_CreateIndex 'Bane_UrinalysisRecord','ur_id',1,1;
exec P_CreateDefault 'Bane_UrinalysisRecord','approve_status',0;
GO


--������ԱȨ�޹�ϵ��
if exists (select * from sysobjects where name = 'Bane_UserPermissRelation')
drop table Bane_UserPermissRelation;
exec P_CreateTable 'Bane_UserPermissRelation','������ԱȨ�޹�ϵ��',
'tb_id,����,int,1
|user_id,�û�id,int,1
|folder_id,����id,int,1
|per_id,Ȩ��ID,int,1';
exec P_CreateIndex 'Bane_UserPermissRelation','tb_id';
GO

--���ʱ������
--user_type����Ա���:�����䶾����������
if exists (select * from sysobjects where name = 'Bane_UrinalysisTimeSet')
drop table Bane_UrinalysisTimeSet;
EXEC P_CreateTable 'Bane_UrinalysisTimeSet','���ʱ������',
'ut_id,����,int,1
|user_type,��Ա���,20,1
|min_month,��С����,int,1
|max_month,�������,int,1
|gap_month,����������,int,1',1;
exec P_CreateIndex 'Bane_UrinalysisTimeSet','ut_id',1,1;
GO
--�����䶾
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('�����䶾',0,12,1);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('�����䶾',13,24,2);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('�����䶾',25,0,3);
--��������
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('��������',0,12,2);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('��������',13,24,3);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('��������',25,0,6);