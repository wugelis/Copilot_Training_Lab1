namespace Domain.RentalCar
{
    /// <summary>
    /// 汽車類別
    /// </summary>
    public class Car : IVehicle
    {
        private ModelType _modelName = ModelType.Toyota;
        private string _cc;
        private string _carModelName;

        public ModelType Model { get => _modelName; set => _modelName = value; }
        public string CC { get => _cc; set => _cc = value; }
        /// <summary>
        /// 車型名稱
        /// </summary>
        public string CarModelName { get => _carModelName; set => _carModelName = value; }

        /// <summary>
        /// 建構函式
        /// </summary>
        public Car(ModelType modelName)
        {
            _modelName = modelName;
        }
        
        /// <summary>
        /// 計算租車費用
        /// </summary>
        /// <param name="daysRented">租車天數</param>
        /// <returns>租車費用</returns>
        public int CalculateRentalCost(int daysRented)
        {
            return daysRented * (int)_modelName; // 假設為美元
        }

        /// <summary>
        /// 選擇租車時間
        /// </summary>
        /// <param name="start">開始時間</param>
        /// <param name="end">結束時間</param>
        /// <returns>租車時間長度</returns>
        public TimeSpan ChoiseRentalTime(DateTime start, DateTime end)
        {
            return end - start;
        }
        
        /// <summary>
        /// 取得車輛類型
        /// </summary>
        /// <returns>車輛類型</returns>
        public VehicleType GetVehicleType() => VehicleType.Car;
    }
}

