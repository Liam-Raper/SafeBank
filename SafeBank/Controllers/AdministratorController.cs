using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Business.Interfaces;
using Business.Models;
using SafeBank.Models;
using SafeBank.Models.Bank;
using SafeBank.Models.Branch;
using SafeBank.Models.Employee;
using SafeBank.Models.Organisation;
using SafeBank.Models.User;
using Security.Interfaces.User;
using Security.Providers;
using Membership = System.Web.Security.Membership;

namespace SafeBank.Controllers
{
    public class AdministratorController : Controller
    {

        private IOrganisationService _organisationService;
        private IBranchService _branchService;
        private IBankService _bankService;
        private IEmployeeService _employeeService;
        private IUserManager _userManager;

        public AdministratorController(IOrganisationService organisationService, IBranchService branchService, IBankService bankService, IEmployeeService employeeService, IUserManager userManager)
        {
            _organisationService = organisationService;
            _branchService = branchService;
            _bankService = bankService;
            _employeeService = employeeService;
            _userManager = userManager;
        }

        public ActionResult Dashboard()
        {
            var model = new AdministratorDashboardDetails
            {
                OrganisationsDetailses = _organisationService.GetOrganisations()
                    .Select(x => new OrganisationDetails {Id = x.Id, Name = x.Name, Code = x.Code, CanDelete = x.BranchCount == 0})
                    .ToArray()
            };
            return View(model);
        }

        public ActionResult AddOrganisation()
        {
            return View(new AddOrganisationDetails());
        }

        [HttpPost]
        public ActionResult AddOrganisation(AddOrganisationDetails model)
        {
            if (!ModelState.IsValid || _organisationService.OrganisationExist(model.Name) ||
                _organisationService.OrganisationCodeExist(model.Code ?? 0)) return View(model);
            _organisationService.AddOrganisation(new OrganisationBO
            {
                Name = model.Name,
                Code = model.Code ?? 0
            });
            return RedirectToAction("Dashboard");
        }

        public ActionResult EditOrganisation(int organisationId)
        {
            if (!_organisationService.OrganisationIdExists(organisationId)) return RedirectToAction("Dashboard");
            var model = new EditOrganisationDetails();
            var org = _organisationService.GetOrganisation(organisationId);
            model.Id = org.Id;
            model.Name = org.Name;
            model.Code = org.Code;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrganisation(EditOrganisationDetails model)
        {
            if (!ModelState.IsValid || !_organisationService.OrganisationIdExists(model.Id)) return View(model);
            _organisationService.UpdateOrganisation(new OrganisationBO {Id = model.Id,Code = model.Code ?? 0, Name = model.Name});
            return RedirectToAction("Dashboard");
        }

        public ActionResult DeleteOrganisation(int organisationId)
        {
            if (_organisationService.OrganisationIdExists(organisationId))
            {
                _organisationService.DeleteOrganisation(organisationId);
            }
            return RedirectToAction("Dashboard");
        }

        public ActionResult OrganisationBranchesList(int organisationId)
        {
            var model = new BranchesDetails
            {
                OrganisationId = organisationId,
                BranchDetailses =
                    _branchService.GetAllBranchesAtAnOrganisation(organisationId).Select(x => new BranchDetails
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                        CanDelete = x.BankCount == 0
                    })
            };
            return View(model);
        }

