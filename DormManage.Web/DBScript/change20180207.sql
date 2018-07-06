
-- 2018-02-07 添加与离职系统对接相关

use DormManage;

-- 增加离职签退标记
-- CanLeave=0, 界面没有勾同步签退离职系统, 还不允许走
-- CanLeave=null,当作旧数据,作为1处理
-- CanLeave=1, 可以走人了
--
ALTER TABLE TB_EmployeeCheckOut
	ADD CanLeave BIT DEFAULT(0);
GO


-- 检查员工是否没有办退宿，或已办了退宿，但不允许签退
-- @employeeNo 工号
-- @idCardNO   身份证
-- 工号和身份证至少需要提供一个。
-- Returns:
--  -1 -- 已办了退宿，但不允许签退
--  -2 -- 没有办退宿
--  -7 -- 其它错误
--   0 -- 允许签退，可以走人了
--
CREATE PROCEDURE [dbo].CanEmployeeLeave(@employeeNo varchar(20), @idCardNO varchar(18))
AS
BEGIN
	DECLARE @nFound INT = 0
	DECLARE @sCheckIn nvarchar(300) = N'select @nFound= count(*) from TB_EmployeeCheckIn where 1=1 '
	-- CanLeave=null的话是旧数据了
	DECLARE @sCheckOut nvarchar(400) = N'select @nFound= isnull(t.CanLeave,1) from TB_EmployeeCheckOut t where 1=1 '

	if (@employeeNo IS NULL OR LEN(LTRIM(@employeeNo))=0) 
		AND (@idCardNO IS NULL OR LEN(LTRIM(@idCardNO))=0)
		return 0

	if not (@employeeNo IS NULL OR rtrim(@employeeNo)='')
	BEGIN
		SET @sCheckIn = @sCheckIn + ' AND (EmployeeNo=''' + rtrim(@employeeNo)+''''
		if not (@idCardNO IS NULL OR len(rtrim(@idCardNO))<>18)
		BEGIN
			SET @sCheckIn = @sCheckIn + ' OR CardNo=''' + rtrim(@idCardNO)+''''
		END
		SET @sCheckIn = @sCheckIn + ')'
	END
	else
		if not (@idCardNO IS NULL OR len(rtrim(@idCardNO))<>18)
		BEGIN
			SET @sCheckIn = @sCheckIn + ' AND CardNo=''' + rtrim(@idCardNO)+''''
		END

	EXEC sp_executesql @sCheckIn, N'@nFound int output', @nFound output
	if @@error <> 0 
		return -7
	else if @nFound > 0
      return -2
	
	SET @nFound = -99
	--检查签退标记
	if not (@employeeNo IS NULL OR rtrim(@employeeNo)='')
	BEGIN
		SET @sCheckOut = @sCheckOut + ' AND (EmployeeNo=''' + rtrim(@employeeNo)+''''
		if not (@idCardNO IS NULL OR len(rtrim(@idCardNO))<>18)
		BEGIN
			SET @sCheckOut = @sCheckOut + ' OR CardNo=''' + rtrim(@idCardNO)+''''
		END
		SET @sCheckOut = @sCheckOut + ')'
	END
	else
		if not (@idCardNO IS NULL OR len(rtrim(@idCardNO))<>18)
		BEGIN
			SET @sCheckOut = @sCheckOut + ' AND CardNo=''' + rtrim(@idCardNO)+''''
		END
	SET @sCheckOut = @sCheckOut + ' order by t.CanLeave, t.CheckOutDate DESC'
	EXEC sp_executesql @sCheckOut, N'@nFound int output', @nFound output

	--PRINT @sCheckOut

	if -99=@nFound
		--未住宿
		return 0
	else if 1=@nFound
		return 0
	else if 0=@nFound
		return -1
	return -7
END;
GO







