using DominicaAddressAPI.DTOs;
using DominicaAddressAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DominicaAddressAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[ResponseCache(Duration = 3600)]
public class ParishesController : ControllerBase
{
    private readonly IAddressService _addressService;

    public ParishesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ParishDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ParishDto>>> GetParishes()
    {
        var parishes = await _addressService.GetAllParishesAsync();
        return Ok(parishes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ParishDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParishDetailDto>> GetParish(int id)
    {
        var parish = await _addressService.GetParishByIdAsync(id);
        if (parish is null)
            return NotFound();
        return Ok(parish);
    }

    [HttpGet("{id}/settlements")]
    [ProducesResponseType(typeof(IEnumerable<SettlementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SettlementDto>>> GetParishSettlements(int id)
    {
        var settlements = await _addressService.GetSettlementsByParishAsync(id);
        return Ok(settlements);
    }

    [HttpGet("{id}/streets")]
    [ProducesResponseType(typeof(IEnumerable<StreetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StreetDto>>> GetParishStreets(int id)
    {
        var streets = await _addressService.GetStreetsByParishAsync(id);
        return Ok(streets);
    }
}
