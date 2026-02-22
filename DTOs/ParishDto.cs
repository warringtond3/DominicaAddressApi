namespace DominicaAddressAPI.DTOs;

public record ParishDto(
    int Id,
    string Name,
    string? Code,
    int SettlementCount
);

public record ParishDetailDto(
    int Id,
    string Name,
    string? Code,
    IEnumerable<SettlementSummaryDto> Settlements
);
