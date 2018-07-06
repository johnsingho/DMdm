
-- 2018-02-07 �������ְϵͳ�Խ����

use DormManage;

-- ������ְǩ�˱��
-- CanLeave=0, ����û�й�ͬ��ǩ����ְϵͳ, ����������
-- CanLeave=null,����������,��Ϊ1����
-- CanLeave=1, ����������
--
ALTER TABLE TB_EmployeeCheckOut
	ADD CanLeave BIT DEFAULT(0);
GO


-- ���Ա���Ƿ�û�а����ޣ����Ѱ������ޣ���������ǩ��
-- @employeeNo ����
-- @idCardNO   ���֤
-- ���ź����֤������Ҫ�ṩһ����
-- Returns:
--  -1 -- �Ѱ������ޣ���������ǩ��
--  -2 -- û�а�����
--  -7 -- ��������
--   0 -- ����ǩ�ˣ�����������
--
CREATE PROCEDURE [dbo].CanEmployeeLeave(@employeeNo varchar(20), @idCardNO varchar(18))
AS
BEGIN
	DECLARE @nFound INT = 0
	DECLARE @sCheckIn nvarchar(300) = N'select @nFound= count(*) from TB_EmployeeCheckIn where 1=1 '
	-- CanLeave=null�Ļ��Ǿ�������
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
	--���ǩ�˱��
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
		--δס��
		return 0
	else if 1=@nFound
		return 0
	else if 0=@nFound
		return -1
	return -7
END;
GO







