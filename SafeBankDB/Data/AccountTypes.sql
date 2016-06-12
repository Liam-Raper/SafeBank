IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Accounts' AND TABLE_NAME = 'AccountTypes'))
BEGIN
	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccountTypes] WHERE [Name] = 'Current Account'))
		INSERT INTO [Accounts].[AccountTypes] ([Name]) VALUES ('Current Account')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccountTypes] WHERE [Name] = 'Cash Account'))
		INSERT INTO [Accounts].[AccountTypes] ([Name]) VALUES ('Cash Account')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccountTypes] WHERE [Name] = 'Savings Account'))
		INSERT INTO [Accounts].[AccountTypes] ([Name]) VALUES ('Savings Account')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccountTypes] WHERE [Name] = 'High Earners'))
		INSERT INTO [Accounts].[AccountTypes] ([Name]) VALUES ('High Earners')

	IF(NOT EXISTS(SELECT * FROM [Accounts].[AccountTypes] WHERE [Name] = 'ISA'))
		INSERT INTO [Accounts].[AccountTypes] ([Name]) VALUES ('ISA')

END
