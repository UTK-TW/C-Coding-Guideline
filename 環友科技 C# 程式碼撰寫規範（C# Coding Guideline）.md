# 環友科技 C# 程式碼撰寫規範（C# Coding Guideline）

這份文件旨在提供一個全面的 C# 程式碼撰寫指南，協助開發團隊撰寫出 **一致**、**易於維護**且**安全**的程式碼。遵循這些準則能夠提升程式碼品質，並確保團隊協作的效率。

> **更新說明**：本規範已更新至 **.NET 8** 標準，包含最新的 C# 語言功能和最佳實務。所有範例程式碼都經過實際測試，並提供完整的 CI/CD 自動化流程。

## 🚀 快速開始

本規範提供完整的範例專案，位於 `examples/` 目錄：

- **BasicExample**: 展示基本 C# 語法和 .NET 8 新功能
- **WebApiExample**: 展示 Minimal API 和現代 Web 開發模式  
- **ClassLibraryExample**: 展示類別庫設計最佳實務

執行範例：
```bash
# 建置所有專案
dotnet build

# 執行控制台範例
cd examples/BasicExample && dotnet run

# 執行 Web API 範例
cd examples/WebApiExample && dotnet run
```

## 1. 通則

* **目標框架**：使用 **.NET 8** 或更新版本，啟用最新 C# 語言版本。  
  ```xml
  <Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
      <TargetFramework>net8.0</TargetFramework>
      <LangVersion>latest</LangVersion>
      <Nullable>enable</Nullable>
      <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
  </Project>
  ```

* **縮排**：使用 **空格** 進行縮排，每層 **4 個空格**。  
  讓程式碼整齊易讀，避免用 Tab 縮排造成對齊不一致。

* **字元編碼**：所有檔案應使用 **UTF-8-BOM** 字元編碼。  
  確保專案（包含程式碼、文字檔等）內各種字元能正確顯示，特別適合包含中文的專案。

* **檔案結尾換行**：檔案結尾**不**需要插入新的一行。  
  避免多餘空行，利於版本控管時差異比對。

* **Nullable 參考型別**：專案中應啟用 nullable 參考型別檢查。  
  提升程式碼安全性，減少 null 參考例外。

* **隱含 using**：啟用隱含 using 指示詞，減少重複程式碼。  
  使用 `global using` 管理全域命名空間引用。

---

## 2. 程式碼結構

### 2.1. using 指示詞

* **全域 using**：使用 `global using` 統一管理常用命名空間。
  ```csharp
  // GlobalUsings.cs
  global using System.Text.Json;
  global using Microsoft.Extensions.DependencyInjection;
  ```

* **排序**：
  1. 全域 `using` 指示詞放在獨立檔案（`GlobalUsings.cs`）  
  2. 系統命名空間（`System.*`）排在最前面  
  3. 不同的 `using` 區塊以空白行分隔

* **位置**：`using` 指示詞應放在命名空間之外。  
  確保作用範圍涵蓋整個檔案並符合通用程式碼風格。

* **簡化宣告**：建議使用簡化的 `using` 宣告形式。
  ```csharp
  // ✅ 推薦：簡化 using 宣告
  using var fileStream = new FileStream("data.txt", FileMode.Open);
  // 自動處理資源釋放
  
  // ❌ 避免：傳統 using 區塊（除非有複雜邏輯）
  using (var fileStream = new FileStream("data.txt", FileMode.Open))
  {
      // ...
  }
  ```

### 2.2. 命名空間

* **File-scoped namespace**：強制使用檔案範圍命名空間宣告。
  ```csharp
  // ✅ 推薦：File-scoped namespace
  namespace MyCompany.MyProject.Services;
  
  public class UserService
  {
      // 類別內容
  }
  ```
  
  ```csharp
  // ❌ 避免：傳統命名空間區塊
  namespace MyCompany.MyProject.Services
  {
      public class UserService
      {
          // 類別內容
      }
  }
  ```

