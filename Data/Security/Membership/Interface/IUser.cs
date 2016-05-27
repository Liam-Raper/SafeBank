using Data.Standard.Interfaces;

namespace Data.Security.Membership.Interface
{
    public interface IUser<TId> : IRequester<TId>, ICreater, IUpdater<TId>, IDeleter<TId>
    {
         
    }
}