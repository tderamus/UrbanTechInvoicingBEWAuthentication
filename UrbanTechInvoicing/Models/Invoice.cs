using System.ComponentModel.DataAnnotations;

namespace UrbanTechInvoicing.Models
{
    public class Invoice
    {
        public enum InvoiceStatus
        {
            Paid,
            Unpaid,
            Overdue,
            Cancelled,
            Refunded,
            PartiallyPaid,
            Pending,
            Draft,
            AwaitingPayment,
        }

        [Key]
        public Guid InvoiceId { get; set; }
        public required string InvoiceNumber { get; set; }
        public required DateTime InvoiceDate { get; set; }
        public required DateTime DueDate { get; set; }
        public required decimal InvoiceTotal { get; set; }
        public required InvoiceStatus Status { get; set; } // e.g., Paid, Unpaid, Overdue
        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; } // Navigation property
       
        public virtual ICollection<InvoicePayments>? InvoicePayments { get; set; } // Navigation property
        public virtual ICollection<InvoiceService>? InvoiceServices { get; set; } // Navigation property
        public virtual ICollection<InvoiceProduct>? InvoiceProducts { get; set; } // Navigation property
    }
}
