using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Security.Account.Interfaces;

namespace Data.Security.Account.Classes
{
    public class UserAccountAccessTable : IUserAccountAccessTable<int, DatabaseModel.UserAccountAccess>
    {

        private readonly DbSet<DatabaseModel.UserAccountAccess> _table;

        public UserAccountAccessTable(DbSet<DatabaseModel.UserAccountAccess> table)
        {
            _table = table;
        }

        public IQueryable<DatabaseModel.UserAccountAccess> GetAll()
        {
            return _table;
        }

        public IQueryable<DatabaseModel.UserAccountAccess> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public DatabaseModel.UserAccountAccess GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(DatabaseModel.UserAccountAccess set)
        {
            var subtable = _table.Add(set);
            return subtable.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<DatabaseModel.UserAccountAccess> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(DatabaseModel.UserAccountAccess set)
        {
            var subtable = GetAll().ToArray();
            return subtable.Select(subselection => subselection.Id).Select(subselectionId => UpdateSingle(subselectionId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, DatabaseModel.UserAccountAccess set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, DatabaseModel.UserAccountAccess set)
        {
            try
            {
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