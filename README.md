# Flight Booking Microservices system

A backend system that simulates a **Flight Booking Engine**, built using **.NET 9** and **Microservices Architecture**.  
This project demonstrates clean code practices, architecture understanding, system design, and integration between distributed services.

## Project Goal

The goal of this project is to evaluate:

- Software architecture skills
- Clean Architecture implementation
- Microservices communication
- Integration with external systems
- Caching and messaging patterns
- Code quality and scalability

## Architecture Overview

- Microservices-based architecture
- Each service is a separate project
- Each service follows Clean Architecture layers:
  - Domain
  - Application
  - Infrastructure
  - API
- Event-driven communication using RabbitMQ
- Caching using Redis
- Authentication & Authorization using JWT and ASP.NET Core Identity

## Technologies Used

- .NET 9 (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server
- Clean Architecture
- Microservices
- RabbitMQ
- Redis Cache
- ASP.NET Core Identity
- JWT Authentication
- Swagger (OpenAPI)
- HttpClient (External Integration)

## Services

### 1. Auth Service
**Endpoint:**
```http
POST /api/auth/register
```
```http
POST /api/auth/login
```
Responsible for authentication and authorization.

**Features:**
- User registration
- User login
- JWT authentication
- Role-based authorization

**Roles:**
- Admin
- Agent

### 2. Flight Search Service

Handles searching for available flights.

**Endpoint:**
```http
GET /api/flights/search
```
### Search Parameters

- From
- To
- Date
- CabinClass
- PassengersCount

### Behavior

- Validate input data
- Search flights from SQL Server database
- Cache search results in Redis for 5 minutes
- If cached data exists, return it without querying the database

---

## Booking Service

Handles flight booking operations.

### Endpoint

```http
POST /api/bookings
```
### Behavior

- Create booking with status `Pending`
- Validate flight availability
- Publish `BookingCreated` event to RabbitMQ

---

## RabbitMQ Messaging

- Event-based communication between services
- `BookingCreated` event is published by Booking Service
- A consumer listens for the event and processes booking confirmation

### Flow

1. Booking created → Status: Pending
2. `BookingCreated` event published
3. Consumer receives event
4. Simulates supplier confirmation
5. Booking status updated to Confirmed

---

## External Integration (Mock API)

- Simulates external flight supplier APIs

### Used For

- Flight availability check
- Booking confirmation

### Implementation

- HttpClient
- Timeout handling
- Mock responses

---

## Database Design

- SQL Server
- Entity Framework Core
- Code First approach

---

## How to Run the Project

1. Ensure the following services are running:
   - SQL Server
   - RabbitMQ
   - Redis
2. Update connection strings in `appsettings.json`
3. Apply EF Core migrations:
```bash
dotnet ef database update
```
4. Run each service separately:

```bash
dotnet run
```
5. open Swagger UI:
   
```http
https://localhost:{port}/swagger
```
## Future Improvements

- Add API Gateway
- Add distributed logging (Serilog + Seq)
- Add monitoring (Prometheus / Grafana)
- Implement Saga pattern for booking flow
- Add unit & integration tests
- Docker & Docker Compose support
