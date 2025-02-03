# 環友科技 C# 程式碼撰寫規範（C# Coding Guideline）

這份文件旨在提供一個全面的 C# 程式碼撰寫指南，協助開發團隊撰寫出 **一致**、**易於維護**且**安全**的程式碼。遵循這些準則能夠提升程式碼品質，並確保團隊協作的效率。

## 1. 通則

* **縮排**：使用 **空格** 進行縮排，每層 **4 個空格**。  
  讓程式碼整齊易讀，避免用 Tab 縮排造成對齊不一致。

* **字元編碼**：所有檔案應使用 **UTF-8-BOM** 字元編碼。  
  確保專案（包含程式碼、文字檔等）內各種字元能正確顯示，特別適合包含中文的專案。

* **檔案結尾換行**：檔案結尾**不**需要插入新的一行。  
  避免多餘空行，利於版本控管時差異比對。

---

## 2. 程式碼結構

### 2.1. using 指示詞

* **排序**：
  1. 系統命名空間（`System.*`）排在最前面。  
  2. 不同的 `using` 區塊以空白行分隔。

* **位置**：`using` 指示詞應放在命名空間之外。  
  確保作用範圍涵蓋整個檔案並符合通用程式碼風格。

* **簡化宣告**：建議使用簡化的 `using` 宣告形式（如 `using var foo = ...;`）以使程式碼更精簡。

### 2.2. 命名空間

* **file-scoped namespace**：
  ```csharp
  namespace MyNamespace;
  ```
  此宣告方式更清爽且明確表示命名空間範圍。

* **資料夾結構一致**：命名空間名稱盡量與資料夾路徑對應，方便管理與維護。

---

## 3. 程式碼樣式

### 3.1. 關鍵字與型別

* 內建型別請使用 C# 關鍵字（`int` 而非 `System.Int32`）。
* 本地變數、參數、成員存取時統一使用預先定義的型別關鍵字。

### 3.2. `this`

* **不**強制使用 `this.` 來存取事件、欄位、方法或屬性，除非需要解決命名衝突。

### 3.3. 括號

* **必加括號**：  
  - 在算術、關係等二元運算子中，加上括號區分運算順序。  
* **非必要不加括號**：  
  - 避免視覺雜訊，盡量精簡。

### 3.4. 修飾詞

* 非介面成員，**總是**加上存取修飾詞（`public`、`private`、`protected`…）。
* 修飾詞順序建議：
  ```
  public, private, protected, internal, file, const, static, extern, new,
  virtual, abstract, sealed, override, readonly, unsafe, required, volatile, async
  ```

### 3.5. 表達式層級偏好

* **使用 `??` 空合併運算子**
* **使用集合初始器**  
  ```csharp
  var list = new List<int> { 1, 2, 3 };
  ```
* **使用明確的元組名稱**  
  讓元組內部成員易於識別。
* **使用空值傳播運算子 `?.`**
* **使用物件初始化器**  
  ```csharp
  var person = new Person { Name = "John", Age = 30 };
  ```
* **運算子若需換行，將運算子置於行首**
* **優先使用自動屬性**  
  ```csharp
  public int Age { get; set; }
  ```
* **型別鬆散匹配時可使用集合表達式**
* **使用複合賦值運算子（`+=`, `-=`）**
* **條件賦值或回傳時，善用條件運算子 (`?:`)**  
* **foreach 迴圈中，若型別明確可使用明確型別宣告**
* **推斷匿名型別成員名稱與元組名稱**
* **使用 `is null`** 檢查空值
* **簡化布林表達式**  
  如 `if (x)` 而非 `if (x == true)`
* **簡化插值字串**  
  `$"{x}"` 而非 `$"{x.ToString()}"`

### 3.6. 欄位

* 若欄位初始化後不再改變，建議使用 `readonly`，避免意外改動。

### 3.7. 參數

* 移除未使用參數，避免混淆或導致警告。

### 3.8. 變數宣告

* 區域變數可以使用 `var`，但**不**針對內建型別使用。  
* 只有在型別明顯可推斷時才用 `var`。

### 3.9. 表達式主體成員

* **可**使用表達式主體：  
  - 定義存取子、索引子、lambda、簡單屬性。  
* **不**使用表達式主體：  
  - 建構子、局部函式、方法、運算子若相對複雜時。

### 3.10. 模式比對

* **優先使用模式比對**（取代 `as` + null 檢查 / `is` + cast）。
* **擴展屬性模式**、**`not` 模式**、**switch 表達式** 同樣能使邏輯更簡潔。

### 3.11. 空值檢查

* **使用條件委派呼叫 `?.Invoke`**  
  例如：
  ```csharp
  MyEvent?.Invoke(this, EventArgs.Empty);
  ```

### 3.12. 其他

