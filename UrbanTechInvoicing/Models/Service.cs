using System.ComponentModel.DataAnnotations;

namespace UrbanTechInvoicing.Models
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; }
        public required string ServiceName { get; set; }
        public required string Description { get; set; }
        public virtual ICollection<InvoiceService>? InvoiceServices { get; set; } // Navigation property for related InvoiceServices
    }
}
