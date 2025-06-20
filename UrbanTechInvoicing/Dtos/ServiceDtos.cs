namespace UrbanTechInvoicing.Dtos
{
    public record ServiceDto(Guid ServiceId,
    string ServiceName,
    string Description,
    string? CreatorUserId);
}
