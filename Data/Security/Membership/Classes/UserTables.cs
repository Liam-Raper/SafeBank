using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DatabaseModel;
using Data.Security.Membership.Interfaces;

namespace Data.Security.Membership.Classes
{
    public class UserTables : IUserTables<int,User>
    {

        private readonly DbSet<User> _table;

        public UserTables(DbSet<User> table)
        {
            _table = table;
        }

        public IQueryable<User> GetAll()
        {
            return _table;
        }

        public IQueryable<User> GetMany(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public User GetSingle(int id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public int AddSingle(User set)
        {
            var user = _table.Add(set);
            return user.Id;
        }

        public IEnumerable<int> AddMany(IEnumerable<User> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(User set)
        {
            var users = GetAll().ToArray();
            return users.Select(user => user.Id).Select(userId => UpdateSingle(userId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<int> ids, User set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(int id, User set)
        {
            try
            {
                var user = GetSingle(id);
                user.UserSecurityQuestionAndAnswer.Answer = set.UserSecurityQuestionAndAnswer.Answer;
                user.UserSecurityQuestionAndAnswer.QuestionId = set.UserSecurityQuestionAndAnswer.QuestionId;
                user.UserActivity.IsApproved = set.UserActivity.IsApproved;
                user.UserActivity.IsLockedOut = set.UserActivity.IsLockedOut;
                user.UserActivity.LastActiveDate = set.UserActivity.LastActiveDate;
                user.UserActivity.LastLockedOutDate = set.UserActivity.LastLockedOutDate;
                user.UserActivity.LastLoggedInDate = set.UserActivity.LastLoggedInDate;
                user.UserDetail.Email = set.UserDetail.Email;
                user.UserDetail.Comment = set.UserDetail.Comment;
                user.UserAndPassword.Password = set.UserAndPassword.Password;
                user.UserAndPassword.LastChanged = set.UserAndPassword.LastChanged;
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

        public bool Validate(User validate)
        {
            if (string.IsNullOrEmpty(validate.UserDetail.Username)) return false;
            if (string.IsNullOrEmpty(validate.UserDetail.Email)) return false;
            if (string.IsNullOrEmpty(validate.UserAndPassword.Password)) return false;
            return !string.IsNullOrEmpty(validate.UserSecurityQuestionAndAnswer.Answer);
        }
    }
}