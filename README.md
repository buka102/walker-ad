# wa-test

# How to Run the Project

## Prerequisites

Ensure you have the following installed:

- Node.js (v20+ recommended)
- Angular CLI (`npm install -g @angular/cli`)
- .NET 9 SDK
- Docker & Docker Compose

## Running with Docker Compose

To run both the backend and frontend using Docker Compose, execute:

```sh
docker-compose up --build
```

This will build and start the backend and frontend containers.

- **API** will be available at [http://localhost:8080/](http://localhost:8080/)
- **Angular App** will be available at [http://localhost:4200/](http://localhost:4200/leads)

To stop the services, use:
```sh
docker-compose down
```

## Backend (.NET 9 API)

### 1. Navigate to the API Project
```sh
cd backend
```

### 2. Restore Dependencies
```sh
dotnet restore
```

### 3. Run the API
```sh
dotnet run
```
The API will be available at [http://localhost:5139/](http://localhost:5139/)

## Frontend (Angular Application)

### 1. Navigate to the Angular Project
```sh
cd angular/leads-dashboard
```

### 2. Install Dependencies
```sh
npm install
```

### 3. Start the Development Server
```sh
ng serve --proxy-config proxy.conf.json
```
The app will be available at [http://localhost:4200/leads](http://localhost:4200/leads)


## Running Tests

### Backend Tests

Navigate to the backend folder and run:

```sh
cd backend
```

#### Unit Tests
```sh
dotnet test Wa.Api.UnitTests
```

## API Testing

Use the included postman_collection.json to test API endpoints with Postman


### Third-Party Webhook

This project includes a webhook endpoint that allows third-party services to send data to our system. The webhook listens for HTTP POST requests containing JSON payloads.

#### How It Works

A third-party system sends a POST request to our webhook endpoint with structured data.

The system validates the request payload.

If valid, the data is processed, stored, and any necessary notifications are triggered.

The response status indicates success or failure.

#### Expected Responses:
200 OK: The request was successful, and data was processed.

400 Bad Request: The request payload is invalid or missing required fields.

401 Unauthorized: Invalid or missing API key. Ensure you are using x-api-key: 123456.

## Use provides Postman Collection to test for all responses.

## Enterprise-Level Features Used
This project includes several best practices commonly used in enterprise applications:

* FluentValidation: Provides a structured and testable way to validate input models (see `Wa.Application/Dto/CreateLeadDto.cs`) 
* Custom Exception Handling: Centralized error handling improves security and maintainability.
* Logging (Serilog): Ensures structured logging for better debugging and monitoring.
* Dependency Injection: Promotes modularity and testability.
* Docker Support: Simplifies deployment and environment consistency.
* AutoMapper: a better way to copy values from DTOs to Objects
* Swagger: API definition exposure for operability 

### Disclaimer

While these features are overkill for a small project, they showcase best practices for large-scale applications.

## Potential Improvements for Enterprise Readiness
To make this solution more secure, maintainable, and scalable:

**Security Enhancements**

* Implement authentication and authorization (e.g., Asymetrical JWT, OAuth2).
* Secure API endpoints with role-based access control.
* Validate and sanitize all user inputs to prevent security vulnerabilities.

**Maintainability and Scalability**

* Use API versioning for backward compatibility.
* Separate configuration settings into environment variables for better flexibility.

**Testability and Observability**

* Expand unit and integration test coverage.
* Add distributed tracing (e.g., OpenTelemetry) for better debugging.
* Implement structured logging with correlation IDs.

**Performance**
* Imlement caching (Redis)


