using BudgetPlanner.Model;

namespace BudgetPlanner.Interfaces
{
    internal interface IExpenseManager
    {
        bool AddExpenseToDb(Expense pExpense);
        bool DeleteExpenseFromDb(Expense pExpenseId);
        IEnumerable<Expense> LoadAllExpensesByUser();
    }
}
