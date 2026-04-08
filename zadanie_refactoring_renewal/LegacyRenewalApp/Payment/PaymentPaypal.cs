namespace LegacyRenewalApp.Payment
{
    public class PaymentPaypal : IPayment
    {
        public CalcNote Calculate(string method, decimal amount)
        {
            if (method == "PAYPAL")
            {
                return new CalcNote(amount*0.035m, "paypal fee; ");
            }
            return new CalcNote(0, "");
        }
    }
}