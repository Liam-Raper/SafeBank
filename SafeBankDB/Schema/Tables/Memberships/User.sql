CREATE TABLE [Memberships].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SecurityQuestionAnswerId] INT NOT NULL, 
    [UserAndPasswordId] INT NOT NULL, 
    [UserActivityId] INT NOT NULL,
    [UserDetailsId] INT NOT NULL, 
    [RoleId] INT NOT NULL,
    CONSTRAINT [FK_User_UserSecurityQuestionAndAnswer] FOREIGN KEY ([SecurityQuestionAnswerId]) REFERENCES [Memberships].[UserSecurityQuestionAndAnswer]([Id]), 
    CONSTRAINT [FK_User_UserAndPassword] FOREIGN KEY ([UserAndPasswordId]) REFERENCES [Memberships].[UserAndPassword]([Id]), 
    CONSTRAINT [FK_User_UserActivity] FOREIGN KEY ([UserActivityId]) REFERENCES [Memberships].[UserActivity]([Id]),
    CONSTRAINT [FK_User_Details] FOREIGN KEY ([UserDetailsId]) REFERENCES [Memberships].[UserDetails]([Id]),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [Memberships].[Roles]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'This is the ID of the user',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the security question and answer record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = 'SecurityQuestionAnswerId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user and password record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = 'UserAndPasswordId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user activity record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'UserActivityId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user details record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'UserDetailsId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the users role',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'RoleId'