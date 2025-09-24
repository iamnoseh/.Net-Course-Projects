# Food Delivery API - Clean Architecture with Repository Pattern

## Overview
This project implements a Food Delivery API using Clean Architecture principles with Repository Pattern and Unit of Work pattern.

## Architecture Layers

### 1. Domain Layer (`Domain/`)
- **Entities**: Core business entities (Product, Category, Order, etc.)
- **DTOs**: Data Transfer Objects for API communication
- **Interfaces**: Repository interfaces and service contracts
- **Enums**: Business enums (Status, UserRole)
- **Filters**: Query filters for pagination and filtering

### 2. Infrastructure Layer (`Infrastructure/`)
- **Data**: Entity Framework DbContext with Identity
- **Repositories**: Repository implementations with Unit of Work pattern
- **Services**: Business logic services
- **Responses**: API response wrappers

### 3. WebApp Layer (`WebApp/`)
- **Controllers**: API controllers with authentication
- **Program.cs**: Application configuration and DI setup

## Key Features

### Authentication & Authorization
- ASP.NET Core Identity integration
- JWT token-based authentication
- Role-based authorization (Admin, User, Courier)
- Automatic admin user seeding

### Repository Pattern
- Generic `IRepository<T>` interface
- Base `Repository<T>` implementation
- Specific repositories for each entity
- Unit of Work pattern for transaction management

### Clean Architecture Benefits
- **Separation of Concerns**: Each layer has specific responsibilities
- **Dependency Inversion**: High-level modules don't depend on low-level modules
- **Testability**: Easy to mock repositories and services
- **Maintainability**: Changes in one layer don't affect others

## API Endpoints

### Authentication
- `POST /api/Auth/login` - User login
- `POST /api/Auth/register` - User registration
- `GET /api/Auth/test` - Test authenticated endpoint

### Products (Protected)
- `GET /api/Product` - Get all products
- `GET /api/Product/{id}` - Get product by ID
- `GET /api/Product/category/{categoryId}` - Get products by category
- `GET /api/Product/pagination` - Get paginated products with filters
- `POST /api/Product` - Create new product
- `PUT /api/Product` - Update product
- `DELETE /api/Product/{id}` - Delete product

### Categories (Protected)
- `GET /api/Category` - Get all categories
- `GET /api/Category/{id}` - Get category by ID
- `POST /api/Category` - Create new category
- `PUT /api/Category` - Update category
- `DELETE /api/Category/{id}` - Delete category

## Database Configuration

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=productdb;User Id=postgres;Password=12345;"
  }
}
```

### JWT Configuration
```json
{
  "Jwt": {
    "Key": "ThisIsAVeryLongSecretKeyForJWTTokenGeneration123456789",
    "Issuer": "FoodDeliveryAPI",
    "Audience": "FoodDeliveryAPIUsers",
    "ExpiryInMinutes": 60
  }
}
```

## Default Admin User
- **Email**: admin@fooddelivery.com
- **Password**: Admin123!
- **Role**: Admin

## Repository Pattern Implementation

### Generic Repository Interface
```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync();
    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    // ... more methods
}
```

### Unit of Work Pattern
```csharp
public interface IUnitOfWork : IDisposable
{
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<Order> Orders { get; }
    // ... other repositories
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
```

## Benefits of This Architecture

1. **Testability**: Easy to unit test services by mocking repositories
2. **Flexibility**: Can easily switch data access technologies
3. **Maintainability**: Clear separation of concerns
4. **Scalability**: Easy to add new features without affecting existing code
5. **Transaction Management**: Unit of Work handles complex transactions
6. **Code Reusability**: Generic repository reduces code duplication

## Getting Started

1. **Install Dependencies**:
   ```bash
   dotnet restore
   ```

2. **Update Database**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **Run Application**:
   ```bash
   dotnet run
   ```

4. **Test Authentication**:
   - Register a new user via `/api/Auth/register`
   - Login via `/api/Auth/login` to get JWT token
   - Use the token in Authorization header: `Bearer {token}`

## Swagger Documentation
Access Swagger UI at: `https://localhost:7000/swagger`

The API includes JWT authentication in Swagger, allowing you to test protected endpoints directly from the UI.
