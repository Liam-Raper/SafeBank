using Data.Standard.Interfaces;

namespace Data.Account.Interfaces
{
    public interface IAccountAccessLevelTable<TId, TDataSet> : IRequester<TId, TDataSet>
    {
    }
}
