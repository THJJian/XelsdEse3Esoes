GO
IF EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID(N'[dbo].[do_common_paging]') AND OBJECTPROPERTY(id,N'IsProcedure')=1)
BEGIN
	DROP PROC [dbo].[do_common_paging]
END
GO
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[do_common_paging]
(
	@chvSQL VARCHAR(7700),
	@intPageIndex int = 1,
	@intPageSize int = 30,
	@chvPrimKey VARCHAR(100),
	@chvSort VARCHAR(256) = '',
	@bitReturnCount bit = 0
)
WITH ENCRYPTION
AS
SET NOCOUNT ON

DECLARE @chvRunStr NVARCHAR(4000)

IF ISNULL(@chvSort, '') = ''
BEGIN
	SELECT @chvSort = @chvPrimKey + ' DESC'
END
ELSE
BEGIN
	IF CHARINDEX(@chvPrimKey, @chvSort)=0
	BEGIN
		SELECT @chvSort = @chvSort + ',' + @chvPrimKey + ' DESC'
	END
END

IF @intPageIndex = 0
BEGIN 
	SELECT @intPageIndex = 1 
END

DECLARE @startRowNumber INT,@endRowNumber INT

SELECT @endRowNumber = @intPageIndex * @intPageSize, @startRowNumber = @endRowNumber - @intPageSize + 1


SET @chvRunStr = 
	N'WITH tbl AS(' + @chvSQL + ') SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ' + @chvSort + ')  AS RowNumber, * FROM tbl) paging WHERE paging.RowNumber>=' + CAST(@startRowNumber AS VARCHAR) 
	+ ' AND paging.RowNumber<=' + CAST(@endRowNumber AS VARCHAR) + ' ORDER BY RowNumber'

EXEC sp_ExecuteSql @chvRunStr

IF @bitReturnCount = 1
BEGIN
	SELECT @chvRunStr = N'SELECT COUNT(0) FROM (' + @chvSQL + ') T'
END

EXEC sp_executesql @chvRunStr
GO


