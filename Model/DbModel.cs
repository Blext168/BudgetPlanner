using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Model
{
    internal class DbModel : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Expense> Expense { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL(Properties.Settings.Default.ConnectionString);
        }
    }
}
