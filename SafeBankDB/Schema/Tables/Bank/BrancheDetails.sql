CREATE TABLE [Bank].[BrancheDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Code] INT NOT NULL, 
    [OrganisationDetailsId] INT NOT NULL,
    CONSTRAINT [FK_Branche_Organisation] FOREIGN KEY ([OrganisationDetailsId]) REFERENCES [Bank].[OrganisationDetails]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the branche record',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BrancheDetails',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the branche',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BrancheDetails',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The code for the branche',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BrancheDetails',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the organisation the branche is under',
    @level0type = N'SCHEMA',
    @level0name = N'Bank',
    @level1type = N'TABLE',
    @level1name = N'BrancheDetails',
    @level2type = N'COLUMN',
    @level2name = N'OrganisationDetailsId'