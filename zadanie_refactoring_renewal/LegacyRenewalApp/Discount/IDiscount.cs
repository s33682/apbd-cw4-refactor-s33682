namespace LegacyRenewalApp.Discount
{
    public interface IDiscount
    {
        CalcNote CalculateDiscount(Customer customer,
            SubscriptionPlan subscriptionPlan,
            decimal baseAmount,
            int seatCount,
            bool useLoyaltyPoints);
    }
}