using System;

namespace LegacyRenewalApp
{
    public class RenewalInvoice
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string PlanCode { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public int SeatCount { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SupportFee { get; set; }
        public decimal PaymentFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }

        public override string ToString()
        {
            return $"InvoiceNumber={InvoiceNumber}, Customer={CustomerName}, Plan={PlanCode}, Seats={SeatCount}, FinalAmount={FinalAmount:F2}, Notes={Notes}";
        }
        
        public static RenewalInvoice Create(
            Customer customer, 
            string planCode, 
            string paymentMethod, 
            int seatCount, 
            decimal baseAmount, 
            decimal discountAmount, 
            decimal supportFee, 
            decimal paymentFee, 
            decimal taxAmount, 
            decimal finalAmount, 
            string notes)
        {
            return new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customer.Id}-{planCode}",
                CustomerName = customer.FullName,
                PlanCode = planCode,
                PaymentMethod = paymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };
        }
    }
}
