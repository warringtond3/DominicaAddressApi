namespace DominicaAddressAPI.DTOs;

public record StreetSummaryDto(
    int Id,
    string Name,
    double? Latitude,
    double? Longitude
);

public record StreetDto(
    int Id,
    string Name,
    double? Latitude,
    double? Longitude,
    int SettlementId,
    string SettlementName,
    string SettlementType,
    int ParishId,
    string ParishName
);
