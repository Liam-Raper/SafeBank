IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Memberships' AND TABLE_NAME = 'Roles'))
BEGIN
	IF(NOT EXISTS(SELECT * FROM [Memberships].[Roles] WHERE [Name] = 'Administrator'))
		INSERT INTO [Memberships].[Roles] ([Name],[SystemDefault]) VALUES ('Administrator',1)

	IF(NOT EXISTS(SELECT * FROM [Memberships].[Roles] WHERE [Name] = 'Bank Manager'))
		INSERT INTO [Memberships].[Roles] ([Name],[SystemDefault]) VALUES ('Bank Manager',1)

	IF(NOT EXISTS(SELECT * FROM [Memberships].[Roles] WHERE [Name] = 'Banker'))
		INSERT INTO [Memberships].[Roles] ([Name],[SystemDefault]) VALUES ('Banker',1)

	IF(NOT EXISTS(SELECT * FROM [Memberships].[Roles] WHERE [Name] = 'Customer'))
		INSERT INTO [Memberships].[Roles] ([Name],[SystemDefault]) VALUES ('Customer',1)

END
