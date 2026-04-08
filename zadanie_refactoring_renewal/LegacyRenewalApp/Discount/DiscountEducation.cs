namespace LegacyRenewalApp.Discount
{
    public class DiscountEducation : IDiscount
    {
        public CalcNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (customer.Segment == "Education" && subscriptionPlan.IsEducationEligible)
            {
                return new CalcNote(baseAmount * 0.20m, "education discount; ");
            }
            return new CalcNote(0, "");
        }
    }
}