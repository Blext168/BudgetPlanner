namespace BudgetPlanner.Model
{
    internal class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int DayInMonth { get; set; } = 1;
        public int UserId { get; set; }
    }
}
