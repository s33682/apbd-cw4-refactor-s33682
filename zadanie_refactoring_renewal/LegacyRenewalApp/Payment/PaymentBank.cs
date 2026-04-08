namespace LegacyRenewalApp.Payment
{
    public class PaymentBank : IPayment
    {
        public CalcNote Calculate(string method, decimal amount)
        {
            if (method == "BANK_TRANSFER")
            {
                return new CalcNote(amount*0.01m, "bank transfer fee; ");
            }
            return new CalcNote(0, "");
        }
    }
}