CREATE PROCEDURE [dbo].[sp_GetCompanyById]
	@companyId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   
	SELECT * FROM Companies cmp WHERE cmp.Id = @companyId
END