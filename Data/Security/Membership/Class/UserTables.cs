﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Data.DatabaseModel;
using Data.Security.Membership.Interface;
using Data.Standard.Classed;
using Data.Standard.Interfaces;

namespace Data.Security.Membership.Class
{
    public class UserTables : IUserTables<IntId,User>
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

        public IQueryable<User> GetMany(IEnumerable<IntId> ids)
        {
            var idList = ids.ToList();
            return GetAll().Where(x => idList.Exists(y => y.Equals(x.Id)));
        }

        public User GetSingle(IntId id)
        {
            return GetAll().Single(x => id.Equals(x.Id));
        }

        public IntId AddSingle(User set)
        {
            var user = _table.Add(set);
            return new IntId(user.Id);
        }

        public IEnumerable<IntId> AddMany(IEnumerable<User> set)
        {
            return set.Select(AddSingle).ToList();
        }

        public bool UpdateAll(User set)
        {
            var users = GetAll().ToArray();
            return users.Select(user => new IntId(user.Id)).Select(userId => UpdateSingle(userId, set)).All(result => result);
        }

        public bool UpdateMany(IEnumerable<IntId> ids, User set)
        {
            return ids.Select(intId => UpdateSingle(intId, set)).All(result => result);
        }

        public bool UpdateSingle(IntId id, User set)
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

        public bool DeleteMany(IEnumerable<IntId> ids)
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

        public bool DeleteSingle(IntId id)
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