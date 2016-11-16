using Data.Standard.Interfaces;

namespace Data.Account.Interfaces
{
    public interface IUserAccountAccessTable<TId, TDataSet> : IRequester<TId,TDataSet>,IDeleter<TId>
    {
    }
}
