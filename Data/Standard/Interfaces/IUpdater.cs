using System.Collections.Generic;

namespace Data.Standard.Interfaces
{
    public interface IUpdater<in TId, in TDataSet>
    {
        bool UpdateAll(TDataSet set);
        bool UpdateMany(IEnumerable<TId> ids, TDataSet set);
        bool UpdateSingle(TId id, TDataSet set);
    }
}