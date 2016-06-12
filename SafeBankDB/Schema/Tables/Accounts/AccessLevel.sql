CREATE TABLE [Accounts].[AccessLevel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(30) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user access level record',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccessLevel',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the account access level',
    @level0type = N'SCHEMA',
    @level0name = N'Accounts',
    @level1type = N'TABLE',
    @level1name = N'AccessLevel',
    @level2type = N'COLUMN',
    @level2name = N'Name'