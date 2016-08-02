using Data.Standard.Interfaces;

namespace Data.Bank.Interfaces
{
    public interface IBankTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>
    {
    }
}