* **匿名函式 / 局部函式**  
  - 不用外部變數時請宣告為 `static`。
* **結構體 (`struct`)**  
  - 若初始化後不變，建議標示為 `readonly struct`，並將成員也標示 `readonly`。
* **使用大括號 `{}`**  
  - `if`, `for`, `while`, `switch` 等控制流程都要有大括號。
* **使用簡單 `using`**  
  - 只需釋放資源時，保持簡潔。
* **主要建構子 (Primary Constructor)**  
  - 若類別/結構只有單一建構子，可用 Primary Constructor。
* **最上層語句 (Top-level statements)**  
  - 適用於程式碼邏輯簡單的應用。
* **簡單 `default` 表達式**  
  - 可用 `default` 取代 `default(int)` 等冗長寫法。
* **解構變數宣告**
  ```csharp
  var (x, y) = GetCoordinates();
  ```
* **隱式物件建立**  
  ```csharp
  List<string> names = new() { "Alice", "Bob" };
  ```
* **內聯變數宣告**  
  - 變數只用一次，可直接在使用處宣告。
* **索引運算子**  
  - 訪問集合元素時以 `list[i]` 為主。
* **局部函式優先於匿名函式**  
  - 若不需捕捉外部變數，局部函式效能較佳。
* **優先進行空值檢查而非型別檢查**  
  - 減少不必要型別錯誤。
* **範圍運算子**  
  - 善用 `..` 提升可讀性。
* **元組交換**  
  - `(a, b) = (b, a);` 簡潔達成交換。
* **UTF-8 字串文字**
* **`throw` 表達式**  
  - 簡化錯誤拋出。
* **棄置字元 `_`**  
  - 忽略未使用變數或回傳值。

---

## 4. 格式化規則

### 4.1. 換行

* `catch` / `else` / `finally` **前**應有換行。
* 匿名型別 / 物件初始化器成員之間應換行。
* 左大括號 `{` 前應有換行。
* 查詢表達式子句之間換行。

### 4.2. 縮排

* 區塊內容需縮排。
* 大括號 `{}` 與控制語句平齊。
* `case` 內容及其區塊 `{}` 需縮排。
* `label` 比當前程式碼層縮排少一層。

### 4.3. 空格

* `(int)x` — 轉型後不留空格。
* 繼承子句 `:` 前後、逗號 `,` 後要空格；逗號前則不要空格。
* 點號 `.` 前後不留空格。
* `if` / `for` / `while` / `switch` 等關鍵字後面要空格。  
* `for` 迴圈中分號後空一格。  
* 二元運算子前後空一格。
* 宣告語句周圍不應有多餘空格。
* `[]` / `()` 中不留空格，方法呼叫括號內也不留空格。

### 4.4. 單行區塊 & 單行語句

* 若能放在單行，請保留單行，不要強制展開或換行。

---

## 5. 命名規則

### 5.1. 命名慣例

| 項目                           | 命名範例                             |
|--------------------------------|--------------------------------------|
| **型別 & 命名空間**            | `MyClass` / `MyNamespace`（PascalCase）         |
| **介面 (Interface)**           | `IMyInterface`（IPascalCase）                   |
| **型別參數 (Type Parameter)**  | `TMyType`（TPascalCase）                       |
| **方法 (Method)**              | `MyMethod()`（PascalCase）                     |
| **屬性 (Property)**            | `MyProperty`（PascalCase）                     |
| **事件 (Event)**               | `MyEvent`（PascalCase）                        |
| **本地變數 (Local Variable)**  | `myVariable`（camelCase）                      |
| **本地常數 (Local Constant)**  | `myConstant`（camelCase）                      |
| **參數 (Parameter)**           | `myParameter`（camelCase）                     |
| **公開欄位 (Public Field)**    | `MyField`（PascalCase）                        |
| **私有欄位 (Private Field)**   | `_myField`（_camelCase）                       |
| **私有靜態欄位**               | `s_myStaticField`（s_camelCase）                |
| **公開常數欄位**               | `MyConstant`（PascalCase）                     |
| **私有常數欄位**               | `MyPrivateConstant`（PascalCase）              |
| **公開靜態 `readonly` 欄位**   | `MyStaticReadonlyField`（PascalCase）          |
| **私有靜態 `readonly` 欄位**   | `MyPrivateStaticReadonlyField`（PascalCase）    |
| **列舉 (Enum)**                | `MyEnum`（PascalCase）                         |
| **局部函式 (Local Function)**  | `MyLocalFunction()`（PascalCase）               |
| **非欄位成員 (Non-Field Member)** | `MyMember`（PascalCase）                  |

### 5.2. 命名規則定義

