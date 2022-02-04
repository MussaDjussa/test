using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
namespace WeatherConsoleApplicaiton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите город, в котором хотите узнать погоду:");

                string city = Console.ReadLine();

                WebRequest webRequest = WebRequest.Create($@"https://api.openweathermap.org/data/2.5/weather?q={city}&APPID=ad65da604c31a382a6fc8b5478f3f08f");

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-urlencoded";

                WebResponse webResponse = webRequest.GetResponse();

                string answer = string.Empty;

                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        answer = reader.ReadToEnd();
                    }
                }

                webResponse.Close();

                OpenWeather ow = JsonConvert.DeserializeObject<OpenWeather>(answer);

                Console.WriteLine("Город: " + ow.Name);
                Console.WriteLine("Температура: " + ow.main.Temperature);
                Console.WriteLine("Давление: " + ow.main.Pressure);
                Console.WriteLine("Влажность: " + ow.main.Humidity);
                Console.WriteLine("Скорость ветра: " + ow.wind.Speed);
                Console.WriteLine("Описание Погоды: " + ow.weather[0].Description);
                Console.WriteLine();
            }
        }
    }
}
