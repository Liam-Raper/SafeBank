using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Bank.Interfaces;
using Data.DatabaseModel;

namespace Data.Bank.Classes
{
    public class BranchTable : IBranchTable<int,BrancheDetail>
    {

        private readonly DbSet<BrancheDetail> _table;

        public BranchTable(DbSet<BrancheDetail> table)
        {
            _table = table;
        }

        public IQueryable<BrancheDetail> GetAll()
        {
            return _table;
        }

        public IQueryable<BrancheDetail> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public BrancheDetail GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(BrancheDetail set)
        {
            var added = _table.Add(set);
            return added.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<BrancheDetail> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(BrancheDetail set)
        {
            var allRecords = GetAll().ToArray();
            return allRecords.Select(all => all.Id).Select(singleId => UpdateSingle(singleId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, BrancheDetail set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, BrancheDetail set)
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