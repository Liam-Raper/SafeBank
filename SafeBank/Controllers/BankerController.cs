using System.Linq;
using System.Web.Mvc;
using Business.Interfaces;
using SafeBank.Models.Customer;

namespace SafeBank.Controllers
{
    public class BankerController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private ICustomerService _customerService;

        public BankerController(IEmployeeService employeeService, ICustomerService customerService)
        {
            _employeeService = employeeService;
            _customerService = customerService;
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
            _customerService.AddCustomer(new Business.Models.CustomerBO
            {
                GivenName = model.GivenName,
                FamilyName = model.FamilyName,
                Email = model.UserDetails.Email,
                Phone = model.Phone
            });
            return View(model);
        }
    }
}