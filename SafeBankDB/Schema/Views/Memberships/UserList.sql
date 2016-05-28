CREATE VIEW [Memberships].[UserList] AS
	SELECT
		[UserDetails].Id,
		[UserDetails].Username,
		[UserDetails].Email,
		[UserAndPassword].[Password],
		[UserSecurityQuestionAndAnswer].QuestionId,
		[UserSecurityQuestionAndAnswer].Answer
	FROM [Memberships].[User] [User]
	JOIN [Memberships].[UserDetails] [UserDetails] on [UserDetails].Id = [User].UserDetailsId
	JOIN [Memberships].[UserActivity] [UserActivity] on [UserActivity].Id = [User].UserActivityId
	JOIN [Memberships].[UserSecurityQuestionAndAnswer] [UserSecurityQuestionAndAnswer] on [UserSecurityQuestionAndAnswer].Id = [User].SecurityQuestionAnswerId
	JOIN [Memberships].[UserAndPassword] [UserAndPassword] on [UserAndPassword].Id = [User].UserAndPasswordId