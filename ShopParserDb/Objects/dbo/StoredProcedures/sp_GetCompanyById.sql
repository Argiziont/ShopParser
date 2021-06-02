CREATE PROCEDURE [dbo].[sp_GetCompanyById]
	@companyId int
AS
BEGIN	
	SELECT * FROM Categories ctg WHERE	ctg.Id	 = @companyId
END
