# AvailableCardActionsMicroservice

Minimal proof of concept that determines which system actions are available for a payment card. Authorization depends on card type, status, and whether a PIN is set. The solution exposes an ASP.NET Core Web API plus a dedicated MSTest project.

## Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) (includes the `dotnet` CLI)

## Getting Started

1. Clone the repository and switch into the root directory.
2. Restore and build the solution:

   ```bash
   dotnet restore
   dotnet build
   ```

3. Run the API (listens on `https://localhost:7035` and `http://localhost:5251` by default):

   ```bash
   dotnet run --project AvailableCardActions.Api
   ```

## Solution Overview

While everything resides in a single ASP.NET Core project, folders provide the separation of concerns:

- **Domain (`AvailableCardActions.Api/Domain/`)** - enums (`CardType`, `CardStatus`, `SystemAction`), `CardDetails` record, and `AccessRuleSet` describing the attribute-based rules.
- **Services (`AvailableCardActions.Api/Services/`)** - `CardService` performs lookups and coordinates authorization; `ActionAuthorizator` evaluates the rule sets; both are registered via DI.
- **Interfaces (`AvailableCardActions.Api/Interfaces/`)** - abstractions for the services, used for injection and mocking in tests.
- **Controllers (`AvailableCardActions.Api/Controllers/`)** - `CardController` exposes `GET /users/{userId}/cards/{cardNumber}/available-actions` returning the authorized actions for a user/card pair; enums are serialized as strings for readability.
- **Configuration (`AvailableCardActions.Api/Program.cs`, `appsettings*.json`)** - logging, Swagger, JSON serialization, and HTTPS settings.
- **Tests (`AvailableCardActions.Api.Tests/`)** - MSTest project with Moq covering controllers and services behaviour.

## Notable Features

- Attribute-based authorization rules defined entirely in code for quick experimentation.
- Centralized logging with dependency-injected `ILogger<T>` in services and controller.
- JSON responses emit enum names instead of numeric values for better client ergonomics.
