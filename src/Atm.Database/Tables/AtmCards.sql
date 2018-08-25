CREATE TABLE [dbo].[AtmCards] (
    [Id]     INT            NOT NULL,
    [Number] NVARCHAR (MAX) NULL,
    [Pin]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AtmCards] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AtmCards_dbo.Users_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[AtmCards]([Id] ASC);

