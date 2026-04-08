namespace LegacyRenewalApp.Discount
{
    public class DiscountPlatinium : IDiscount
    {
        public CalcNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Platinum")
            {
                return new CalcNote(baseAmount * 0.15m, "platinum discount; ");
            }
            return new CalcNote(0, "");
        }
    }
}