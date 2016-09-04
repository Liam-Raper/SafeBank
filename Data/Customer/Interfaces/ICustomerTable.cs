using Data.Standard.Interfaces;

namespace Data.Customer.Interfaces
{
    public interface ICustomerTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>
    {
        
    }
}