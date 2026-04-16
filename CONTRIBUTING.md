# 🌱 Contributing to GrowPots

Thank you for your interest in contributing to **GrowPots**! This guide explains how to get involved.

## 📋 Code of Conduct

Please be respectful and constructive. We follow the [Contributor Covenant](https://www.contributor-covenant.org/).

## 🚀 How to Contribute

### 1. Fork & Clone

```bash
git clone https://github.com/sanchezfrancisco/grow-pots.git
cd grow-pots
```

### 2. Create a Branch

Use a descriptive name for your branch:

```bash
git checkout -b feature/add-humidity-sensor
# or
git checkout -b fix/watering-schedule-bug
```

Branch naming conventions:
- `feature/` — new features
- `fix/` — bug fixes
- `docs/` — documentation changes
- `refactor/` — code improvements

### 3. Make Your Changes

- Follow the existing code style
- Add tests for new functionality
- Keep commits focused and descriptive

### 4. Submit a Pull Request

1. Push your branch: `git push origin feature/your-feature`
2. Open a PR on GitHub against `main`
3. Fill in the PR template with a clear description

## 🛠️ Development Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [PostgreSQL](https://www.postgresql.org/) (or use Docker Compose)

### Running Locally

```bash
# Start dependencies (PostgreSQL)
docker-compose up -d db

# Run the API
cd src/SmartPot.Api
dotnet run
```

## 📁 Project Structure

```
grow-pots/
├── src/SmartPot.Api/     ← ASP.NET Core REST API + SignalR
├── hardware/esp32/       ← ESP32 firmware (C++)
├── docs/                 ← Project documentation
└── docker-compose.yml    ← Full stack deployment
```

## 🐛 Reporting Issues

Please use [GitHub Issues](https://github.com/sanchezfrancisco/grow-pots/issues) and include:
- A clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Environment details (OS, .NET version, etc.)

## 📄 License

By contributing, you agree your contributions will be licensed under the same license as this project.

---
© 2026 Luis Francisco Valdes Sánchez
