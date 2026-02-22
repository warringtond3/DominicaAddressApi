namespace DominicaAddressAPI.DTOs;

public record SearchResultDto(
    IEnumerable<ParishDto> Parishes,
    IEnumerable<SettlementDto> Settlements,
    IEnumerable<StreetDto> Streets
);
