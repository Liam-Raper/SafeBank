using System;
using System.Linq;
using Data.Standard.Interfaces;
using Security.Interfaces.User;

namespace Security.Classes.User
{
    public class UserActivities : IUserActivities
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserActivities(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void UpdateLoggedInDateTime(string username)
        {
            _unitOfWork.User.GetAll().Single(x => x.UserDetail.Username == username).UserActivity.LastLoggedInDate = DateTime.Now;
            _unitOfWork.Commit();
        }

        public void UpdateLastActionDateTime(string username)
        {
            _unitOfWork.User.GetAll().Single(x => x.UserDetail.Username == username).UserActivity.LastActiveDate = DateTime.Now;
            _unitOfWork.Commit();
        }
    }
}