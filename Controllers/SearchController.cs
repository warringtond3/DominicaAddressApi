using DominicaAddressAPI.DTOs;
using DominicaAddressAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DominicaAddressAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[ResponseCache(Duration = 3600)]
public class SearchController : ControllerBase
{
    private readonly IAddressService _addressService;

    public SearchController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(SearchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SearchResultDto>> Search([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Search query 'q' is required");
        }

        var results = await _addressService.SearchAsync(q);
        return Ok(results);
    }
}
