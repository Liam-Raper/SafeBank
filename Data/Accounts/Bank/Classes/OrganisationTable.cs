using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Accounts.Bank.Interfaces;
using Data.DatabaseModel;

namespace Data.Accounts.Bank.Classes
{
    public class OrganisationTable : IOrganisationTable<int,OrganisationDetail>
    {

        private readonly DbSet<OrganisationDetail> _table;

        public OrganisationTable(DbSet<OrganisationDetail> table)
        {
            _table = table;
        }

        public IQueryable<OrganisationDetail> GetAll()
        {
            return _table;
        }

        public IQueryable<OrganisationDetail> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id))); 
        }

        public OrganisationDetail GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(OrganisationDetail set)
        {
            var org = _table.Add(set);
            return org.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<OrganisationDetail> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(OrganisationDetail set)
        {
            var orgs = GetAll().ToArray();
            return orgs.Select(org => org.Id).Select(orgId => UpdateSingle(orgId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, OrganisationDetail set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, OrganisationDetail set)
        {
            try
            {
                var org = GetSingle(id);
                org.Name = set.Name;
                org.Code = set.Code;
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