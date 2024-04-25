````
# Employee Management System

Developed using .NET 8.0.204 and ASP.NET Core 8.0.4, this project efficiently manages employee data for companies. It includes basic CRUD operations to handle employee details, as well as time tracking functionality.

## Getting Started

Follow these instructions to set up and run the project on your local machine for development and testing purposes.

### Prerequisites

Ensure you have the following installed:

- .NET SDK 8.0.204
- ASP.NET Core Runtime 8.0.4
- Microsoft SQL Server (2017 or later recommended)
- Visual Studio Code or another IDE that supports .NET
- Git

### Installation

Execute the following steps to prepare your development environment:

1. **Clone the repository**
   ```bash
   git clone https://github.com/LovroPopovic/EmployeeManagement.git
````

2. **Navigate to the project directory**

   ```bash
   cd EmployeeManagement
   ```

3. **Restore project dependencies**

   ```bash
   dotnet restore
   ```

4. **Install Entity Framework tools**

   ```bash
   dotnet tool install --global dotnet-ef
   ```

5. **Configure `appsettings.json` for SQL Server**

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EmployeeDB;User Id=sa;Password=your_password;"
     }
   }
   ```

6. **Apply database migrations (ensure SQL Server is running)**

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

7. **Build the project**

   ```bash
   dotnet build
   ```

8. **Run the project**
   ```bash
   dotnet run
   ```
   Access the application at `http://localhost:5000`.
   Access Swagger at `http://localhost:5000/swagger`

### Using the API

The API endpoints for managing employee records are as follows:

- **GET /api/employees**: Retrieve all employees.
- **GET /api/employees/{id}**: Retrieve a specific employee by ID.
- **POST /api/employees**: Create a new employee.
- **PUT /api/employees/{id}**: Update an existing employee.
- **DELETE /api/employees/{id}**: Delete an employee by ID.

The API endpoints for time tracking are as follows:

- **POST /api/timerecords/start/{employeeId}**: Start time tracking for the specified employee.
- **PUT /api/timerecords/stop/{id}**: Stop time tracking for the specified time record.

## Authors

- **Lovro Popovic** - Initial development - [GitHub](https://github.com/LovroPopovic)

## License

This software is released under the MIT License.

```

```
