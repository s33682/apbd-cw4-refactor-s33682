namespace LegacyRenewalApp
{
    public class CalcNote
    {
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        
        public CalcNote(decimal amount, string notes)
        {
            Amount = amount;
            Notes = notes;
        }
        
    }
}