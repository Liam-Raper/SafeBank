CREATE TABLE [Bank].[BankDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Code] INT NOT NULL, 
    [BrancheDetailsId] INT NOT NULL,
    CONSTRAINT [FK_Bank_Branche] FOREIGN KEY ([BrancheDetailsId]) REFERENCES [Bank].[BrancheDetails]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The Id of the bank',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BankDetails',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the bank',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BankDetails',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The code of the bank',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BankDetails',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the branche the bank belongs to',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BankDetails',
    @level2type = N'COLUMN',
    @level2name = N'BrancheDetailsId'