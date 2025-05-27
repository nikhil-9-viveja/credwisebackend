<!-- ## Team Structure and Responsibilities

### Team Members and Roles

| Name | Role | Assigned Module | Responsibilities |
|------|------|-----------------|------------------|
| Nikhil | Full Stack | Configuration | End-to-end dev: UI, API, DB for Configurations |


### Configuration Module (Nikhil) - Detailed Responsibilities

#### 1. UI Development
- Create user interfaces for managing loan products
- Build forms for configuring:
  - Loan types (Home, Personal, Gold)
  - Interest rates
  - Tenure options
  - Processing fees
  - Other loan parameters
- Design interfaces for managing FD types and their configurations

#### 2. API Development
- Create endpoints for:
  - CRUD operations on loan products
  - CRUD operations on FD types
  - Managing loan parameters
  - Managing FD configurations
- Implement proper validation and error handling
- Ensure secure API endpoints with proper authentication

#### 3. Database Development
- Work with the following tables:
  - `LoanProducts`
  - `HomeLoanDetails`
  - `PersonalLoanDetails`
  - `GoldLoanDetails`
  - `FDTypes`
  - `LoanProductDocuments`
- Implement proper data validation
- Ensure data integrity

#### 4. Specific Features to Implement
- Loan Product Management:
  - Create/Edit/Delete loan products
  - Configure loan-specific parameters
  - Upload and manage loan product documents
  - Set interest rates and tenure
  - Configure processing fees
   
- FD Type Management:
  - Create/Edit/Delete FD types
  - Set interest rates
  - Configure minimum and maximum amounts
  - Set duration options
  - Manage FD product descriptions

#### 5. Integration Points
- Work with the Admin Module team (Venkat and Raviteja) to ensure proper integration
- Ensure configuration module provides necessary data for loan processing
- Coordinate with the Customer Module team for proper product display

#### 6. Technical Responsibilities
- Follow clean architecture principles
- Implement proper error handling
- Add appropriate logging
- Write unit tests
- Ensure code quality and maintainability
- Follow security best practices

#### 7. Documentation
- Document API endpoints
- Create user guides for the configuration module
- Document database schema changes
- Maintain technical documentation

### Implementation Roadmap for Configuration Module

1. Database Setup
   - Scaffold the database using the provided schema
   - Set up initial data and configurations

2. Project Structure
   - Set up the basic project structure following clean architecture
   - Implement core entities and repositories
   - Create the service layer for business logic

3. API Development
   - Develop API controllers
   - Implement authentication and authorization
   - Add validation and error handling

4. UI Development
   - Build UI components
   - Implement responsive design
   - Add user-friendly forms and validations

5. Testing and Documentation
   - Write unit tests
   - Create API documentation
   - Prepare user guides

6. Integration
   - Integrate with other modules
   - Perform end-to-end testing
   - Deploy and monitor

   =====================================================================
   
// step by step procedure for proceding further.
## Configuration Module - Database Implementation

### Relevant Database Tables

#### 1. LoanProducts (Base Table)
```sql
- LoanProductId (Primary Key)
- ImageUrl (For product images)
- Title (Product name)
- Description (Product details)
- MaxLoanAmount (Maximum loan amount)
- LoanType (HOME, PERSONAL, GOLD)
- IsActive (For soft delete)
- Audit fields (CreatedAt, CreatedBy, etc.)
```

#### 2. HomeLoanDetails
```sql
- LoanProductId (Foreign Key)
- InterestRate
- TenureMonths
- ProcessingFee
- DownPaymentPercentage
```

#### 3. PersonalLoanDetails
```sql
- LoanProductId (Foreign Key)
- InterestRate
- TenureMonths
- ProcessingFee
- MinSalaryRequired
```

#### 4. GoldLoanDetails
```sql
- LoanProductId (Foreign Key)
- InterestRate
- TenureMonths
- ProcessingFee
- GoldPurityRequired
- RepaymentType
```

#### 5. FDTypes
```sql
- FDTypeId (Primary Key)
- Name
- Description
- InterestRate
- MinAmount
- MaxAmount
- Duration
```

