<div align="center">
  <h1>MWalks</h1>
  <p>MWalks API is a designed for managing walking trails.</p>
      <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="dotnet" />
      <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />
      <img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" alt="sql-server" />
      <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white" alt="jwt">
      <img src = "https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=white" alt="Swagger">
      <img src = "https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=Postman&logoColor=white" alt="Swagger">

  </div>

## Technical Features

- **Repository Pattern**: For abstracting data access.
- **DTOs & AutoMapper**: For data transfer and object mapping.
- **Identity & JWT Bearer**: For user authentication and authorization.
- **File Upload**: Support for uploading images to both database (as byte[]) and server (wwwroot).
- **Filtering, Sorting, Pagination**: Manage large datasets efficiently.
- **Custom Exceptions & Global Error Handling**: For robust error management.
- **Custom Attributes**: Validation of file uploads.
- **Options Pattern**: Configuration management.

## Models

## Endpoints

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Cloudinary Account](https://cloudinary.com/)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/yahya-saad/MWalks-API.git
   cd MWalks-API
   ```

2. **Restore packages**

   ```bash
   dotnet restore
   ```

3. **Set up the database**

   - Update the connection string in `appsettings.json` to point to your database.
   - Run the migrations to set up the database schema.
     ```bash
     dotnet ef database update
     ```

4. **Configure appsetting.json**

   - Add your jwtSettings , ConnectionString inside the `appsettings.json`.

```json
"ConnectionStrings": {
  "Default": "Server=(localdb)\\ProjectModels;Database=NZWalks;Trusted_Connection=True;",
  "Auth": "Server=(localdb)\\ProjectModels;Database=AuthDb;Trusted_Connection=True;"
}
```

```json
  "Jwt": {
 "SecretKey": "",
 "Issuer": "",
 "Audience": "",
 "ExpirationInMinutes": 30
}
```

5. **Run the application**
   ```bash
   dotnet run
   ```

### Usage

Using Postman Collection attatched with repo or [Swagger] to interact with the API endpoints.
