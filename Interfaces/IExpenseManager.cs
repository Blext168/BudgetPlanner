using BudgetPlanner.Model;
using BudgetPlanner.Result;

namespace BudgetPlanner.Interfaces
{
    public interface IExpenseManager
    {
        [Obsolete("Use \"IManager\" instead.")]
        bool AddExpenseToDb(Expense pExpense);
        [Obsolete("Use \"IManager\" instead.")]
        bool DeleteExpenseFromDb(Expense pExpenseId);
        [Obsolete("Use \"IManager\" instead.")]
        IEnumerable<Expense> LoadAllExpensesByUser();
    }
}
