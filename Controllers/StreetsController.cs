using DominicaAddressAPI.DTOs;
using DominicaAddressAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DominicaAddressAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[ResponseCache(Duration = 3600)]
public class StreetsController : ControllerBase
{
    private readonly IAddressService _addressService;

    public StreetsController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<StreetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<StreetDto>>> GetStreets(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;
        if (pageSize > 100) pageSize = 100;

        var streets = await _addressService.GetAllStreetsAsync(page, pageSize);
        return Ok(streets);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StreetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StreetDto>> GetStreet(int id)
    {
        var street = await _addressService.GetStreetByIdAsync(id);
        if (street is null)
            return NotFound();
        return Ok(street);
    }
}
