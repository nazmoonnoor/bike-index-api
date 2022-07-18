# Swapfiets Bike Theft API

![example workflow](https://github.com/nazmoonnoor/sf-assignment/actions/workflows/dotnet.yml/badge.svg)

This is a .Net 6 solution building with:

- [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Entity Framework 6](https://docs.microsoft.com/en-us/ef/core/)
- [Polly](https://github.com/App-vNext/Polly)
- [AutoMapper](https://automapper.org/)
- [xUnit](https://xunit.net/)

## Clone the project

```bash
  git clone https://github.com/nazmoonnoor/swapfiets.git
```

## Run using Visual Studio 2022

- Open the `Swapfiets.sln` on `Visual Studio 2022`.
- `Build & run` the solution.

## Run Locally

Go to the project directory

```bash
  cd Swapfiets
```

Restore packages, build & run

```bash
  .\swapfiets.api.ps1
```

Test

```bash
  .\swapfiets.api.tests.ps1
```

### Swagger page

https://localhost:7117/swagger/index.html

## Demo

![](https://i.ibb.co/YNYNFvj/l-LJvresy-Uu.png)

## Acceptance Criteria

| #   | Criteria                                                                                |
| :-- | :-------------------------------------------------------------------------------------- |
| `1` | Load number of registered bike theft in a given city.                                   |
| `2` | Swapfiets is operating in some cities, but wants to expand in more cities in next year. |

### API Reference

#### Get bike theft count

```http
  GET /api/v1/bike-theft/count
```

| Parameter  | Type      | Description                           |
| :--------- | :-------- | :------------------------------------ |
| `city`     | `string`  | **City or Lat/long** must be provided |
| `lat/long` | `string`  | **City or Lat/long** must be provided |
| `distance` | `integer` | **Optional**. Default value is 20     |

#### Get bike thefts

```http
  GET /api/v1/bike-theft
```

| Parameter     | Type      | Description                           |
| :------------ | :-------- | :------------------------------------ |
| `city`        | `string`  | **City or Lat/long** must be provided |
| `lat/long`    | `string`  | **City or Lat/long** must be provided |
| `distance`    | `integer` | **Optional**. Default value is 20     |
| `page_size`   | `integer` | **Optional**. Default value is 20     |
| `page_number` | `integer` | **Optional**. Default value is 1      |

** To add more cities in the system, I am storing cities in database **

## Feedback

If you have any feedback, please reach out to me at nazmoonnoor@gmail.com

## Design decisions

- Used HttpClientFactory instead of HttpClient
- Polly to retry the operatioin
- Layered architecture/Clean architechture
  - API
  - Services
  - Core/Infrustucture

https://github.com/jasontaylordev/CleanArchitecture
https://github.com/fullstackhero/dotnet-webapi-boilerplate
Overview
Design and architecture
Design decisions
