CREATE PROCEDURE [dbo].[sp_UpdateProduct]
	 @productId		 INT,
	 @companyId      INT            = NULL,
	 @externalId     NVARCHAR (MAX) = NULL,
	 @title          NVARCHAR (MAX) = NULL,
	 @url            NVARCHAR (MAX) = NULL,
	 @syncDate       DATETIME2 (7)  = '12:12:12.1234567',
	 @expirationDate DATETIME2 (7)  = '12:12:12.1234567',
	 @productState   INT            = 0,
	 @description    NVARCHAR (MAX) = NULL,
	 @price          NVARCHAR (MAX) = NULL,
	 @keyWords       NVARCHAR (MAX) = NULL,
	 @jsonData       NVARCHAR (MAX) = NULL,
	 @jsonDataSchema NVARCHAR (MAX) = NULL


AS
BEGIN
	UPDATE Products

	SET Products.CompanyId			=	@companyId,
		Products.ExternalId			=	@externalId,
		Products.Title				=	@title,
		Products.Url				=	@url,
		Products.SyncDate			=	@syncDate,
		Products.ExpirationDate		=	@expirationDate,
		Products.ProductState		=	@productState,
		Products.Description		=	@description,
		Products.Price				=	@price,
		Products.KeyWords			=	@keyWords,
		Products.JsonData			=	@jsonData,
		Products.JsonDataSchema		=	@jsonDataSchema


	WHERE Id = @productId;
END