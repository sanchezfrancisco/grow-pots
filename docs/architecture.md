# 🏗️ Arquitectura del Sistema GrowPots

## Visión General

GrowPots es un sistema IoT de monitoreo y riego automático de plantas. La arquitectura combina hardware embebido (ESP32), una API REST en la nube/Raspberry Pi, y una capa de datos con PostgreSQL.

```
┌─────────────────────────────────────────────────────────┐
│                     GrowPots System                     │
│                                                         │
│  ┌──────────┐        ┌──────────────────┐               │
│  │  ESP32   │──WiFi──▶  SmartPot.Api   │               │
│  │(Firmware)│        │  (ASP.NET Core)  │               │
│  │          │◀──HTTP─│                  │               │
│  └──────────┘        │  REST API        │               │
│                      │  SignalR (WS)    │               │
│                      └────────┬─────────┘               │
│                               │ EF Core                 │
│                      ┌────────▼─────────┐               │
│                      │   PostgreSQL DB  │               │
│                      └──────────────────┘               │
└─────────────────────────────────────────────────────────┘
```

## Componentes

### 1. ESP32 Firmware (`hardware/esp32-smartpot/`)
- Lee sensores: humedad del suelo, temperatura, luz
- Envía datos vía HTTP POST a la API cada N segundos
- Recibe comandos de riego desde la API
- Controla relé para la bomba de agua

### 2. SmartPot API (`src/SmartPot.Api/`)
- **Framework**: ASP.NET Core 8 (Web API)
- **Endpoints REST**: recibe datos del ESP32, devuelve comandos
- **SignalR**: WebSockets para alertas y datos en tiempo real al dashboard
- **EF Core**: ORM para acceso a PostgreSQL

### 3. Base de Datos (PostgreSQL)
- Modelo principal: `PlantData` (lecturas de sensores)
- Persistencia de historial de riego y alertas

### 4. Despliegue (Semana 4)
- Docker Compose orquesta API + PostgreSQL
- Corre en Raspberry Pi, accesible 24/7 en la red local

## Flujo de Datos

```
ESP32 → POST /api/smartpot/data → API → PostgreSQL
                                    ↓
                               SignalR Hub → Dashboard (Browser)
                                    ↓
                          Reglas de alerta → Notificaciones
```

## Stack Tecnológico

| Capa | Tecnología |
|---|---|
| Firmware | C++ / Arduino / ESP-IDF |
| API | ASP.NET Core 8 + SignalR |
| ORM | Entity Framework Core |
| Base de datos | PostgreSQL 16 |
| Contenedores | Docker + Docker Compose |
| Despliegue | Raspberry Pi OS |
