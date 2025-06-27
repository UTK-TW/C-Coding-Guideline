namespace ClassLibraryExample.Models;

/// <summary>
/// 展示記錄類型 (Record Types) 與 Primary Constructor
/// 記錄類型提供不可變性和值語意
/// </summary>
/// <param name="Name">人員姓名</param>
/// <param name="Age">人員年齡</param>
public record Person(string Name, int Age)
{
    // 可以添加額外的屬性和方法
    public string DisplayName => $"{Name} ({Age}歲)";
    
    // 展示驗證邏輯
    public bool IsAdult => Age >= 18;
}