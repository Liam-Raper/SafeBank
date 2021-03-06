﻿CREATE TABLE [Employee].[EmployeeDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Given name] NVARCHAR(50) NOT NULL, 
    [Family name] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(14) NOT NULL, 
    [Email] NVARCHAR(254) NOT NULL, 
    [Code] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the employee details record',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeDetails',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The employees first name',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeDetails',
    @level2type = N'COLUMN',
    @level2name = N'Given name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The employees surename',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeDetails',
    @level2type = N'COLUMN',
    @level2name = N'Family name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The employees phone number',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeDetails',
    @level2type = N'COLUMN',
    @level2name = N'Phone'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The employees email',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeDetails',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The employees code',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeDetails',
    @level2type = N'COLUMN',
    @level2name = N'Code'