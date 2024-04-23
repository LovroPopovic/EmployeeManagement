
# Employee Management System

Developed using .NET 8.0.204 and ASP.NET Core 8.0.4, this project efficiently manages employee data for companies. It includes basic CRUD operations to handle employee details.

## Getting Started

Follow these instructions to set up and run the project on your local machine for development and testing purposes.

### Prerequisites

Ensure you have the following installed:

- .NET SDK 8.0.204
- ASP.NET Core Runtime 8.0.4
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

5. **Initialize SQLite and apply database migrations**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

6. **Build the project**
   ```bash
   dotnet build
   ```

7. **Run the project**
   ```bash
   dotnet run
   ```
   Access the application at `http://localhost:5000`.

### Using the API

The API endpoints to create, retrieve, update, and delete employee records are under development. Details will be provided as updates are made.

## Authors

- **Lovro Popovic** - Initial development - [GitHub](https://github.com/LovroPopovic)

## License

This software is released under the MIT License.

