CREATE PROCEDURE [dbo].[sp_GetCompanyByName]
	@companyName nvarchar(MAX)NULL
AS
BEGIN
	SELECT * FROM Companies cmp WHERE cmp.Name = @companyName
END
