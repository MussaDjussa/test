using Newtonsoft.Json;
namespace WeatherConsoleApplicaiton
{
    internal class OpenWeather
    {
        public Coord coord;
        public Weather[] weather;
        public Main main;
        public Wind wind;
        public Clouds clouds;
        public Sys sys;
        [JsonProperty("base")]
        public string Base { get; set; }

        public double Visibility { get; set; }
        public double DT { get; set; }
        public int ID { get; set; }

        public string Name { get; set; }

        public double COD { get; set; }

    }
}
