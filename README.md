# .NET Test Case API

## Overview

This API is designed to manage financial transactions. It provides functionality for importing transactions from a CSV file, exporting transactions to an Excel file, and retrieving transactions for specific periods with consideration for user time zones.

## Architecture

The project is structured into the following layers to promote separation of concerns and scalability:

- **API Layer**: Entry point of the application which defines the endpoints.
- **Application Layer**: Contains all application logic. It acts as the middleman between the API layer and Domain layer.
- **Domain Layer**: Includes domain entities which are the core business logic entities.
- **Infrastructure Layer**: Handles data access and other infrastructure concerns such as database connections and external services.

## Technologies Used

- **Dapper**: Utilized for crafting SQL queries to the database due to its performance benefits.
- **Entity Framework Core**: Employed for database migration purposes.
- **GeoTimeZone**: Used for retrieving a user's time zone based on latitude and longitude coordinates. More information can be found at [GeoTimeZone GitHub](https://github.com/mattjohnsonpint/GeoTimeZone).
- **NodaTime**: Applied for robust handling of dates and times. NodaTime relies on the IANA time zone database, enabling the comparison of time zones through their properties like offsets from UTC without directly using identifiers.

## Features

- Import transactions from a CSV file.
- Export transactions to an Excel file.
- Retrieve transactions filtered by year and user time zone.

## API Usage

### Import Transactions

`POST /import-transactions-csv`

Imports transactions from a CSV file.

**Parameters:**

- `file`: The CSV file containing transactions to be imported.

**Response:**

- `200 OK`: Transactions have been successfully processed and imported.

### Export Transactions to Excel

`GET /export-transactions-excel`

Exports transactions to an Excel file.

**Response:**

- `File`: A downloadable Excel file named `transactions.xlsx`.

### Retrieve Transactions for 2023

`GET /transactions/2023-user-time-zone`

Retrieves transactions for the year 2023 adjusted to the user's time zone.

**Response:**

- `200 OK`: List of transactions for 2023 in user time zone.

`GET /transactions/2023`

Retrieves transactions for the year 2023 without time zone adjustments.

**Response:**

- `200 OK`: List of transactions for 2023.

### Retrieve Transactions for January 2024

`GET /transactions/2024-january-user-time-zone`

Retrieves transactions for January 2024 adjusted to the user's time zone.

**Response:**

- `200 OK`: List of transactions for January 2024 in user time zone.

`GET /transactions/2024-january`

Retrieves transactions for January 2024 without time zone adjustments.

**Response:**

- `200 OK`: List of transactions for January 2024.

## General Requirements

- No use of automappers.
- Swagger documentation for API testing.
- Use Entity Framework for database migrations.
- Do not use the UnitOfWork or Repository pattern.
- Database queries must be written in SQL using Dapper.

## Acknowledgements

This project was developed based on the test case provided by [emleonid](https://github.com/emleonid/test-case-dotnet).
