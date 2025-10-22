using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.RentalCar.Persistance
{
    /// <summary>
    /// 車輛資料實體類別 - 基礎設施層資料持久化實體
    /// </summary>
    /// <remarks>
    /// 此類別代表租車系統中車輛資料在資料庫中的實體結構，
    /// 作為基礎設施層的資料存取物件（DAO），負責與資料庫進行 ORM 映射。
    /// 遵循六角架構（Hexagonal Architecture）的設計原則，
    /// 此實體類別專門處理資料持久化層面的需求，與領域模型分離，
    /// 透過適配器模式與領域層的 IVehicle 介面進行轉換。
    /// 
    /// 此類別使用 Entity Framework Core 進行資料庫操作，
    /// 支援 Code First 方式建立資料庫結構。
    /// </remarks>
    public class CarsEnt
    {
        /// <summary>
        /// 車輛唯一識別碼
        /// </summary>
        /// <value>
        /// 車輛的主鍵識別碼，由資料庫自動產生。
        /// 此值在資料庫中必須是唯一的，用於識別特定的車輛記錄。
        /// </value>
        /// <remarks>
        /// 在 Entity Framework Core 中，此屬性通常會被設定為自動遞增的主鍵。
        /// 當新增車輛到資料庫時，資料庫會自動分配一個唯一的 ID 值。
        /// </remarks>
        /// <example>
        /// <code>
        /// var car = new CarsEnt { Model = "Toyota Camry" };
        /// // 新增到資料庫後，Id 會被自動設定
        /// context.Cars.Add(car);
        /// context.SaveChanges();
        /// Console.WriteLine($"新車輛 ID: {car.Id}");
        /// </code>
        /// </example>
        public int Id { get; set; }

        /// <summary>
        /// 車輛型號名稱
        /// </summary>
        /// <value>
        /// 車輛的型號或品牌名稱，例如："Toyota Camry"、"Honda Civic" 等。
        /// 此欄位不能為空值，是識別車輛的重要資訊。
        /// </value>
        /// <remarks>
        /// 此屬性對應到領域模型中的車輛型號資訊，
        /// 在進行領域物件與資料實體轉換時，需要正確映射此欄位。
        /// 建議在資料庫層面設定適當的字串長度限制和非空約束。
        /// </remarks>
        /// <example>
        /// <code>
        /// var car = new CarsEnt 
        /// { 
        ///     Model = "BMW 320i",
        ///     cc = 2000,
        ///     Type = "Sedan"
        /// };
        /// </code>
        /// </example>
        public string Model { get; set; }

        /// <summary>
        /// 車輛排氣量（立方公分）
        /// </summary>
        /// <value>
        /// 車輛引擎的排氣量，以立方公分（cc）為單位。
        /// 此值必須為正整數，用於計算租金和分類車輛等級。
        /// </value>
        /// <remarks>
        /// 排氣量是車輛的重要規格之一，影響租金計算和車輛分類。
        /// 在租車系統中，通常會根據排氣量來區分車輛的等級和定價策略。
        /// 建議在應用層或資料庫層面加入適當的資料驗證，確保此值為合理範圍內的正整數。
        /// </remarks>
        /// <example>
        /// <code>
        /// // 一般小型車排氣量範例
        /// var smallCar = new CarsEnt { cc = 1600 };
        /// 
        /// // 中型車排氣量範例  
        /// var mediumCar = new CarsEnt { cc = 2000 };
        /// 
        /// // 大型車排氣量範例
        /// var largeCar = new CarsEnt { cc = 3000 };
        /// </code>
        /// </example>
        public int cc { get; set; }

        /// <summary>
        /// 車輛製造日期
        /// </summary>
        /// <value>
        /// 車輛的出廠製造日期，用於判斷車輛年份和估算車輛價值。
        /// 此日期會影響租金計算和車輛的可租用狀態。
        /// </value>
        /// <remarks>
        /// 製造日期是車輛的重要屬性，在租車業務中用於：
        /// 1. 計算車輛折舊和租金調整
        /// 2. 判斷車輛是否符合租用標準（例如：不超過特定年份）
        /// 3. 提供給客戶參考的車輛資訊
        /// 
        /// 在資料儲存時建議使用 UTC 時間格式，避免時區轉換問題。
        /// </remarks>
        /// <example>
        /// <code>
        /// var newCar = new CarsEnt 
        /// { 
        ///     Model = "Toyota Prius",
        ///     ManufacturingDate = new DateTime(2023, 1, 15),
        ///     cc = 1800
        /// };
        /// 
        /// // 計算車齡
        /// var carAge = DateTime.Now.Year - newCar.ManufacturingDate.Year;
        /// Console.WriteLine($"車齡: {carAge} 年");
        /// </code>
        /// </example>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// 車輛類型分類
        /// </summary>
        /// <value>
        /// 車輛的類型分類，例如："轎車"、"休旅車"、"跑車"、"貨車" 等。
        /// 此欄位用於車輛分類和篩選功能。
        /// </value>
        /// <remarks>
        /// 車輛類型是租車系統中重要的分類依據，用於：
        /// 1. 客戶根據需求篩選適合的車輛類型
        /// 2. 系統根據類型套用不同的租金計算邏輯
        /// 3. 車隊管理和庫存分類
        /// 
        /// 建議在應用層定義標準的車輛類型枚舉，確保資料一致性。
        /// 在實際應用中，可以考慮將此欄位與領域模型中的 VehicleType 枚舉進行映射。
        /// </remarks>
        /// <example>
        /// <code>
        /// // 不同類型的車輛範例
        /// var sedan = new CarsEnt { Type = "轎車", Model = "Toyota Camry" };
        /// var suv = new CarsEnt { Type = "休旅車", Model = "Honda CR-V" };
        /// var sports = new CarsEnt { Type = "跑車", Model = "BMW Z4" };
        /// 
        /// // 根據類型篩選車輛
        /// var sedanCars = context.Cars.Where(c => c.Type == "轎車").ToList();
        /// </code>
        /// </example>
        public string Type { get; set; }
    }
}
