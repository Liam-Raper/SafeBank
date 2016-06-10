using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using csBCrypt;
using CodeBits;
using Data.DatabaseModel;
using Data.Standard.Interfaces;

namespace Security.Providers
{
    public class Membership : MembershipProvider
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string ProviderName = "SafeBankMembership";

        public Membership()
        {
            _unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var user = new User
            {
                UserDetail = new UserDetail
                {
                    Username = username,
                    Email = email
                },
                UserActivity = new UserActivity
                {
                    CreatedDate = DateTime.Now,
                    IsApproved = isApproved,
                    IsLockedOut = false,
                    LastActiveDate = DateTime.Now
                },
                UserAndPassword = new UserAndPassword
                {
                    Password = GetPasswordToStore(password),
                    LastChanged = DateTime.Now
                },
                UserSecurityQuestionAndAnswer = new UserSecurityQuestionAndAnswer
                {
                    Answer = passwordAnswer,
                    SecurityQuestion = new SecurityQuestion
                    {
                        Text = passwordQuestion
                    }
                }
            };
            if (!_unitOfWork.User.Validate(user))
            {
                status = MembershipCreateStatus.UserRejected;
                return GetMembershipUserFromUser(user);
            }
            _unitOfWork.User.AddSingle(user);
            _unitOfWork.Commit();
            var userId = user.Id;
            var newUser = _unitOfWork.User.GetSingle(userId);
            status = MembershipCreateStatus.Success;
            return GetMembershipUserFromUser(newUser);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            var userMembership = GetUser(username, false);
            if (userMembership?.ProviderUserKey == null) return string.Empty;
            var id = (int)userMembership.ProviderUserKey;
            var user = _unitOfWork.User.GetSingle(id);
            return user.UserSecurityQuestionAndAnswer.Answer == answer ? user.UserAndPassword.Password : string.Empty;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword)) return false;
            var userMembership = GetUser(username, false);
            if (userMembership != null && userMembership.ProviderUserKey == null) return false;
            if (userMembership?.ProviderUserKey != null)
            {
                var id = (int) userMembership.ProviderUserKey;
                var user = _unitOfWork.User.GetSingle(id);
                user.UserAndPassword.Password = GetPasswordToStore(newPassword);
                user.UserAndPassword.LastChanged = DateTime.Now;
                _unitOfWork.User.UpdateSingle(id, user);
            }
            _unitOfWork.Commit();
            return true;
        }

        public override string ResetPassword(string username, string answer)
        {
            var userMembership = GetUser(username, false);
            if (userMembership?.ProviderUserKey == null) return string.Empty;
            var id = (int)userMembership.ProviderUserKey;
            var user = _unitOfWork.User.GetSingle(id);
            var newPassword = GetGeneratedPassword();
            user.UserAndPassword.Password = GetPasswordToStore(newPassword);
            user.UserAndPassword.LastChanged = DateTime.Now;
            _unitOfWork.User.UpdateSingle(id, user);
            _unitOfWork.Commit();
            return user.UserSecurityQuestionAndAnswer.Answer == answer ? newPassword : string.Empty;
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var userMembership = GetUser(username, false);
            if (userMembership?.ProviderUserKey == null) return false;
            var id = (int) userMembership.ProviderUserKey;
            var userpassword = _unitOfWork.User.GetSingle(id).UserAndPassword.Password;
            return ValidatePassword(password, userpassword);
        }

        public override bool UnlockUser(string userName)
        {
            var userMembership = GetUser(userName, false);
            if (userMembership?.ProviderUserKey == null) return false;
            var id = (int)userMembership.ProviderUserKey;
            var user = _unitOfWork.User.GetSingle(id);
            user.UserActivity.IsLockedOut = false;
            _unitOfWork.User.UpdateSingle(id, user);
            _unitOfWork.Commit();
            return true;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var id = (int) providerUserKey;
            var allUser = _unitOfWork.User.GetAll().Where(x => x.Id == id);
            if (userIsOnline)
            {
                allUser = allUser.Where(x => x.UserActivity.LastActiveDate > DateTime.Now.AddHours(-1));
            }
            var user = allUser.Single();
            return GetMembershipUserFromUser(user);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var allUser = _unitOfWork.User.GetAll().Where(x => x.UserDetail.Username == username);
            if (userIsOnline)
            {
                allUser = allUser.Where(x => x.UserActivity.LastActiveDate > DateTime.Now.AddHours(-1));
            }
            var user = allUser.Single();
            return GetMembershipUserFromUser(user);
        }

        public override string GetUserNameByEmail(string email)
        {
            return _unitOfWork.User.GetAll().Single(x => x.UserDetail.Email == email).UserDetail.Username;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            var userMembership = GetUser(username, false);
            if (userMembership?.ProviderUserKey == null) return false;
            var id = (int)userMembership.ProviderUserKey;
            var deleted = _unitOfWork.User.DeleteSingle(id);
            _unitOfWork.Commit();
            return deleted;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var usersList = _unitOfWork.User.GetAll();
            totalRecords = usersList.Count();
            return GetCollectionFromArray(usersList.Take(pageSize).Skip(pageIndex * pageSize).ToArray());
        }

        public override int GetNumberOfUsersOnline()
        {
            return _unitOfWork.User.GetAll().Count(x => x.UserActivity.LastActiveDate < DateTime.Now.AddHours(-1));
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var usersList = _unitOfWork.User.GetAll().Where(x => x.UserDetail.Username == usernameToMatch);
            totalRecords = usersList.Count();
            return GetCollectionFromArray(usersList.Take(pageSize).Skip(pageIndex * pageSize).ToArray());
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var usersList = _unitOfWork.User.GetAll().Where(x => x.UserDetail.Email == emailToMatch);
            totalRecords = usersList.Count();
            return GetCollectionFromArray(usersList.Take(pageSize).Skip(pageIndex * pageSize).ToArray());
        }

        private static MembershipUser GetMembershipUserFromUser(User user)
        {
            return new MembershipUser(ProviderName, user.UserDetail.Username, user.Id, user.UserDetail.Email,
                user.UserSecurityQuestionAndAnswer.SecurityQuestion.Text, user.UserDetail.Comment,
                user.UserActivity.IsApproved, user.UserActivity.IsLockedOut, user.UserActivity.CreatedDate,
                user.UserActivity.LastLoggedInDate ?? DateTime.MinValue, user.UserActivity.LastActiveDate,
                user.UserAndPassword.LastChanged,
                user.UserActivity.LastLockedOutDate ?? DateTime.MinValue);
        }

        private static MembershipUserCollection GetCollectionFromArray(IEnumerable<User> users)
        {
            var userCollection = new MembershipUserCollection();
            foreach (var user in users)
            {
                userCollection.Add(GetMembershipUserFromUser(user));
            }
            return userCollection;
        }

        private static string GetPasswordToStore(string password)
        {
            return BCrypt.CreateHash(password, BCrypt.GenerateSalt(5));
        }

        private static bool ValidatePassword(string password, string userpassword)
        {
            return BCrypt.VerifyPlaintext(password, userpassword);
        }

        private static string GetGeneratedPassword()
        {
            return PasswordGenerator.Generate(12);
        }

        public override string ApplicationName { get; set; }

        public override bool EnablePasswordRetrieval => false;

        public override bool EnablePasswordReset => true;

        public override bool RequiresQuestionAndAnswer => true;

        public override int MaxInvalidPasswordAttempts => 3;

        public override int PasswordAttemptWindow => 10;

        public override bool RequiresUniqueEmail => true;

        public override MembershipPasswordFormat PasswordFormat => MembershipPasswordFormat.Hashed;

        public override int MinRequiredPasswordLength => 8;

        public override int MinRequiredNonAlphanumericCharacters => 1;

        public override string PasswordStrengthRegularExpression => "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d$@$!%*?&]{8,}";
    }
}