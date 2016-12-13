using System.Linq;
using System.Web.Mvc;
using Business.Interfaces;
using SafeBank.Models.Customer;
using Business.Models;
using System.Web.Security;
using Security.Interfaces.User;
using SafeBank.Models.Accounts;

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

        public ActionResult EditCustomer(int customerId)
        {
            if (!_customerService.CustomerExist(customerId)) return RedirectToAction("CustomerManager");
            var model = new EditCustomerDetails();
            var customer = _customerService.GetCustomer(customerId);
            model.Id = customer.id;
            model.FamilyName = customer.FamilyName;
            model.GivenName = customer.GivenName;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCustomer(EditCustomerDetails model)
        {
            if (!ModelState.IsValid || !_customerService.CustomerExist(model.Id)) return View(model);
            _customerService.UpdateCustomer(new CustomerBO
            {
                id = model.Id,
                FamilyName = model.FamilyName,
                GivenName = model.GivenName
            });
            return RedirectToAction("CustomerManager");
        }
        
        public ActionResult DeleteCustomer(int customerId)
        {
            _customerService.DeleteCustomer(customerId);
            return RedirectToAction("CustomerManager");
        }
        
        public ActionResult CustomerAccounts(int customerId)
        {
            if (!_customerService.CustomerExist(customerId)) return RedirectToAction("CustomerManager");
            var model = new AccountsDetails
            {
                CustomerId = customerId,
                AccountDetailses = _accountService.GetAccountsForACustomer(customerId).Select(x => new AccountDetails
                {
                    Id = x.Id,
                    AccountName = x.Name,
                    AccountNumber = x.Number,
                    AccountType = x.Type.Name,
                    AccountBalance = x.Balance,
                    AccountOverdraft = x.Overdraft
                })
            };
            return View(model);
        }

        public ActionResult AddAccount(int CustomerId)
        {
            var model = new AddAccountDetails();
            model.CustomerId = CustomerId;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAccount(AddAccountDetails model)
        {
            var employeeId = _employeeService.GetEmployeeId(User.Identity.Name);
            var bankId = _employeeService.GetBankId(employeeId);
            var customer = _customerService.GetCustomer(model.CustomerId);
            if (_accountService.AccountExist(model.AccountNumber.Value, bankId))
            {
                ModelState.AddModelError("Account", "The account number is already an account at this bank.");
                return View(model);
            }
            _accountService.CreateAccountForCustomer(customer.Username, bankId, new AccountBO
            {
                Name = model.AccountName,
                Number = model.AccountNumber.Value,
                Type = new AccountType
                {
                    Name = model.AccountType
                }
            });
            _accountService.GiveUserAccessToAccount(customer.Username, "Owner", model.AccountNumber.Value);
            return RedirectToAction("CustomerAccounts",new { customerId = model.CustomerId });
        }

        public ActionResult EditAccount(int accountId, int customerId)
        {
            var accounts = _accountService.GetAccountsForACustomer(customerId);
            if (!accounts.Any(x => x.Id == accountId)) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var account = accounts.Single(x => x.Id == accountId);
            var model = new EditAccountDetails();
            model.CustomerId = customerId;
            model.AccountId = accountId;
            model.AccountName = account.Name;
            model.AccountOverdraft = account.Overdraft;
            return View(model);
        }
        
        [HttpPost]
        public ActionResult EditAccount(EditAccountDetails model)
        {
            if (!ModelState.IsValid || !_customerService.CustomerExist(model.CustomerId)) return View(model);
            var accounts = _accountService.GetAccountsForACustomer(model.CustomerId);
            if (!accounts.Any(x => x.Id == model.AccountId)) return RedirectToAction("CustomerAccounts", new { customerId = model.CustomerId });
            _accountService.UpdateAccount(new AccountBO
            {
                Id = model.AccountId,
                Name = model.AccountName,
                Overdraft = model.AccountOverdraft
            });
            return RedirectToAction("CustomerAccounts", new { customerId = model.CustomerId });
        }

        public ActionResult DeleteAccount(int accountId, int customerId)
        {
            var customer = _customerService.GetCustomer(customerId);
            var account = _accountService.GetAccountsForACustomer(customerId).Single(x => x.Id == accountId);
            _accountService.RemoveUserAccessToAccount(customer.Username, account.Number);
            _accountService.DeleteAccount(accountId);
            return RedirectToAction("CustomerAccounts", new { customerId = customerId });
        }

        public ActionResult AccountsTransactions(int accountId, int customerId)
        {
            if (!_customerService.CustomerExist(customerId)) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var account = _accountService.GetAccountsForACustomer(customerId).SingleOrDefault(x => x.Id == accountId);
            if(account == null) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var model = new AccountsTransactions();
            model.accountId = accountId;
            model.customerId = customerId;
            model.AccountName = account.Name;
            model.Transactions = _accountService.GetTransactionsForAccount(accountId).Select(x => new AccountsTransaction
            {
                Deposeted = x.Deposeted,
                Withdrawn = x.Withdrawn
            });
            return View(model);
        }
        
        public ActionResult AccountsDeposeted(int accountId, int customerId)
        {
            if(!_customerService.CustomerExist(customerId)) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var accounts = _accountService.GetAccountsForACustomer(customerId);
            var account = accounts.SingleOrDefault(x => x.Id == accountId);
            if(account == null) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var model = new AccountDeposeted()
            {
                customerId = customerId,
                accountId = account.Id,
                accountName = account.Name
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AccountsDeposeted(AccountDeposeted model)
        {
            if (!ModelState.IsValid) return View(model);
            _accountService.Deposet(model.accountId, model.ammount);
            return RedirectToAction("CustomerAccounts", new { customerId = model.customerId });
        }

        public ActionResult AccountsWithdrawn(int accountId, int customerId)
        {
            if (!_customerService.CustomerExist(customerId)) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var accounts = _accountService.GetAccountsForACustomer(customerId);
            var account = accounts.SingleOrDefault(x => x.Id == accountId);
            if (account == null) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var model = new AccountWithdrawn()
            {
                customerId = customerId,
                accountId = account.Id,
                accountName = account.Name
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AccountsWithdrawn(AccountWithdrawn model)
        {
            if (!ModelState.IsValid) return View(model);
            if(!_accountService.Withdrawn(model.accountId, model.ammount))
            {
                ModelState.AddModelError("ammount", "The ammount will take the customer below there overdraft.");
                return View(model);
            }
            return RedirectToAction("CustomerAccounts", new { customerId = model.customerId });
        }

    }
}