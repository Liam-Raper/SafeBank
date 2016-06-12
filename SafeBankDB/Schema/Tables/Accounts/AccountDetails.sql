CREATE TABLE [Accounts].[AccountDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[BankId] INT NOT NULL, 
    [AccountNumber] INT NOT NULL, 
	[AccountName] NVARCHAR(30),
    [Balance] DECIMAL(19, 4) NOT NULL, 
    [Overdraft] DECIMAL(19, 4) NOT NULL, 
    CONSTRAINT [FK_AccountDetails_BankId] FOREIGN KEY ([BankId]) REFERENCES [Bank].[BankDetails]([Id]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account details record',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountDetails',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the bank the account belongs to',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountDetails',
    @level2type = N'COLUMN',
    @level2name = N'BankId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The account number for the account',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountDetails',
    @level2type = N'COLUMN',
    @level2name = N'AccountNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The current balance of the account',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountDetails',
    @level2type = N'COLUMN',
    @level2name = N'Balance'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The overdraft allowed on the account',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountDetails',
    @level2type = N'COLUMN',
    @level2name = N'Overdraft'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the account',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountDetails',
    @level2type = N'COLUMN',
    @level2name = N'AccountName'