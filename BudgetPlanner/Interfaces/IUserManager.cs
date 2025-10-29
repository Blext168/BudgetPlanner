using PlannerModel;

namespace BudgetPlanner.Interfaces
{
    public interface IUserManager
    {
        Task<bool> RegisterUserAsync(User pUser);
        Task<bool> LogInUserAsync(string pUsername, string pPassword);
        Task<bool> LogInUserAsync(string pUsername, bool pBiometricLogin);
        Task<bool> UsernameAvailableAsync(string pUsername);
        Task LogoffUser();
    }
}
