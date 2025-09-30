# RentalCar 線上預先車輛租用系統

## 系統說明：
這是一個提供租車用戶可在線上預先租用車輛的系統。系統功能為，租車用戶可以在線上預約租車，而在租用車輛之前，租車用戶必須先註冊自已的帳戶資料後，並進行登入後才可預先租用車輛。在租用車輛時，可以選擇車型、租用時間區間、並計算租金。

請使用 PlantUML 語法，設計租車用戶可進行線上租車的 Sequence Diagram，(租車用戶/Actor) 可呼叫 ToRentalCar() 方法線上租用車輛 ，但在租用車輛之前，得先對 Account 領域物件 呼叫註冊帳號 RegisterAccount() 的方法。


## 選擇車型
車型有轎車 Car、休旅車 SUV、貨車 Truck、跑車 SportsCar 等等可供選擇

```csharp
    /// <summary>
    /// 車輛類型
    /// </summary>
    public enum VehicleType { Car, SUV, Truck, SportsCar }
    /// <summary>
    /// 車輛型號
    /// </summary>
    public enum ModelType { Toyota = 100, Lexus = 150, Ford = 120, BMW = 200 }
```

## 選擇租用時間
先選擇車型之後，才可選擇租用時間。

## 計算租金
先選擇車型與租用時間後，才能夠計算租金，租金也由天數計算出來。

```csharp
    /// <summary>
    /// 計算租金
    /// </summary>
    /// <param name="vehicleType">車輛類型</param>
    /// <param name="modelType">車輛型號</param>
    /// <param name="rentalDays">租用天數</param>
    /// <returns>租金</returns>
    public decimal CalculateRentalFee(VehicleType vehicleType, ModelType modelType, int rentalDays)
    {
        decimal baseRate = (decimal)modelType; // 以車型型號作為基礎費率
        return baseRate * rentalDays; // 租金 = 基礎費率 * 租用天數
    }
```

## 車輛租用價格
Car 1000 元/天、SUV 1500 元/天、Truck 2000 元/天、SportsCar 3000 元/天