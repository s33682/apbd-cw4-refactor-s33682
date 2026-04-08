namespace LegacyRenewalApp.Discount
{
    public class DiscountPoints : IDiscount
    {
        public DiscountNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (useLoyaltyPoints && customer.LoyaltyPoints > 0)
            {
                int pointsToUse = customer.LoyaltyPoints > 200 ? 200 : customer.LoyaltyPoints;
                return new DiscountNote(pointsToUse, $"loyalty points used: {pointsToUse}; ");
            }
            return new DiscountNote(0, "");
        }
    }
}