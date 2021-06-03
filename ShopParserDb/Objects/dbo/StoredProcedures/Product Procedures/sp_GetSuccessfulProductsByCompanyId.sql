CREATE PROCEDURE [dbo].[sp_GetSuccessfulProductsByCompanyId]
	 @companyId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
	SELECT prd.*
	FROM	
		Products prd
	WHERE prd.CompanyId=@companyId AND prd.ProductState=1

END