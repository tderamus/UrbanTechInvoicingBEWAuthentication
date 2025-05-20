namespace UrbanTechInvoicing.Models
{
    public class InvoiceService
    {
        // join table for Invoice and Service
        public Guid InvoiceId { get; set; } // Foreign key to Invoice
        public Guid ServiceId { get; set; } // Foreign key to Service
        public virtual Invoice? Invoice { get; set; } // Navigation property to Invoice
        public virtual Service? Service { get; set; } // Navigation property to Service
        public required decimal ServiceLineAmount { get; set; } // Amount for this service in the invoice
        public int InvoiceQuantity { get; set; } // Quantity of the service in the invoice
    }
}
