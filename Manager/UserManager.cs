using BudgetPlanner.Cache;
using BudgetPlanner.Interfaces;
using BudgetPlanner.Model;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Manager
{
    internal class UserManager : IUserManager
    {
        public async Task<bool> LogInUserAsync(string pUsername, string pPassword)
        {
            try
            {
                using DbModel context = new();
                User? user = await context.User.Where(w => w.Name.Equals(pUsername)).FirstOrDefaultAsync();

                // Verify username and password
                if (user is null || !VerifyPassword(pPassword, user.Password))
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
                pUser.Password = HashPassword(pUser.Password);

                // Add to DB
                using DbModel context = new();
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
                using DbModel context = new();
                User? user = await context.User.Where(w => w.Name.Equals(pUsername)).FirstOrDefaultAsync();

                return user is null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string HashPassword(string pPassword)
            => BCrypt.Net.BCrypt.HashPassword(pPassword);

        public static bool VerifyPassword(string pPassword, string pHashedPassword)
            => BCrypt.Net.BCrypt.Verify(pPassword, pHashedPassword);
    }
}
