using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RentalCar
{
    public class RV : IVehicle
    {
        private ModelType _modelName = ModelType.Lexus;
        private string _cc;

        public ModelType Model { get => _modelName; set => value = _modelName; }
        public string CC { get => _cc; set => value = _cc; }

        public int CalculateRentalCost(int daysRented)
        {
            return daysRented * (int)_modelName; // 假設為美元
        }

        public TimeSpan ChoiseRentalTime(DateTime start, DateTime end)
        {
            return end - start;
        }

        public VehicleType GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}
