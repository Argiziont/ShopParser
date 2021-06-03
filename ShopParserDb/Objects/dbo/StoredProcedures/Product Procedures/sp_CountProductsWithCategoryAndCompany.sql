CREATE PROCEDURE [dbo].[sp_CountProductsWithCategoryAndCompany]
	 @categoryId int,
	 @companyId int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
	SELECT COUNT(*)
	FROM	
		Products prd
	JOIN CategoryDataProductData cdpd ON prd.Id = cdpd.ProductsId
	JOIN Categories ctg ON cdpd.CategoriesId = ctg.Id
	WHERE ctg.Id=@categoryId AND prd.CompanyId=@companyId AND prd.ProductState=1
	

END