namespace LegacyRenewalApp.Discount
{
    public class DiscountSilver : IDiscount
    {
        public DiscountNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Silver")
            {
                return new DiscountNote(baseAmount * 0.05m, "silver discount; ");
            }
            return new DiscountNote(0, "");
        }
    }
}