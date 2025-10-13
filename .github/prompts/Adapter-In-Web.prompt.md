# 這是六角架構中 RentalCarV2.Web 專案程式碼邏輯說明

## 專案概述

RentalCarV2.Web 是六角架構（Hexagonal Architecture）中的 **Adapter-In（輸入適配器）** 層，負責處理外部請求並將其轉換為應用程式核心能夠理解的格式。此專案採用 ASP.NET Core 8.0 開發，包含 Razor Pages 和 Web API 控制器。

---

## 專案結構

```
RentalCarV2.Web/
├── Controllers/          # API 和 MVC 控制器
├── Models/              # 視圖模型和 DTO
├── Views/               # Razor 視圖檔案
└── Program.cs           # 應用程式進入點和服務配置
```

---

## 核心檔案說明

### 1. Program.cs - 應用程式進入點

**用途：** 應用程式的啟動和配置檔案，負責註冊所有服務、中介軟體和路由設定。

#### 主要配置區塊：

##### 1.1 服務容器配置（Service Container Configuration）

**AppSettings 配置：**
```csharp
IConfigurationSection configRoot = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(configRoot);
```
- 載入應用程式設定檔
- 使用選項模式（Options Pattern）管理配置

**MVC 服務註冊：**
```csharp
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
```
- `AddControllersWithViews()`：註冊 MVC 控制器和視圖支援
- `AddHttpContextAccessor()`：提供 HttpContext 的依賴注入支援

##### 1.2 身分驗證配置（Authentication Configuration）

```csharp
builder.Services.AddAuthentication(configure =>
{
    configure.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configure.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToReturnUrl = async (context) =>
        {
            context.HttpContext.Response.Cookies.Delete(AuthenticateRequest.LOGIN_USER_INFO);
        }
    };
});
```
- 使用 Cookie 驗證機制
- Cookie 有效期限：3 分鐘
- HttpOnly 設定防止 XSS 攻擊
- 自動清理登入資訊 Cookie

##### 1.3 模擬帳號資料註冊

```csharp
builder.Services.AddScoped<IEnumerable<IAccountEnt>, List<AccountEnt>>(account =>
    new List<AccountEnt>(
        new AccountEnt[]
        {
            new AccountEnt() { Id=1, UserID="gelis", AID="F123456789" },
            new AccountEnt() { Id=2, UserID="mary", AID="F123456780" },
            new AccountEnt() { Id=3, UserID="allan", AID="F123456781" },
        }));
```
- 註冊模擬的使用者帳號清單
- 用於開發和測試環境
- 包含三個測試帳號：gelis, mary, allan

##### 1.4 六角架構依賴注入配置

**使用者服務：**
```csharp
builder.Services.AddScoped<IUserService, UserService>();
```
- 註冊使用者身分驗證和授權服務

**租車服務（六角架構端口和適配器）：**
```csharp
builder.Services.AddScoped<RentalCarRepository>();
builder.Services.AddScoped<IRentalCarRepository, RentalCarRepository>(service => service.GetRequiredService<RentalCarRepository>());
builder.Services.AddScoped<IRentalCarUseCase, RentalCarRepository>(service => service.GetRequiredService<RentalCarRepository>());
builder.Services.AddScoped<RentalCarSerAppServices>();
```
- `RentalCarRepository`：資料持久化適配器（Adapter-Out）
- `IRentalCarRepository`：輸出端口（Port-Out）介面
- `IRentalCarUseCase`：輸入端口（Port-In）介面
- `RentalCarSerAppServices`：應用服務層

**URI 擴充服務：**
```csharp
builder.Services.AddScoped<IUriExtensions, UriExtensions>();
```
- 提供 URI 相關的擴充功能

##### 1.5 中介軟體管道配置（Middleware Pipeline）

```csharp
app.UseHttpsRedirection();        // HTTPS 重導向
app.UseStaticFiles();              // 靜態檔案服務
app.UseRouting();                  // 路由配置
app.UseAuthentication();           // 身分驗證
app.UseAuthorization();            // 授權驗證
app.UseJWTAuthorizedMiddleware();  // JWT 自訂中介軟體
```

**路由配置：**
```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```
- 預設路由：Home/Index
- 支援選擇性 id 參數

---

### 2. Controllers/HomeController.cs - MVC 首頁控制器

**用途：** 處理網站的主要頁面導航和錯誤處理。

#### 類別說明：

```csharp
public class HomeController : Controller
```
- 繼承自 `Controller` 基底類別
- 處理 MVC 視圖請求

#### 建構函式：

```csharp
public HomeController(ILogger<HomeController> logger)
```
- **參數：** `ILogger<HomeController>` - ASP.NET Core 內建的日誌記錄器
- **用途：** 依賴注入日誌服務，用於記錄應用程式執行資訊

