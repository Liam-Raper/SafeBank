namespace Security.Interfaces.User
{
    public interface IUserActivities
    {
        void UpdateLoggedInDateTime(string username);
        void UpdateLastActionDateTime(string username);
    }
}