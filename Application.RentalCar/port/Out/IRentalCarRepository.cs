//using Domain.Lab120240821;
using Domain.RentalCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RentalCar.port.Out
{
    public interface IRentalCarRepository
    {
        
        bool AddCar(IVehicle car);
        bool RemoveCar(IVehicle car);
        bool UpdateCar(IVehicle car);
    }
}

