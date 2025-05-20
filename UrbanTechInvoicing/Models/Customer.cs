using System.ComponentModel.DataAnnotations;

namespace UrbanTechInvoicing.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        public required string Name { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