* **資料夾結構一致**：命名空間名稱盡量與資料夾路徑對應，方便管理與維護。

### 2.3. 最上層語句 (Top-level statements)

* **適用場景**：適合程式碼邏輯簡單的控制台應用程式。
  ```csharp
  // Program.cs - 展示最上層語句
  using MyLibrary.Services;
  
  Console.WriteLine("應用程式啟動");
  
  var service = new CalculatorService();
  var result = service.Add(10, 20);
  Console.WriteLine($"結果: {result}");
  
  await ProcessDataAsync();
  
  // 局部函式
  static async Task ProcessDataAsync()
  {
      await Task.Delay(1000);
      Console.WriteLine("處理完成");
  }
  ```

---

## 3. 現代 C# 語法與最佳實務

### 3.1. 記錄類型 (Record Types)

* **使用情境**：數據傳輸物件、不可變模型、值物件。
  ```csharp
  // ✅ 推薦：記錄類型與 Primary Constructor
  public record Person(string Name, int Age)
  {
      public string DisplayName => $"{Name} ({Age}歲)";
      public bool IsAdult => Age >= 18;
  }
  
  // 使用範例
  var person = new Person("張三", 30);
  var updated = person with { Age = 31 }; // 非破壞性更新
  ```

### 3.2. Required 成員

* **強制初始化**：使用 `required` 確保重要屬性必須在建立時設定。
  ```csharp
  public class Employee
  {
      public required int Id { get; init; }
      public required string Name { get; init; }
      public required string Email { get; init; }
      public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
      public string? Department { get; init; }
  }
  
  // 使用時必須提供 required 屬性
  var employee = new Employee 
  { 
      Id = 1, 
      Name = "李四", 
      Email = "li@example.com" 
  };
  ```

### 3.3. 集合表達式 (Collection Expressions)

* **.NET 8 新功能**：簡化集合初始化語法。
  ```csharp
  // ✅ 推薦：集合表達式
  int[] numbers = [1, 2, 3, 4, 5];
  List<string> names = ["Alice", "Bob", "Charlie"];
  Dictionary<string, int> scores = new() { ["Alice"] = 95, ["Bob"] = 87 };
  
  // 展開運算子
  int[] moreNumbers = [..numbers, 6, 7, 8];
  ```

### 3.4. Primary Constructor

* **類別簡化**：適用於簡單的依賴注入和參數傳遞。
  ```csharp
  // ✅ 推薦：Primary Constructor
  public class OrderService(IRepository repository, ILogger<OrderService> logger)
  {
      public async Task<Order?> GetOrderAsync(int id)
      {
          logger.LogInformation("取得訂單 {OrderId}", id);
          return await repository.GetByIdAsync<Order>(id);
      }
  }
  ```

### 3.5. 模式比對與 Switch 表達式

* **強化的模式比對**：使用現代模式比對語法。
  ```csharp
  // ✅ 推薦：Switch 表達式
  public string GetAgeCategory(int age) => age switch
  {
      < 13 => "兒童",
      >= 13 and < 20 => "青少年", 
      >= 20 and < 65 => "成年人",
      >= 65 => "老年人"
  };
  
  // 屬性模式
  public decimal GetDiscount(Customer customer) => customer switch
  {
      { Type: CustomerType.Premium, YearsActive: > 5 } => 0.2m,
      { Type: CustomerType.Premium } => 0.1m,
      { YearsActive: > 10 } => 0.05m,
      _ => 0m
  };
  ```

### 3.6. 關鍵字與型別

* 內建型別請使用 C# 關鍵字（`int` 而非 `System.Int32`）。
* 本地變數、參數、成員存取時統一使用預先定義的型別關鍵字。

```csharp
// ✅ 推薦：使用 C# 關鍵字
int count = 10;
string message = "Hello";
bool isValid = true;
decimal price = 99.99m;

// ❌ 避免：使用 .NET 型別名稱
Int32 count = 10;
String message = "Hello";
Boolean isValid = true;
Decimal price = 99.99m;
```

