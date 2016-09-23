using System;
using System.Collections.Generic;
using System.Linq;
using Data.Account.Interfaces;
using Data.DatabaseModel;
using Data.Standard.Interfaces;
using System.Data.Entity;

namespace Data.Account.Classes
{
    public class AccountAccessLevelTable : IAccountAccessLevelTable<int, AccessLevel>
    {
        
        private readonly DbSet<AccessLevel> _table;

        public AccountAccessLevelTable(DbSet<AccessLevel> table)
        {
            _table = table;
        }
        
        public IQueryable<AccessLevel> GetAll()
        {
            return _table;
        }

        public IQueryable<AccessLevel> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public AccessLevel GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }
    }
}
