using PlannerModel.Enums;

namespace PlannerModel.ViewModel
{
    public sealed class Settings
    {
        public int Id { get; }

        public required int UserId { get; set; }

        public string PreferredCurrencyId { get; set; } = "EUR";
    }
}
