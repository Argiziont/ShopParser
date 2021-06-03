CREATE PROCEDURE [dbo].[sp_AddCompany]
        @sourceId INT=NULL ,
        @externalId NVARCHAR (MAX)= NULL,
        @name NVARCHAR (MAX)= NULL,
        @url NVARCHAR (MAX)= NULL,
        @syncDate DATETIME2 (7) ='12:10:16.1234567',
        @jsonData NVARCHAR (MAX)= NULL,
        @jsonDataSchema NVARCHAR (MAX)= NULL,
        @companyState  INT=0
AS
BEGIN   
    INSERT INTO dbo.Companies
    (
        --Id - column value is auto-generated
        SourceId,
        ExternalId,
        Name,
        Url,
        SyncDate,
        JsonData,
        JsonDataSchema,
        CompanyState
    )
    VALUES
    (
        -- Id - int
        @sourceId, -- SourceId - int
        @externalId, -- ExternalId - nvarchar
        @name, -- Name - nvarchar
        @url, -- Url - nvarchar
        @syncDate, -- SyncDate - datetime2
        @jsonData, -- JsonData - nvarchar
        @jsonDataSchema, -- JsonDataSchema - nvarchar
        @companyState -- CompanyState - int
    )

END