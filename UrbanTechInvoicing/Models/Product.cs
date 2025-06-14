using System.ComponentModel.DataAnnotations;

namespace UrbanTechInvoicing.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public string? CreatorUserId { get; set; }

    }
}
