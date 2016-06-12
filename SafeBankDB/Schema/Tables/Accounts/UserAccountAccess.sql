CREATE TABLE [Accounts].[UserAccountAccess]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [AccessLevelId] INT NOT NULL,
	CONSTRAINT [FK_AccountAccess_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts].[Account]([Id]),
	CONSTRAINT [FK_AccountAccess_UserId] FOREIGN KEY ([UserId]) REFERENCES [Memberships].[User]([Id]),
	CONSTRAINT [FK_AccountAccess_AccessLevelId] FOREIGN KEY ([AccessLevelId]) REFERENCES [Accounts].[AccessLevel]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user account access  record',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'UserAccountAccess',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account the user has access to',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'UserAccountAccess',
    @level2type = N'COLUMN',
    @level2name = N'AccountId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user the account allows access to',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'UserAccountAccess',
    @level2type = N'COLUMN',
    @level2name = N'UserId'