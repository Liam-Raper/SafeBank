CREATE USER [MembershipManager] FOR LOGIN [SafeBankMembershipManager] WITH DEFAULT_SCHEMA=[Memberships]
GO
GRANT CONNECT TO [MembershipManager]
GO
GRANT EXECUTE ON [Memberships].[AddUser] TO [MembershipManager]
GO
GRANT SELECT ON [Memberships].[UserList] TO [MembershipManager]
GO