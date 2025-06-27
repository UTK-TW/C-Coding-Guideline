// 展示 .NET 8+ Minimal API 的現代設計模式

var builder = WebApplication.CreateBuilder(args);

// 註冊服務 - 展示依賴注入
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 開發環境設定
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Minimal API 端點定義 - 展示現代 API 設計
app.MapGet("/", () => "環友科技 C# Web API 範例 (.NET 8+)")
   .WithTags("General");

app.MapGet("/person/{name}/{age:int}", (string name, int age) =>
{
    // 參數驗證
    if (string.IsNullOrWhiteSpace(name))
    {
        return Results.BadRequest("姓名不能為空");
    }
    
    if (age < 0 || age > 150)
    {
        return Results.BadRequest("年齡必須在 0-150 之間");
    }
    
    var person = new Person(name, age);
    return Results.Ok(person);
})
.WithTags("Person")
.WithSummary("建立人員物件")
.WithDescription("根據姓名和年齡建立人員物件");

app.MapPost("/calculate", async (CalculationRequest request, ICalculatorService calculator) =>
{
    // 展示異步操作與錯誤處理
    try
    {
        var result = request.Operation.ToLowerInvariant() switch
        {
            "add" => calculator.Add((int)request.A, (int)request.B),
            "subtract" => calculator.Subtract((int)request.A, (int)request.B),
            "divide" => (double)calculator.Divide((decimal)request.A, (decimal)request.B),
            "sqrt" => await calculator.CalculateAsync(request.A),
            _ => throw new ArgumentException($"不支援的運算: {request.Operation}")
        };
        
        return Results.Ok(new { Result = result, Operation = request.Operation });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Calculator")
.WithSummary("執行數學運算")
.WithDescription("支援加、減、除法和開根號運算");

app.MapGet("/employees", () =>
{
    // 展示集合表達式與物件初始化
    Employee[] employees = 
    [
        new() { Id = 1, Name = "張三", Email = "zhang@example.com", Department = "開發部" },
        new() { Id = 2, Name = "李四", Email = "li@example.com", Department = "測試部" },
        new() { Id = 3, Name = "王五", Email = "wang@example.com" }
    ];
    
    return Results.Ok(employees);
})
.WithTags("Employee")
.WithSummary("取得員工清單")
.WithDescription("回傳所有員工的資訊");

app.Run();

// 請求模型 - 展示記錄類型與驗證
public record CalculationRequest
{
    [Required(ErrorMessage = "運算類型為必填")]
    public required string Operation { get; init; }
    
    [Range(-1000000, 1000000, ErrorMessage = "數值 A 必須在 -1000000 到 1000000 之間")]
    public required double A { get; init; }
    
    [Range(-1000000, 1000000, ErrorMessage = "數值 B 必須在 -1000000 到 1000000 之間")]
    public required double B { get; init; }
}
