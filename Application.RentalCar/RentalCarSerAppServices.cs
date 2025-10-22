using Application.RentalCar.port.In;
using Application.RentalCar.port.Out;
using Domain.RentalCar;
//using Domain.Lab120240821;

namespace Application.RentalCar
{
    /// <summary>
    /// �����A�����ε{���A��
    /// </summary>
    public class RentalCarSerAppServices
    {
        private readonly IRentalCarRepository _rentalCarRepository;
        private readonly IRentalCarUseCase _rentalCarUseCase;

        /// <summary>
        /// �غc�禡�A��l�ƪA��
        /// </summary>
        /// <param name="rentalCarRepository">������Ʈw�s������</param>
        /// <param name="rentalCarUseCase">�����ΨҤ���</param>
        public RentalCarSerAppServices(IRentalCarRepository rentalCarRepository, IRentalCarUseCase rentalCarUseCase)
        {
            _rentalCarRepository = rentalCarRepository;
            _rentalCarUseCase = rentalCarUseCase;
        }

        /// <summary>
        /// ���o�Ҧ��i������
        /// </summary>
        /// <returns>�����M��</returns>
        public IEnumerable<IVehicle>? GetAllCars()
        {
            return _rentalCarUseCase.GetAllCars();
        }

        /// <summary>
        /// ���Ψ���
        /// </summary>
        /// <param name="car">������T</param>
        /// <returns>�O�_���\����</returns>
        public bool ToRentCar(IVehicle car)
        {
            Car mycar = new Car(car.Model) { CC = car.CC };

            return _rentalCarRepository.AddCar(car);
        }
    }
}

