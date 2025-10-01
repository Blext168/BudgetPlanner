namespace BudgetPlanner.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string GetData()
            => $"{Id};{Name};{Email};{Password}";
        
        public static string HashPassword(string pPassword)
            => BCrypt.Net.BCrypt.HashPassword(pPassword);

        public static bool VerifyPassword(string pPassword, string pHashedPassword)
            => BCrypt.Net.BCrypt.Verify(pPassword, pHashedPassword);
    }
}