### 3.7. `this` 關鍵字

* **不**強制使用 `this.` 來存取事件、欄位、方法或屬性，除非需要解決命名衝突。

```csharp
public class UserService
{
    private readonly IRepository _repository;
    
    public UserService(IRepository repository)
    {
        _repository = repository; // ✅ 不需要 this.
    }
    
    public void ProcessUser(string name)
    {
        // ✅ 只在需要解決衝突時使用 this
        this.name = name; // 假設有欄位 name
    }
}
```

### 3.8. 括號使用

* **必加括號**：在算術、關係等二元運算子中，加上括號區分運算順序。  
* **非必要不加括號**：避免視覺雜訊，盡量精簡。

```csharp
// ✅ 推薦：清楚的運算優先順序
var result = (a + b) * (c - d);
var isValid = (age >= 18) && (hasPermission || isAdmin);

// ❌ 避免：不必要的括號
var simple = (x);
var basic = (GetValue());
```

### 3.9. 修飾詞順序

* 非介面成員，**總是**加上存取修飾詞（`public`、`private`、`protected`…）。
* 修飾詞順序建議：

```csharp
// 正確的修飾詞順序
public static readonly string DefaultValue = "test";
private static readonly ILogger s_logger = LoggerFactory.Create();
public virtual async Task<string> ProcessAsync();
protected internal sealed override void HandleEvent();
```

### 3.10. 表達式層級偏好

#### 空值處理
```csharp
// ✅ 推薦：使用 null 條件運算子
var length = text?.Length ?? 0;
customer?.UpdateLastAccess();

// ✅ 推薦：使用 is null 檢查
if (value is null) return;
if (value is not null) Process(value);
```

#### 集合與物件初始化
```csharp
// ✅ 推薦：集合初始化器與表達式
List<string> items = ["apple", "banana", "cherry"];
Dictionary<string, int> scores = new() 
{ 
    ["Alice"] = 95, 
    ["Bob"] = 87 
};

// ✅ 推薦：物件初始化器
var person = new Person 
{ 
    Name = "John", 
    Age = 30,
    Address = new Address { City = "台北", ZipCode = "100" }
};
```

#### 元組與解構
```csharp
// ✅ 推薦：明確的元組名稱
(string FirstName, string LastName) GetFullName() => ("John", "Doe");

// ✅ 推薦：解構賦值
var (first, last) = GetFullName();
var (x, y) = GetCoordinates();

// ✅ 推薦：元組交換
(a, b) = (b, a);
```

#### 條件運算與模式比對
```csharp
// ✅ 推薦：條件運算子用於簡單賦值
var status = age >= 18 ? "成年" : "未成年";

// ✅ 推薦：Switch 表達式用於複雜邏輯
var category = customer.Type switch
{
    CustomerType.Premium => "白金會員",
    CustomerType.Gold => "金卡會員", 
    CustomerType.Silver => "銀卡會員",
    _ => "一般會員"
};
```

## 4. 異步程式設計最佳實務

### 4.1. Async/Await 模式

```csharp
// ✅ 推薦：正確的異步方法命名和實作
public async Task<User> GetUserAsync(int id)
{
    var user = await _repository.GetByIdAsync(id).ConfigureAwait(false);
    return user ?? throw new UserNotFoundException(id);
}

// ✅ 推薦：ValueTask 用於可能同步完成的操作
public async ValueTask<bool> ValidateAsync(string input)
{
    if (string.IsNullOrEmpty(input))
        return false;
        
    return await _validator.IsValidAsync(input).ConfigureAwait(false);
}
```

### 4.2. ConfigureAwait 使用

```csharp
// ✅ 在類別庫中使用 ConfigureAwait(false)
public async Task ProcessAsync()
{
    var data = await GetDataAsync().ConfigureAwait(false);
    await SaveDataAsync(data).ConfigureAwait(false);
}

// ✅ 在 UI 應用中可省略 ConfigureAwait
private async void Button_Click(object sender, EventArgs e)
{
    var result = await ProcessUserInputAsync(); // UI context needed
    DisplayResult(result);
}
```

