# Contributing

Thanks for your interest in contributing to the Dominica Address API!

## Development Setup

1. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
2. Clone the repository and run:
   ```bash
   dotnet run
   ```
3. The SQLite database is created and seeded automatically

## Adding Data

Settlement and street data lives in `Data/DbInitializer.cs`. To add new entries:

1. Add settlements to the appropriate parish array in `DbInitializer.Initialize()`
2. Include GPS coordinates (`Latitude` and `Longitude`)
3. Add streets with their `SettlementId` reference
4. Delete `dominica.db` to re-seed on next run

## Data Attribution

The sample GPS coordinates are sourced from OpenStreetMap (ODbL). If you add coordinates derived from OpenStreetMap, include a note in your PR so attribution is maintained. See `README.md` for details.

## Pull Requests

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/add-more-streets`)
3. Make your changes
4. Ensure the project builds (`dotnet build`)
5. Submit a pull request with a clear description of what you changed

## Reporting Issues

Open an issue on GitHub with:
- A clear title and description
- Steps to reproduce (for bugs)
- Expected vs actual behavior