#### 6. LoanProductDocuments
```sql
- LoanProductDocumentId (Primary Key)
- LoanProductId (Foreign Key)
- DocumentName
- DocumentContent
```

### Detailed Implementation Steps

#### Phase 1: Core Entities
1. Create entity classes in `CredWiseAdmin.Core/Entities`:
   - `LoanProduct.cs`
   - `HomeLoanDetail.cs`
   - `PersonalLoanDetail.cs`
   - `GoldLoanDetail.cs`
   - `FDType.cs`
   - `LoanProductDocument.cs`
2. Implement proper relationships and validations
3. Add necessary data annotations

#### Phase 2: Repository Layer
1. Create repository interfaces in `CredWiseAdmin.Repository`:
   - `ILoanProductRepository`
   - `IFDTypeRepository`
   - `IDocumentRepository`
2. Implement repository classes:
   - `LoanProductRepository`
   - `FDTypeRepository`
   - `DocumentRepository`
3. Add CRUD operations for each entity

#### Phase 3: Service Layer
1. Create service interfaces in `CredWiseAdmin.Service`:
   - `ILoanProductService`
   - `IFDTypeService`
   - `IDocumentService`
2. Implement business logic for:
   - Loan product management
   - FD type management
   - Document management
3. Add validation and business rules

#### Phase 4: API Layer
1. Create controllers in `CredWiseAdmin.API/Controllers`:
   - `LoanProductController`
   - `FDTypeController`
   - `DocumentController`
2. Implement endpoints for:
   - CRUD operations
   - Product configuration
   - Document upload/download
3. Add proper validation and error handling

#### Phase 5: UI Development
1. Create forms for:
   - Loan product configuration
   - FD type management
   - Document upload
2. Implement validation
3. Add user-friendly interfaces
4. Ensure responsive design

### API Endpoints to Implement

#### Loan Products
- GET `/api/loanproducts` - Get all loan products
- GET `/api/loanproducts/{id}` - Get loan product by ID
- POST `/api/loanproducts` - Create new loan product
- PUT `/api/loanproducts/{id}` - Update loan product
- DELETE `/api/loanproducts/{id}` - Delete loan product

#### FD Types
- GET `/api/fdtypes` - Get all FD types
- GET `/api/fdtypes/{id}` - Get FD type by ID
- POST `/api/fdtypes` - Create new FD type
- PUT `/api/fdtypes/{id}` - Update FD type
- DELETE `/api/fdtypes/{id}` - Delete FD type

#### Documents
- GET `/api/documents/{loanProductId}` - Get documents for a loan product
- POST `/api/documents` - Upload new document
- DELETE `/api/documents/{id}` - Delete document

### Validation Rules

#### Loan Products
- Title: Required, max 150 characters
- Description: Required
- MaxLoanAmount: Required, must be positive
- LoanType: Must be one of: HOME, PERSONAL, GOLD
- ImageUrl: Required, valid URL format

#### FD Types
- Name: Required, max 50 characters
- InterestRate: Required, between 0 and 100
- MinAmount: Required, must be positive
- MaxAmount: Required, must be greater than MinAmount
- Duration: Required, must be positive

#### Documents
- DocumentName: Required, max 100 characters
- DocumentContent: Required, valid file format
- File size: Maximum 10MB

====================================================================================

## Configuration Module - Step by Step Implementation Plan

### Phase 1: Backend Development

#### Step 1: Project Setup and Database
1. Create database and run the schema script
   - [ ] Create CredWiseDB database
   - [ ] Execute the schema script
   - [ ] Verify all tables are created correctly
   - [ ] Add initial test data for verification

#### Step 2: Core Entities Implementation
1. Create base entity class
   - [ ] Create `BaseEntity.cs` with common properties
   - [ ] Add audit fields (CreatedAt, ModifiedAt, etc.)

2. Implement Loan Product Entities
   - [ ] Create `LoanProduct.cs`
   - [ ] Create `HomeLoanDetail.cs`
   - [ ] Create `PersonalLoanDetail.cs`
   - [ ] Create `GoldLoanDetail.cs`
   - [ ] Add proper relationships and validations
   - [ ] Test entity relationships

