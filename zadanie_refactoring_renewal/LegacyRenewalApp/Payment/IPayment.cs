namespace LegacyRenewalApp.Payment
{
    public interface IPayment
    {
        CalcNote Calculate(string method, decimal amount);
    }
}