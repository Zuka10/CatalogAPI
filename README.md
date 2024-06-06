    ASP.NET Web API Project with Clean Architecture
This project is an ASP.NET Web API built on .NET 8, following the principles of Clean Architecture. The application uses MSSQL as the database and provides functionality for managing a catalog, including products, categories, and ordering. The project includes both RESTful APIs and gRPC services to interact with the system.

    Project Structure
The project is organized into several layers to ensure separation of concerns and maintainability:

* Domain: Contains the core interfaces and entities.
* Application: Contains the application logic, including services, gRPC services, use cases.
* Infrastructure: Contains the implementation details for data access, entity configurations, and other infrastructure concerns.
* Presentation: Contains the API controllers.
  
      Technologies Used
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* MSSQL
* gRPC
* AutoMapper
* FluentValidation
* Serilog
