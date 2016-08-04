using Data.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Employees.Interfaces
{
    public interface IEmployeeTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>
    {

    }
}
