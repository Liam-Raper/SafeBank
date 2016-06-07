using Data.DatabaseModel;
using Data.Security.Membership.Class;
using Data.Security.Membership.Interface;
using Data.Standard.Interfaces;

namespace Data.Standard.Classed
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SafeBankDBMembershipEntity _database;
        
        public UnitOfWork()
        {
            _database = new SafeBankDBMembershipEntity();
            User = new User(_database);
        }

        public IUser<IntId> User { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}