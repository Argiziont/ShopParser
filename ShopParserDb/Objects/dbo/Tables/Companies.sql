CREATE TABLE [dbo].[Companies] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [SourceId]       INT            NULL,
    [ExternalId]     NVARCHAR (MAX) NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [Url]            NVARCHAR (MAX) NULL,
    [SyncDate]       DATETIME2 (7)  NOT NULL,
    [JsonData]       NVARCHAR (MAX) NULL,
    [JsonDataSchema] NVARCHAR (MAX) NULL,
    [CompanyState]   INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Companies_Sources_SourceId] FOREIGN KEY ([SourceId]) REFERENCES [dbo].[Sources] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Companies_SourceId]
    ON [dbo].[Companies]([SourceId] ASC);