#### 方法說明：

##### 2.1 Index() - 首頁

```csharp
public IActionResult Index()
{
    return View();
}
```
- **用途：** 顯示網站首頁
- **回傳：** 渲染 `Views/Home/Index.cshtml` 視圖
- **路由：** `/` 或 `/Home/Index`

##### 2.2 Privacy() - 隱私權政策頁面

```csharp
public IActionResult Privacy()
{
    return View();
}
```
- **用途：** 顯示隱私權政策頁面
- **回傳：** 渲染 `Views/Home/Privacy.cshtml` 視圖
- **路由：** `/Home/Privacy`

##### 2.3 Error() - 錯誤處理頁面

```csharp
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
```
- **用途：** 顯示錯誤頁面
- **屬性：** `[ResponseCache]` - 禁用快取，確保錯誤資訊即時顯示
- **回傳：** 包含請求追蹤 ID 的錯誤視圖模型
- **追蹤 ID 來源：** 
  - 優先使用 `Activity.Current.Id`（分散式追蹤）
  - 備用使用 `HttpContext.TraceIdentifier`（本地追蹤）

---

### 3. Controllers/RentalCarApiController.cs - 租車 API 控制器

**用途：** 提供租車服務的 RESTful API 端點，是六角架構中的主要輸入適配器（Adapter-In）。

#### 類別說明：

```csharp
public class RentalCarApiController : EasyArchiectV2ControllerBase
```
- 繼承自 `EasyArchiectV2ControllerBase` 自訂基底控制器
- 整合身分驗證和日誌功能

#### 建構函式：

```csharp
public RentalCarApiController(
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    IRentalCarUseCase rentalCarUseCase)
    : base(httpContextAccessor, userService)
```
- **參數說明：**
  - `IHttpContextAccessor`：HTTP 上下文存取器
  - `IUserService`：使用者身分驗證服務
  - `IRentalCarUseCase`：租車業務邏輯的輸入端口（Port-In）
- **用途：** 依賴注入所需服務，並傳遞給基底類別

#### API 端點方法：

##### 3.1 GetPersonsAsync() - 取得人員清單（範例）

```csharp
[NeedAuthorize]
[HttpGet]
[Route("GetPersons")]
[ApiLogonInfo]
public async Task<IEnumerable<Person>> GetPersonsAsync()
```
- **用途：** 範例 API，展示身分驗證和日誌功能
- **HTTP 方法：** GET
- **路由：** `/GetPersons`
- **屬性：**
  - `[NeedAuthorize]`：需要身分驗證
  - `[ApiLogonInfo]`：記錄 API 呼叫資訊
- **回傳：** 人員物件集合（範例資料）
- **回應代碼：**
  - 200：成功
  - 401：未授權

##### 3.2 GetAllCarsAsync() - 取得所有可租用車輛

```csharp
[HttpGet]
[Route("GetAllCars")]
[ApiLogonInfo]
public async Task<IEnumerable<IVehicle>> GetAllCarsAsync()
{
    return await Task.FromResult(_rentalCarUseCase.GetAllCars());
}
```
- **用途：** 查詢所有可租用的車輛清單
- **HTTP 方法：** GET
- **路由：** `/GetAllCars`
- **屬性：** `[ApiLogonInfo]` - 記錄 API 呼叫資訊
- **回傳：** `IVehicle` 車輛介面集合
- **業務邏輯：** 透過 `IRentalCarUseCase.GetAllCars()` 取得車輛資料
- **回應代碼：**
  - 200：成功回傳車輛清單
  - 500：伺服器錯誤
- **特點：** 無需身分驗證即可存取

##### 3.3 ToRentalCarAsync() - 車輛租用作業（已註解）

```csharp
//[HttpPost]
//[Route("ToRentalCar")]
//public async Task<bool?> ToRentalCarAsync(RentalCarDto rentalCarDto)
//{
//    Car car = new Car(ModelType.Toyota);
//    return await Task.FromResult(_rentalCarUseCase.ToRentCar(car));
//}
```
- **用途：** 執行車輛租用作業（待實作）
- **HTTP 方法：** POST
- **路由：** `/ToRentalCar`
- **參數：** `RentalCarDto` - 租車資料傳輸物件
- **回傳：** `bool?` - 租車是否成功
- **狀態：** 目前已註解，等待完整實作
- **待實作功能：**
  - 車輛可用性檢查
  - 租用期間驗證
  - 租金計算
  - 交易處理

---

### 4. Models/ErrorViewModel.cs - 錯誤視圖模型

**用途：** 錯誤頁面的資料模型，用於顯示錯誤追蹤資訊。

#### 類別說明：

