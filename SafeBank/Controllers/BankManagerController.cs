using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Business.Interfaces;
using Business.Models;
using SafeBank.Models;
using SafeBank.Models.Employee;
using SafeBank.Models.User;
using Security.Interfaces.User;

namespace SafeBank.Controllers
{
    public class BankManagerController : Controller
    {

        private IEmployeeService _employeeService;
        private IUserManager _userManager;

        public BankManagerController(IEmployeeService employeeService, IUserManager userManager)
        {
            _employeeService = employeeService;
            _userManager = userManager;
        }

        public ActionResult EmployeeManager()
        {
            var employeeId = _employeeService.GetEmployeeId(User.Identity.Name);
            var bankId = _employeeService.GetBankId(employeeId);
            var model = new EmployeesDetails
            {
                BankId = bankId,
                LoggedInEmployeeId = employeeId,
                EmployeeDetailses = _employeeService.GetAllEmployeesAtABank(bankId).Select(x => new EmployeeDetails
                {
                    FamilyName = x.FamilyName,
                    GivenName = x.GivenName,
                    Email = x.Email,
                    Phone = x.Phone,
                    Code = x.Code,
                    Id = x.Id,
                    Role = Roles.GetRolesForUser(x.Username).Single()
                })
            };
            return View(model);
        }
        
        public ActionResult AddEmployee(int bankId)
        {
            var model = new AddEmployeeDetails { BankId = bankId };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEmployee(AddEmployeeDetails model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_employeeService.EmployeeExist(model.BankId, model.EmployeeCode ?? 0))
            {
                ModelState.AddModelError("Employee", "The employee code is already in use.");
                return View(model);
            }
            if (_userManager.DoseUserExist(model.Username))
            {
                ModelState.AddModelError("User", "The username is already taken.");
                return View(model);
            }
            MembershipCreateStatus status;
            Membership.CreateUser(model.Username, model.Password, model.Email, model.Question, model.Answer, true, out status);
            if (!ModelState.IsValid || status != MembershipCreateStatus.Success)
            {
                return View(model);
            }
            Roles.AddUserToRole(model.Username, "Banker");
            _employeeService.AddEmployee(model.BankId, new EmployeeBO
            {
                Code = model.EmployeeCode ?? 0,
                GivenName = model.GivenName,
                FamilyName = model.FamilyName,
                Phone = model.Phone,
                Email = model.Email,
                Username = model.Username
            });
            return RedirectToAction("EmployeeManager");
        }
        
        public ActionResult EditEmployee(int bankId, int employeeCode)
        {
            if (!_employeeService.EmployeeExist(bankId, employeeCode)) return RedirectToAction("EmployeeManager");
            var model = new EditEmployeeDetails();
            var employeeId = _employeeService.GetEmployeeId(bankId, employeeCode);
            var employee = _employeeService.GetEmployee(employeeId);
            model.Id = employee.Id;
            model.GivenName = employee.GivenName;
            model.FamilyName = employee.FamilyName;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployee(EditEmployeeDetails model)
        {
            if (!ModelState.IsValid || !_employeeService.EmployeeExist(model.Id)) return View(model);
            _employeeService.UpdateEmployee(new EmployeeBO
            {
                Id = model.Id,
                FamilyName = model.FamilyName,
                GivenName = model.GivenName
            });
            return RedirectToAction("EmployeeManager");
        }

        public ActionResult DeleteEmployee(int employeeId)
        {
            var loggedInEmployeeId = _employeeService.GetEmployeeId(User.Identity.Name);
            if (employeeId == loggedInEmployeeId) return RedirectToAction("EmployeeManager");
            _employeeService.DeleteEmployee(employeeId);
            return RedirectToAction("EmployeeManager");
        }

        public ActionResult EditEmployeeRole(int bankId, int employeeCode)
        {
            if (!_employeeService.EmployeeExist(bankId, employeeCode)) return RedirectToAction("EmployeeManager", new { bankId });
            var employeeId = _employeeService.GetEmployeeId(bankId, employeeCode);
            var loggedInEmployeeId = _employeeService.GetEmployeeId(User.Identity.Name);
            if (employeeId == loggedInEmployeeId) return RedirectToAction("EmployeeManager");
            var employee = _employeeService.GetEmployee(employeeId);
            var model = new EditEmployeeRole
            {
                EmployeeId = employeeId,
                EmployeeName = employee.GivenName + ", " + employee.FamilyName,
                Username = employee.Username,
                Role = Roles.GetRolesForUser(employee.Username).Single(),
                Roles = Roles.GetAllRoles().ToArray().Where(x => x != "Customer" && x != "Administrator").Select(allRole => new RolesDetails
                {
                    Name = allRole
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployeeRole(EditEmployeeRole model)
        {
            if (!ModelState.IsValid || !_employeeService.EmployeeExist(model.EmployeeId)) return View(model);
            var employee = _employeeService.GetEmployee(model.EmployeeId);
            Roles.AddUserToRole(employee.Username, model.Role);
            return RedirectToAction("EmployeeManager");
        }

    }
}