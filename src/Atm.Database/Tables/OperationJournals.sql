CREATE TABLE [dbo].[OperationJournals] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Date]          DATETIME        NOT NULL,
    [OperationCode] INT             NOT NULL,
    [Amount]        DECIMAL (18, 2) NOT NULL,
    [AtmCard_Id]    INT             NULL,
    CONSTRAINT [PK_dbo.OperationJournals] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.OperationJournals_dbo.AtmCards_AtmCard_Id] FOREIGN KEY ([AtmCard_Id]) REFERENCES [dbo].[AtmCards] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AtmCard_Id]
    ON [dbo].[OperationJournals]([AtmCard_Id] ASC);

