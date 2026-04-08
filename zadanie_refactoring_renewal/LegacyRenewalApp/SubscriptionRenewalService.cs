using System;
using System.Collections.Generic;
using LegacyRenewalApp.Discount;
using LegacyRenewalApp.Payment;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private ICustomerRepository customerRepository;
        private ISubscriptionPlanRepository planRepository;
        private ILegacyBillingGateway billingGateway;
        private List<IDiscount> discounts;
        private ISuportFee suportFeeCalc;
        private ITaxRate taxRateProv;
        private List<IPayment>  paymentMethods;

        public SubscriptionRenewalService()
        {
            customerRepository = new CustomerRepository();
            planRepository = new SubscriptionPlanRepository();
            billingGateway = new LegacyBillingGatewayAddon();
            discounts = new List<IDiscount>();
            discounts.Add(new DiscountSilver());
            discounts.Add(new DiscountGold());
            discounts.Add(new DiscountPlatinium());
            discounts.Add(new DiscountEducation());
            discounts.Add(new DiscountTime());
            discounts.Add(new DiscountSeat());
            discounts.Add(new DiscountPoints());
            suportFeeCalc = new SupportFee();
            taxRateProv = new TaxRate();
            paymentMethods = new List<IPayment>();
            paymentMethods.Add(new PaymentCard());
            paymentMethods.Add(new PaymentBank());
            paymentMethods.Add(new PaymentInvoice());
            paymentMethods.Add(new PaymentPaypal());
        }

        public SubscriptionRenewalService(ICustomerRepository customerRepository,
            ISubscriptionPlanRepository planRepository, ILegacyBillingGateway billingGateway, List<IDiscount> discounts,
            ISuportFee suportFeeCalc, ITaxRate taxRateProv, List<IPayment> paymentMethods)
        {
            this.customerRepository = customerRepository;
            this.planRepository = planRepository;
            this.billingGateway = billingGateway;
            this.discounts = discounts;
            this.suportFeeCalc = suportFeeCalc;
            this.taxRateProv = taxRateProv;
            this.paymentMethods = paymentMethods;
        }

        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer id must be positive");
            }

            if (string.IsNullOrWhiteSpace(planCode))
            {
                throw new ArgumentException("Plan code is required");
            }

            if (seatCount <= 0)
            {
                throw new ArgumentException("Seat count must be positive");
            }

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Payment method is required");
            }

            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customer = customerRepository.GetById(customerId);
            var plan = planRepository.GetByCode(normalizedPlanCode);

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
            decimal discountAmount = 0m;
            string notes = string.Empty;
            
            for (int i=0; i<discounts.Count; i++)
            {
                CalcNote dn = discounts[i].CalculateDiscount(customer, plan, baseAmount, seatCount, useLoyaltyPoints);
                discountAmount += dn.Amount;
                notes += dn.Notes;
            }
            
            
            decimal subtotalAfterDiscount = baseAmount - discountAmount;
            if (subtotalAfterDiscount < 300m)
            {
                subtotalAfterDiscount = 300m;
                notes += "minimum discounted subtotal applied; ";
            }

            decimal supportFee = suportFeeCalc.CalculateFee(normalizedPlanCode, includePremiumSupport);
            
            decimal paymentFee = 0m;
            decimal baseAmountPayment = subtotalAfterDiscount + supportFee;
            string notesBackup = notes;
            
            for (int i=0; i<paymentMethods.Count; i++)
            {
                CalcNote dn = paymentMethods[i].Calculate(normalizedPaymentMethod, baseAmountPayment);
                paymentFee += dn.Amount;
                notes += dn.Notes;
            }

            if (notesBackup.Equals(notes))
            {
                throw new ArgumentException("Unsupported payment method");
            }

            decimal taxRate = taxRateProv.GetTaxRate(customer.Country);
            
            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
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

            billingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                billingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }
    }
}
