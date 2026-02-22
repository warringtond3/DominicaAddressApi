# Dominica Address API

A RESTful API providing geographic data for the Commonwealth of Dominica, including parishes, settlements (cities, towns, villages), streets, and GPS coordinates.

This is an open source project. The API ships with a minimal example dataset — you are expected to populate it with your own data.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Getting Started

```bash
git clone <repo-url>
cd DominicaAddressAPI
dotnet run
```

The API will start at `http://localhost:5038`. The SQLite database is created and seeded with example data automatically on first run.

### Interactive API Docs

In development mode, browse to `/scalar` for an interactive API reference powered by [Scalar](https://github.com/scalar/scalar).

### Health Check

```
GET /health
```

### Docker

```bash
docker build -t dominica-api .
docker run -p 8080:8080 dominica-api
```

## API Endpoints

### Parishes

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/parishes` | List all parishes |
| GET | `/api/parishes/{id}` | Get a parish with its settlements |
| GET | `/api/parishes/{id}/settlements` | List settlements in a parish |
| GET | `/api/parishes/{id}/streets` | List streets in a parish |

### Settlements

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/settlements` | List all settlements (paginated) |
| GET | `/api/settlements?type=City` | Filter by type: `City`, `Town`, or `Village` |
| GET | `/api/settlements?page=1&pageSize=10` | Pagination controls |
| GET | `/api/settlements/{id}` | Get a settlement with its streets |
| GET | `/api/settlements/{id}/streets` | List streets in a settlement |

### Streets

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/streets` | List all streets (paginated) |
| GET | `/api/streets?page=1&pageSize=10` | Pagination controls |
| GET | `/api/streets/{id}` | Get a street by ID |

### Search

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/search?q=roseau` | Search parishes, settlements, and streets by name |

## Adding Data

The API seeds from `Data/DbInitializer.cs`. To add your own data:

1. Edit `DbInitializer.cs` with new settlements and streets
2. Include GPS coordinates (`Latitude` and `Longitude`)
3. Delete `dominica.db` to re-seed on next run

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed instructions.

## Data Attribution

The sample GPS coordinates in `Data/DbInitializer.cs` are sourced from [OpenStreetMap](https://www.openstreetmap.org/) contributors, available under the [Open Database License (ODbL)](https://opendatacommons.org/licenses/odbl/). If you add new coordinates derived from OpenStreetMap, you must retain this attribution.

## License

This project is licensed under the [MIT License](LICENSE).