        public ActionResult AddBranch(int organisationId)
        {
            var model = new AddBranchDetails {OrganisationId = organisationId};
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddBranch(AddBranchDetails model)
        {
            if (!ModelState.IsValid || _branchService.BranchExist(model.OrganisationId, model.Name) ||
                _branchService.BranchCodeExist(model.OrganisationId, model.Code ?? 0)) return View(model);
            _branchService.AddBranch(model.OrganisationId,new BranchBO
            {
                Name = model.Name,
                Code = model.Code ?? 0
            });
            return RedirectToAction("OrganisationBranchesList",new { organisationId = model.OrganisationId });
        }

        public ActionResult EditBranch(int branchId, int organisationId)
        {
            if (!_branchService.BranchIdExists(branchId)) return RedirectToAction("OrganisationBranchesList",new { organisationId });
            var model = new EditBranchDetails();
            var branch = _branchService.GetBranch(branchId);
            model.Id = branch.Id;
            model.Name = branch.Name;
            model.Code = branch.Code;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBranch(EditBranchDetails model)
        {
            if (!ModelState.IsValid || !_branchService.BranchIdExists(model.Id)) return View(model);
            _branchService.UpdateBranch(new BranchBO { Id = model.Id, Code = model.Code ?? 0, Name = model.Name });
            return RedirectToAction("OrganisationBranchesList",
                new {organisationId = _branchService.GetOrganisationId(model.Id)});
        }

        public ActionResult DeleteBranch(int branchId, int organisationId)
        {
            _branchService.DeleteBranch(branchId);
            return RedirectToAction("OrganisationBranchesList", new {organisationId});
        }

        public ActionResult BanksList(int branchId)
        {
            var model = new BanksDetails
            {
                BranchId = branchId,
                OrganisationId = _branchService.GetOrganisationId(branchId),
                BankDetailses =
                    _bankService.GetAllBanksUnderABranch(branchId).Select(x => new BankDetails
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                        CanDelete = x.EmployeeCount == 0
                    })
            };
            return View(model);
        }

        public ActionResult AddBank(int branchId)
        {
            var model = new AddBankDetails { BranchId = branchId };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBank(AddBankDetails model)
        {
            if (!ModelState.IsValid || _bankService.BankExist(model.BranchId, model.Name) ||
                _bankService.BankCodeExist(model.BranchId, model.Code ?? 0)) return View(model);
            _bankService.AddBank(model.BranchId, new BankBO
            {
                Name = model.Name,
                Code = model.Code ?? 0
            });
            return RedirectToAction("BanksList", new { branchId = model.BranchId });
        }
        
        public ActionResult EditBank(int branchId, int bankId)
        {
            if (!_bankService.BankIdExists(bankId)) return RedirectToAction("BanksList", new { branchId });
            var model = new EditBankDetails();
            var bank = _bankService.GetBank(bankId);
            model.Id = bank.Id;
            model.Name = bank.Name;
            model.Code = bank.Code;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBank(EditBankDetails model)
        {
            if (!ModelState.IsValid || !_bankService.BankIdExists(model.Id)) return View(model);
            _bankService.UpdateBank(new BankBO { Id = model.Id, Code = model.Code ?? 0, Name = model.Name });
            return RedirectToAction("BanksList", new { branchId = _bankService.GetBranchId(model.Id) });
        }

        public ActionResult DeleteBank(int bankId, int branchId)
        {
            _bankService.DeleteBank(bankId);
            return RedirectToAction("BanksList", new { branchId = branchId });
        }

        public ActionResult BankEmployeeList(int bankId)
        {
            var model = new EmployeesDetails
            {
                BankId = bankId,
                BranchId = _bankService.GetBranchId(bankId),
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
            var model = new AddEmployeeDetails {BankId = bankId};
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
                ModelState.AddModelError("Employee","The employee code is already in use.");
                return View(model);
            }
            if (_userManager.DoseUserExist(model.Username))
            {
                ModelState.AddModelError("User","The username is already taken.");
                return View(model);
            }
            MembershipCreateStatus status;
            Membership.CreateUser(model.Username, model.Password, model.Email, model.Question, model.Answer, true, out status);
            if (!ModelState.IsValid || status != MembershipCreateStatus.Success)
            {
                return View(model);
            }
            Roles.AddUserToRole(model.Username, "Banker");
            _employeeService.AddEmployee(model.BankId,new EmployeeBO
            {
                Code = model.EmployeeCode ?? 0,
                GivenName = model.GivenName,
                FamilyName = model.FamilyName,
                Phone = model.Phone,
                Email = model.Email,
                Username = model.Username
            });
            return RedirectToAction("BankEmployeeList", new { bankId = model.BankId });
        }

        public ActionResult EditEmployee(int bankId, int employeeCode)
        {
            if(!_employeeService.EmployeeExist(bankId, employeeCode)) return RedirectToAction("BankEmployeeList", new { bankId });
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
            return RedirectToAction("BankEmployeeList", new { bankId = _employeeService.GetBankId(model.Id) });
        }

        public ActionResult DeleteEmployee(int bankId, int employeeId)
        {
            _employeeService.DeleteEmployee(employeeId);
            return RedirectToAction("BankEmployeeList", new {bankId = bankId});
        }

        public ActionResult EditEmployeeRole(int bankId, int employeeCode)
        {
            if (!_employeeService.EmployeeExist(bankId, employeeCode)) return RedirectToAction("BankEmployeeList", new { bankId });
            var employeeId = _employeeService.GetEmployeeId(bankId, employeeCode);
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
            return RedirectToAction("BankEmployeeList", new { bankId = _employeeService.GetBankId(model.EmployeeId) });
        }

    }
}