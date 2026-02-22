using DominicaAddressAPI.DTOs;
using DominicaAddressAPI.Enums;

namespace DominicaAddressAPI.Services;

public interface IAddressService
{
    // Parishes
    Task<IEnumerable<ParishDto>> GetAllParishesAsync();
    Task<ParishDetailDto?> GetParishByIdAsync(int id);
    Task<IEnumerable<SettlementDto>> GetSettlementsByParishAsync(int parishId);
    Task<IEnumerable<StreetDto>> GetStreetsByParishAsync(int parishId);

    // Settlements
    Task<PagedResult<SettlementDto>> GetAllSettlementsAsync(int page, int pageSize, SettlementType? type = null);
    Task<SettlementDetailDto?> GetSettlementByIdAsync(int id);
    Task<IEnumerable<StreetDto>> GetStreetsBySettlementAsync(int settlementId);

    // Streets
    Task<PagedResult<StreetDto>> GetAllStreetsAsync(int page, int pageSize);
    Task<StreetDto?> GetStreetByIdAsync(int id);

    // Search
    Task<SearchResultDto> SearchAsync(string query);
}
