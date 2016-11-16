using System;
using System.Collections.Generic;
using System.Linq;
using Data.Account.Interfaces;
using Data.DatabaseModel;
using System.Data.Entity;

namespace Data.Account.Classes
{
    public class AccoutTransactionTable : IAccoutTransactionTable<int, Transaction>
    {

        private readonly DbSet<Transaction> _table;

        public AccoutTransactionTable(DbSet<Transaction> table)
        {
            _table = table;
        }

        public IEnumerable<int> AddMany(IEnumerable<Transaction> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public int AddSingle(Transaction set)
        {
            var added = _table.Add(set);
            return added.Id;
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

        public IQueryable<Transaction> GetAll()
        {
            return _table;
        }

        public IQueryable<Transaction> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public Transaction GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public bool UpdateAll(Transaction set)
        {
            var allRecords = GetAll().ToArray();
            return allRecords.Select(all => all.Id).Select(singleId => UpdateSingle(singleId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, Transaction set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, Transaction set)
        {
            try
            {
                var record = GetSingle(id);
                record.Deposeted = set.Deposeted;
                record.Withdrawn = set.Withdrawn;
                return true;
            }
            catch (Exception e)
            {
                //TODO: Log the exception
                return false;
            }
        }
    }
}
