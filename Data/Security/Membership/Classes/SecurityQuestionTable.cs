using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DatabaseModel;
using Data.Security.Membership.Interfaces;

namespace Data.Security.Membership.Classes
{
    public class SecurityQuestionTables : ISecurityQuestionTable<int,SecurityQuestion>
    {

        private readonly DbSet<SecurityQuestion> _table;

        public SecurityQuestionTables(DbSet<SecurityQuestion> table)
        {
            _table = table;
        }

        public IQueryable<SecurityQuestion> GetAll()
        {
            return _table;
        }

        public IQueryable<SecurityQuestion> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public SecurityQuestion GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(SecurityQuestion set)
        {
            var question = _table.Add(set);
            return question.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<SecurityQuestion> set)
        {
            return set.Select(AddSingle).ToList();
        }
    }
}