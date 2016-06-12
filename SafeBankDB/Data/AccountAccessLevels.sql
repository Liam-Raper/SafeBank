IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Accounts' AND TABLE_NAME = 'AccessLevel'))
BEGIN
	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccessLevel] WHERE [Name] = 'Owner'))
		INSERT INTO [Accounts].[AccessLevel] ([Name]) VALUES ('Owner')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccessLevel] WHERE [Name] = 'Manager'))
		INSERT INTO [Accounts].[AccessLevel] ([Name]) VALUES ('Manager')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccessLevel] WHERE [Name] = 'View'))
		INSERT INTO [Accounts].[AccessLevel] ([Name]) VALUES ('View')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccessLevel] WHERE [Name] = 'Audit'))
		INSERT INTO [Accounts].[AccessLevel] ([Name]) VALUES ('Audit')
END
