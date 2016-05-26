CREATE TABLE [Memberships].[UserAndPassword]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [LastChanged] DATETIME NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user and password record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserAndPassword',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The password of the user',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserAndPassword',
    @level2type = N'COLUMN',
    @level2name = N'Password'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time the password was last changed',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserAndPassword',
    @level2type = N'COLUMN',
    @level2name = N'LastChanged'