CREATE PROCEDURE [dbo].[sp_GetCompanyByName]
	@companyName nvarchar(MAX)NULL
AS
BEGIN
	SELECT * FROM Categories ctg WHERE	ctg.Name = @companyName
END
