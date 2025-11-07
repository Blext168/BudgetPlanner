namespace PlannerModel.Extensions
{
    public static class StringExtensions
    {
        private const int MINIMUM_PASSWORD_LENGTH = 8;

        public static bool HasCapitalLetter(this string value) => value.Any(char.IsUpper);

        public static bool HasLowerLetter(this string value) => value.Any(char.IsLower);

        public static bool HasDigit(this string value) => value.Any(char.IsDigit);

        public static bool HasPunctuation(this string value) => value.Any(char.IsPunctuation);

        public static bool HasEightOrMoreCharacters(this string value) => value.Length >= MINIMUM_PASSWORD_LENGTH;
    }
}
