namespace LegacyRenewalApp.Payment
{
    public class PaymentInvoice : IPayment
    {
        public CalcNote Calculate(string method, decimal amount)
        {
            if (method == "INVOICE")
            {
                return new CalcNote(0m, "invoice fee; ");
            }
            return new CalcNote(0, "");
        }
    }
}