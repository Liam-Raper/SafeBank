using System;
using System.Collections.Generic;
using Data.Employees.Interfaces;
using Data.DatabaseModel;
using System.Linq;
using System.Data.Entity;

namespace Data.Employees.Classes
{
    public class EmployeeTable : IEmployeeTable<int, Employee>
    {

        private readonly DbSet<Employee> _table;
        public EmployeeTable(DbSet<Employee> table)
        {
            _table = table;
        }

        public IEnumerable<int> AddMany(IEnumerable<Employee> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public int AddSingle(Employee set)
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

        public IQueryable<Employee> GetAll()
        {
            return _table;
        }

        public IQueryable<Employee> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public Employee GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public bool UpdateAll(Employee set)
        {
            var allRecords = GetAll().ToArray();
            return allRecords.Select(all => all.Id).Select(singleId => UpdateSingle(singleId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, Employee set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, Employee set)
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
