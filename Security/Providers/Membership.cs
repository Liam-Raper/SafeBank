using System;
using System.Web.Security;
using Data.DatabaseModel;
using Data.Security.Membership.Interface;
using Data.Standard.Classed;
using Data.Standard.Interfaces;

namespace Security.Providers
{
    public class Membership : MembershipProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public Membership(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    Password = password,
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
            var userId = _unitOfWork.User.AddSingle(user);
            _unitOfWork.Commit();
            var newUser = _unitOfWork.User.GetSingle(userId);
            status = MembershipCreateStatus.Success;
            return new MembershipUser("", newUser.UserDetail.Username, newUser.Id, newUser.UserDetail.Email,
                newUser.UserSecurityQuestionAndAnswer.SecurityQuestion.Text, newUser.UserDetail.Comment,
                newUser.UserActivity.IsApproved, newUser.UserActivity.IsLockedOut, newUser.UserActivity.CreatedDate,
                DateTime.MinValue, newUser.UserActivity.LastActiveDate, newUser.UserAndPassword.LastChanged,
                DateTime.MinValue);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
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