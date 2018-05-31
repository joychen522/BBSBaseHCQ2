if exists (select * from sysobjects where name = 'Bane_User')
drop table Bane_User;
--user_type：人员类别:社区戒毒，社区康复
--user_edu：文化程度:文盲，小学，初中，高中（含中专，技校），大学（含大专，本科），研究生以上
--job_status：就业情况:无业，企事业职工，娱乐场所从业，公务员，个体经营，农民，在校生，演艺人员，其他
--bane_type：毒品种类:海洛因，鸦片，大麻，吗啡，冰毒，冰毒片剂，吗啡因，安钠咖，氯胺酮，安眠剂，杜冷丁，可卡因，摇头丸，其他苯丙胺类毒品，埃托啡，其他毒品
--marital_status：婚姻状况:未婚，已婚，离异
--user_status：目前状态:无业，从事临时性工作，有固定工作
EXEC P_CreateTable 'Bane_User','戒毒人员基本信息表',
'user_id,主键,int,1
|user_name,姓名,50,1
|alias_name,别名,50, 
|user_sex,性别,10,
|user_birth,出生日期,date,
|user_height,身高,20,
|user_identify,身份证号,20,1
|user_edu,文化程度,20,
|job_status,就业情况,20,
|bane_type,毒品种类,20,
|birth_url,户籍所在地,200,
|family_phone,家庭电话,20,
|live_url,现居住地,200,
|move_phone,移动电话,20,
|attn_name,主要联系人,50,
|attn_url,主要联系人地址,200,
|attn_relation,主要联系人关系,50,
|attn_phone,主要联系人电话,20,
|marital_status,婚姻状况,20,
|is_live_parent,是否和父母居住,bit,1
|user_status,目前状态,50,
|is_pro_train,是否参加职业培训,bit,1
|user_skill,技能特长,500,
|user_type,人员类别,100,
|user_phone,联系电话,20,
|ur_next_date,下次尿检时间,date,1
|user_photo,照片,500,
|user_note,备注,1000,
|org_id,组织机构,int,1
|is_send,是否下发,bit,1
|update_date,更新时间,date,1
|iris_data1,第一段虹膜,4000,
|iris_data2,第二段虹膜,4000,
|user_resume,个人简历,2000,
|control_date,管控到期时间,date,
|user_guid,guid唯一标识,50,1
|user_pwd,密码,100,1
|user_mobile,手机号码,20,
|user_total,我的积分,int,1
|last_score,上次答题分数,int,1
|the_score,本次答题分数,int,1
|the_num,历史答题次数,int,1',1;
exec P_CreateIndex 'Bane_User','user_id',1,1;
exec P_CreateDefault 'Bane_User','user_sex','男';
exec P_CreateDefault 'Bane_User','is_send','0';
exec P_CreateDefault 'Bane_User','update_date','getdate()';--更新时间
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
--生成guid
--ALTER TABLE Bane_User
--ADD user_guid NVARCHAR(50) NOT NULL DEFAULT(NEWID())
--密码
--ALTER TABLE Bane_User
--ADD user_pwd NVARCHAR(100) NOT NULL DEFAULT('') 
--手机号
--ALTER TABLE Bane_User
--ADD user_mobile NVARCHAR(20) NULL
--我的积分
--ALTER TABLE Bane_User
--ADD user_total INT NOT NULL DEFAULT(0)
----上次答题分数
--ALTER TABLE Bane_User
--ADD last_score INT NOT NULL DEFAULT(0)
----本次答题分数
--ALTER TABLE Bane_User
--ADD the_score INT NOT NULL DEFAULT(0)
----答题历史次数
--ALTER TABLE Bane_User
--ADD the_num INT NOT NULL DEFAULT(0)
----管控到期时间
--ALTER TABLE Bane_User
--ADD control_date date NULL
----是否被管控
--ALTER TABLE Bane_User
--ADD is_control BIT NOT NULL DEFAULT(0)

----新闻、通知浏览次数
--ALTER TABLE T_MessageNotice
--ADD browse_num int NOT NULL DEFAULT(0)
----焦点新闻图片
--ALTER TABLE T_MessageNotice
--ADD focus_imgage NVARCHAR(200) NULL
----列表新闻图片
--ALTER TABLE T_MessageNotice
--ADD messList_imgage NVARCHAR(200) NULL
----新闻详情图片
--ALTER TABLE T_MessageNotice
--ADD messDetail_imgage NVARCHAR(200) NULL 

