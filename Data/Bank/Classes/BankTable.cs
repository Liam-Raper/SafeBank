using Data.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Bank.Interfaces;

namespace Data.Bank.Classes
{
    class BankTable : IBankTable<int, BankDetail>
    {

        private readonly DbSet<BankDetail> _table;

        public BankTable(DbSet<BankDetail> table)
        {
            _table = table;
        }

        public IEnumerable<int> AddMany(IEnumerable<BankDetail> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public int AddSingle(BankDetail set)
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

        public IQueryable<BankDetail> GetAll()
        {
            return _table;
        }

        public IQueryable<BankDetail> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public BankDetail GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public bool UpdateAll(BankDetail set)
        {
            var allRecords = GetAll().ToArray();
            return allRecords.Select(all => all.Id).Select(singleId => UpdateSingle(singleId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, BankDetail set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, BankDetail set)
        {
            try
            {
                var record = GetSingle(id);
                record.Name = set.Name;
                record.Code = set.Code;
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
