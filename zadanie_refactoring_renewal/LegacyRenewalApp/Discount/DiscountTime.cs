namespace LegacyRenewalApp.Discount
{
    public class DiscountTime : IDiscount
    {
        public DiscountNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.YearsWithCompany >= 5)
            {
                return new DiscountNote(baseAmount * 0.07m, "long-term loyalty discount; ");
            }
            else if (customer.YearsWithCompany >= 2)
            {
                return new DiscountNote(baseAmount * 0.03m, "basic loyalty discount; ");
            }
            return new DiscountNote(0, "");
        }
    }
}