using BudgetPlanner.Cache;
using BudgetPlanner.Interfaces;
using BudgetPlanner.Model;

namespace BudgetPlanner.Manager
{
    public class ExpenseManager : IExpenseManager
    {
        private static DbModel GetContext() => new();

        public IEnumerable<Expense> LoadAllExpensesByUser()
        {
            try
            {
                using DbModel context = GetContext();
                List<Expense> expenses = [.. context.Expense.Where(w => w.UserId == UserCache.UserId)];
                // Return only monthly expenses or expenses for current month
                return expenses.Where(w => w.MonthOfExpense?.Month == DateTime.Now.Month && w.OneTime || !w.OneTime);
            }
            catch (Exception)
            {
                return [];
            }
        }

        public bool AddExpenseToDb(Expense pExpense)
        {
            try
            {
                using DbModel context = GetContext();
                context.Expense.Add(pExpense);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteExpenseFromDb(Expense pExpenseId)
        {
            try
            {
                using DbModel context = GetContext();
                context.Expense.Remove(pExpenseId);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
