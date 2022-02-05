using System;
using System.Collections.Generic;
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
                Console.WriteLine("Запись в файл... Файл будет создан на рабочем столе!");

                List<OpenWeather> collection = new List<OpenWeather>();

                collection.Add(ow);

                string getDekstop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


                string getNameFile = getDekstop + "\\weather.txt";

                if (File.Exists(getNameFile))
                {
                    Method();
                }
                else
                {
                    File.Create(getNameFile);
                    Method();

                }
                 void Method()
                {
                    using (StreamWriter streamWriter = new StreamWriter($"C://Users//{Environment.UserName}//Desktop//weather.txt", true, System.Text.Encoding.Default))
                    {

                        streamWriter.WriteLine("Город: " + ow.Name);
                        streamWriter.WriteLine("Температура: " + ow.main.Temperature);
                        streamWriter.WriteLine("Давление: " + ow.main.Pressure);
                        streamWriter.WriteLine("Влажность: " + ow.main.Humidity);
                        streamWriter.WriteLine("Скорость ветра: " + ow.wind.Speed);
                        streamWriter.WriteLine("Описание Погоды: " + ow.weather[0].Description);
                        streamWriter.WriteLine();
                        streamWriter.Dispose();
                        streamWriter.Close();
                    }
                }
            }



        }
    }
}
