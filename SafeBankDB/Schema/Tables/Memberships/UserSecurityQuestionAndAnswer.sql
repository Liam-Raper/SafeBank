CREATE TABLE [Memberships].[UserSecurityQuestionAndAnswer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuestionId] INT NOT NULL, 
    [Answer] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_UserSecurityQuestionAndAnswer_SecurityQuestion] FOREIGN KEY ([QuestionId]) REFERENCES [Memberships].[SecurityQuestion]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user security question and answer record',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserSecurityQuestionAndAnswer',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the question the user is using',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserSecurityQuestionAndAnswer',
    @level2type = N'COLUMN',
    @level2name = N'QuestionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The answer to the question',
    @level0type = N'SCHEMA',
    @level0name = N'Memberships',
    @level1type = N'TABLE',
    @level1name = N'UserSecurityQuestionAndAnswer',
    @level2type = N'COLUMN',
    @level2name = N'Answer'