using DominicaAddressAPI.Data;
using DominicaAddressAPI.Entities;
using DominicaAddressAPI.Enums;
using DominicaAddressAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DominicaAddressAPI.Tests;

public class AddressServiceTests : IDisposable
{
    private readonly DominicaDbContext _context;
    private readonly AddressService _service;

    public AddressServiceTests()
    {
        var options = new DbContextOptionsBuilder<DominicaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DominicaDbContext(options);
        SeedData();
        _service = new AddressService(_context);
    }

    private void SeedData()
    {
        var parish = new Parish { Id = 1, Name = "St. George", Code = "STG" };
        _context.Parishes.Add(parish);

        var settlement = new Settlement
        {
            Id = 1,
            Name = "Roseau",
            Type = SettlementType.City,
            ParishId = 1,
            Latitude = 15.299,
            Longitude = -61.387
        };
        _context.Settlements.Add(settlement);

        var street = new Street
        {
            Id = 1,
            Name = "Great George Street",
            SettlementId = 1,
            Latitude = 15.299,
            Longitude = -61.387
        };
        _context.Streets.Add(street);

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllParishes_ReturnsAllParishes()
    {
        var result = await _service.GetAllParishesAsync();

        var parishes = result.ToList();
        Assert.Single(parishes);
        Assert.Equal("St. George", parishes[0].Name);
    }

    [Fact]
    public async Task GetParishById_ReturnsParish_WhenExists()
    {
        var result = await _service.GetParishByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("St. George", result.Name);
    }

    [Fact]
    public async Task GetParishById_ReturnsNull_WhenNotFound()
    {
        var result = await _service.GetParishByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllSettlements_ReturnsPaged()
    {
        var result = await _service.GetAllSettlementsAsync(1, 10);

        Assert.Single(result.Items);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task GetAllSettlements_FiltersByType()
    {
        var cities = await _service.GetAllSettlementsAsync(1, 10, SettlementType.City);
        var towns = await _service.GetAllSettlementsAsync(1, 10, SettlementType.Town);

        Assert.Single(cities.Items);
        Assert.Empty(towns.Items);
    }

    [Fact]
    public async Task GetSettlementById_ReturnsSettlement_WhenExists()
    {
        var result = await _service.GetSettlementByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Roseau", result.Name);
    }

    [Fact]
    public async Task GetStreetById_ReturnsStreet_WhenExists()
    {
        var result = await _service.GetStreetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Great George Street", result.Name);
    }

    [Fact]
    public async Task Search_FindsMatchingEntities()
    {
        var result = await _service.SearchAsync("roseau");

        Assert.Single(result.Settlements);
        Assert.Empty(result.Parishes);
    }

    [Fact]
    public async Task Search_IsCaseInsensitive()
    {
        var result = await _service.SearchAsync("GEORGE");

        Assert.Single(result.Parishes);
        Assert.Single(result.Streets);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
