using Business.Models;

namespace SafeBank.Models
{
    public class UserAccount
    {
        public string SortCode { get; private set; }
        public string AccountNumber;
        public string AccountName;
        public decimal Balance;
        public decimal Overdraft;
        public string AccessLevel;

        public UserAccount(string accountNumber, string accountName, decimal balance, decimal overdraft, int bankCode, int brancheCode, int organisationCode, string accessLevel)
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
            Balance = balance;
            Overdraft = overdraft;
            AccessLevel = accessLevel;
            SetSortCode(bankCode, brancheCode, organisationCode);
        }

        public UserAccount(UserAccountBO userAccountBo)
        {
            AccountNumber = userAccountBo.AccountNumber;
            AccountName = userAccountBo.AccountName;
            Balance = userAccountBo.Balance;
            Overdraft = userAccountBo.Overdraft;
            AccessLevel = userAccountBo.AccessLevel;
            SortCode = userAccountBo.SortCode;
        }

        private void SetSortCode(int bankCode, int brancheCode, int organisationCode)
        {
            SortCode = organisationCode + "-" + brancheCode + "-" + bankCode;
        }

    }
}