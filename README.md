# 🌱 GrowPots

A smart IoT system to monitor and manage multiple plant pots. Automates watering, lighting, and soil care via an ESP32 + ASP.NET Core API. Ideal for urban gardens and IoT plant enthusiasts.

## Features

- 📡 Real-time soil & environment monitoring (ESP32 sensor data)
- 💧 Automated watering control via relay
- 🔔 WebSocket alerts (SignalR) for threshold breaches
- 🗄️ PostgreSQL persistence with Entity Framework Core
- 🐳 Docker Compose for easy deployment
- 🌐 REST API with Swagger UI

## Project Structure

```
grow-pots/
├── src/
│   └── SmartPot.Api/          ← ASP.NET Core 8 REST API + SignalR
│       ├── Controllers/       ← SmartPotController
│       ├── Models/            ← PlantData
│       ├── DTOs/              ← PlantDataRequest
│       └── Data/              ← AppDbContext + EF Migrations
├── hardware/
│   └── esp32-smartpot/        ← ESP32 firmware (C++) — Week 2
├── docs/
│   ├── architecture.md
│   ├── hardware.md
│   └── deployment.md
├── Dockerfile
├── docker-compose.yml
└── CONTRIBUTING.md
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/smartpot/data` | Receive sensor data from ESP32 |
| `GET`  | `/api/smartpot/data/{deviceId}/latest` | Get latest reading for a device |

Swagger UI is available at `http://localhost:5050/` when running in Development mode.

## Quick Start (Development)

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for PostgreSQL)

### 1. Start the database

```bash
docker-compose up -d db
```

### 2. Run and auto-migrate

```bash
cd src/SmartPot.Api
dotnet run
```

The API will auto-apply EF Core migrations on startup in Development mode.

### 3. Full stack with Docker

```bash
docker-compose up --build
```

### 4. Test the endpoint

```bash
curl -X POST http://localhost:5050/api/smartpot/data \
  -H "Content-Type: application/json" \
  -d '{
    "deviceId": "esp32-pot-01",
    "soilMoisture": 45.5,
    "temperature": 22.3,
    "airHumidity": 60.0,
    "lightLevel": 800
  }'
```

## License

- **Open Source**: MIT License (free for personal, educational, and non-commercial use).
- **Commercial Use**: Requires a separate commercial license. Contact vsluisfrancisco@gmail.com for details.

> 💡 *Note: The MIT license allows commercial use, but if you want to ensure you benefit from commercial use, use the dual license model above.*

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## Contact

For commercial licensing or support:
Email: vsluisfrancisco@gmail.com

© 2026 Luis Francisco Valdes Sánchez — All rights reserved.

