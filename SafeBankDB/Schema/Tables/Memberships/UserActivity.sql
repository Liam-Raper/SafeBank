CREATE TABLE [Memberships].[UserActivity]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [IsApproved] BIT NOT NULL,
    [IsLockedOut] BIT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [LastLoggedInDate] DATETIME NOT NULL, 
    [LastActiveDate] DATETIME NOT NULL, 
    [LastLockedOutDate] DATETIME NULL, 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the users activity record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Is the user allowed to login',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'IsApproved'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Is the user locked',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'IsLockedOut'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time of the users creation',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'CreatedDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time the user last logged in',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'LastLoggedInDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time the user last did an action',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'LastActiveDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date and time the user last got locked out',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserActivity',
    @level2type = N'COLUMN',
    @level2name = N'LastLockedOutDate'