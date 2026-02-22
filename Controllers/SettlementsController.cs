using DominicaAddressAPI.DTOs;
using DominicaAddressAPI.Enums;
using DominicaAddressAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DominicaAddressAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[ResponseCache(Duration = 3600)]
public class SettlementsController : ControllerBase
{
    private readonly IAddressService _addressService;

    public SettlementsController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<SettlementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<SettlementDto>>> GetSettlements(
        [FromQuery] string? type = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        SettlementType? settlementType = null;

        if (!string.IsNullOrEmpty(type))
        {
            if (Enum.TryParse<SettlementType>(type, ignoreCase: true, out var parsedType))
            {
                settlementType = parsedType;
            }
            else
            {
                return BadRequest($"Invalid settlement type: {type}. Valid values are: City, Town, Village");
            }
        }

        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;
        if (pageSize > 100) pageSize = 100;

        var settlements = await _addressService.GetAllSettlementsAsync(page, pageSize, settlementType);
        return Ok(settlements);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SettlementDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SettlementDetailDto>> GetSettlement(int id)
    {
        var settlement = await _addressService.GetSettlementByIdAsync(id);
        if (settlement is null)
            return NotFound();
        return Ok(settlement);
    }

    [HttpGet("{id}/streets")]
    [ProducesResponseType(typeof(IEnumerable<StreetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StreetDto>>> GetSettlementStreets(int id)
    {
        var streets = await _addressService.GetStreetsBySettlementAsync(id);
        return Ok(streets);
    }
}
