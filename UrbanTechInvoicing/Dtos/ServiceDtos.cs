namespace UrbanTechInvoicing.Dtos
{
    public record ServiceDto(Guid ServiceId,
    string ServiceName,
    string Description,
    string? CreatorUserId);
    public record ServiceCreateDto(string ServiceName, string Description, string? CreatorUserId);
    
    public record ServiceUpdateDto(Guid ServiceId, string ServiceName, string Description);
}
