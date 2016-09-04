using Data.Standard.Interfaces;

namespace Data.Account.Interfaces
{
    public interface IAccountTypeTable<TId, TDataSet> : IRequester<TId, TDataSet>
    {
        
    }
}