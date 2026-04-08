namespace LegacyRenewalApp.Discount
{
    public class DiscountSilver : IDiscount
    {
        public CalcNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Silver")
            {
                return new CalcNote(baseAmount * 0.05m, "silver discount; ");
            }
            return new CalcNote(0, "");
        }
    }
}