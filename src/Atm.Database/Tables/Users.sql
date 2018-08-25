CREATE TABLE [dbo].[Users] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [UserName]          NVARCHAR (MAX) NULL,
    [LoginAttemptCount] INT            NOT NULL,
    [IsLocked]          BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

