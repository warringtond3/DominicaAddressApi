using DominicaAddressAPI.Entities;
using DominicaAddressAPI.Enums;

namespace DominicaAddressAPI.Data;

public static class DbInitializer
{
    public static void Initialize(DominicaDbContext context)
    {
        if (context.Parishes.Any())
            return;

        // 10 Parishes of Dominica
        var parishes = new Parish[]
        {
            new() { Name = "St. George", Code = "STG" },
            new() { Name = "St. John", Code = "STJ" },
            new() { Name = "St. Peter", Code = "STP" },
            new() { Name = "St. Joseph", Code = "STJO" },
            new() { Name = "St. Paul", Code = "STPA" },
            new() { Name = "St. Luke", Code = "STL" },
            new() { Name = "St. Mark", Code = "STM" },
            new() { Name = "St. Patrick", Code = "STPAT" },
            new() { Name = "St. David", Code = "STD" },
            new() { Name = "St. Andrew", Code = "STA" }
        };
        context.Parishes.AddRange(parishes);
        context.SaveChanges();

        // Example settlements — add your own data here.
        // GPS coordinates below are sourced from OpenStreetMap (ODbL).
        // See README.md for attribution details.

        var roseau = new Settlement { Name = "Roseau", Type = SettlementType.City, ParishId = parishes[0].Id, Latitude = 15.299192, Longitude = -61.387287 };
        var portsmouth = new Settlement { Name = "Portsmouth", Type = SettlementType.Town, ParishId = parishes[1].Id, Latitude = 15.575915, Longitude = -61.455588 };
        var marigot = new Settlement { Name = "Marigot", Type = SettlementType.Village, ParishId = parishes[9].Id, Latitude = 15.536463, Longitude = -61.279364 };

        context.Settlements.AddRange(roseau, portsmouth, marigot);
        context.SaveChanges();

        // Example streets
        var streets = new Street[]
        {
            new() { Name = "Great George Street", SettlementId = roseau.Id, Latitude = 15.2991583, Longitude = -61.3875479 },
            new() { Name = "Victoria Street", SettlementId = roseau.Id, Latitude = 15.2938620, Longitude = -61.3826039 },
            new() { Name = "Bay Street", SettlementId = portsmouth.Id, Latitude = 15.5792841, Longitude = -61.4582280 }
        };
        context.Streets.AddRange(streets);
        context.SaveChanges();
    }
}
