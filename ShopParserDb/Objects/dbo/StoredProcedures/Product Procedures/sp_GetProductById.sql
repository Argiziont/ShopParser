CREATE PROCEDURE [dbo].[sp_GetProductById]
	@productId int
AS
BEGIN	
	SELECT * FROM	Products WHERE	dbo.Products.Id=@productId
END
