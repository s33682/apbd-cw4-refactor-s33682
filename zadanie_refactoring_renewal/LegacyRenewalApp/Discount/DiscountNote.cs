namespace LegacyRenewalApp
{
    public class DiscountNote
    {
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        
        public DiscountNote(decimal amount, string notes)
        {
            Amount = amount;
            Notes = notes;
        }
        
    }
}