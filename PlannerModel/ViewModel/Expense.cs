namespace PlannerModel.ViewModel
{
    public sealed class Expense
    {
        public int Id { get; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool OneTime { get; set; }
        public DateTime? MonthOfExpense { get; set; }
        public int DayInMonth { get; set; } = DateTime.Now.Day;
        public int UserId { get; set; }
    }
}