### 4.3. 異步列舉 (IAsyncEnumerable)

```csharp
// ✅ 推薦：異步流處理大量資料
public async IAsyncEnumerable<Order> GetOrdersAsync(
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
{
    var page = 1;
    List<Order> orders;
    
    do
    {
        orders = await _repository.GetOrdersPageAsync(page, 100, cancellationToken);
        
        foreach (var order in orders)
        {
            yield return order;
        }
        
        page++;
    } while (orders.Count > 0);
}
```

## 5. 效能最佳化指南

### 5.1. Span<T> 和 Memory<T>

```csharp
// ✅ 推薦：使用 Span<T> 避免記憶體分配
public static int CountVowels(ReadOnlySpan<char> text)
{
    const string vowels = "aeiouAEIOU";
    var count = 0;
    
    foreach (var ch in text)
    {
        if (vowels.Contains(ch))
            count++;
    }
    
    return count;
}

// 使用範例
var text = "Hello World";
var vowelCount = CountVowels(text.AsSpan());
```

### 5.2. 字串插值與格式化

```csharp
// ✅ 推薦：使用字串插值
var message = $"用戶 {userName} 於 {DateTime.Now:yyyy-MM-dd} 登入";

// ✅ 推薦：複雜格式化使用 StringBuilder
var sb = new StringBuilder();
foreach (var item in items)
{
    sb.AppendLine($"項目：{item.Name}，價格：{item.Price:C}");
}
var result = sb.ToString();
```

### 5.3. 例外處理最佳化

```csharp
// ✅ 推薦：使用 TryParse 避免例外
public bool TryParseId(string input, out int id)
{
    return int.TryParse(input, out id) && id > 0;
}

// ✅ 推薦：特定例外處理
public async Task<User> GetUserSafelyAsync(int id)
{
    try
    {
        return await _repository.GetUserAsync(id);
    }
    catch (UserNotFoundException)
    {
        return CreateGuestUser();
    }
    catch (DatabaseConnectionException ex)
    {
        _logger.LogError(ex, "資料庫連線失敗");
        throw;
    }
}
```

---

## 6. 格式化規則

### 6.1. 換行規則

* `catch` / `else` / `finally` **前**應有換行。
* 匿名型別 / 物件初始化器成員之間應換行。
* 左大括號 `{` 前應有換行。
* 查詢表達式子句之間換行。

```csharp
// ✅ 推薦：正確的換行格式
if (condition)
{
    DoSomething();
}
else
{
    DoSomethingElse();
}

try
{
    RiskyOperation();
}
catch (SpecificException ex)
{
    HandleException(ex);
}
finally
{
    Cleanup();
}
```

### 6.2. 縮排規則

* 區塊內容需縮排（4 個空格）。
* 大括號 `{}` 與控制語句平齊。
* `case` 內容及其區塊 `{}` 需縮排。
* `label` 比當前程式碼層縮排少一層。

```csharp
// ✅ 推薦：正確的縮排
switch (value)
{
    case 1:
        ProcessOne();
        break;
    case 2:
        {
            var temp = ProcessTwo();
            return temp;
        }
    default:
        ProcessDefault();
        break;
}
```

### 6.3. 空格規則

```csharp
// ✅ 推薦：正確的空格使用

// 轉型後不留空格
var number = (int)value;

// 繼承子句前後要空格
public class Child : Parent, IInterface

// 逗號後要空格，逗號前不要空格
Method(param1, param2, param3);

// 控制流程關鍵字後要空格
if (condition) { }
for (int i = 0; i < count; i++) { }
while (isRunning) { }

// 二元運算子前後要空格
var result = a + b * c;
var isValid = x > 0 && y < 100;

// 方法呼叫括號內不留空格
CallMethod(arg1, arg2);
var item = array[index];
```

### 6.4. 單行區塊與語句

