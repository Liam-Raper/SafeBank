using Data.DatabaseModel;
using Data.Security.Membership.Interface;
using Data.Standard.Classed;

namespace Data.Standard.Interfaces
{
    public interface IUnitOfWork
    {
        IUserTables<int,User> User { get; }
        void Commit();
    }
}