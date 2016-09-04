using System.Collections.Generic;

namespace SafeBank.Models
{
    public class CustomersDetails
    {
        public int BankId { get; set; }
        public IEnumerable<CustomerDetails> CustomerDetailses { get; set; }
    }
}