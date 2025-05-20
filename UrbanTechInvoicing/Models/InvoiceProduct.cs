namespace UrbanTechInvoicing.Models
{
    public class InvoiceProduct
    {
        // join table for Invoice and Product
        public Guid InvoiceId { get; set; } // Foreign key to Invoice
        public Guid ProductId { get; set; } // Foreign key to Product
        public virtual Invoice? Invoice { get; set; } // Navigation property to Invoice
        public virtual Product? Product { get; set; } // Navigation property to Product
        public required decimal ProductLineAmount { get; set; } // Amount for this product in the invoice
        public int InvoiceQuantity { get; set; } // Quantity of the product in the invoice
    }
}
