namespace LegacyRenewalApp
{
    public interface ISuportFee
    {
        decimal CalculateFee(string planCode, bool includePremiumSupport);
    }
}