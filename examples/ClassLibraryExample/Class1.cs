namespace ClassLibraryExample;

/// <summary>
/// 展示靜態類別與擴展方法
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 檢查字串是否為空值或空白
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? value) 
        => string.IsNullOrWhiteSpace(value);
    
    /// <summary>
    /// 安全地擷取字串子字串
    /// </summary>
    public static string SafeSubstring(this string value, int startIndex, int length)
    {
        // 防禦性程式設計
        ArgumentNullException.ThrowIfNull(value);
        
        if (startIndex < 0 || startIndex >= value.Length)
        {
            return string.Empty;
        }
        
        var actualLength = Math.Min(length, value.Length - startIndex);
        return value.Substring(startIndex, actualLength);
    }
}
