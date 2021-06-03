CREATE PROCEDURE [dbo].[sp_GetCategoriesByProductId]
	@productId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT c.* 
	FROM dbo.Categories c 
	JOIN CategoryDataProductData cpd ON c.Id=cpd.CategoriesId 
	WHERE	ProductsId=@productId
END