CREATE TABLE [Customer].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
	[CustomerDetailsId] INT NOT NULL, 
    CONSTRAINT [FK_Customer_UserId] FOREIGN KEY ([UserId]) REFERENCES [Memberships].[User]([Id]),
    CONSTRAINT [FK_Customer_EmployeeDetails] FOREIGN KEY ([CustomerDetailsId]) REFERENCES [Customer].[CustomerDetails]([Id]),
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the customer record',
    @level0type = N'SCHEMA',
    @level0name = N'Customer',
    @level1type = N'TABLE',
    @level1name = N'Customer',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the user the customer login in with',
    @level0type = N'SCHEMA',
    @level0name = N'Customer',
    @level1type = N'TABLE',
    @level1name = N'Customer',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The id of the customer details record',
    @level0type = N'SCHEMA',
    @level0name = N'Customer',
    @level1type = N'TABLE',
    @level1name = N'Customer',
    @level2type = N'COLUMN',
    @level2name = N'CustomerDetailsId'