--社区戒毒/社区康复情况
--end_reason:结束原因:变更戒毒康复地点，期满解除，严重违反戒毒康复协议，刑事处罚，死亡，转强制隔离戒毒
if exists (select * from sysobjects where name = 'Bane_RecoveryInfo')
drop table Bane_RecoveryInfo;
EXEC P_CreateTable 'Bane_RecoveryInfo','社区戒毒/社区康复情况表',
'ri_id,主键,int,1
|user_identify,身份证号,20,1
|exec_area,执行地区,200,
|exec_unit,执行单位详称,200,
|order_unit,责令单位详称,200,
|is_aids,是否感染艾滋病,bit,1
|isolation_url,隔离场所,200,
|isolation_out_date,隔离场所出所日期,date,
|cure_ups,参加药物治疗门诊,100,
|in_recovery,是否进入康复场所,bit,
|start_date,报道日期,date,
|end_date,结束日期,date,
|end_reason,结束原因,100,',1;
exec P_CreateIndex 'Bane_RecoveryInfo','ri_id',1,1;
exec P_CreateDefault 'Bane_RecoveryInfo','is_aids','0';
exec P_CreateDefault 'Bane_RecoveryInfo','isolation_out_date','getdate()';
exec P_CreateDefault 'Bane_RecoveryInfo','in_recovery','0';
exec P_CreateDefault 'Bane_RecoveryInfo','start_date','getdate()';
GO

--个人违法犯罪记录
if exists (select * from sysobjects where name = 'Bane_CriminalRecord')
drop table Bane_CriminalRecord;
EXEC P_CreateTable 'Bane_CriminalRecord','个人违法犯罪记录表',
'cr_id,主键,int,1
|user_identify,身份证号,20,1
|start_drug_date,开始吸毒日期,date,
|drug_year,吸毒史(年),10,
|force_time,强制戒毒次数,int,
|force_insulate,强制隔离次数,int,
|other_record,其他违法犯罪记录,200,',1;
exec P_CreateIndex 'Bane_CriminalRecord','cr_id',1,1;
exec P_CreateDefault 'Bane_CriminalRecord','start_drug_date','getdate()';
exec P_CreateDefault 'Bane_CriminalRecord','force_time',1;
exec P_CreateDefault 'Bane_CriminalRecord','force_insulate',0;
GO


--家庭成员/社会关系表
--关系类别:0:家庭成员，1：社会关系人员
if exists (select * from sysobjects where name = 'Bane_FamilyRecord')
drop table Bane_FamilyRecord;
EXEC P_CreateTable 'Bane_FamilyRecord','家庭成员',
'fr_id,主键,int,1
|user_identify,身份证号,20,1
|fr_name,姓名,50,1
|fr_sex,性别,10,
|fr_birth,家庭成员出生日期,date,
|fr_edu,文化程度,20,
|fr_family_url,家庭住址,200,
|fr_job,职业,100,
|fr_unit,工作单位,200,
|fr_relation,相互关系,50,
|fr_phone,联系电话,20,
|fr_type,关系类别,int,1',1;
exec P_CreateIndex 'Bane_FamilyRecord','fr_id',1,1;
exec P_CreateDefault 'Bane_FamilyRecord','fr_type',0;
GO


--定期尿检记录表
if exists (select * from sysobjects where name = 'Bane_UrinalysisRecord')
drop table Bane_UrinalysisRecord;
EXEC P_CreateTable 'Bane_UrinalysisRecord','定期尿检记录表',
'ur_id,主键,int,1
|ur_code,编号,100,
|user_identify,身份证号,20,1
|ur_should_date,本次应到尿检时间,datetime,1
|ur_reality_date,实际尿检时间,datetime,1
|ur_site,现场检测地点,200,
|ur_method,现场检测方法,200,
|ur_manager,尿检监督人员,50,
|ur_result,尿检结果,1000,
|ur_result_show,结果呈现,50,
|ur_file_name,附件名称,100,
|ur_attach,尿检附件文件路径,2000,
|ur_input_date,录入时间,date,
|ur_note,备注,1000,
|approve_status,任务完成状态,int,1',1;
exec P_CreateIndex 'Bane_UrinalysisRecord','ur_id',1,1;
exec P_CreateDefault 'Bane_UrinalysisRecord','approve_status',0;
GO


--禁毒人员权限关系表
if exists (select * from sysobjects where name = 'Bane_UserPermissRelation')
drop table Bane_UserPermissRelation;
exec P_CreateTable 'Bane_UserPermissRelation','禁毒人员权限关系表',
'tb_id,主键,int,1
|user_id,用户id,int,1
|folder_id,区域id,int,1
|per_id,权限ID,int,1';
exec P_CreateIndex 'Bane_UserPermissRelation','tb_id';
GO

--尿检时间设置
--user_type：人员类别:社区戒毒，社区康复
if exists (select * from sysobjects where name = 'Bane_UrinalysisTimeSet')
drop table Bane_UrinalysisTimeSet;
EXEC P_CreateTable 'Bane_UrinalysisTimeSet','尿检时间设置',
'ut_id,主键,int,1
|user_type,人员类别,20,1
|min_month,最小月数,int,1
|max_month,最大月数,int,1
|gap_month,间隔月数检查,int,1',1;
exec P_CreateIndex 'Bane_UrinalysisTimeSet','ut_id',1,1;
GO
--社区戒毒
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('社区戒毒',0,12,1);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('社区戒毒',13,24,2);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('社区戒毒',25,0,3);
--社区康复
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('社区康复',0,12,2);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('社区康复',13,24,3);
INSERT INTO Bane_UrinalysisTimeSet(user_type,min_month,max_month,gap_month) VALUES('社区康复',25,0,6);