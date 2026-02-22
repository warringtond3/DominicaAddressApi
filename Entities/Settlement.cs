using DominicaAddressAPI.Enums;

namespace DominicaAddressAPI.Entities;

public class Settlement
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public SettlementType Type { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public int ParishId { get; set; }
    public Parish Parish { get; set; } = null!;

    public ICollection<Street> Streets { get; set; } = [];
}
