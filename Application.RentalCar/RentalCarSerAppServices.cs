using Application.RentalCar.port.In;
using Application.RentalCar.port.Out;
using Domain.RentalCar;
//using Domain.Lab120240821;

namespace Application.RentalCar
{
    /// <summary>
    /// 租車服務應用程式服務
    /// </summary>
    public class RentalCarSerAppServices
    {
        private readonly IRentalCarRepository _rentalCarRepository;
        private readonly IRentalCarUseCase _rentalCarUseCase;

        /// <summary>
        /// 建構函式，初始化服務
        /// </summary>
        /// <param name="rentalCarRepository">租車資料庫存取介面</param>
        /// <param name="rentalCarUseCase">租車用例介面</param>
        public RentalCarSerAppServices(IRentalCarRepository rentalCarRepository, IRentalCarUseCase rentalCarUseCase)
        {
            _rentalCarRepository = rentalCarRepository;
            _rentalCarUseCase = rentalCarUseCase;
        }

        /// <summary>
        /// 取得所有可租車輛
        /// </summary>
        /// <returns>車輛清單</returns>
        public IEnumerable<IVehicle>? GetAllCars()
        {
            return _rentalCarUseCase.GetAllCars();
        }

        /// <summary>
        /// 租用車輛
        /// </summary>
        /// <param name="car">車輛資訊</param>
        /// <returns>是否成功租用</returns>
        public bool ToRentCar(IVehicle car)
        {
            Car mycar = new Car(car.Model) { CC = car.CC };

            return _rentalCarRepository.AddCar(car);
        }
    }
}

