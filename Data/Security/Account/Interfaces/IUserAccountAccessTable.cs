using Data.Standard.Interfaces;

namespace Data.Security.Account.Interfaces
{
    public interface IUserAccountAccessTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>
    {
         
    }
}