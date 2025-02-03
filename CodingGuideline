# 環友科技 C# 程式碼撰寫規範（C# Coding Guideline）

這份文件旨在提供一個全面的 C# 程式碼撰寫指南，協助開發團隊編寫出 **一致**、**易於維護**的程式碼。遵循這些準則能夠提升程式碼品質，並確保團隊協作的效率。

## 1. 通則

* **縮排**：使用 **空格** 進行縮排，每層 **4 個空格**。  
  讓程式碼整齊易讀，避免用 Tab 縮排造成的對齊不一致問題。

* **字元編碼**：所有檔案應使用 **UTF-8-BOM** 字元編碼。  
  確保專案中（包含程式碼與文字檔）使用各種字元都能正確顯示，特別適合包含中文的專案。

* **檔案結尾換行**：檔案結尾**不**需要插入新的一行。  
  避免多餘空行，有助於維持版本控管時的差異清晰度。

---

## 2. 程式碼結構

### 2.1. using 指示詞

* **排序**：  
  1. 系統命名空間（`System.*`）排在最前面。  
  2. 不同的 `using` 區塊中間以空白行分隔。

* **位置**：`using` 指示詞應放在命名空間之外，確保作用範圍涵蓋整個檔案。

* **簡化宣告**：建議使用簡化的 `using` 宣告形式（如 `using var foo = ...;`）以使程式碼更精簡。

### 2.2. 命名空間

* **file-scoped namespace**：  
  ```csharp
  namespace MyNamespace;
  ```
  這種方式更清爽易讀，並明確顯示命名空間作用範圍。

* **資料夾結構一致**：命名空間名稱盡量與資料夾路徑對應，方便管理與維護。

---

## 3. 程式碼樣式

### 3.1. 關鍵字與型別

* 使用 C# 內建關鍵字（`int` 而非 `System.Int32`）表示基本型別。  
  能讓程式碼符合 C# 語言慣例，也較為精簡。

* 對本地變數、參數、成員存取都使用預先定義的型別關鍵字，保持一致性。

### 3.2. `this`

* **不**強制使用 `this.` 來存取事件、欄位、方法和屬性，除非需要解決命名衝突。  
  讓程式碼保持簡潔度。

### 3.3. 括號

* **必加括號**：  
  - 在算術、關係等二元運算子中，務必加上括號明確運算子優先順序。  
* **非必要則不加括號**：  
  - 能避免視覺雜訊，使程式碼更簡潔。

### 3.4. 修飾詞

* 非介面成員，**總是**標明存取修飾詞（`public`、`private`、`protected`…）。
* **修飾詞順序建議**：  
  ```
  public, private, protected, internal, file, const, static, extern, new,
  virtual, abstract, sealed, override, readonly, unsafe, required, volatile, async
  ```

### 3.5. 表達式層級偏好

* **使用 `??` 空合併運算子**  
  簡化判斷物件是否為 null 的流程。
* **使用集合初始器**  
  範例：
  ```csharp
  var list = new List<int> { 1, 2, 3 };
  ```
* **使用明確的元組名稱**  
  讓元組內的成員易於辨識。
* **使用空值傳播運算子 `?.`**  
  簡化空值檢查：
  ```csharp
  myObject?.DoSomething();
  ```
* **使用物件初始化器**  
  範例：
  ```csharp
  var person = new Person { Name = "John", Age = 30 };
  ```
* **運算子換行時，將運算子置於行首**  
  提升可讀性與一致性。
* **優先使用自動屬性**  
  ```csharp
  public int Age { get; set; }
  ```
* **鬆散型別匹配時可使用集合表達式**  
  讓程式碼更直覺。
* **使用複合賦值運算子 (`+=`, `-=`)**  
  範例：
  ```csharp
  total += value;
  ```
* **條件賦值或回傳時，盡量使用條件運算子 (`?:`) 而非 `if/else`**  
  可使邏輯精簡：
  ```csharp
  return condition ? valueA : valueB;
  ```
* **在 `foreach` 有明確型別時，使用明確的型別宣告**  
  提高型別安全與可讀性。
* **推斷匿名型別成員名稱和元組名稱**  
  減少冗餘。
* **使用 `is null`** 進行空值檢查  
  取代 `== null` 或 `!= null`，更貼近 C# 新式寫法。