* 若能放在單行，請保留單行，不要強制展開或換行。

```csharp
// ✅ 推薦：簡單語句保持單行
if (isValid) return true;

// ✅ 推薦：簡單屬性單行定義
public string Name { get; set; }
public bool IsActive => _status == Status.Active;

// ✅ 推薦：簡單 lambda 單行
items.Where(x => x.IsValid).ToList();
```

---

## 7. 命名規則與範例

### 7.1. 命名慣例總覽

| 項目 | 命名範例 | 說明 |
|------|----------|------|
| **型別 & 命名空間** | `UserService` / `MyCompany.Services` | PascalCase |
| **介面** | `IUserRepository` | I + PascalCase |
| **型別參數** | `TEntity`, `TKey` | T + PascalCase |
| **方法** | `GetUserAsync()` | PascalCase |
| **屬性** | `UserName` | PascalCase |
| **事件** | `UserLoggedIn` | PascalCase |
| **本地變數** | `userName` | camelCase |
| **參數** | `userId` | camelCase |
| **公開欄位** | `MaxRetryCount` | PascalCase |
| **私有欄位** | `_repository` | _camelCase |
| **私有靜態欄位** | `s_logger` | s_camelCase |
| **常數** | `DefaultTimeout` | PascalCase |
| **列舉** | `OrderStatus` | PascalCase |

### 7.2. 實際命名範例

#### 介面與實作
```csharp
// ✅ 推薦：清楚的介面命名
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetActiveUsersAsync();
}

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;
    private static readonly ILogger s_logger = LoggerFactory.Create();
    
    // 實作細節...
}
```

#### 方法命名
```csharp
// ✅ 推薦：清楚表達意圖的方法名
public async Task<bool> TryAuthenticateUserAsync(string email, string password)
public void ValidateUserInput(UserInput input)
public User CreateUserFromRequest(CreateUserRequest request)
public async Task NotifyUserRegistrationAsync(User user)
```

#### 變數與參數命名
```csharp
// ✅ 推薦：描述性的變數名
public async Task ProcessOrderAsync(int orderId, bool sendNotification = true)
{
    var existingOrder = await _repository.GetOrderAsync(orderId);
    var customerEmail = existingOrder.Customer.Email;
    var orderTotal = existingOrder.Items.Sum(item => item.Price * item.Quantity);
    
    if (sendNotification)
    {
        await _emailService.SendOrderConfirmationAsync(customerEmail, existingOrder);
    }
}
```

#### 常數與列舉
```csharp
// ✅ 推薦：清楚的常數定義
public static class ValidationConstants
{
    public const int MaxUserNameLength = 50;
    public const int MinPasswordLength = 8;
    public const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
}

// ✅ 推薦：描述性的列舉
public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1, 
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Refunded = 6
}
```

### 7.3. 泛型型別參數命名

```csharp
// ✅ 推薦：描述性的型別參數
public interface IRepository<TEntity, TKey> 
    where TEntity : class
    where TKey : struct
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}

public class BaseService<TEntity, TDto, TKey>
    where TEntity : BaseEntity<TKey>
    where TDto : BaseDto
    where TKey : struct
{
    // 服務實作
}
```

---

## 9. 現代 Web API 開發

### 9.1. Minimal API 設計

