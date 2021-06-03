CREATE PROCEDURE [dbo].[sp_UpdateProductState]
	 @productId		 INT,
	 @productState   INT

AS
BEGIN
	UPDATE Products

	SET	Products.ProductState =	@productState

	WHERE Id = @productId;
END