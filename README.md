
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
   ```

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
### Detailed API Endpoints

#### Employee Endpoints

- **GET /api/employees**
  - Retrieves a list of all employees.
  - **Response**: Array of employee objects.

- **GET /api/employees/{id}**
  - Retrieves a specific employee by their unique ID.
  - **Parameters**:
    - `id`: The ID of the employee.
  - **Response**: Employee object or 404 error if not found.

- **POST /api/employees**
  - Creates a new employee.
  - **Request Body**: Employee object (excluding ID, which is auto-generated).
  - **Response**: Returns the created employee object along with a URI in the `Location` header pointing to the newly created employee.

- **PATCH /api/employees/{id}**
  - Partially updates an existing employee.
  - **Parameters**:
    - `id`: The ID of the employee to update.
  - **Request Body**: Fields within the employee object that should be updated.
  - **Response**: 204 No Content on success, or 404 if the employee does not exist.

- **DELETE /api/employees/{id}**
  - Deletes an employee by ID.
  - **Parameters**:
    - `id`: The ID of the employee to delete.
  - **Response**: 204 No Content on success, or 404 if the employee does not exist.

#### Time Record Endpoints

- **GET /api/TimeRecords/{id}**
  - Retrieves a specific time record by its ID.
  - **Parameters**:
    - `id`: The ID of the time record.
  - **Response**: Time record object or 404 error if not found.

- **POST /api/TimeRecords/start**
  - Starts time tracking for an employee.
  - **Request Body**: Contains the `employeeId`.
  - **Response**: Returns the started time record object, or 404 if the employee is not found.

- **PUT /api/TimeRecords/stop/{id}**
  - Stops time tracking for an active time record.
  - **Parameters**:
    - `id`: The ID of the time record to stop.
  - **Response**: 204 No Content on successful stop, or 404 if no active record is found.

- **GET /api/TimeRecords/employee/{employeeId}/from/{startDate}/to/{endDate}**
  - Retrieves working hours for a specific employee between specified dates.
  - **Parameters**:
    - `employeeId`: The ID of the employee.
    - `startDate`: Start date for the record query.
    - `endDate`: End date for the record query.
  - **Response**: Dictionary of total hours worked each day, or 404 if no records are found.

- **GET /api/TimeRecords/all/from/{startDate}/to/{endDate}**
  - Retrieves time records for all employees between specified dates.
  - **Parameters**:
    - `startDate`: Start date for the record query.
    - `endDate`: End date for the record query.
  - **Response**: List of time records, ordered by the hours worked in descending order.

## Authors

- **Lovro Popovic** - Initial development - [GitHub](https://github.com/LovroPopovic)

## License

This software is released under the MIT License.


