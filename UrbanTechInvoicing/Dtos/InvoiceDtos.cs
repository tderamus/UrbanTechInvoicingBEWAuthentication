using UrbanTechInvoicing.Models;

namespace UrbanTechInvoicing.Dtos
{
    public record InvoiceDto(
        Guid InvoiceId,
        string CreatorUserId,
        string InvoiceNumber,
        Guid? CustomerId,
        DateTime InvoiceDate,
        DateTime DueDate,
        Models.Invoice.InvoiceStatus Status,
        decimal InvoiceTotal
    );

    public record CreateInvoiceDto(
        Guid? CustomerId,
        DateTime InvoiceDate,
        DateTime DueDate,
        Models.Invoice.InvoiceStatus Status,
        decimal InvoiceTotal
    );

    public record UpdateInvoiceDto(
        Guid? CustomerId,
        DateTime InvoiceDate,
        DateTime DueDate,
        Models.Invoice.InvoiceStatus Status,
        decimal InvoiceTotal
    );

}
