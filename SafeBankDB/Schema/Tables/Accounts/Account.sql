CREATE TABLE [Accounts].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[AccountTypeId] INT NOT NULL,
    [AccountDetailsId] INT NOT NULL,
    CONSTRAINT [FK_Account_AccountType] FOREIGN KEY ([AccountTypeId]) REFERENCES [Accounts].[AccountTypes]([Id]), 
    CONSTRAINT [FK_Account_AccountDetails] FOREIGN KEY ([AccountDetailsId]) REFERENCES [Accounts].[AccountDetails]([Id]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account type',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'AccountTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account details',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'AccountDetailsId'