using BudgetPlanner.Model;

namespace BudgetPlanner.Interfaces
{
    public interface IExpenseManager
    {
        bool AddExpenseToDb(Expense pExpense);
        bool DeleteExpenseFromDb(Expense pExpenseId);
        IEnumerable<Expense> LoadAllExpensesByUser();
    }
}
