namespace LegacyRenewalApp.Discount
{
    public class DiscountGold : IDiscount
    {
        public DiscountNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Gold")
            {
                return new DiscountNote(baseAmount * 0.10m, "gold discount; ");
            }
            return new DiscountNote(0, "");
        }
    }
}