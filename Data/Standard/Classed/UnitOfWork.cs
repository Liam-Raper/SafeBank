using Data.DatabaseModel;
using Data.Security.Membership.Class;
using Data.Security.Membership.Interface;
using Data.Standard.Interfaces;

namespace Data.Standard.Classed
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Entities _database;
        
        public UnitOfWork()
        {
            _database = new Entities();
            User = new UserTables(_database.Users);
            SecurityQuestion = new SecurityQuestionTables(_database.SecurityQuestions);
        }

        public IUserTables<int, User> User { get; }
        public ISecurityQuestionTable<int, SecurityQuestion> SecurityQuestion { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}