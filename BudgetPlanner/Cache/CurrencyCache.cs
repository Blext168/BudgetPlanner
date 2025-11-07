using PlannerModel.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetPlanner.Cache
{
    public static class CurrencyCache
    {
        private static List<Currency> _currencies;

        public static List<Currency> Currencies
        {
            get
            {
                if (_currencies is null)
                    _currencies = GetAllCurrencies();

                return _currencies;
            }
        }

        private static List<Currency> GetAllCurrencies()
        {

        }
    }
}
