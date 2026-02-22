namespace DominicaAddressAPI.Entities;

public class Street
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public int SettlementId { get; set; }
    public Settlement Settlement { get; set; } = null!;
}
