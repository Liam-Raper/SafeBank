CREATE TABLE [Accounts].[AccountTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL, 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the account type',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountTypes',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the account type',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccountTypes',
    @level2type = N'COLUMN',
    @level2name = N'Name'