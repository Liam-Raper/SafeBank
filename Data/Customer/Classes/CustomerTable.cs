using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Customer.Interfaces;

namespace Data.Customer.Classes
{
    public class CustomerTable : ICustomerTable<int,DatabaseModel.Customer>
    {

        private readonly DbSet<DatabaseModel.Customer> _table;

        public CustomerTable(DbSet<DatabaseModel.Customer> table)
        {
            _table = table;
        }

        public IQueryable<DatabaseModel.Customer> GetAll()
        {
            return _table;
        }

        public IQueryable<DatabaseModel.Customer> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public DatabaseModel.Customer GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(DatabaseModel.Customer set)
        {
            var added = _table.Add(set);
            return added.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<DatabaseModel.Customer> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(DatabaseModel.Customer set)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateMany(IEnumerable<int> ids, DatabaseModel.Customer set)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateSingle(int id, DatabaseModel.Customer set)
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