namespace BudgetPlanner.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string GetData()
            => $"{Id};{Name};{Email};{Password}";
    }

    public static class LoggedInUser
    {
        public static string? Username { get; set; }
    }
}
