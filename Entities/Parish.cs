namespace DominicaAddressAPI.Entities;

public class Parish
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Code { get; set; }

    public ICollection<Settlement> Settlements { get; set; } = [];
}
