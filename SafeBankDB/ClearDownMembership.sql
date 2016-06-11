DELETE FROM [Memberships].[User]
DELETE FROM [Memberships].[UserDetails]
DELETE FROM [Memberships].[UserActivity]
DELETE FROM [Memberships].[UserAndPassword]
DELETE FROM [Memberships].[UserSecurityQuestionAndAnswer]
DELETE FROM [Memberships].[SecurityQuestion]
DELETE FROM [Memberships].[Roles]
DBCC CHECKIDENT ('Memberships.User', RESEED, 0);
DBCC CHECKIDENT ('Memberships.UserDetails', RESEED, 0);
DBCC CHECKIDENT ('Memberships.UserActivity', RESEED, 0);
DBCC CHECKIDENT ('Memberships.UserAndPassword', RESEED, 0);
DBCC CHECKIDENT ('Memberships.UserSecurityQuestionAndAnswer', RESEED, 0);
DBCC CHECKIDENT ('Memberships.SecurityQuestion', RESEED, 0);
DBCC CHECKIDENT ('Memberships.Roles', RESEED, 0);