3. Implement FD Type Entity
   - [ ] Create `FDType.cs`
   - [ ] Add validations and constraints
   - [ ] Test entity structure

4. Implement Document Entity
   - [ ] Create `LoanProductDocument.cs`
   - [ ] Add file handling properties
   - [ ] Test document entity

#### Step 3: Repository Layer
1. Create Base Repository
   - [ ] Create `IGenericRepository.cs` interface
   - [ ] Implement `GenericRepository.cs`
   - [ ] Add common CRUD operations

2. Implement Specific Repositories
   - [ ] Create `ILoanProductRepository.cs`
   - [ ] Implement `LoanProductRepository.cs`
   - [ ] Create `IFDTypeRepository.cs`
   - [ ] Implement `FDTypeRepository.cs`
   - [ ] Create `IDocumentRepository.cs`
   - [ ] Implement `DocumentRepository.cs`
   - [ ] Test all repository operations

#### Step 4: Service Layer
1. Create Base Service
   - [ ] Create `IGenericService.cs` interface
   - [ ] Implement `GenericService.cs`
   - [ ] Add common business logic

2. Implement Specific Services
   - [ ] Create `ILoanProductService.cs`
   - [ ] Implement `LoanProductService.cs`
   - [ ] Create `IFDTypeService.cs`
   - [ ] Implement `FDTypeService.cs`
   - [ ] Create `IDocumentService.cs`
   - [ ] Implement `DocumentService.cs`
   - [ ] Add business rules and validations
   - [ ] Test all service operations

#### Step 5: API Layer
1. Create Base Controller
   - [ ] Create `BaseApiController.cs`
   - [ ] Add common API functionality
   - [ ] Implement error handling

2. Implement Specific Controllers
   - [ ] Create `LoanProductController.cs`
   - [ ] Implement all CRUD endpoints
   - [ ] Create `FDTypeController.cs`
   - [ ] Implement all CRUD endpoints
   - [ ] Create `DocumentController.cs`
   - [ ] Implement document upload/download
   - [ ] Test all API endpoints

3. Add API Documentation
   - [ ] Add Swagger configuration
   - [ ] Document all endpoints
   - [ ] Add request/response examples

#### Step 6: Testing and Validation
1. Unit Tests
   - [ ] Create test project
   - [ ] Write tests for repositories
   - [ ] Write tests for services
   - [ ] Write tests for controllers

2. Integration Tests
   - [ ] Test database operations
   - [ ] Test file upload/download
   - [ ] Test API endpoints

### Phase 2: Frontend Development

#### Step 1: Project Setup
1. Create Frontend Project
   - [ ] Set up React/Angular project
   - [ ] Configure routing
   - [ ] Set up API client

#### Step 2: Component Development
1. Create Base Components
   - [ ] Create layout components
   - [ ] Create common UI components
   - [ ] Implement error handling

2. Implement Feature Components
   - [ ] Create loan product management components
   - [ ] Create FD type management components
   - [ ] Create document management components

#### Step 3: Page Development
1. Create Pages
   - [ ] Create loan product configuration page
   - [ ] Create FD type configuration page
   - [ ] Create document management page

2. Implement Features
   - [ ] Add form validations
   - [ ] Implement file upload
   - [ ] Add success/error notifications

#### Step 4: Testing and Optimization
1. Frontend Testing
   - [ ] Write unit tests
   - [ ] Test all components
   - [ ] Test all pages

2. Performance Optimization
   - [ ] Optimize API calls
   - [ ] Implement caching
   - [ ] Optimize file uploads

### Phase 3: Integration and Deployment

#### Step 1: Integration
1. Connect Frontend and Backend
   - [ ] Test all integrations
   - [ ] Fix any issues
   - [ ] Optimize performance

#### Step 2: Deployment
1. Prepare for Deployment
   - [ ] Configure production settings
   - [ ] Set up CI/CD pipeline
   - [ ] Prepare deployment documentation

