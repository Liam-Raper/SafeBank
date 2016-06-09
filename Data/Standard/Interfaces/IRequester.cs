using System.Collections.Generic;
using System.Linq;

namespace Data.Standard.Interfaces
{
    public interface IRequester<in TId, out TDataSet>
    {
        IQueryable<TDataSet> GetAll();
        IQueryable<TDataSet> GetMany(IEnumerable<TId> ids);
        TDataSet GetSingle(TId id);
    }
}