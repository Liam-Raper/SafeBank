using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Data.DatabaseModel;
using Data.Standard.Interfaces;

namespace Security.Providers
{
    public class RoleManager : RoleProvider
    {

        private readonly IUnitOfWork _unitOfWork;

        public RoleManager()
        {
            _unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _unitOfWork.User.GetAll().Any(x => x.UserDetail.Username == username && x.Role.Name == roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return _unitOfWork.User.GetAll().Where(x => x.UserDetail.Username == username).Select(x => x.Role.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            _unitOfWork.Role.AddSingle(new Role() {Name = roleName});
            _unitOfWork.Commit();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (GetUsersInRole(roleName).Any()) return false;
            var roleId = _unitOfWork.Role.GetAll().Where(x => x.Name == roleName).Select(x => x.Id).Single();
            _unitOfWork.Role.DeleteSingle(roleId);
            return true;
        }

        public override bool RoleExists(string roleName)
        {
            return _unitOfWork.Role.GetAll().Any(x => x.Name == roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var allRoles = _unitOfWork.Role.GetAll().ToArray();
            for (var userIndex = 0; userIndex < usernames.Length; userIndex++)
            {
                var username = usernames[userIndex];
                var roleNameForUser = roleNames[userIndex];
                var roleId = allRoles.Where(x => x.Name == roleNameForUser).Select(x => x.Id).Single();
                var user = _unitOfWork.User.GetAll().Single(x => x.UserDetail.Username == username);
                user.RoleId = roleId;
            }
            _unitOfWork.Commit();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return _unitOfWork.User.GetAll().Where(x => x.Role.Name == roleName).Select(x => x.UserDetail.Username).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return _unitOfWork.Role.GetAll().Select(x => x.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return GetUsersInRole(roleName).Where(x => x.Contains(usernameToMatch)).ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}