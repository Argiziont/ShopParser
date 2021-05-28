CREATE PROCEDURE [dbo].[sp_GetNestedCategoryByParentIdAndCompanyId]
	-- Add the parameters for the stored procedure here
	@categoryId int,
	@companyId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   
	--SELECT * FROM Categories ctg WHERE ctg.SupCategoryDataId = @categoryId && ctg

	SELECT Distinct ctg.*
	FROM	
		Categories ctg
	JOIN CategoryDataProductData cdpd ON ctg.Id = cdpd.CategoriesId
	JOIN Products prd ON cdpd.ProductsId = prd.Id
	WHERE 
		ctg.Id=@categoryId  AND prd.CompanyId=@companyId

END