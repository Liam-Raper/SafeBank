using System.Collections.Generic;

namespace SafeBank.Models.Customer
{
    public class CustomersDetails
    {
        public int BankId { get; set; }
        public IEnumerable<CustomerDetails> CustomerDetailses { get; set; }
    }
}