using Application.RentalCar.port.In;
using Application.RentalCar.port.Out;
using Domain.RentalCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RentalCar
{
    public class RentalCarRepository : IRentalCarRepository, IRentalCarUseCase
    {
        private List<IVehicle> vehicles;
        public RentalCarRepository()
        {
            vehicles = new List<IVehicle>(
                new IVehicle[]
                {
                    new Car() { Model = ModelType.Toyota, CC = "2000" },
                    new RV() { Model = ModelType.Ford, CC = "5000" }
                });
        }
        public bool AddCar(IVehicle car)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVehicle> GetAllCars()
        {
            return vehicles;
        }

        public bool RemoveCar(IVehicle car)
        {
            throw new NotImplementedException();
        }

        public bool ToRentCar(IVehicle car)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCar(IVehicle car)
        {
            throw new NotImplementedException();
        }
    }
}
