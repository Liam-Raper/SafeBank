using System.Collections.Generic;

namespace Data.Standard.Interfaces
{
    public interface ICreater<TId>
    {
        TId AddSingle();
        IEnumerable<IIdentify<TId>> AddMany();
    }
}