namespace UrbanTechInvoicing.Models
{
    public class InvoicePayments
    {
        // join table for Invoice and Payments
        
        public Guid InvoiceId { get; set; } // Foreign key to Invoice
        public Guid PaymentId { get; set; } // Foreign key to Payments
        public virtual Invoice? Invoice { get; set; } // Navigation property to Invoice
        public virtual Payments? Payment { get; set; } // Navigation property to Payments
        public required decimal PaymentAmount { get; set; } // Amount paid in this transaction
    }
}
