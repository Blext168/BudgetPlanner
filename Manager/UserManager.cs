using BudgetPlanner.Cache;
using BudgetPlanner.Interfaces;
using BudgetPlanner.Model;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Manager
{
    public class UserManager : IUserManager
    {
        public async Task<bool> LogInUserAsync(string pUsername, string pPassword)
        {
            try
            {
                using DbModel context = DbModel.GetContext();
                User? user = await context.User.Where(w => w.Name.Equals(pUsername)).FirstOrDefaultAsync();

                // Verify username and password
                if (user is null || !User.VerifyPassword(pPassword, user.Password))
                    return false;

                // Login user
                UserCache.Username = user.Name;
                UserCache.UserId = user.Id;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterUserAsync(User pUser)
        {
            try
            {
                // Hash Password
                pUser.Password = User.HashPassword(pUser.Password);

                // Add to DB
                using DbModel context = DbModel.GetContext();
                await context.User.AddAsync(pUser);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UsernameAvailableAsync(string pUsername)
        {
            try
            {
                using DbModel context = DbModel.GetContext();
                User? user = await context.User.Where(w => w.Name.Equals(pUsername)).FirstOrDefaultAsync();

                return user is null;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public async Task LogoffUser()
        {
            UserCache.Clear();
            await Task.CompletedTask;
        }
    }
}
