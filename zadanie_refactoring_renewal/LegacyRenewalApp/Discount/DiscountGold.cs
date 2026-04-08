namespace LegacyRenewalApp.Discount
{
    public class DiscountGold : IDiscount
    {
        public CalcNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Gold")
            {
                return new CalcNote(baseAmount * 0.10m, "gold discount; ");
            }
            return new CalcNote(0, "");
        }
    }
}