# Barometric Pressure + Temperature Sensor Adafruit BMP280
Raspberry PI and Windows 10 IoT Core. Using Barometric Pressure + Temperature Sensor Adafruit BMP280. 

## What is this sensor?
This sensor is used for measuring barometric pressure with ±1 hPa absolute accuraccy, and temperature with ±1.0°C accuracy. 
Because pressure changes with altitude, and the pressure measurements are so good, you can also use it as an altimeter with  ±1 meter accuracy.

https://www.adafruit.com/products/2651

![image](https://cloud.githubusercontent.com/assets/13704023/22854209/fbba7afc-f071-11e6-89b4-7ac42eec75ca.png)

Temperature is calculated in degrees C, you can convert this to F by using the classic F = C * 9/5 + 32 equation.

Pressure is returned in the SI units of Pascals. 100 Pascals = 1 hPa = 1 millibar.
You can also calculate Altitude. However, you can only really do a good accurate job of calculating altitude if you know the hPa pressure at sea level for your location and day! The sensor is quite precise but if you do not have the data updated for the current day then it can be difficult to get more accurate than 10 meters.

## How to connect this sensor into Raspberry PI?
To connect this sensor to Raspberry PI you need 4 wires. Two of the wires used for voltage Vin (+3V from Raspberry) and ground GND and remaining two are used for data. 
As this is digital sensor, it uses I2C protocol to communicate with the Raspberry. For I2C we need two wires, Data (SDA) and Clock (SCL).
Connect sensor SDA and SCL pins accordingly to Raspberry SDA and SCL pins. 

## How do I write the code?
You need to add NuGet packages Glovebox.IoT.Devices and UnitsNet to your project and you are almost done :)

After adding these NuGet packages, you just need to write 2 lines of code.

1. Create an object for sensor: 
````C#
        BMP280 tempAndPressure = new BMP280();
````

2. Write a while-loop, to read data from the sensor every 1 sec.
````C#
            while (true)
            {
                double temperature = tempAndPressure.Temperature.DegreesCelsius;
                double pressure = tempAndPressure.Pressure.Hectopascals;
                Task.Delay(1000).Wait();
            }
````
Final code looks like this. 

If you run it, you do not see anything, because it just reads the data, but it doesnt show it anywhere.
You need to integrate this project with my other example, where I teach how to send this data into Azure.

````C#
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
````

## Can I change I2C address?
I2C address is used to communicate with the sensor. Many sensors have I2C address hardcoded, and this sensor exactly this. 
**You cannot change I2C address, it is fixed to 0x77.**
