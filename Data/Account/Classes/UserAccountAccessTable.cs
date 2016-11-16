using Data.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Data.DatabaseModel;
using System.Linq;

namespace Data.Account.Classes
{
    public class UserAccountAccessTable : IUserAccountAccessTable<int, DatabaseModel.UserAccountAccess>
    {

        private readonly DbSet<DatabaseModel.UserAccountAccess> _table;

        public UserAccountAccessTable(DbSet<DatabaseModel.UserAccountAccess> table)
        {
            _table = table;
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

        public IQueryable<UserAccountAccess> GetAll()
        {
            return _table;
        }

        public IQueryable<UserAccountAccess> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public UserAccountAccess GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }
    }
}
