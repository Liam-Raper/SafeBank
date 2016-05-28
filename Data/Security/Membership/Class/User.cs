using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Data.DatabaseModel;
using Data.Security.Membership.Interface;
using Data.Standard.Classed;
using Data.Standard.Interfaces;

namespace Data.Security.Membership.Class
{
    public class User : IUser<IntId>
    {
        private SafeBankDBMembershipEntity _database;

        public User()
        {
            _database = new SafeBankDBMembershipEntity();
        }

        public IQueryable GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable GetMany(IEnumerable<IIdentify<IntId>> ids)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable GetSingle(IIdentify<IntId> id)
        {
            throw new System.NotImplementedException();
        }

        public IntId AddSingle()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IIdentify<IntId>> AddMany()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateAll()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateMany(IEnumerable<IIdentify<IntId>> ids)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateSingle(IIdentify<IntId> id)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteMany(IEnumerable<IIdentify<IntId>> ids)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteSingle(IIdentify<IntId> id)
        {
            throw new System.NotImplementedException();
        }

    }
}