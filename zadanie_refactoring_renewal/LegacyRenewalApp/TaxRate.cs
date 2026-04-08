using System.Collections.Generic;

namespace LegacyRenewalApp
{
    public class TaxRate :  ITaxRate
    {
        private Dictionary<string, decimal> Rates = new Dictionary<string, decimal>
        {
            { "Poland", 0.23m },
            { "Germany", 0.19m },
            { "Czech Republic", 0.21m },
            { "Norway", 0.25m }
        };

        public decimal GetTaxRate(string country)
        {
            return  Rates.ContainsKey(country) ? Rates[country] : 20m;
        }
    }
}