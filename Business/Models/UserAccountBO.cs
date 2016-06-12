namespace Business.Models
{
    public class UserAccountBO
    {
        public string SortCode { get; private set; }
        public string AccountNumber;
        public string AccountName;
        public decimal Balance;
        public decimal Overdraft;
        public string AccessLevel;

        public UserAccountBO(string accountNumber, string accountName, decimal balance, decimal overdraft, int bankCode, int brancheCode, int organisationCode, string accessLevel)
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
            Balance = balance;
            Overdraft = overdraft;
            AccessLevel = accessLevel;
            SetSortCode(bankCode,brancheCode,organisationCode);
        }

        private void SetSortCode(int bankCode, int brancheCode, int organisationCode)
        {
            SortCode = organisationCode + "-" + brancheCode + "-" + bankCode;
        }

    }
}