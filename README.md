# BookingApp

BookingApp is a simple and user-friendly application that allows you to make hotel reservations. It is developed with ASP.NET Core 8 and offers various features to simplify reservation management.

## Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio or any C# compatible IDE

## Features

- User login with JWT authentication
- Swagger for API documentation
- Layered architect
- Entity Framework Code First
- Authentication
- Authorization
- ASP.NET Core Custom Identity
- Middleware 
- Action Filter
- Model validation
- Dependency Injection
- Data Protection

### Installing

1. **Clone the Repository**
   ```bash
   git clone https://github.com/aliberkayberber/BookingApp
   ```
2. **Configure the database connection**

```bash
    
   - Update the connection string in `appsettings.json`:
    
     "ConnectionStrings": {
     "Default": "Server=your_server;Database=your_database;trusted_connection=true;TrustServerCertificate=true;"
       },
     "Jwt": {
       "Key": "your_secret_key",
       "Issuer": "your_issuer",
       "Audience": "your_audience"
       }
```
3. **Run database migrations**:
  
    ```bash
    
    dotnet ef database update
    ```
4. **Run the application**:
   
   ```bash
   
    dotnet run
    ```

    

End with an example of getting some data out of the system or using it
for a little demo

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or support, please email aliberkayberber@gmail.com