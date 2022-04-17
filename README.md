# playground_microservices

## Whats Including In This Repository

#### Product microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository Pattern Implementation (but no db, no unit test)
* Swagger Open API implementation

#### Customer microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository Pattern Implementation (but no db, no unit test)
* Swagger Open API implementation

#### Ordering Microservice
* Implementing **DDD, CQRS, and Clean Architecture** with using Best Practices
* Developing **CQRS with using MediatR, FluentValidation and AutoMapper packages**
* **SqlServer database** connection and containerization
* Using **Entity Framework Core ORM** and auto migrate to SqlServer when application startup
* (no unit test)
	
## Run The Project
You will need the following tools:

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net 6 or later](https://dotnet.microsoft.com/download/dotnet-core/6)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)
1. Clone the repository
2. Once Docker for Windows is installed, go to the **Settings > Advanced option**, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so:
* **Memory: 4 GB**
* CPU: 2
3. At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

>Note: If you get connection timeout error Docker for Mac please [Turn Off Docker's "Experimental Features".](https://github.com/aspnetrun/run-aspnetcore-microservices/issues/33)

4. Wait for docker compose all microservices. That’s it! (some microservices need extra time to work so please wait if not worked in first shut)

5. You can **launch microservices** as below urls:

* **Product API -> http://host.docker.internal:8000/swagger/index.html**
* **Customer API -> http://host.docker.internal:8001/swagger/index.html**
* **Ordering API -> http://host.docker.internal:8004/swagger/index.html**




