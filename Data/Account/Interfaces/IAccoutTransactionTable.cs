using Data.Standard.Interfaces;

namespace Data.Account.Interfaces
{
    public interface IAccoutTransactionTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>
    {
    }
}
