using System.Collections.Generic;
using System.Linq;
using Data.Account.Interfaces;
using System.Data.Entity;

namespace Data.Account.Classes
{
    public class AccountTable : IAccountTable<int,DatabaseModel.Account>
    {


        private readonly DbSet<DatabaseModel.Account> _table;

        public AccountTable(DbSet<DatabaseModel.Account> table)
        {
            _table = table;
        }


        public IQueryable<DatabaseModel.Account> GetAll()
        {
            return _table;
        }

        public IQueryable<DatabaseModel.Account> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public DatabaseModel.Account GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(DatabaseModel.Account set)
        {
            var added = _table.Add(set);
            return added.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<DatabaseModel.Account> set)
        {
            return set.Select(AddSingle).ToList();
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