- **介面 (Interface)**：`IMyInterface`  
- **列舉 (Enum)**、**事件 (Event)**、**方法 (Method)**、**屬性 (Property)** 等：PascalCase。  
- **公開欄位**：PascalCase；**私有欄位**：`_camelCase`；**私有靜態欄位**：`s_camelCase`。  
- **常數欄位**（公開或私有）與靜態 `readonly` 欄位：PascalCase。  
- **型別與命名空間**：PascalCase；**型別參數**：`TMyType`。  
- **本地變數**、**本地常數**、**參數**：camelCase。  
- **局部函式**：PascalCase。

### 5.3. 命名風格

* **PascalCase**：`MyClassName`
* **IPascalCase**：`IMyInterface`
* **TPascalCase**：`TMyType`
* **_camelCase**：`_myField`
* **camelCase**：`myVariable`
* **s_camelCase**：`s_myStaticField`

---

## 6. 其他

* **適用範圍**：本規範適用於所有 C# 程式碼。  
* **疑問諮詢**：對於規範不清楚之處，請與團隊成員討論。  
* **可更新性**：可依團隊需求彈性調整或增修。

---

## 7. 防禦性程式設計與安全措施

在程式開發中，安全性與防禦性設計是不可或缺的考量。以下為建議的安全作法：

### 7.1. 防禦性程式設計

1. **資料驗證**：  
   - 對外部輸入（參數、使用者輸入、設定檔）皆需檢查合法性，避免超出預期範圍或格式。  
   - 避免直接信任使用者輸入，搭配商業邏輯及格式檢核。
2. **Exception Handling**：  
   - 在關鍵流程中使用 `try-catch` 捕捉可能錯誤，並進行適當的錯誤紀錄或回復（Rollback）。  
   - 勿在 catch 區塊中隱藏例外，除非有清楚理由，並視情況重新拋出或記錄。
3. **邏輯邊界檢查**：  
   - 針對陣列索引、清單存取、計算結果等，皆要防範超出邏輯範圍的狀況。  
   - 避免用硬編碼來假設特定範圍，務必在程式中適度檢查。
4. **最小權限原則**：  
   - 盡量以低權限執行程式，如需較高權限操作，應在個別流程或函式中「最短時間」取得並釋放該權限。

### 7.2. 避免 SQL Injection

1. **參數化查詢**：  
   - 在使用 SQL 指令時，應使用參數化，避免直接串接字串。  
   - 例如在 ADO.NET 中使用 `SqlParameter`，或在 ORM（如 Entity Framework）中使用 LINQ 查詢。
2. **Stored Procedure**（視情況）  
   - 可將複雜邏輯放入預存程序（Stored Procedure），減少組合 SQL 字串的風險。
3. **輸入消毒**：  
   - 對原本就不該出現「特殊字元」的輸入，加以檢查或濾除，如 `--`, `;`, `'` 等（視應用情境而定）。  
   - 並確保不在前端或後端將使用者輸入直接拼接成 SQL 指令。

### 7.3. 避免 XSS（跨站腳本攻擊）

1. **編碼輸出**：  
   - 在顯示使用者輸入（或外部資料）到網頁時，請進行 HTML 編碼（HTML-encode），避免惡意 JavaScript 注入。  
   - 如 Razor 預設會進行自動編碼，但若使用 `@Html.Raw()` 或前端框架，則需特別留意。
2. **輸入過濾**：  
   - 從前端到後端，都應該針對輸入值做基礎檢核，並且只允許合法格式。  
3. **Content Security Policy (CSP)**（若前端架構允許）  
   - 限制頁面可載入的外部資源、執行的 script 來源，降低 XSS 風險。

### 7.4. 密碼與憑證

1. **避免明碼儲存**：  
   - 密碼及機敏資訊請使用散雜（hash）與加密存放，伺服器端務必妥善保護金鑰。
2. **憑證管理**：  
   - 在程式庫或檔案中，切勿硬編碼憑證或 API 金鑰，可使用安全儲存（如 Azure Key Vault、AWS Secrets Manager）。
3. **鹽值（Salt）**  
   - 雜湊前加鹽值，防禦彩虹表攻擊。

### 7.5. 日誌與審計

1. **記錄關鍵操作**：  
   - 像是登入登出、資料變更、批次處理等動作都應記錄。  
2. **避免記錄敏感資訊**：  
   - 在日誌中勿包含密碼或完整憑證。  
   - 使用適當遮罩（mask）或摘要（hash）。

### 7.6. 其他建議

1. **使用 SSL/TLS**：  
   - 任何傳輸機敏資料的介面應採用加密協定。
2. **程式碼審核**：  
   - Pull Request 或合併前，建議進行程式碼審核（Code Review），並驗證安全性需求。
3. **更新框架與套件**：  
   - 定期更新 .NET / NuGet 套件，避免使用含有已知漏洞的舊版元件。
