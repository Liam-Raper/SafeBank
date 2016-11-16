﻿using System.Linq;
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
            var model = new AccountsDetails();
            model.CustomerId = customerId;
            model.AccountDetailses = _accountService.GetAccountsForACustomer(customerId).Select(x => new AccountDetails {
                Id = x.Id,
                AccountName = x.Name,
                AccountNumber = x.Number,
                AccountType = x.Type.Name
            });
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

        //TODO: Edit account

        public ActionResult DeleteAccount(int accountId, int customerId)
        {
            var customer = _customerService.GetCustomer(customerId);
            var account = _accountService.GetAccountsForACustomer(customerId).Single(x => x.Id == accountId);
            _accountService.RemoveUserAccessToAccount(customer.Username, account.Number);
            _accountService.DeleteAccount(accountId);
            return RedirectToAction("CustomerAccounts", new { customerId = customerId });
        }
        //TODO: View transactions

    }
}