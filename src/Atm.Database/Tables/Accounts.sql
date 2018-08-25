CREATE TABLE [dbo].[Accounts] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [Amount]  DECIMAL (18, 2) NOT NULL,
    [User_Id] INT             NULL,
    CONSTRAINT [PK_dbo.Accounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Accounts_dbo.Users_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[Accounts]([User_Id] ASC);

