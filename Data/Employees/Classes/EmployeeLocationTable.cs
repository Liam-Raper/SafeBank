using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DatabaseModel;
using Data.Employees.Interfaces;

namespace Data.Employees.Classes
{
    public class EmployeeLocationTable : IEmployeeLocationTable<int,EmployeeLocation>
    {

        private readonly DbSet<EmployeeLocation> _table;
        public EmployeeLocationTable(DbSet<EmployeeLocation> table)
        {
            _table = table;
        }

        public IQueryable<EmployeeLocation> GetAll()
        {
            return _table;
        }

        public IQueryable<EmployeeLocation> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public EmployeeLocation GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(EmployeeLocation set)
        {
            var added = _table.Add(set);
            return added.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<EmployeeLocation> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(EmployeeLocation set)
        {
            var allRecords = GetAll().ToArray();
            return allRecords.Select(all => all.Id).Select(singleId => UpdateSingle(singleId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, EmployeeLocation set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, EmployeeLocation set)
        {
            try
            {
                var record = GetSingle(id);
                record.EmployeeId = set.EmployeeId;
                record.BankId = set.BankId;
                record.Employee = set.Employee;
                record.BankDetail = set.BankDetail;
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