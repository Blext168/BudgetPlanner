using PlannerModel.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BudgetPlanner.Cache
{
    public static class CurrencyCache
    {
        private static IEnumerable<Currency> _currencies;
        public static IEnumerable<Currency> Currencies
        {
            get
            {
                if (_currencies is null)
                    _currencies = GetAllCurrencies();

                return _currencies;
            }
        }

        private static IEnumerable<Currency> GetAllCurrencies() 
            => [.. CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                          .Select(culture => new RegionInfo(culture.Name))
                          .GroupBy(region => region.ISOCurrencySymbol)
                          .Select(group => group.First())
                          .Select(region => new Currency
                          {
                              Code = region.ISOCurrencySymbol,
                              Name = region.CurrencyEnglishName,
                              Symbol = region.CurrencySymbol,
                              Country = region.EnglishName
                          })
                          .OrderBy(currency => currency.Code)
                          .Skip(1)];
    }
}
