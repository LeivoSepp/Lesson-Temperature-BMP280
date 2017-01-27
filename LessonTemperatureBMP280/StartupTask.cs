using System;
using Windows.ApplicationModel.Background;
using Glovebox.Graphics.Drivers;
using Glovebox.Graphics.Components;
using Glovebox.IoT.Devices.Sensors;
using System.Threading.Tasks;

namespace LessonTemperatureBMP280
{
    public sealed class StartupTask : IBackgroundTask
    {
        Ht16K33 driver = new Ht16K33(new byte[] { 0x72 }, Ht16K33.Rotate.None);
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            LED8x8Matrix matrix = new LED8x8Matrix(driver);
            BMP280 tempAndPressure = new BMP280();
            
            while (true)
            {
                var message = $"{Math.Round(tempAndPressure.Temperature.DegreesCelsius, 1)}C, {Math.Round(tempAndPressure.Pressure.Hectopascals, 1)}hPa ";
                matrix.ScrollStringInFromRight(message, 70);
                Task.Delay(1000).Wait();
            }
        }
    }
}
