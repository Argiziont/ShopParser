
CREATE PROCEDURE [dbo].[sp_GetAllProducts]
AS
BEGIN
	SELECT * FROM Products prd WHERE prd.ProductState=1
END