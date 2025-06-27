using ClassLibraryExample.Models;
using ClassLibraryExample.Services;

// 展示 .NET 8+ 的最上層語句 (Top-level statements)
// 這個範例展示了現代 C# 程式設計的最佳實務

Console.WriteLine("=== 環友科技 C# 程式碼範例 (.NET 8+) ===");

// 1. 記錄類型 (Record Types) 與 Primary Constructor
var person = new Person("張三", 30);
Console.WriteLine($"人員資訊: {person}");

// 2. 模式比對與 switch 表達式
var status = person.Age switch
{
    < 18 => "未成年",
    >= 18 and < 65 => "成年人",
    >= 65 => "老年人"
};
Console.WriteLine($"年齡狀態: {status}");

// 3. 空值檢查與條件委派呼叫
Action<string>? onMessage = msg => Console.WriteLine($"訊息: {msg}");
onMessage?.Invoke("使用條件委派呼叫");

// 4. 集合表達式 (Collection Expressions) - .NET 8 新功能
int[] numbers = [1, 2, 3, 4, 5];
List<string> names = ["Alice", "Bob", "Charlie"];

Console.WriteLine($"數字陣列: [{string.Join(", ", numbers)}]");
Console.WriteLine($"名稱清單: [{string.Join(", ", names)}]");

// 5. 使用服務類別展示依賴注入模式
var calculator = new CalculatorService();
var result = calculator.Add(10, 20);
Console.WriteLine($"計算結果: {result}");

// 6. 異步程式設計與 ConfigureAwait(false)
await ProcessDataAsync();

// 7. 使用 using 陳述式進行資源管理
using var fileStream = new MemoryStream();
Console.WriteLine("資源已自動管理");

// 8. 範圍運算子與索引
var slice = numbers[1..4]; // 取得索引 1 到 3 的元素
Console.WriteLine($"切片結果: [{string.Join(", ", slice)}]");

// 9. 元組與解構
var (min, max) = FindMinMax(numbers);
Console.WriteLine($"最小值: {min}, 最大值: {max}");

// 10. 展示 required 成員
var employee = new Employee 
{ 
    Id = 1, 
    Name = "李四", 
    Email = "lisi@example.com" 
};
Console.WriteLine($"員工: {employee}");

Console.WriteLine("\n程式執行完成。");

// 局部函式 (Local Functions) 展示
static async Task ProcessDataAsync()
{
    await Task.Delay(100);
    Console.WriteLine("異步處理完成");
}

static (int Min, int Max) FindMinMax(int[] values)
{
    return values.Length == 0 
        ? (0, 0) 
        : (values.Min(), values.Max());
}
