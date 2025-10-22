using Application.RentalCar;
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
        private readonly RentalCarSerAppServices _rentalCarSerAppServices;

        /// <summary>
        /// 初始化租車服務 API 控制器
        /// </summary>
        /// <param name="httpContextAccessor">HTTP 上下文存取器，用於存取當前請求的上下文資訊</param>
        /// <param name="userService">使用者服務，用於處理使用者身分驗證和授權</param>
        /// <param name="rentalCarUseCase">租車使用案例服務，提供租車業務邏輯</param>
        /// <param name="rentalCarSerAppServices">租車應用程式服務，處理租車相關業務流程</param>
        public RentalCarApiController(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IRentalCarUseCase rentalCarUseCase,
            RentalCarSerAppServices rentalCarSerAppServices)
            : base(httpContextAccessor, userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _rentalCarUseCase = rentalCarUseCase;
            _rentalCarSerAppServices = rentalCarSerAppServices;
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
        /// 取回目前所有可出租的車輛
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCars")]
        [ApiLogonInfo]
        public async Task<IEnumerable<IVehicle>> GetAllCarsAsync()
        {
            return await Task.FromResult(_rentalCarUseCase.GetAllCars());
        }

        /// <summary>
        /// 進行車輛租用作業
        /// </summary>
        /// <param name="car">要租用的車輛資訊</param>
        /// <returns>租用結果，成功回傳 true，失敗回傳 false</returns>
        /// <response code="200">成功租用車輛</response>
        /// <response code="400">租用失敗，可能是車輛不可用或資料有誤</response>
        /// <response code="401">未授權，需要先登入</response>
        /// <remarks>
        /// 透過應用程式服務層處理租車業務邏輯
        /// 此方法會呼叫 RentalCarSerAppServices 的 ToRentCar 方法
        /// </remarks>
        [NeedAuthorize]
        [HttpPost]
        [Route("ToRentalCar")]
        [ApiLogonInfo]
        public async Task<IActionResult> ToRentalCarAsync([FromBody] IVehicle car)
        {
            try
            {
                if (car == null)
                {
                    return BadRequest("車輛資訊不能為空");
                }

                bool result = await Task.FromResult(_rentalCarSerAppServices.ToRentCar(car));

                if (result)
                {
                    return Ok(new { Success = true, Message = "車輛租用成功" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "車輛租用失敗" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"系統錯誤: {ex.Message}" });
            }
        }
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
