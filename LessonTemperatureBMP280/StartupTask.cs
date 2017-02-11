using System;
using Windows.ApplicationModel.Background;
using Glovebox.IoT.Devices.Sensors;
using System.Threading.Tasks;

namespace LessonTemperatureBMP280
{
    public sealed class StartupTask : IBackgroundTask
    {
        BMP280 tempAndPressure = new BMP280();
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            while (true)
            {
                double temperature = tempAndPressure.Temperature.DegreesCelsius;
                double pressure = tempAndPressure.Pressure.Hectopascals;
                Task.Delay(1000).Wait();
            }
        }
    }
}
