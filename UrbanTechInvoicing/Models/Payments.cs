using System.ComponentModel.DataAnnotations;

namespace UrbanTechInvoicing.Models
{
    public class Payments
    {
        public enum PmtType
        {
            Cash,
            CreditCard,
            DebitCard,
            BankTransfer,
            PayPal,
            Stripe,
            ACH,
            Venmo,
            Zelle,
            ApplePay,
            GooglePay,
            Other
        }

        [Key]
        public Guid PaymentId { get; set; }
        public Guid InvoiceId { get; set; } // Foreign key to Invoice
        public required decimal PaymentAmount { get; set; }
        public required DateTime PaymentDate { get; set; }
        public required PmtType PaymentType { get; set; }
        
        public virtual Invoice? Invoice { get; set; } // Navigation property to Invoice
    }
}
