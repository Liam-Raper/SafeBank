using System.Linq;
using System.Web.Mvc;
using Business.Interfaces;
using SafeBank.Models.Customer;
using Business.Models;
using System.Web.Security;
using Security.Interfaces.User;

namespace SafeBank.Controllers
{
    public class BankerController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private ICustomerService _customerService;
        private IAccountService _accountService;
        private IUserManager _userManager;

        public BankerController(IEmployeeService employeeService, ICustomerService customerService, IAccountService accountService, IUserManager userManager)
        {
            _employeeService = employeeService;
            _customerService = customerService;
            _accountService = accountService;
            _userManager = userManager;
        }

        public ActionResult CustomerManager()
        {
            var employeeId = _employeeService.GetEmployeeId(User.Identity.Name);
            var bankId = _employeeService.GetBankId(employeeId);
            var model = new CustomersDetails
            {
                BankId = bankId,
                CustomerDetailses = _customerService.GetCustomers(bankId).Select(x => new CustomerDetails
                {
                    id = x.id,
                    FamilyName = x.FamilyName,
                    GivenName = x.GivenName,
                    Phone = x.Phone,
                    Email = x.Email
                })
            };
            return View(model);
        }

        public ActionResult AddCustomer(int bankId)
        {
            var model = new AddCustomerDetails();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCustomer(AddCustomerDetails model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_userManager.DoseUserExist(model.UserDetails.Username))
            {
                ModelState.AddModelError("User", "The username is already taken.");
                return View(model);
            }
            var employeeId = _employeeService.GetEmployeeId(User.Identity.Name);
            var bankId = _employeeService.GetBankId(employeeId);
            if (_accountService.AccountExist(model.AccountDetails.AccountNumber.Value, bankId))
            {
                ModelState.AddModelError("Account", "The account number is already an account at this bank.");
                return View(model);
            }
            MembershipCreateStatus status;
            Membership.CreateUser(model.UserDetails.Username, model.UserDetails.Password, model.UserDetails.Email, model.UserDetails.Question, model.UserDetails.Answer, true, out status);
            if (!ModelState.IsValid || status != MembershipCreateStatus.Success)
            {
                return View(model);
            }
            _customerService.AddCustomer(new CustomerBO
            {
                GivenName = model.GivenName,
                FamilyName = model.FamilyName,
                Phone = model.Phone,
                Email = model.UserDetails.Email,
                Username = model.UserDetails.Username
            });
            _accountService.CreateAccountForCustomer(model.UserDetails.Username, bankId, new AccountBO
            {
                Name = model.AccountDetails.AccountName,
                Number = model.AccountDetails.AccountNumber.Value,
                Type = new AccountType
                {
                    Name = model.AccountDetails.AccountType
                }
            });
            _accountService.GiveUserAccessToAccount(model.UserDetails.Username, "Owner", model.AccountDetails.AccountNumber.Value);
            return RedirectToAction("CustomerManager");
        }
    }
}