using Application.RentalCar.port.In;
using Application.RentalCar.port.Out;
using Domain.RentalCar;
//using Domain.Lab120240821;

namespace Application.RentalCar
{
    public class RentalCarSerAppServices
    {
        private readonly IRentalCarRepository _rentalCarRepository;
        private readonly IRentalCarUseCase _rentalCarUseCase;

        public RentalCarSerAppServices(IRentalCarRepository rentalCarRepository, IRentalCarUseCase rentalCarUseCase)
        {
            _rentalCarRepository = rentalCarRepository;
            _rentalCarUseCase = rentalCarUseCase;
        }
        public IEnumerable<Car>? GetAllCars()
        {
            return _rentalCarUseCase.GetAllCars() as IEnumerable<Car>;
        }

        public bool ToRentCar(IVehicle car)
        {
            Car mycar = new Car() { CC = car.CC, Model = car.Model };

            return _rentalCarRepository.AddCar(car);
        }
    }
}

