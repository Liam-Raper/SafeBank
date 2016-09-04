using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Account.Interfaces;
using Data.DatabaseModel;

namespace Data.Account.Classes
{
    public class AccountTypeTable : IAccountTypeTable<int,AccountType>
    {

        private readonly DbSet<AccountType> _table;

        public AccountTypeTable(DbSet<AccountType> table)
        {
            _table = table;
        }

        public IQueryable<AccountType> GetAll()
        {
            return _table;
        }

        public IQueryable<AccountType> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public AccountType GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }
    }
}