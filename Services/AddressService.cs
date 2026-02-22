using DominicaAddressAPI.Data;
using DominicaAddressAPI.DTOs;
using DominicaAddressAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace DominicaAddressAPI.Services;

public class AddressService : IAddressService
{
    private readonly DominicaDbContext _context;

    public AddressService(DominicaDbContext context)
    {
        _context = context;
    }

    // Parishes
    public async Task<IEnumerable<ParishDto>> GetAllParishesAsync()
    {
        return await _context.Parishes
            .OrderBy(p => p.Name)
            .Select(p => new ParishDto(
                p.Id,
                p.Name,
                p.Code,
                p.Settlements.Count
            ))
            .ToListAsync();
    }

    public async Task<ParishDetailDto?> GetParishByIdAsync(int id)
    {
        return await _context.Parishes
            .Where(p => p.Id == id)
            .Select(p => new ParishDetailDto(
                p.Id,
                p.Name,
                p.Code,
                p.Settlements.OrderBy(s => s.Name).Select(s => new SettlementSummaryDto(
                    s.Id,
                    s.Name,
                    s.Type.ToString(),
                    s.Streets.Count,
                    s.Latitude,
                    s.Longitude
                ))
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SettlementDto>> GetSettlementsByParishAsync(int parishId)
    {
        return await _context.Settlements
            .Where(s => s.ParishId == parishId)
            .OrderBy(s => s.Name)
            .Select(s => new SettlementDto(
                s.Id,
                s.Name,
                s.Type.ToString(),
                s.ParishId,
                s.Parish.Name,
                s.Streets.Count,
                s.Latitude,
                s.Longitude
            ))
            .ToListAsync();
    }

    public async Task<IEnumerable<StreetDto>> GetStreetsByParishAsync(int parishId)
    {
        return await _context.Streets
            .Where(st => st.Settlement.ParishId == parishId)
            .OrderBy(st => st.Name)
            .Select(st => new StreetDto(
                st.Id,
                st.Name,
                st.Latitude,
                st.Longitude,
                st.SettlementId,
                st.Settlement.Name,
                st.Settlement.Type.ToString(),
                st.Settlement.ParishId,
                st.Settlement.Parish.Name
            ))
            .ToListAsync();
    }

    // Settlements
    public async Task<PagedResult<SettlementDto>> GetAllSettlementsAsync(int page, int pageSize, SettlementType? type = null)
    {
        var query = _context.Settlements.AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(s => s.Type == type.Value);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(s => s.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new SettlementDto(
                s.Id,
                s.Name,
                s.Type.ToString(),
                s.ParishId,
                s.Parish.Name,
                s.Streets.Count,
                s.Latitude,
                s.Longitude
            ))
            .ToListAsync();

        return new PagedResult<SettlementDto>(items, page, pageSize, totalCount, (int)Math.Ceiling(totalCount / (double)pageSize));
    }

    public async Task<SettlementDetailDto?> GetSettlementByIdAsync(int id)
    {
        return await _context.Settlements
            .Where(s => s.Id == id)
            .Select(s => new SettlementDetailDto(
                s.Id,
                s.Name,
                s.Type.ToString(),
                s.ParishId,
                s.Parish.Name,
                s.Latitude,
                s.Longitude,
                s.Streets.OrderBy(st => st.Name).Select(st => new StreetSummaryDto(st.Id, st.Name, st.Latitude, st.Longitude))
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<StreetDto>> GetStreetsBySettlementAsync(int settlementId)
    {
        return await _context.Streets
            .Where(st => st.SettlementId == settlementId)
            .OrderBy(st => st.Name)
            .Select(st => new StreetDto(
                st.Id,
                st.Name,
                st.Latitude,
                st.Longitude,
                st.SettlementId,
                st.Settlement.Name,
                st.Settlement.Type.ToString(),
                st.Settlement.ParishId,
                st.Settlement.Parish.Name
            ))
            .ToListAsync();
    }

    // Streets
    public async Task<PagedResult<StreetDto>> GetAllStreetsAsync(int page, int pageSize)
    {
        var query = _context.Streets.AsQueryable();
        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(st => st.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(st => new StreetDto(
                st.Id,
                st.Name,
                st.Latitude,
                st.Longitude,
                st.SettlementId,
                st.Settlement.Name,
                st.Settlement.Type.ToString(),
                st.Settlement.ParishId,
                st.Settlement.Parish.Name
            ))
            .ToListAsync();

        return new PagedResult<StreetDto>(items, page, pageSize, totalCount, (int)Math.Ceiling(totalCount / (double)pageSize));
    }

    public async Task<StreetDto?> GetStreetByIdAsync(int id)
    {
        return await _context.Streets
            .Where(st => st.Id == id)
            .Select(st => new StreetDto(
                st.Id,
                st.Name,
                st.Latitude,
                st.Longitude,
                st.SettlementId,
                st.Settlement.Name,
                st.Settlement.Type.ToString(),
                st.Settlement.ParishId,
                st.Settlement.Parish.Name
            ))
            .FirstOrDefaultAsync();
    }

    // Search
    public async Task<SearchResultDto> SearchAsync(string query)
    {
        var normalizedQuery = query.ToLower();

        var parishes = await _context.Parishes
            .Where(p => p.Name.ToLower().Contains(normalizedQuery))
            .OrderBy(p => p.Name)
            .Select(p => new ParishDto(
                p.Id,
                p.Name,
                p.Code,
                p.Settlements.Count
            ))
            .ToListAsync();

        var settlements = await _context.Settlements
            .Where(s => s.Name.ToLower().Contains(normalizedQuery))
            .OrderBy(s => s.Name)
            .Select(s => new SettlementDto(
                s.Id,
                s.Name,
                s.Type.ToString(),
                s.ParishId,
                s.Parish.Name,
                s.Streets.Count,
                s.Latitude,
                s.Longitude
            ))
            .ToListAsync();

        var streets = await _context.Streets
            .Where(st => st.Name.ToLower().Contains(normalizedQuery))
            .OrderBy(st => st.Name)
            .Select(st => new StreetDto(
                st.Id,
                st.Name,
                st.Latitude,
                st.Longitude,
                st.SettlementId,
                st.Settlement.Name,
                st.Settlement.Type.ToString(),
                st.Settlement.ParishId,
                st.Settlement.Parish.Name
            ))
            .ToListAsync();

        return new SearchResultDto(parishes, settlements, streets);
    }
}
