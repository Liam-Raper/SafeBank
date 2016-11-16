using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Customer.Interfaces;
using System;

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
            var allRecords = GetAll().ToArray();
            return allRecords.Select(all => all.Id).Select(singleId => UpdateSingle(singleId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, DatabaseModel.Customer set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, DatabaseModel.Customer set)
        {
            try
            {
                var record = GetSingle(id);
                record.CustomerDetail.Family_name = set.CustomerDetail.Family_name;
                record.CustomerDetail.Given_name = set.CustomerDetail.Given_name;
                record.CustomerDetail.Phone = set.CustomerDetail.Phone;
                record.CustomerDetail.Email = set.CustomerDetail.Email;
                record.User.UserActivity.IsApproved = set.User.UserActivity.IsApproved;
                record.User.UserActivity.IsLockedOut = set.User.UserActivity.IsLockedOut;
                record.User.UserDetail.Email = set.User.UserDetail.Email;
                record.User.UserDetail.Comment = set.User.UserDetail.Comment;
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