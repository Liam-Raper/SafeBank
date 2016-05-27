using System.Collections.Generic;
using System.Linq;

namespace Data.Standard.Interfaces
{
    public interface IRequester<TId>
    {
        IQueryable GetAll();
        IQueryable GetMany(IEnumerable<IIdentify<TId>> ids);
        IQueryable GetSingle(IIdentify<TId> id);
    }
}