//using Domain.Lab120240821;
using Domain.RentalCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RentalCar.port.Out
{
    /// <summary>
    /// 租車資料存取介面 - 應用層輸出埠
    /// </summary>
    /// <remarks>
    /// 此介面定義了租車系統中車輛資料的基本 CRUD 操作，
    /// 作為應用層的輸出埠，供基礎設施層實作具體的資料存取邏輯。
    /// 遵循六角架構（Hexagonal Architecture）的設計原則，
    /// 透過依賴反轉原則實現應用層與基礎設施層的解耦。
    /// </remarks>
    public interface IRentalCarRepository
    {
        /// <summary>
        /// 新增車輛到系統中
        /// </summary>
        /// <param name="car">要新增的車輛物件，必須實作 IVehicle 介面</param>
        /// <returns>
        /// 回傳 <c>true</c> 表示成功新增車輛到資料儲存體；
        /// 回傳 <c>false</c> 表示新增失敗，可能原因包括車輛已存在、資料驗證失敗或資料庫連線問題等
        /// </returns>
        /// <exception cref="ArgumentNullException">當 <paramref name="car"/> 為 null 時拋出</exception>
        /// <example>
        /// <code>
        /// var newCar = new Car { Model = "Toyota Camry", CC = 2000 };
        /// bool result = repository.AddCar(newCar);
        /// if (result)
        /// {
        ///     Console.WriteLine("車輛新增成功");
        /// }
        /// </code>
        /// </example>
        bool AddCar(IVehicle car);

        /// <summary>
        /// 從系統中移除指定的車輛
        /// </summary>
        /// <param name="car">要移除的車輛物件，必須實作 IVehicle 介面</param>
        /// <returns>
        /// 回傳 <c>true</c> 表示成功從資料儲存體移除車輛；
        /// 回傳 <c>false</c> 表示移除失敗，可能原因包括車輛不存在、車輛正在使用中或資料庫連線問題等
        /// </returns>
        /// <exception cref="ArgumentNullException">當 <paramref name="car"/> 為 null 時拋出</exception>
        /// <remarks>
        /// 移除車輛前應確認該車輛目前沒有被租用，
        /// 建議在實作時加入相關的業務規則檢查。
        /// </remarks>
        /// <example>
        /// <code>
        /// var carToRemove = existingCar;
        /// bool result = repository.RemoveCar(carToRemove);
        /// if (result)
        /// {
        ///     Console.WriteLine("車輛移除成功");
        /// }
        /// </code>
        /// </example>
        bool RemoveCar(IVehicle car);

        /// <summary>
        /// 更新系統中現有車輛的資訊
        /// </summary>
        /// <param name="car">包含更新資訊的車輛物件，必須實作 IVehicle 介面</param>
        /// <returns>
        /// 回傳 <c>true</c> 表示成功更新車輛資訊到資料儲存體；
        /// 回傳 <c>false</c> 表示更新失敗，可能原因包括車輛不存在、資料驗證失敗或資料庫連線問題等
        /// </returns>
        /// <exception cref="ArgumentNullException">當 <paramref name="car"/> 為 null 時拋出</exception>
        /// <remarks>
        /// 更新操作通常基於車輛的唯一識別碼（如 ID）來定位要更新的車輛，
        /// 實作時應確保車輛識別碼的一致性和資料完整性。
        /// </remarks>
        /// <example>
        /// <code>
        /// var updatedCar = existingCar;
        /// updatedCar.Model = "Updated Model Name";
        /// bool result = repository.UpdateCar(updatedCar);
        /// if (result)
        /// {
        ///     Console.WriteLine("車輛資訊更新成功");
        /// }
        /// </code>
        /// </example>
        bool UpdateCar(IVehicle car);
    }
}

