CREATE TABLE [Employee].[EmployeeLocations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeId] INT NOT NULL, 
    [BankId] INT NOT NULL, 
    CONSTRAINT [FK_EmployeeLocations_BankId] FOREIGN KEY ([BankId]) REFERENCES [Bank].[BankDetails]([Id]),  
    CONSTRAINT [FK_EmployeeLocations_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee].[Employee]([Id]),
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the employee location record',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeLocations',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the employee',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeLocations',
    @level2type = N'COLUMN',
    @level2name = N'EmployeeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the bank the employee can access',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'EmployeeLocations',
    @level2type = N'COLUMN',
    @level2name = N'BankId'