CREATE TABLE [Employee].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
	[EmployeeDetailsId] INT NOT NULL, 
    CONSTRAINT [FK_Employee_UserId] FOREIGN KEY ([UserId]) REFERENCES [Memberships].[User]([Id]),
    CONSTRAINT [FK_Employee_EmployeeDetails] FOREIGN KEY ([EmployeeDetailsId]) REFERENCES [Employee].[EmployeeDetails]([Id]),
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the employee record',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'Employee',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user the employee login in with',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'Employee',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the employee details record',
    @level0type = N'SCHEMA',
    @level0name = N'Employee',
    @level1type = N'TABLE',
    @level1name = N'Employee',
    @level2type = N'COLUMN',
    @level2name = N'EmployeeDetailsId'