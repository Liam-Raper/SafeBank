using Data.Standard.Interfaces;

namespace Data.Security.Membership.Interfaces
{
    public interface ISecurityQuestionTable<TId, TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>
    {
         
    }
}