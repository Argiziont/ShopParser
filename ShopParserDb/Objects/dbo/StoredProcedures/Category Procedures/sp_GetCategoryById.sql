CREATE PROCEDURE [dbo].[sp_GetCategoryById]
	@categoryId int
AS
BEGIN	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *FROM dbo.Categories c WHERE	c.Id=@categoryId			
END
