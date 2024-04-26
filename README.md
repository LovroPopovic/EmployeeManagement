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

## Detailed API Endpoints

### Employee Endpoints

#### `GET /api/employees`

- **Description**: Retrieves a list of all employees in the database.
- **Response**: Returns an array of employee objects.

#### `GET /api/employees/{id}`

- **Description**: Retrieves detailed information of a specific employee by their unique ID.
- **Parameters**:
  - `id` (required): Unique identifier of the employee.
- **Response**: Returns a single employee object or a 404 error if the employee is not found.

#### `POST /api/employees`

- **Description**: Creates a new employee record in the database.
- **Request Body**: JSON representation of the employee object, excluding the ID (auto-generated).
- **Response**: Returns the created employee object with a `Location` header containing the URI of the new resource.

#### `PATCH /api/employees/{id}`

- **Description**: Partially updates an existing employee's details.
- **Parameters**:
  - `id` (required): Unique identifier of the employee to update.
- **Request Body**: JSON representation of fields within the employee object that need updating.
- **Response**: Returns 204 No Content on successful update, or 404 if the employee is not found.

#### `DELETE /api/employees/{id}`

- **Description**: Deletes an employee record from the database.
- **Parameters**:
  - `id` (required): Unique identifier of the employee to delete.
- **Response**: Returns 204 No Content on successful deletion, or 404 if the employee is not found.

#### `POST /api/{employeeId}/start`

- **Description**: Starts time tracking for an employee.
- **Request Body**: Contains the `employeeId` indicating the employee to track.
- **Response**: Returns the started time record object, or 404 if the employee does not exist.

#### `POST /api/{employeeId}/stop`

- **Description**: Stops time tracking for an active time record.
- **Parameters**:
  - `id` (required): Unique identifier of the time record to stop.
- **Response**: Returns 204 No Content on successful stop, or 404 if no active record is found.

### Time Record Endpoints

#### `GET /api/TimeRecords/{id}`

- **Description**: Retrieves a specific time record by its ID.
- **Parameters**:
  - `id` (required): Unique identifier of the time record.
- **Response**: Returns a time record object or 404 error if the record is not found.

#### `GET /api/TimeRecords/employee/{employeeId}/from/{startDate}/to/{endDate}`

- **Description**: Retrieves working hours for a specific employee between specified dates.
- **Parameters**:
  - `employeeId` (required): ID of the employee.
  - `startDate` (required): Start date for the record query.
  - `endDate` (required): End date for the record query.
- **Response**: Returns a dictionary of total hours worked in that range, and all records in that range, or 404 if no records are found.

#### `GET /api/TimeRecords/all/from/{startDate}/to/{endDate}`

- **Description**: Retrieves time records for all employees between specified dates.
- **Parameters**:
  - `startDate` (required): Start date for the record query.
  - `endDate` (required): End date for the record query.
- **Response**: Returns a list of time records, ordered by the total hours employee worked in descending order.

## Authors

- **Lovro Popovic** - Initial development - [GitHub](https://github.com/LovroPopovic)

## License

This software is released under the MIT License.
