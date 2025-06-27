namespace ClassLibraryExample.Services;

/// <summary>
/// 展示服務類別的現代設計模式
/// </summary>
public interface ICalculatorService
{
    int Add(int a, int b);
    int Subtract(int a, int b);
    decimal Divide(decimal a, decimal b);
    Task<double> CalculateAsync(double value);
}

/// <summary>
/// 計算服務實作 - 展示防禦性程式設計
/// </summary>
public class CalculatorService : ICalculatorService
{
    /// <summary>
    /// 加法運算
    /// </summary>
    public int Add(int a, int b)
    {
        // 防範溢位
        try
        {
            return checked(a + b);
        }
        catch (OverflowException)
        {
            throw new InvalidOperationException($"加法運算溢位: {a} + {b}");
        }
    }
    
    /// <summary>
    /// 減法運算
    /// </summary>
    public int Subtract(int a, int b)
    {
        try
        {
            return checked(a - b);
        }
        catch (OverflowException)
        {
            throw new InvalidOperationException($"減法運算溢位: {a} - {b}");
        }
    }
    
    /// <summary>
    /// 除法運算 - 展示參數驗證
    /// </summary>
    public decimal Divide(decimal a, decimal b)
    {
        // 防禦性程式設計 - 參數驗證
        if (b == 0)
        {
            throw new ArgumentException("除數不能為零", nameof(b));
        }
        
        return a / b;
    }
    
    /// <summary>
    /// 異步計算 - 展示現代異步模式
    /// </summary>
    public async Task<double> CalculateAsync(double value)
    {
        // 模擬複雜計算
        await Task.Delay(50).ConfigureAwait(false);
        
        // 使用模式比對
        return value switch
        {
            < 0 => throw new ArgumentException("值不能為負數", nameof(value)),
            0 => 0,
            _ => Math.Sqrt(value)
        };
    }
}