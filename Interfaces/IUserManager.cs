using BudgetPlanner.Model;

namespace BudgetPlanner.Interfaces
{
    public interface IUserManager
    {
        Task<bool> RegisterUserAsync(User pUser);
        Task<bool> LogInUserAsync(string pUsername, string pPassword);
        Task<bool> UsernameAvailableAsync(string pUsername);
        Task LogoffUser();
    }
}
