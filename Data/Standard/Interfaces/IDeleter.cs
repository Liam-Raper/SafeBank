using System.Collections.Generic;

namespace Data.Standard.Interfaces
{
    public interface IDeleter<TId>
    {
        bool DeleteAll();
        bool DeleteMany(IEnumerable<IIdentify<TId>> ids);
        bool DeleteSingle(IIdentify<TId> id);
    }
}