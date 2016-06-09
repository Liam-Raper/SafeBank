using System.Collections.Generic;

namespace Data.Standard.Interfaces
{
    public interface IDeleter<in TId>
    {
        bool DeleteAll();
        bool DeleteMany(IEnumerable<TId> ids);
        bool DeleteSingle(TId id);
    }
}