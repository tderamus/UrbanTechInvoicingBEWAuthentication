namespace UrbanTechInvoicing.DTOS
{
    public record PaymentDto (Guid PaymentId, 
        Guid InvoiceId, 
        decimal PaymentAmount, 
        DateTime PaymentDate, 
        Models.Payments.PmtType PaymentType, 
        string? CreatorUserId);
    
    public record PaymentCreateDto (Guid InvoiceId, 
        decimal PaymentAmount, 
        DateTime PaymentDate, 
        Models.Payments.PmtType PaymentType, 
        string? CreatorUserId);
}
