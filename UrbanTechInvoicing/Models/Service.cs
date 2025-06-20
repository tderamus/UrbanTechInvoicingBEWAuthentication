using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UrbanTechInvoicing.Models
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? CreatorUserId { get; set; }
        [JsonIgnore]
        public virtual ICollection<InvoiceService>? InvoiceServices { get; set; } = new List<InvoiceService>();
    }
}
