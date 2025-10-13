using Application.RentalCar.port.In;
using Domain.RentalCar;
using EasyArchitectV2Lab1.ApiControllerBase.ApiLog.Filters;
using EasyArchitectV2Lab1.ApiControllerBase1;
using EasyArchitectV2Lab1.AuthExtensions1.Filters;
using EasyArchitectV2Lab1.AuthExtensions1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Lab20240823.Controllers
{
    /// <summary>
    /// 租車服務 API 控制器
    /// 提供租車相關的 RESTful API 端點
    /// </summary>
    public class RentalCarApiController : EasyArchiectV2ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IRentalCarUseCase _rentalCarUseCase;

        /// <summary>
        /// 初始化租車服務 API 控制器
        /// </summary>
        /// <param name="httpContextAccessor">HTTP 上下文存取器，用於存取當前請求的上下文資訊</param>
        /// <param name="userService">使用者服務，用於處理使用者身分驗證和授權</param>
        /// <param name="rentalCarUseCase">租車使用案例服務，提供租車業務邏輯</param>
        public RentalCarApiController(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IRentalCarUseCase rentalCarUseCase)
            : base(httpContextAccessor, userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _rentalCarUseCase = rentalCarUseCase;
        }

        /// <summary>
        /// 取得人員清單（範例程式，需要驗證）
        /// </summary>
        /// <returns>回傳人員清單的集合</returns>
        /// <response code="200">成功回傳人員清單</response>
        /// <response code="401">未授權，需要先登入</response>
        /// <remarks>
        /// 此為範例程式，展示如何使用身分驗證屬性
        /// 實際使用時，請從資料庫或其他資料來源取得人員資料
        /// </remarks>
        [NeedAuthorize]
        [HttpGet]
        [Route("GetPersons")]
        //[ApiLogException]
        [ApiLogonInfo]
        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await Task.FromResult(new Person[]
            {
                new Person()
                {
                    ID = 1,
                    Name = "Gelis Wu"
                }
            });
        }

        /// <summary>
        /// 取得所有可租用的車輛清單
        /// </summary>
        /// <returns>回傳所有可租用車輛的集合，包含車輛類型、車種、排氣量等資訊</returns>
        /// <response code="200">成功回傳車輛清單</response>
        /// <response code="500">伺服器內部錯誤</response>
        /// <remarks>
        /// 此 API 不需要身分驗證即可存取
        /// 回傳的車輛清單包含所有可供租用的車輛資訊
        /// 
        /// 範例回應:
        /// ```json
        /// [
        ///   {
        ///     "model": "Toyota",
        ///     "cc": "2000",
        ///     "carModelName": "Camry"
        ///   }
        /// ]
        /// ```
        /// </remarks>
        [HttpGet]
        [Route("GetAllCars")]
        [ApiLogonInfo]
        public async Task<IEnumerable<IVehicle>> GetAllCarsAsync()
        {
            return await Task.FromResult(_rentalCarUseCase.GetAllCars());
        }

        ///// <summary>
        ///// 執行車輛租用作業
        ///// </summary>
        ///// <param name="rentalCarDto">租車資料傳輸物件，包含租車所需的資訊</param>
        ///// <returns>回傳租車是否成功的布林值，true 表示成功，false 表示失敗，null 表示發生錯誤</returns>
        ///// <response code="200">租車作業執行成功</response>
        ///// <response code="400">請求資料不正確</response>
        ///// <response code="500">伺服器內部錯誤</response>
        ///// <remarks>
        ///// 此方法目前已被註解，待實作完整的租車功能後啟用
        ///// 實作時需要考慮：
        ///// - 車輛可用性檢查
        ///// - 租用期間驗證
        ///// - 租金計算
        ///// - 交易處理
        ///// </remarks>
        //[HttpPost]
        //[Route("ToRentalCar")]
        //public async Task<bool?> ToRentalCarAsync(RentalCarDto rentalCarDto)
        //{
        //    Car car = new Car(ModelType.Toyota);

        //    return await Task.FromResult( _rentalCarUseCase.ToRentCar(car));
        //}
    }

    /// <summary>
    /// 人員資料模型
    /// </summary>
    /// <remarks>
    /// 此為範例程式，實際專案中應將此類別放置在 Models/Dto/VO 專案資料夾中
    /// 建議使用適當的資料驗證屬性（Data Annotations）來確保資料完整性
    /// </remarks>
    public class Person
    {
        /// <summary>
        /// 取得或設定人員的唯一識別碼
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 取得或設定人員姓名
        /// </summary>
        public string Name { get; set; }
    }
}