* **簡化布林表達式**  
  如 `if (x)` 取代 `if (x == true)`.
* **簡化插值字串**  
  如 `$"{x}"` 取代 `$"{x.ToString()}"`.

### 3.6. 欄位

* 若欄位初始化後不會改變，請使用 `readonly` 修飾，避免意外被改寫。

### 3.7. 參數

* 若參數未使用，請移除，避免混淆與警告。

### 3.8. 變數宣告

* 建議：  
  - 在「區域變數」宣告上可以使用 `var`。  
  - **不**針對內建型別（`int`, `string` 等）使用 `var`。  
  - 只有在型別明顯可推斷時才用 `var`。

### 3.9. 表達式主體成員

* **可使用表達式主體**：  
  - 定義存取子、索引子、lambda、簡單屬性等。
* **不**使用表達式主體：  
  - 建構子、局部函式、方法和運算子較複雜時，建議用區塊語法提升可讀性。

### 3.10. 模式比對

* **優先使用模式比對** 而不是傳統的 `as + null 檢查` 或 `is` + 轉型。
* **擴展屬性模式、`not` 模式、switch 表達式** 都是 C# 現代寫法，能使邏輯更乾淨直覺。

### 3.11. 空值檢查

* **使用條件委派呼叫 `?.Invoke`**  
  範例：
  ```csharp
  MyEvent?.Invoke(this, EventArgs.Empty);
  ```

### 3.12. 其他

* **匿名函式 / 局部函式**  
  - 若不需使用外部變數，請宣告為 `static`，能提升效能與安全性。
* **結構體**  
  - 若初始化後不變，更建議宣告為 `readonly struct`，並將成員也標示為 `readonly`。
* **使用大括號 `{}`**  
  - `if`, `for`, `while` 等所有控制流程語句，都應有大括號。
* **使用簡單 `using` 語句**  
  - 只需釋放資源時，保持簡單結構即可。
* **優先使用主要建構子 (Primary Constructor)**  
  - 若類別/結構體只有一個主要建構子，可使程式碼更精簡。
* **最上層語句 (Top-level statements)**  
  - 適用於程式碼簡單的情境，能更快上手。
* **簡單的 `default` 表達式**  
  - 例如 `default(int)` 可寫成 `default`。
* **解構變數宣告**  
  - 可用解構語法來取得元組值，如：  
    ```csharp
    var (x, y) = GetCoordinates();
    ```
* **型別明顯時可用隱式物件建立**  
  - 例如右側已顯示 `List<string>`，可省略重複的型別：
    ```csharp
    List<string> names = new() { "Alice", "Bob" };
    ```
* **內聯變數宣告**  
  - 變數只在當下使用，可直接在需要處宣告。
* **優先使用索引運算子**  
  - 訪問集合元素時以 `list[i]` 等方式呈現。
* **優先使用局部函式而非匿名函式**  
  - 若不需要捕捉外部變數，局部函式比匿名函式更有效率。
* **空值檢查優先於型別檢查**  
  - 避免不必要的型別錯誤。
* **優先使用範圍運算子**  
  - C# 8 的範圍運算子 `..` 提升可讀性。
* **優先使用元組交換**  
  - `(a, b) = (b, a);`
* **優先使用 UTF-8 字串文字**  
  - 確保字串的編碼正確。
* **使用 `throw` 表達式**  
  - 提升程式碼簡潔性。
* **使用棄置字元 `_`**  
  - 忽略未使用的值或回傳值。
  
---

## 4. 格式化規則

### 4.1. 換行

* `catch` / `else` / `finally` **前**一律換行。  
  ```csharp
  try
  {
      // ...
  }
  catch (Exception e)
  {
      // ...
  }
  ```
* **匿名型別** / **物件初始化器** 中各成員之間應換行。  
  提升結構與可讀性。
* 左大括號 `{` 前應有換行。  
* 查詢表達式子句之間換行。

### 4.2. 縮排

* **區塊內容** 需縮排。
* **大括號 `{}` 本身** 不縮排，與所屬的控制語句平齊。
* **`case` 內容**、使用區塊 `{}` 時都要再縮排。
* **`label`** 與當前程式碼層次對應，較外層一級。

### 4.3. 空格

