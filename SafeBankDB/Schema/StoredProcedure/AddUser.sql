CREATE PROCEDURE [Memberships].[AddUser]
	@Username NVARCHAR(MAX),
	@Email NVARCHAR(254),
	@Password NVARCHAR(MAX),
	@QuestionId INT,
	@Answer NVARCHAR(MAX)
AS
	DECLARE @Inserted DATETIME
	SET @Inserted = GETDATE()

	DECLARE @SecurityQuestionAnswerId INT
	INSERT INTO [Memberships].[UserSecurityQuestionAndAnswer]
	([QuestionId],[Answer])
	VALUES
	(@QuestionId,@Answer)
	SET @SecurityQuestionAnswerId = SCOPE_IDENTITY()

	DECLARE @UserAndPasswordId INT
	INSERT INTO [Memberships].[UserAndPassword]
    ([Password],[LastChanged])
    VALUES
	(@Password,@Inserted)
	SET @UserAndPasswordId = SCOPE_IDENTITY()
	
	DECLARE @UserActivityId INT
	INSERT INTO [Memberships].[UserActivity]
    ([IsApproved],[IsLockedOut],[CreatedDate],[LastLoggedInDate],[LastActiveDate],[LastLockedOutDate])
    VALUES
	(1,0,@Inserted,NULL,@Inserted,NULL)
	SET @UserActivityId = SCOPE_IDENTITY()

	DECLARE @UserDetailsId INT
	INSERT INTO [Memberships].[UserDetails]
	([Username],[Email],[Comment])
	VALUES
    (@Username,@Email,NULL)
	SET @UserDetailsId = SCOPE_IDENTITY()

	INSERT INTO [Memberships].[User]
    ([SecurityQuestionAnswerId],[UserAndPasswordId],[UserActivityId],[UserDetailsId])
    VALUES
	(@SecurityQuestionAnswerId,@UserAndPasswordId,@UserActivityId,@UserDetailsId)
RETURN 0
