CREATE PROCEDURE [dbo].[sp_AddCompany]
        @SourceId INT=NULL ,
        @ExternalId NVARCHAR (MAX)= NULL,
        @Name NVARCHAR (MAX)= NULL,
        @Url NVARCHAR (MAX)= NULL,
        @SyncDate DATETIME2 (7) ='12:10:16.1234567',
        @JsonData NVARCHAR (MAX)= NULL,
        @JsonDataSchema NVARCHAR (MAX)= NULL,
        @CompanyState  INT=0
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
        @SourceId, -- SourceId - int
        @ExternalId, -- ExternalId - nvarchar
        @Name, -- Name - nvarchar
        @Url, -- Url - nvarchar
        @SyncDate, -- SyncDate - datetime2
        @JsonData, -- JsonData - nvarchar
        @JsonDataSchema, -- JsonDataSchema - nvarchar
        @CompanyState -- CompanyState - int
    )

END