namespace LegacyRenewalApp.Discount
{
    public class DiscountPlatinium : IDiscount
    {
        public DiscountNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Platinum")
            {
                return new DiscountNote(baseAmount * 0.15m, "platinum discount; ");
            }
            return new DiscountNote(0, "");
        }
    }
}