2. Deploy
   - [ ] Deploy backend
   - [ ] Deploy frontend
   - [ ] Verify all functionality

### Progress Tracking
- [ ] Phase 1: Backend Development (0%)
- [ ] Phase 2: Frontend Development (0%)
- [ ] Phase 3: Integration and Deployment (0%)

### Next Steps
1. Start with Phase 1, Step 1: Project Setup and Database
2. Complete each step and mark it as done
3. Test thoroughly before moving to the next step
4. Document any issues or learnings
5. Regular commits and code reviews

====================================================================================

## Technical Implementation Details

### Project Structure
```
CredWiseAdmin/
├── src/
│   ├── CredWiseAdmin.API/                 # API Layer
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── CredWiseAdmin.Core/                # Domain Layer
│   │   ├── Entities/
│   │   ├── Interfaces/
│   │   └── DTOs/
│   ├── CredWiseAdmin.Infrastructure/      # Infrastructure Layer
│   │   ├── Data/
│   │   ├── Repositories/
│   │   └── Services/
│   └── CredWiseAdmin.Application/         # Application Layer
│       ├── Services/
│       ├── Mappings/
│       └── Validators/
├── tests/
│   ├── CredWiseAdmin.UnitTests/
│   └── CredWiseAdmin.IntegrationTests/
└── libs/                                  # Third-party DLLs
    ├── Auth.dll
    └── Logging.dll
```

### Core Entities

```csharp
// CredWiseAdmin.Core/Entities/LoanType.cs
public class LoanType : BaseEntity
{
    public string Name { get; set; }
    public decimal InterestRate { get; set; }
    public int TenureMonths { get; set; }
    public decimal MaxAmount { get; set; }
    public decimal MinAmount { get; set; }
    public bool IsActive { get; set; }
}

// CredWiseAdmin.Core/Entities/FDType.cs
public class FDType : BaseEntity
{
    public string Name { get; set; }
    public decimal InterestRate { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public int DurationMonths { get; set; }
    public bool IsActive { get; set; }
}
```

### DTOs

```csharp
// CredWiseAdmin.Core/DTOs/LoanTypeDto.cs
public class LoanTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal InterestRate { get; set; }
    public int TenureMonths { get; set; }
    public decimal MaxAmount { get; set; }
    public decimal MinAmount { get; set; }
}

// CredWiseAdmin.Core/DTOs/FDTypeDto.cs
public class FDTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal InterestRate { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public int DurationMonths { get; set; }
}
```

### Interfaces

