using Data.Standard.Interfaces;

namespace Data.Employees.Interfaces
{
    public interface IEmployeeTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>
    {

    }
}
