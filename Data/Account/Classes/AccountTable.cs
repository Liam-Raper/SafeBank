using System.Collections.Generic;
using System.Linq;
using Data.Account.Interfaces;

namespace Data.Account.Classes
{
    public class AccountTable : IAccountTable<int,DatabaseModel.Account>
    {
        public IQueryable<DatabaseModel.Account> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<DatabaseModel.Account> GetMany(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public DatabaseModel.Account GetSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public int AddSingle(DatabaseModel.Account set)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> AddMany(IEnumerable<DatabaseModel.Account> set)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateAll(DatabaseModel.Account set)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateMany(IEnumerable<int> ids, DatabaseModel.Account set)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateSingle(int id, DatabaseModel.Account set)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteMany(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteSingle(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}