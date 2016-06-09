using System.Collections.Generic;

namespace Data.Standard.Interfaces
{
    public interface ICreater<out TId, in TDataSet>
    {
        TId AddSingle(TDataSet set);
        IEnumerable<TId> AddMany(IEnumerable<TDataSet> set);
    }
}