using Business.Interfaces;
using SafeBank.Models.Accounts;
using System.Web.Mvc;
using System.Linq;

namespace SafeBank.Controllers
{
    public class CustomerController : Controller
    {

        private ICustomerService _customerService;
        private IAccountService _accountService;

        public CustomerController(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }

        public ActionResult Accounts()
        {
            var customerId = _customerService.GetCustomerId(User.Identity.Name);
            var model = new AccountsDetails();
            model.CustomerId = customerId;
            model.AccountDetailses = _accountService.GetAccountsForACustomer(customerId).Select(x => new AccountDetails
            {
                Id = x.Id,
                AccountName = x.Name,
                AccountNumber = x.Number,
                AccountType = x.Type.Name,
                AccountBalance = x.Balance,
                AccountOverdraft = x.Overdraft
            });
            return View(model);
        }
        
        public ActionResult Transactions(int accountId, int customerId)
        {
            if (!_customerService.CustomerExist(customerId)) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
            var account = _accountService.GetAccountsForACustomer(customerId).SingleOrDefault(x => x.Id == accountId);
            if (account == null) return RedirectToAction("CustomerAccounts", new { customerId = customerId });
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

    }
}