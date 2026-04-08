using System.Collections.Generic;

namespace LegacyRenewalApp
{
    public class SupportFee : ISuportFee
    {
        private Dictionary<string, decimal> Fees = new Dictionary<string, decimal>
        {
            { "START", 250m },
            { "PRO", 400m },
            { "ENTERPRISE", 700m }
        };

        public decimal CalculateFee(string planCode, bool includePremiumSupport)
        {
            if (includePremiumSupport)
            {
                return Fees.ContainsKey(planCode) ? Fees[planCode] : 0m;
            }
            return 0m;
        }
    }
}