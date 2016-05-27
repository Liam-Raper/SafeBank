using System.Collections.Generic;

namespace Data.Standard.Interfaces
{
    public interface IUpdater<TId>
    {
        bool UpdateAll();
        bool UpdateMany(IEnumerable<IIdentify<TId>> ids);
        bool UpdateSingle(IIdentify<TId> id);
    }
}