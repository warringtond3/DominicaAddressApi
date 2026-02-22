namespace DominicaAddressAPI.DTOs;

public record SettlementSummaryDto(
    int Id,
    string Name,
    string Type,
    int StreetCount,
    double? Latitude,
    double? Longitude
);

public record SettlementDto(
    int Id,
    string Name,
    string Type,
    int ParishId,
    string ParishName,
    int StreetCount,
    double? Latitude,
    double? Longitude
);

public record SettlementDetailDto(
    int Id,
    string Name,
    string Type,
    int ParishId,
    string ParishName,
    double? Latitude,
    double? Longitude,
    IEnumerable<StreetSummaryDto> Streets
);
