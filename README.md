# 環友科技 C# 程式碼撰寫規範

[![CI/CD Pipeline](https://github.com/UTK-TW/C-Coding-Guideline/actions/workflows/ci.yml/badge.svg)](https://github.com/UTK-TW/C-Coding-Guideline/actions/workflows/ci.yml)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![C#](https://img.shields.io/badge/C%23-latest-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)

這是一份全面的 C# 程式碼撰寫規範，基於 **.NET 8** 和最新的 C# 語言功能，提供完整的程式碼範例和自動化 CI/CD 流程。

## 📚 主要內容

- **[完整程式碼規範](./環友科技%20C%23%20程式碼撰寫規範（C%23%20Coding%20Guideline）.md)** - 詳細的編碼標準和最佳實務
- **實用程式碼範例** - 經過測試的實際應用範例
- **自動化 CI/CD** - 確保所有範例程式碼都能正常執行

## 🚀 快速開始

### 系統需求

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更新版本
- 任何支援 C# 的 IDE（推薦 Visual Studio 2022、Visual Studio Code、JetBrains Rider）

### 執行範例

1. **克隆專案**
   ```bash
   git clone https://github.com/UTK-TW/C-Coding-Guideline.git
   cd C-Coding-Guideline
   ```

2. **建置所有專案**
   ```bash
   dotnet build
   ```

3. **執行控制台範例**
   ```bash
   cd examples/BasicExample
   dotnet run
   ```

4. **執行 Web API 範例**
   ```bash
   cd examples/WebApiExample
   dotnet run
   # 訪問 https://localhost:5001/swagger 查看 API 文件
   ```

## 📁 專案結構

```
├── 環友科技 C# 程式碼撰寫規範.md    # 完整的編碼規範文件
├── examples/                        # 程式碼範例
│   ├── BasicExample/               # 基本 C# 語法範例
│   ├── ClassLibraryExample/        # 類別庫設計範例
│   └── WebApiExample/              # Web API 開發範例
├── .github/workflows/              # CI/CD 自動化流程
├── .editorconfig                   # 編輯器設定
├── .gitignore                      # Git 忽略檔案
└── CodingGuidelineExamples.sln     # Visual Studio 解決方案
```

## 🎯 範例專案說明

### BasicExample - 基本語法範例
展示 .NET 8+ 的核心功能：
- 最上層語句 (Top-level statements)
- 記錄類型 (Record types)
- 模式比對 (Pattern matching)
- 集合表達式 (Collection expressions)
- Required 成員
- 現代異步程式設計

### ClassLibraryExample - 類別庫範例
展示類別庫設計最佳實務：
- 介面設計原則
- 防禦性程式設計
- 擴展方法
- 服務模式

### WebApiExample - Web API 範例
展示現代 Web API 開發：
- Minimal API
- 依賴注入
- 參數驗證
- 錯誤處理
- Swagger 整合

## 🔧 開發工具整合

### EditorConfig
專案包含 `.editorconfig` 檔案，提供一致的程式碼格式化設定：

```bash
# 檢查程式碼格式
dotnet format --verify-no-changes
```

### GitHub Actions
自動化 CI/CD 流程包含：
- 建置所有專案
- 執行程式碼範例
- 程式碼品質檢查
- 格式驗證

## 📖 主要特色

### ✅ .NET 8+ 最佳實務
- Primary Constructors
- Collection Expressions
- Required Members
- File-scoped Namespaces
- Global Using Statements

### ✅ 現代 C# 語法
- Record Types
- Pattern Matching
- Switch Expressions
- Nullable Reference Types
- Init-only Properties

### ✅ 安全程式設計
- 參數驗證
- 異常處理
- 防禦性程式設計
- SQL Injection 防護
- XSS 攻擊防護

### ✅ 效能最佳化
- Span<T> 和 Memory<T>
- 異步程式設計最佳實務
- 資源管理模式
- 記憶體配置最佳化

## 🤝 參與貢獻

歡迎參與改善這份程式碼規範：

1. Fork 這個專案
2. 建立你的功能分支 (`git checkout -b feature/amazing-feature`)
3. 提交你的變更 (`git commit -m 'Add some amazing feature'`)
4. 推送到分支 (`git push origin feature/amazing-feature`)
5. 開啟一個 Pull Request

## 📄 授權

這個專案基於 MIT 授權 - 查看 [LICENSE](LICENSE) 檔案了解詳情。

## 📞 聯絡資訊

如有問題或建議，請通過以下方式聯絡：

- 開啟 GitHub Issue
- 聯絡專案維護人員

---

**環友科技** - 致力於高品質的軟體開發