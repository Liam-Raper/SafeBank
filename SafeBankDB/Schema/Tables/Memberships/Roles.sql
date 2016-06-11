CREATE TABLE [Memberships].[Roles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL, 
    [SystemDefault] BIT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the role',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'Roles',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the role',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'Roles',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Is this record a system default',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'Roles',
    @level2type = N'COLUMN',
    @level2name = N'SystemDefault'