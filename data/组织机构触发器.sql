IF (EXISTS(SELECT name FROM sysobjects WHERE name='tri_T_OrgFolder'))
DROP TRIGGER tri_T_OrgFolder;
GO
CREATE TRIGGER tri_T_OrgFolder
ON T_OrgFolder
FOR INSERT,DELETE
AS 
DECLARE @folder_id INT
DECLARE @folder_pid INT 
DECLARE @folder_path NVARCHAR(50)
DECLARE @addPath NVARCHAR(50)='00'
--²åÈë
IF EXISTS(SELECT 1 FROM Inserted) AND NOT EXISTS(SELECT 1 FROM Deleted)
BEGIN
	SET @folder_pid=(SELECT folder_pid FROM Inserted);
	SET @folder_id=(SELECT folder_id FROM Inserted);
	IF(@folder_pid=0)
	BEGIN
		IF(@folder_id>9)
			SET @addPath='0';
		UPDATE T_OrgFolder SET folder_path=@addPath+convert(varchar(20),@folder_id) WHERE folder_id=@folder_id;
	END
	ELSE
	BEGIN
		DECLARE @child INT
		SET @folder_path=(SELECT folder_path FROM T_OrgFolder WHERE folder_id=@folder_pid);
		SET @child=(SELECT COUNT(*) FROM T_OrgFolder WHERE folder_pid=@folder_pid);
		IF(@child>9)
			SET @addPath='0';
		UPDATE T_OrgFolder SET folder_path=@folder_path+@addPath+convert(varchar(20),@child) WHERE folder_id=@folder_id;
	END
END
--É¾³ý
IF NOT EXISTS(SELECT 1 FROM Inserted) AND EXISTS(SELECT 1 FROM Deleted)
BEGIN
	SET @folder_pid=(SELECT folder_pid FROM Deleted);
	SET @folder_id=(SELECT TOP 1 folder_id FROM T_OrgFolder WHERE folder_pid=@folder_pid ORDER BY folder_path DESC);
	SET @folder_path=(SELECT folder_path FROM Deleted);
	UPDATE T_OrgFolder SET folder_path=@folder_path WHERE folder_id=@folder_id;
END


