using Business.Interfaces;
using Business.Models;
using Data.DatabaseModel;
using Data.Standard.Interfaces;
using Security.Providers;

namespace Business.Classes
{
    class EmployeeService : IEmployeeService
    {

        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddEmployee(EmployeeDetails employeeDetails)
        {
            System.Web.Security.MembershipCreateStatus status;
            var membership = new Membership();
            var role = new RoleManager();
            var user = membership.CreateUser(employeeDetails.username, employeeDetails.password, employeeDetails.email, employeeDetails.question, employeeDetails.answer, true, null, out status);
            role.AddUsersToRoles(new string[] { employeeDetails.username }, new string[] { employeeDetails.employeeType.ToString().Replace('_', ' ') });
            var employee = new Employee()
            {
                UserId = (int) user.ProviderUserKey,
                EmployeeDetail = new EmployeeDetail
                {
                    Email = employeeDetails.email,
                    Family_name = employeeDetails.FamilyName,
                    Given_name = employeeDetails.GivenName,
                    Phone = employeeDetails.Phone
                }
            };
            _unitOfWork.EmployeeTable.AddSingle(employee);
            _unitOfWork.Commit();
        }

    }
}
