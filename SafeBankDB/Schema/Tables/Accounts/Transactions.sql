CREATE TABLE [Accounts].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountId] INT NOT NULL, 
    [Deposeted] DECIMAL(19, 4) NOT NULL, 
    [Withdrawn] DECIMAL(19, 4) NOT NULL,
	CONSTRAINT [FK_Transactions_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts].[Account]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the transaction record',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Transactions',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account the transaction belongs to',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Transactions',
    @level2type = N'COLUMN',
    @level2name = N'AccountId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The amount that is deposeted',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Transactions',
    @level2type = N'COLUMN',
    @level2name = N'Deposeted'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The amount that is withdrawn',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'Transactions',
    @level2type = N'COLUMN',
    @level2name = N'Withdrawn'