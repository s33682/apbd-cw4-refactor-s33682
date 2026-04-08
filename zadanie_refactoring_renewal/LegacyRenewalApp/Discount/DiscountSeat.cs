namespace LegacyRenewalApp.Discount
{
    public class DiscountSeat : IDiscount
    {
        public CalcNote CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount,  int seatCount, bool useLoyaltyPoints)
        {
            if (seatCount >= 50)
            {
                return new CalcNote(baseAmount * 0.12m, "large team discount; ");
            }
            else if (seatCount >= 20)
            {
                return new CalcNote(baseAmount * 0.08m, "medium team discount; ");
            }
            else if (seatCount >= 10)
            {
                return new CalcNote(baseAmount * 0.04m, "small team discount; ");
            }
            return new CalcNote(0, "");
        }
    }
}