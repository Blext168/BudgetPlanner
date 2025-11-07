using PlannerModel.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlannerModel.ViewModel
{
    public sealed class Settings
    {
        public int Id { get; }
        public required int UserId { get; set; }
        public DarkModeEnum DarkMode { get; set; } = DarkModeEnum.Light;
        public CurrencyCodeEnum PreferredCurrency { get; set; } = CurrencyCodeEnum.EUR;
    }
}
