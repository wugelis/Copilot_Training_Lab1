# 租車系統應用層 Application.RentalCar 程式碼說明

## 概述
Application.RentalCar 是租車系統的應用層，負責協調領域服務和基礎設施層，實現具體的業務用例。此層採用 Hexagonal Architecture (六角架構) 的設計模式，透過 Port 和 Adapter 來定義內部和外部的介面。

## 架構組成

### 1. Port (埠) 層
定義應用層的輸入輸出介面，實現依賴反轉原則。

#### In Port (輸入埠)
- **IRentalCarUseCase.cs** - 定義租車服務的主要用例介面

#### Out Port (輸出埠)  
- **IRentalCarRepository.cs** - 定義資料存取的介面

### 2. 應用服務層
- **RentalCarSerAppServices.cs** - 租車服務的應用程式服務實作

---

## 詳細類別說明

### IRentalCarUseCase 介面
**檔案位置**: `Application.RentalCar\port\In\IRentalCarUseCase.cs`

**用途**: 定義租車系統的核心用例介面，作為應用層的輸入埠，供上層(如 Web API 控制器)呼叫。

#### Methods 說明:

1. **ToRentCar(IVehicle car)**
   - **用途**: 進行車輛租用作業
   - **參數**: 
     - `car` (IVehicle): 要租用的車輛物件
   - **回傳值**: bool - 是否成功租用
   - **功能**: 處理車輛租用的核心邏輯

2. **GetAllCars()**
   - **用途**: 取得所有可租用車輛
   - **參數**: 無
   - **回傳值**: IEnumerable<IVehicle> - 所有車輛的集合
   - **功能**: 查詢並回傳系統中所有可用的車輛清單

---

### IRentalCarRepository 介面
**檔案位置**: `Application.RentalCar\port\Out\IRentalCarRepository.cs`

**用途**: 定義租車資料存取的介面，作為應用層的輸出埠，供基礎設施層實作具體的資料存取邏輯。

#### Methods 說明:

1. **AddCar(IVehicle car)**
   - **用途**: 新增車輛到系統中
   - **參數**: 
     - `car` (IVehicle): 要新增的車輛物件
   - **回傳值**: bool - 是否成功新增
   - **功能**: 將車輛資料持久化到資料儲存體

2. **RemoveCar(IVehicle car)**
   - **用途**: 從系統中移除車輛
   - **參數**: 
     - `car` (IVehicle): 要移除的車輛物件
   - **回傳值**: bool - 是否成功移除
   - **功能**: 從資料儲存體中刪除指定車輛

3. **UpdateCar(IVehicle car)**
   - **用途**: 更新車輛資訊
   - **參數**: 
     - `car` (IVehicle): 要更新的車輛物件
   - **回傳值**: bool - 是否成功更新
   - **功能**: 修改資料儲存體中的車輛資訊

---

### RentalCarSerAppServices 類別
**檔案位置**: `Application.RentalCar\RentalCarSerAppServices.cs`

**用途**: 租車服務的應用程式服務實作，負責協調用例和資料存取，實現具體的業務流程。

#### 建構函式:
**RentalCarSerAppServices(IRentalCarRepository rentalCarRepository, IRentalCarUseCase rentalCarUseCase)**
- **用途**: 初始化服務，注入相依性
- **參數**: 
  - `rentalCarRepository` (IRentalCarRepository): 租車資料庫存取介面
  - `rentalCarUseCase` (IRentalCarUseCase): 租車用例介面
- **功能**: 透過依賴注入設定服務所需的相依物件

#### Methods 說明:

1. **GetAllCars()**
   - **用途**: 取得所有可租車輛
   - **參數**: 無
   - **回傳值**: IEnumerable<IVehicle>? - 車輛清單(可為 null)
   - **功能**: 委派給 UseCase 介面來取得所有車輛
   - **實作細節**: 直接呼叫 `_rentalCarUseCase.GetAllCars()`

2. **ToRentCar(IVehicle car)**
   - **用途**: 租用車輛
   - **參數**: 
     - `car` (IVehicle): 車輛資訊
   - **回傳值**: bool - 是否成功租用
   - **功能**: 處理車輛租用的完整流程
   - **實作細節**: 
     - 建立新的 Car 物件，複製輸入車輛的 CC 和 Model 屬性
     - 透過 Repository 介面將車輛加入系統
     - **注意**: 目前實作存在邏輯問題，建立了新車輛但傳入原始車輛給 Repository

---

## 設計模式和架構特點

### 1. Hexagonal Architecture (六角架構)
- **In Port**: 定義應用層接受外部請求的介面
- **Out Port**: 定義應用層對外部資源的需求介面
- **應用服務**: 實作具體的業務邏輯和流程協調

### 2. 依賴反轉原則 (Dependency Inversion Principle)
- 應用層不直接依賴具體實作，而是依賴抽象介面
- 透過建構函式注入實現鬆散耦合

### 3. 關注點分離 (Separation of Concerns)
- **IRentalCarUseCase**: 專注於業務用例定義
- **IRentalCarRepository**: 專注於資料存取抽象
- **RentalCarSerAppServices**: 專注於流程協調和業務邏輯實作

---

## 改善建議

### RentalCarSerAppServices.ToRentCar 方法
目前實作中存在潛在問題：
```csharp
Car mycar = new Car() { CC = car.CC, Model = car.Model };
return _rentalCarRepository.AddCar(car); // 應該傳入 mycar 而非 car
```

建議修正為：
```csharp
return _rentalCarRepository.AddCar(mycar);
```
或者直接使用原始車輛：
```csharp
return _rentalCarRepository.AddCar(car);