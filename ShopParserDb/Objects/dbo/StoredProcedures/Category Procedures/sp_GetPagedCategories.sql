
CREATE PROCEDURE [dbo].[sp_GetPagedCategories]
	-- Add the parameters for the stored procedure here
	  @page int,
      @rowsPerPage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Categories] ORDER BY Id  OFFSET @page*@rowsPerPage ROWS FETCH NEXT @rowsPerPage ROWS ONLY
END