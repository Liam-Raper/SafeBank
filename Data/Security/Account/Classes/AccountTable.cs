using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Security.Account.Interfaces;

namespace Data.Security.Account.Classes
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
            var account = _table.Add(set);
            return account.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<DatabaseModel.Account> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(DatabaseModel.Account set)
        {
            var accounts = GetAll().ToArray();
            return accounts.Select(account => account.Id).Select(accountId => UpdateSingle(accountId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, DatabaseModel.Account set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, DatabaseModel.Account set)
        {
            try
            {
                var account = GetSingle(id);
                account.AccountDetail.AccountName = set.AccountDetail.AccountName;
                account.AccountDetail.Balance = set.AccountDetail.Balance;
                account.AccountDetail.Overdraft = set.AccountDetail.Overdraft;
                return true;
            }
            catch (Exception e)
            {
                //TODO: Log the exception
                return false;
            }
        }

        public bool DeleteAll()
        {
            try
            {
                _table.RemoveRange(GetAll());
                return true;
            }
            catch (Exception e)
            {
                //TODO: log exception
                return false;
            }
        }

        public bool DeleteMany(IEnumerable<int> ids)
        {
            try
            {
                _table.RemoveRange(GetMany(ids));
                return true;
            }
            catch (Exception e)
            {
                //TODO: log exception
                return false;
            }
        }

        public bool DeleteSingle(int id)
        {
            try
            {
                _table.Remove(GetSingle(id));
                return true;
            }
            catch (Exception e)
            {
                //TODO: log exception
                return false;
            }
        }
    }
}