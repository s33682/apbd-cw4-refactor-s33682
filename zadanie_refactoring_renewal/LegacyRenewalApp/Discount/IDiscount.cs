namespace LegacyRenewalApp.Discount
{
    public interface IDiscount
    {
        DiscountNote CalculateDiscount(Customer customer,
            SubscriptionPlan subscriptionPlan,
            decimal baseAmount,
            int seatCount,
            bool useLoyaltyPoints);
    }
}