```csharp
public class ErrorViewModel
```
- 錯誤視圖的資料傳輸物件（DTO）

#### 屬性說明：

##### 4.1 RequestId - 請求識別碼

```csharp
public string? RequestId { get; set; }
```
- **用途：** 儲存請求的唯一識別碼
- **型別：** 可為 null 的字串
- **來源：** 
  - Activity.Current?.Id（分散式追蹤）
  - HttpContext.TraceIdentifier（本地追蹤）

##### 4.2 ShowRequestId - 是否顯示請求 ID

```csharp
public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
```
- **用途：** 計算屬性，判斷是否要在視圖中顯示請求 ID
- **邏輯：** 當 RequestId 不為空時回傳 true
- **使用場景：** 在錯誤頁面視圖中條件式顯示追蹤資訊

---

### 5. Models/Person.cs - 人員資料模型（範例）

**用途：** 範例資料模型，實際專案應移至 Models/Dto/VO 專案。

#### 類別說明：

```csharp
public class Person
```
- 範例用的資料傳輸物件
- 建議重構至獨立的 DTO 專案

#### 屬性說明：

##### 5.1 ID - 人員識別碼

```csharp
public int ID { get; set; }
```
- **用途：** 人員的唯一識別碼
- **型別：** 整數

##### 5.2 Name - 人員姓名

```csharp
public string Name { get; set; }
```
- **用途：** 人員的姓名
- **型別：** 字串
- **建議：** 實際專案應加入資料驗證屬性（Data Annotations）

---

## 六角架構角色定位

### Adapter-In（輸入適配器）職責

RentalCarV2.Web 專案在六角架構中扮演 **Adapter-In** 的角色：

1. **接收外部請求：**
   - HTTP/HTTPS 請求
   - RESTful API 呼叫
   - 網頁表單提交

2. **請求轉換：**
   - 將 HTTP 請求轉換為應用程式核心能理解的指令
   - 驗證輸入資料格式
   - 處理身分驗證和授權

3. **呼叫 Port-In（輸入端口）：**
   - 透過 `IRentalCarUseCase` 介面呼叫業務邏輯
   - 不直接依賴具體實作，保持低耦合

4. **回應轉換：**
   - 將業務層回傳的資料轉換為 HTTP 回應
   - 處理錯誤和例外狀況
   - 回傳適當的 HTTP 狀態碼

### 依賴方向

```
外部請求 → RentalCarV2.Web (Adapter-In) → IRentalCarUseCase (Port-In) → Application.RentalCar (核心業務)
```

- **向內依賴：** Web 層依賴應用核心的介面（Port-In）
- **不向外依賴：** 應用核心不知道 Web 層的存在
- **依賴反轉：** 透過介面解耦，符合 SOLID 原則

---

## 技術特點

### 1. 身分驗證機制

- **Cookie Authentication：** 基於 Cookie 的身分驗證
- **JWT Middleware：** 自訂 JWT 中介軟體支援
- **Authorization Filters：** `[NeedAuthorize]` 屬性控制存取權限

### 2. API 日誌記錄

- **ApiLogonInfo：** 記錄 API 呼叫資訊
- **ApiLogException：** 記錄例外狀況（已註解）
- **分散式追蹤：** 支援 Activity 追蹤機制

### 3. 依賴注入模式

- **Service Lifetime：** 使用 Scoped 生命週期
- **Interface Segregation：** 介面隔離原則
- **Factory Pattern：** 使用 GetRequiredService 工廠方法

### 4. 非同步程式設計

- 所有 API 方法使用 `async/await` 模式
- 回傳 `Task<T>` 型別支援非同步操作

---

## 最佳實踐建議

### 1. DTO 分離
- 將 `Person` 類別移至獨立的 DTO 專案
- 建立專門的 `RentalCarDto` 類別

### 2. 錯誤處理
- 實作全域例外處理器
- 使用 `[ApiLogException]` 記錄錯誤

### 3. API 版本控制
- 加入 API 版本管理
- 路由中包含版本資訊（如：`/api/v1/cars`）

### 4. 資料驗證
- 使用 Data Annotations 驗證輸入
- 實作 FluentValidation 進行複雜驗證

### 5. 安全性強化
- 實作 CORS 政策
- 加入 Rate Limiting
- 使用 HTTPS 強制重導向

---

## 總結

RentalCarV2.Web 專案作為六角架構的 Adapter-In 層，成功實現了：

? **關注點分離：** Web 層只負責請求處理，不包含業務邏輯  
? **依賴反轉：** 透過介面依賴應用核心  
? **可測試性：** 依賴注入使單元測試更容易  
? **可維護性：** 清晰的架構層次和職責劃分  
? **可擴展性：** 易於加入新的 API 端點或驗證機制

