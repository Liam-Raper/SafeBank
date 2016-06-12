using Data.Standard.Interfaces;

namespace Data.Security.Membership.Interfaces
{
    public interface IRoleTable<TId, TDataSet> : IRequester<TId,TDataSet>, ICreater<TId, TDataSet>, IDeleter<TId>
    {
         
    }
}