* 型別轉換後不留空格，如 `(int)x`。
* 繼承子句 `:` 前後、逗號 `,` 後要有空格；逗號前則不要空格。
* 點號 `.` 前後不留空格。
* `if` / `for` / `while` 等關鍵字後面要有空格；`for` 的分號 `;` 後也有空格。
* 二元運算子前後要空格，強調可視性。
* 宣告語句前後不要插入額外空格。
* `[]` / `()` 中不留空格，方法呼叫的括號內也不留空格。

### 4.4. 換行

* 保留**單行程式碼區塊**、**單行程式碼語句**  
  - 如果能放在單行，請保持單行，不要強制展開或換行。

---

## 5. 命名規則

### 5.1. 命名慣例

| 項目                           | 命名範例                |
|--------------------------------|-------------------------|
| **型別 & 命名空間**            | `MyClass` / `MyNamespace`（PascalCase） |
| **介面 (Interface)**           | `IMyInterface`（IPascalCase）           |
| **型別參數 (Type Parameter)**  | `TMyType`（TPascalCase）               |
| **方法 (Method)**              | `MyMethod()`（PascalCase）             |
| **屬性 (Property)**            | `MyProperty`（PascalCase）             |
| **事件 (Event)**               | `MyEvent`（PascalCase）                |
| **本地變數 (Local Variable)**  | `myVariable`（camelCase）              |
| **本地常數 (Local Constant)**  | `myConstant`（camelCase）              |
| **參數 (Parameter)**           | `myParameter`（camelCase）             |
| **公開欄位 (Public Field)**    | `MyField`（PascalCase）                |
| **私有欄位 (Private Field)**   | `_myField`（_camelCase）               |
| **私有靜態欄位**               | `s_myStaticField`（s_camelCase）        |
| **公開常數欄位**               | `MyConstant`（PascalCase）             |
| **私有常數欄位**               | `MyPrivateConstant`（PascalCase）       |
| **公開靜態 `readonly` 欄位**   | `MyStaticReadonlyField`（PascalCase）   |
| **私有靜態 `readonly` 欄位**   | `MyPrivateStaticReadonlyField`（PascalCase） |
| **列舉 (Enum)**                | `MyEnum`（PascalCase）                 |
| **局部函式 (Local Function)**  | `MyLocalFunction()`（PascalCase）       |
| **非欄位成員 (Non-Field Member)** | `MyMember`（PascalCase）            |

### 5.2. 命名規則定義

規範涵蓋各種可存取性的介面、列舉、事件、方法、屬性、欄位、常數欄位、型別與命名空間、局部函式等，詳細歸納如下：

- **介面 (Interface)**：`public`, `private`, `protected`... 統一走 `IMyInterface` 風格。  
- **列舉 (Enum)**、**事件 (Event)**、**方法 (Method)**、**屬性 (Property)**：一律 PascalCase，存取修飾視需求。  
- **公開欄位**：PascalCase。  
- **私有欄位**：`_camelCase`。  
- **私有靜態欄位**：`s_camelCase`。  
- **型別和命名空間**：PascalCase。  
- **型別參數**：`TMyType`。  
- **常數欄位**（公開或私有）：PascalCase。  
- **靜態 `readonly` 欄位**（公開或私有）：皆採 PascalCase。  
- **局部函式**：PascalCase。  
- **本地變數**、**本地常數**、**參數**：camelCase 為主。

### 5.3. 命名風格

* **PascalCase**：首字大寫，其後每個單字的首字亦大寫（`MyClassName`）。
* **IPascalCase**：專用於介面，前綴 `I`，後面跟 PascalCase（`IMyInterface`）。
* **TPascalCase**：專用於泛型型別參數，前綴 `T`（`TMyType`）。
* **_camelCase**：私有欄位通常採用此寫法（例如 `_myField`）。
* **camelCase**：首字小寫，後續單字首字母大寫（如 `myVariable`）。
* **s_camelCase**：私有靜態欄位採 `s_` 開頭（如 `s_myStaticField`）。

---

## 6. 其他

* **適用範圍**：本規範適用於所有 C# 程式碼。
* **疑問諮詢**：對於規範不清楚之處，請與團隊成員討論。
* **可更新性**：可依團隊需求彈性調整或增修。
