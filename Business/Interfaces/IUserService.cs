using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        UserAccountsBo GetUserAccounts(string username);
    }
}