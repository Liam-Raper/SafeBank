using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DatabaseModel;
using Data.Security.Membership.Interface;

namespace Data.Security.Membership.Class
{
    class RoleTable : IRoleTable<int,Role>
    {

        private readonly DbSet<Role> _table;

        public RoleTable(DbSet<Role> table)
        {
            _table = table;
        }

        public IQueryable<Role> GetAll()
        {
            return _table;
        }

        public IQueryable<Role> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public Role GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(Role set)
        {
            var user = _table.Add(set);
            return user.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<Role> set)
        {
            return set.Select(AddSingle).ToList();
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
