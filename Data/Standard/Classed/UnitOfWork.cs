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
        }

        public IUserTables<int, User> User { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}