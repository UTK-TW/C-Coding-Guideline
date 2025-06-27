namespace ClassLibraryExample.Models;

/// <summary>
/// 展示 required 成員與現代類別設計
/// </summary>
public class Employee
{
    /// <summary>
    /// 員工編號 - 必填欄位
    /// </summary>
    public required int Id { get; init; }
    
    /// <summary>
    /// 員工姓名 - 必填欄位
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// 電子郵件 - 必填欄位
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// 建立日期 - 自動設定
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    /// <summary>
    /// 部門 - 可選欄位
    /// </summary>
    public string? Department { get; init; }
    
    public override string ToString() => $"[{Id}] {Name} ({Email})";
}