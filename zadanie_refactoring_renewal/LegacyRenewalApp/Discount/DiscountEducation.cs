namespace LegacyRenewalApp.Discount
{
    public class DiscountEducation : IDiscount
    {
        public DiscountNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Education" && subscriptionPlan.IsEducationEligible)
            {
                return new DiscountNote(baseAmount * 0.20m, "education discount; ");
            }
            return new DiscountNote(0, "");
        }
    }
}