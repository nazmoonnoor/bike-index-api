
# Bike Index API
![example workflow](https://github.com/nazmoonnoor/sf-assignment/actions/workflows/dotnet.yml/badge.svg)

The API project is a .Net 6 solution build with:
- [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Entity Framework 6](https://docs.microsoft.com/en-us/ef/core/)
- [Polly](https://github.com/App-vNext/Polly)
- [AutoMapper](https://automapper.org/)
- [xUnit](https://xunit.net/)

The frontend is build with:
- [React](https://reactjs.org/)

Here is the [Frontend README](https://github.com/nazmoonnoor/swapfiets/tree/main/frontend.react)
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
  .\swapfiets.run.ps1
```

Run unit tests

```bash
  .\swapfiets.tests.ps1
```

### Swagger page

  https://localhost:7117/swagger/index.html



## Acceptance Criteria



| #         | Criteria                                 |
| :-------- | :--------------------------------------- |
| `1`       | In order to access risk, load number of registered `bike theft` in a given city. |
| `2`       | Swapfiets is operating in some cities, but wants to expand in more cities in next year. |

## API Reference
#### Get bike theft count

```http
  GET /api/v1/bike-theft/count
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `city`    | `string` | **City or Lat/long** must be provided |
| `lat/long`| `string` | **City or Lat/long** must be provided |
| `distance`| `integer`| **Optional**. Default value is 10 |

#### Get bike thefts

```http
  GET /api/v1/bike-theft
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `city`    | `string` | **City or Lat/long** must be provided |
| `lat/long`| `string` | **City or Lat/long** must be provided |
| `distance`| `integer`| **Optional**. Default value is 10 |
| `page_size`| `integer` | **Optional**. Default value is 20 |
| `page_number`| `integer` | **Optional**. Default value is 1 |

- As Swapfiets is operating in some cities, and in future it will be expanded to other cities, we should have a City table in database to store them.
- So that frontend can load up all those cities.

#### Assess risk among cities
```http
  GET /api/v1/risk-assess
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `location`    | `string` | **City or Lat/long** must be provided |

#### Get all cities

```http
  GET /api/v1/city
```
#### Create new city

```http
  POST /api/v1/city
```
#### Get city by Id

```http
  GET /api/v1/city/{id}
```
#### Update city
```http
  PUT /api/v1/city
```
#### Delete city
```http
  DELETE /api/v1/city
```