```csharp
// Program.cs - 展示 .NET 8+ Minimal API
var builder = WebApplication.CreateBuilder(args);

// 服務註冊
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// 開發環境配置
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// API 端點群組
var usersApi = app.MapGroup("/api/users")
    .WithTags("Users")
    .WithOpenApi();

usersApi.MapGet("/", GetAllUsers)
    .WithSummary("取得所有用戶")
    .Produces<List<UserDto>>();

usersApi.MapGet("/{id:int}", GetUser)
    .WithSummary("根據 ID 取得用戶")
    .Produces<UserDto>()
    .Produces(404);

usersApi.MapPost("/", CreateUser)
    .WithSummary("建立新用戶")
    .Accepts<CreateUserRequest>("application/json")
    .Produces<UserDto>(201)
    .Produces<ValidationProblemDetails>(400);

app.Run();

// 端點處理函式
static async Task<IResult> GetAllUsers(IUserService userService)
{
    var users = await userService.GetAllUsersAsync();
    return Results.Ok(users);
}

static async Task<IResult> GetUser(int id, IUserService userService)
{
    var user = await userService.GetUserByIdAsync(id);
    return user != null ? Results.Ok(user) : Results.NotFound();
}

static async Task<IResult> CreateUser(
    CreateUserRequest request, 
    IUserService userService,
    IValidator<CreateUserRequest> validator)
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    
    var user = await userService.CreateUserAsync(request);
    return Results.Created($"/api/users/{user.Id}", user);
}
```

### 9.2. 依賴注入最佳實務

```csharp
// ✅ 推薦：服務註冊模式
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 資料存取層
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        // Repository 模式
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        // 服務層
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrderService, OrderService>();
        
        // 外部服務
        services.AddHttpClient<IExternalApiService, ExternalApiService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalApi:BaseUrl"]);
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        
        return services;
    }
}
```

## 10. 測試驅動開發

### 10.1. 單元測試範例

```csharp
// ✅ 推薦：全面的單元測試
[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _mockRepository;
    private Mock<ILogger<UserService>> _mockLogger;
    private UserService _userService;
    
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UserService>>();
        _userService = new UserService(_mockRepository.Object, _mockLogger.Object);
    }
    
    [Test]
    public async Task GetUserAsync_ValidId_ReturnsUser()
    {
        // Arrange
        var userId = 1;
        var expectedUser = new User { Id = userId, Name = "測試用戶" };
        _mockRepository.Setup(r => r.GetByIdAsync(userId))
                      .ReturnsAsync(expectedUser);
        
        // Act
        var result = await _userService.GetUserAsync(userId);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(userId));
        Assert.That(result.Name, Is.EqualTo("測試用戶"));
    }
    
    [Test]
    public async Task GetUserAsync_InvalidId_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _userService.GetUserAsync(-1));
        
        Assert.That(exception.Message, Does.Contain("用戶 ID 必須大於 0"));
    }
    
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    public async Task GetUserAsync_InvalidIds_ThrowsArgumentException(int invalidId)
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _userService.GetUserAsync(invalidId));
    }
}
```

### 10.2. 整合測試

```csharp
// ✅ 推薦：Web API 整合測試
[TestFixture]
public class UserApiIntegrationTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    
    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // 使用測試資料庫
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("TestDb"));
                });
            });
        
        _client = _factory.CreateClient();
    }
    
    [Test]
    public async Task GetUsers_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/users");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(response.Content.Headers.ContentType?.ToString(), 
                   Does.Contain("application/json"));
    }
    
    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }
}
```

## 11. 其他最佳實務

### 11.1. 設定管理

```csharp
// ✅ 推薦：強型別設定
public class ApiSettings
{
    public const string SectionName = "ApiSettings";
    
    public required string BaseUrl { get; init; }
    public required string ApiKey { get; init; }
    public int TimeoutSeconds { get; init; } = 30;
    public bool EnableRetry { get; init; } = true;
}

// 註冊設定
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection(ApiSettings.SectionName));

// 使用設定
public class ExternalApiService(IOptions<ApiSettings> options)
{
    private readonly ApiSettings _settings = options.Value;
    
    public async Task<string> CallApiAsync()
    {
        // 使用 _settings.BaseUrl, _settings.ApiKey 等
    }
}
```

### 11.2. 健康檢查

```csharp
// ✅ 推薦：健康檢查實作
builder.Services.AddHealthChecks()
    .AddDbContext<AppDbContext>()
    .AddUrlGroup(new Uri("https://api.example.com/health"), "External API");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```

## 12. 總結

