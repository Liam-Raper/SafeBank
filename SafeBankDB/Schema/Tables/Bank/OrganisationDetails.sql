CREATE TABLE [Bank].[OrganisationDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Code] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the organisation record',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'OrganisationDetails',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the organisation',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'OrganisationDetails',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The code of the organisation',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'OrganisationDetails',
    @level2type = N'COLUMN',
    @level2name = N'Code'