```csharp
// CredWiseAdmin.Core/Interfaces/IAuthService.cs
public interface IAuthService
{
    Task<bool> ValidateToken(string token);
    Task<string> GenerateToken(string userId);
}

// CredWiseAdmin.Core/Interfaces/ILogger.cs
public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message, Exception ex);
    void LogWarning(string message);
}

// CredWiseAdmin.Core/Interfaces/IRepository.cs
public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

### Service Implementation

```csharp
// CredWiseAdmin.Application/Services/ConfigurationService.cs
public class ConfigurationService : IConfigurationService
{
    private readonly IRepository<LoanType> _loanTypeRepository;
    private readonly IRepository<FDType> _fdTypeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ConfigurationService(
        IRepository<LoanType> loanTypeRepository,
        IRepository<FDType> fdTypeRepository,
        IMapper mapper,
        ILogger logger)
    {
        _loanTypeRepository = loanTypeRepository;
        _fdTypeRepository = fdTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<LoanTypeDto>> GetAllLoanTypesAsync()
    {
        try
        {
            var loanTypes = await _loanTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanTypeDto>>(loanTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching loan types", ex);
            throw;
        }
    }

    // Similar implementations for other methods
}
```

### Controller Implementation

```csharp
// CredWiseAdmin.API/Controllers/ConfigurationController.cs
[ApiController]
[Route("api/config")]
[Authorize]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationService _configService;
    private readonly ILogger _logger;

    public ConfigurationController(
        IConfigurationService configService,
        ILogger logger)
    {
        _configService = configService;
        _logger = logger;
    }

    [HttpGet("loan-types")]
    public async Task<ActionResult<IEnumerable<LoanTypeDto>>> GetLoanTypes()
    {
        try
        {
            var loanTypes = await _configService.GetAllLoanTypesAsync();
            return Ok(loanTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetLoanTypes", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("loan-types")]
    public async Task<ActionResult<LoanTypeDto>> CreateLoanType(LoanTypeDto loanTypeDto)
    {
        try
        {
            var result = await _configService.CreateLoanTypeAsync(loanTypeDto);
            return CreatedAtAction(nameof(GetLoanTypes), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in CreateLoanType", ex);
            return StatusCode(500, "Internal server error");
        }
    }
}
```

### Program.cs Setup

```csharp
// CredWiseAdmin.API/Program.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add DbContext
        builder.Services.AddDbContext<CredWiseDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register third-party services
        builder.Services.AddSingleton<IAuthService>(sp => 
            new AuthService(Path.Combine(AppContext.BaseDirectory, "libs", "Auth.dll")));
        builder.Services.AddSingleton<ILogger>(sp => 
            new Logger(Path.Combine(AppContext.BaseDirectory, "libs", "Logging.dll")));

        // Register application services
        builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
        builder.Services.AddScoped<IRepository<LoanType>, Repository<LoanType>>();
        builder.Services.AddScoped<IRepository<FDType>, Repository<FDType>>();

        // Add AutoMapper
        builder.Services.AddAutoMapper(typeof(Program));

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
```

### Project File (.csproj)

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="12.0.1" />
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>

  <ItemGroup>
    <Reference Include="Auth">
      <HintPath>libs\Auth.dll</HintPath>
    </Reference>
    <Reference Include="Logging">
      <HintPath>libs\Logging.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>
```

### Unit Testing Setup

```csharp
// CredWiseAdmin.UnitTests/Services/ConfigurationServiceTests.cs
public class ConfigurationServiceTests
{
    private readonly Mock<IRepository<LoanType>> _mockLoanTypeRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger> _mockLogger;
    private readonly IConfigurationService _configService;

    public ConfigurationServiceTests()
    {
        _mockLoanTypeRepository = new Mock<IRepository<LoanType>>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger>();
        
        _configService = new ConfigurationService(
            _mockLoanTypeRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllLoanTypes_ShouldReturnMappedDtos()
    {
        // Arrange
        var loanTypes = new List<LoanType> { /* test data */ };
        var expectedDtos = new List<LoanTypeDto> { /* test data */ };

        _mockLoanTypeRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(loanTypes);
        _mockMapper.Setup(x => x.Map<IEnumerable<LoanTypeDto>>(loanTypes))
            .Returns(expectedDtos);

        // Act
        var result = await _configService.GetAllLoanTypesAsync();

        // Assert
        Assert.Equal(expectedDtos, result);
        _mockLoanTypeRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}
```

### Integration Testing Setup

```csharp
// CredWiseAdmin.IntegrationTests/Controllers/ConfigurationControllerTests.cs
public class ConfigurationControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ConfigurationControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetLoanTypes_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        // Add test data to database

        // Act
        var response = await _client.GetAsync("/api/config/loan-types");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
    }
}
```

### Best Practices Implemented

1. **Clean Architecture**
   - Separation of concerns with distinct layers
   - Dependency injection for loose coupling
   - Interface-based design for testability

2. **Security**
   - Authentication middleware
   - Input validation
   - Proper error handling

3. **Performance**
   - Async/await patterns
   - Proper repository pattern
   - Efficient database queries

4. **Maintainability**
   - Consistent coding style
   - Proper documentation
   - Unit and integration tests

5. **Error Handling**
   - Global exception handling
   - Proper logging
   - Meaningful error messages

### Next Steps

1. Implement database migrations
2. Add more comprehensive validation
3. Implement caching where appropriate
4. Add API documentation using Swagger
5. Set up CI/CD pipeline
6. Add monitoring and logging
7. Implement rate limiting
8. Add security headers -->

===============================================================
