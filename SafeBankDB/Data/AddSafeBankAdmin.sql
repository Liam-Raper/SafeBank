IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Memberships' AND TABLE_NAME = 'User'))
BEGIN
	IF (NOT EXISTS(SELECT * FROM [Memberships].[UserDetails] WHERE [Username] = 'SafeBankAdmin'))
	BEGIN
		DECLARE @AdminRoleId INT
		DECLARE @UserDetailsId INT
		DECLARE @UserActivityId INT
		DECLARE @UserAndPasswordId INT
		DECLARE @UserSecurityQuestionAndAnswerId INT
		DECLARE @SecurityQuestionId INT

		INSERT INTO [Memberships].[SecurityQuestion]
		([Text],[SystemDefault])
		VALUES
		('What is the meaning of this?',0)
		SET @SecurityQuestionId = @@IDENTITY

		INSERT INTO [Memberships].[UserSecurityQuestionAndAnswer]
		([QuestionId],[Answer])
		VALUES
		(@SecurityQuestionId,'')
		SET @UserSecurityQuestionAndAnswerId = @@IDENTITY

		INSERT INTO [Memberships].[UserAndPassword]
		([Password],[LastChanged])
		VALUES
		('$2a$05$Q623kKAviQlSJjAJMxqUMeSTOvt.nTAOM2.XDQ4kjuP/AqYqqUhXS',GETDATE())
		SET @UserAndPasswordId = @@IDENTITY

		INSERT INTO [Memberships].[UserActivity]
		([IsApproved],[IsLockedOut],[CreatedDate],[LastLoggedInDate],[LastActiveDate],[LastLockedOutDate])
		VALUES
		(1,0,GETDATE(),NULL,GETDATE(),NULL)
		SET @UserActivityId = @@IDENTITY

		INSERT INTO [Memberships].[UserDetails]
		([Username],[Email],[Comment])
		VALUES
		('SafeBankAdmin','admin@safebank.com','')
		SET @UserDetailsId = @@IDENTITY

		SELECT @AdminRoleId = [Id] FROM [Memberships].[Roles] WHERE [Name] = 'Administrator'

		INSERT INTO [Memberships].[User]
		([SecurityQuestionAnswerId],[UserAndPasswordId],[UserActivityId],[UserDetailsId],[RoleId])
		VALUES
		(@UserSecurityQuestionAndAnswerId,@UserAndPasswordId,@UserActivityId,@UserDetailsId,@AdminRoleId)
	END
END