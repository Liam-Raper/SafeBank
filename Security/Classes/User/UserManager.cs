using System.Linq;
using Data.Standard.Interfaces;
using Security.Interfaces.User;

namespace Security.Classes.User
{
    public class UserManager : IUserManager
    {


        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public bool DoseUserExist(string username)
        {
            return _unitOfWork.User.GetAll().Any(x => x.UserDetail.Username == username);
        }
    }
}