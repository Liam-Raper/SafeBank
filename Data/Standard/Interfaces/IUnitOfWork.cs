﻿using Data.DatabaseModel;
using Data.Security.Membership.Interface;

namespace Data.Standard.Interfaces
{
    public interface IUnitOfWork
    {
        IUserTables<int,User> User { get; }
        ISecurityQuestionTable<int,SecurityQuestion> SecurityQuestion { get; }
        IRoleTable<int, Role> Role { get; }
        void Commit();
    }
}