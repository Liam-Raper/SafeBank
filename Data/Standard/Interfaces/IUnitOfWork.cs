using Data.Security.Membership.Interface;
using Data.Standard.Classed;

namespace Data.Standard.Interfaces
{
    public interface IUnitOfWork
    {
        IUser<IntId> User { get; }
        void Commit();
    }
}