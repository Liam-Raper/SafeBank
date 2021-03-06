﻿using Data.Standard.Interfaces;

namespace Data.Security.Membership.Interfaces
{
    public interface IUserTables<TId,TDataSet> : IRequester<TId, TDataSet>, ICreater<TId, TDataSet>, IUpdater<TId, TDataSet>, IDeleter<TId>, IValidate<TDataSet>
    {
         
    }
}