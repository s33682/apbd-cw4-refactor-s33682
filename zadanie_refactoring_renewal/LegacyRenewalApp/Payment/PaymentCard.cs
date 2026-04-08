namespace LegacyRenewalApp.Payment
{
    public class PaymentCard : IPayment
    {
        public CalcNote Calculate(string method, decimal amount)
        {
            if (method == "CARD")
            {
                return new CalcNote(amount*0.02m, "card payment fee; ");
            }
            return new CalcNote(0, "");
        }
    }
}