--组织机构添加字段
IF not exists(select * from syscolumns where id=object_id('T_OrgFolder') and name='folder_path')
BEGIN
	ALTER TABLE dbo.T_OrgFolder
	ADD folder_path NVARCHAR(50) NULL
END;