* **適用範圍**：本規範適用於所有使用 .NET 8+ 的 C# 專案。  
* **持續改進**：隨著 .NET 和 C# 語言的發展，本規範將持續更新。  
* **團隊協作**：對於規範不清楚之處，請與團隊成員討論並建立共識。  
* **自動化**：利用 CI/CD 流程確保程式碼品質和規範遵循。

### 12.1. 檢查清單

在開發過程中，請確認以下項目：

- [ ] 使用 .NET 8+ 和最新 C# 語言功能
- [ ] 啟用 nullable 參考型別和隱含 using
- [ ] 使用 file-scoped namespace
- [ ] 遵循命名規範（PascalCase、camelCase、_camelCase）
- [ ] 實作適當的參數驗證和異常處理
- [ ] 使用異步模式和 ConfigureAwait(false)
- [ ] 避免 SQL Injection 和 XSS 攻擊
- [ ] 不在程式碼中硬編碼憑證
- [ ] 撰寫單元測試和整合測試
- [ ] 設定 CI/CD 自動化流程

### 12.2. 資源連結

- [.NET 8 官方文件](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [C# 程式設計指南](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [ASP.NET Core 最佳實務](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/best-practices)
- [安全程式設計指南](https://docs.microsoft.com/en-us/dotnet/standard/security/)

---

**環友科技** - 致力於高品質的軟體開發與現代化技術應用

---

## 8. 防禦性程式設計與安全措施

在程式開發中，安全性與防禦性設計是不可或缺的考量。以下為建議的安全作法：

### 8.1. 防禦性程式設計

#### 參數驗證
```csharp
// ✅ 推薦：完整的參數驗證
public async Task<User> CreateUserAsync(CreateUserRequest request)
{
    // .NET 8+ 的參數驗證
    ArgumentNullException.ThrowIfNull(request);
    ArgumentException.ThrowIfNullOrWhiteSpace(request.Email, nameof(request.Email));
    
    // 商業邏輯驗證
    if (request.Age < 0 || request.Age > 150)
    {
        throw new ArgumentOutOfRangeException(nameof(request.Age), "年齡必須在 0-150 之間");
    }
    
    // 檢查重複
    var existingUser = await _repository.GetByEmailAsync(request.Email);
    if (existingUser != null)
    {
        throw new InvalidOperationException($"Email {request.Email} 已被使用");
    }
    
    return new User { /* ... */ };
}
```

#### 異常處理最佳實務
```csharp
// ✅ 推薦：分層異常處理
public class UserService
{
    private readonly ILogger<UserService> _logger;
    
    public async Task<Result<User>> GetUserSafelyAsync(int userId)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
            
            var user = await _repository.GetUserAsync(userId);
            return Result.Success(user);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "無效的用戶 ID: {UserId}", userId);
            return Result.Failure<User>("無效的用戶 ID");
        }
        catch (DatabaseException ex)
        {
            _logger.LogError(ex, "取得用戶時發生資料庫錯誤: {UserId}", userId);
            return Result.Failure<User>("系統暫時無法存取用戶資料");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "取得用戶時發生未預期錯誤: {UserId}", userId);
            return Result.Failure<User>("系統發生錯誤");
        }
    }
}

// Result 模式範例
public readonly record struct Result<T>(bool IsSuccess, T? Value, string? Error)
{
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}
```

### 8.2. 避免 SQL Injection

#### 參數化查詢
```csharp
// ✅ 推薦：使用參數化查詢
public async Task<User?> GetUserByEmailAsync(string email)
{
    const string sql = "SELECT * FROM Users WHERE Email = @Email";
    
    using var connection = _connectionFactory.CreateConnection();
    return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
}

// ✅ 推薦：Entity Framework 的 LINQ 查詢
public async Task<List<User>> SearchUsersAsync(string searchTerm)
{
    return await _context.Users
        .Where(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm))
        .ToListAsync();
}

// ❌ 危險：字串拼接 SQL
public async Task<User> GetUserUnsafe(string email)
{
    // 永遠不要這樣做！
    var sql = $"SELECT * FROM Users WHERE Email = '{email}'";
    // 這會導致 SQL Injection 攻擊
}
```

### 8.3. 避免 XSS（跨站腳本攻擊）

#### 輸出編碼
```csharp
// ✅ 推薦：Web API 自動處理 JSON 編碼
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
            return NotFound();
            
        // JSON 序列化會自動進行適當的編碼
        return Ok(user);
    }
}

// ✅ 推薦：Razor 頁面自動編碼
// 在 .cshtml 中
@Model.UserName // 自動進行 HTML 編碼

// ❌ 危險：使用 Html.Raw
@Html.Raw(Model.UserInput) // 可能導致 XSS 攻擊
```

#### 輸入驗證
```csharp
// ✅ 推薦：輸入驗證與清理
public class InputSanitizer
{
    private static readonly Regex s_htmlTagPattern = new(@"<[^>]*>");
    
    public static string SanitizeInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
            
        // 移除 HTML 標籤
        var sanitized = s_htmlTagPattern.Replace(input, string.Empty);
        
        // 移除潛在危險字符
        sanitized = sanitized.Replace("<script", string.Empty, StringComparison.OrdinalIgnoreCase);
        sanitized = sanitized.Replace("javascript:", string.Empty, StringComparison.OrdinalIgnoreCase);
        
        return sanitized.Trim();
    }
}
```

### 8.4. 密碼與憑證管理

#### 密碼雜湊
```csharp
// ✅ 推薦：使用 BCrypt 進行密碼雜湊
public class PasswordService
{
    public string HashPassword(string password)
    {
        // 使用適當的工作因子（推薦 12 或更高）
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
```

#### 憑證管理
```csharp
// ✅ 推薦：使用 IConfiguration 和 Azure Key Vault
public class ApiService
{
    private readonly string _apiKey;
    
    public ApiService(IConfiguration configuration)
    {
        // 從安全儲存讀取憑證
        _apiKey = configuration["ExternalApi:ApiKey"] 
                  ?? throw new InvalidOperationException("API Key 未設定");
    }
    
    // ❌ 永遠不要在程式碼中硬編碼憑證
    // private const string ApiKey = "sk-1234567890abcdef"; 
}
```

### 8.5. 日誌與監控

#### 安全日誌記錄
```csharp
// ✅ 推薦：安全的日誌記錄
public class SecurityAuditService
{
    private readonly ILogger<SecurityAuditService> _logger;
    
    public void LogSuccessfulLogin(string userId, string ipAddress)
    {
        _logger.LogInformation("用戶 {UserId} 從 {IpAddress} 成功登入", 
            userId, ipAddress);
    }
    
    public void LogFailedLogin(string attemptedEmail, string ipAddress)
    {
        // 注意：不記錄完整的密碼或敏感資訊
        _logger.LogWarning("登入失敗 - Email: {Email}, IP: {IpAddress}", 
            MaskEmail(attemptedEmail), ipAddress);
    }
    
    private static string MaskEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return "未知";
        
        var atIndex = email.IndexOf('@');
        if (atIndex <= 0) return "***";
        
        return email[0] + "***" + email.Substring(atIndex);
    }
}
```

### 8.6. 其他安全建議

#### API 安全
```csharp
// ✅ 推薦：API 限流和驗證
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    [Authorize] // 要求身份驗證
    [ValidateAntiForgeryToken] // CSRF 保護
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserRequest request)
    {
        // 實作邏輯
    }
    
    [HttpGet]
    [ResponseCache(Duration = 300)] // 快取控制
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        // 實作邏輯
    }
}
```

#### 更新與維護
```csharp
// 在 csproj 中設定自動安全更新檢查
/*
<PropertyGroup>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  <WarningsAsErrors />
  <EnableNETAnalyzers>true</EnableNETAnalyzers>
  <AnalysisLevel>latest</AnalysisLevel>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="Microsoft.CodeAnalysis.NetAnalyzers" Version="8.0.0">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
  </PackageReference>
</ItemGroup>
*/
```
