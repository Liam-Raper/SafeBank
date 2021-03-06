﻿CREATE TABLE [Memberships].[UserDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Username] NVARCHAR(MAX) NOT NULL, 
    [Email] NVARCHAR(254) NOT NULL, 
	[Comment] NVARCHAR(MAX) NULL,
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user details table',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserDetails',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The username a user will use to login with',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserDetails',
    @level2type = N'COLUMN',
    @level2name = N'Username'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The email address of a user',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserDetails',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A comment that will be shown to a user when they log in',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserDetails',
    @level2type = N'COLUMN',
    @level2name = N'Comment'
GO