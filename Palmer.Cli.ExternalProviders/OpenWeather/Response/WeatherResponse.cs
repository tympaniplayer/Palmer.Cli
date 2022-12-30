namespace Palmer.Cli.ExternalProviders.OpenWeather.Response;

public sealed record WeatherResponse(
  Coord Coord,
  IEnumerable<Weather> Weather,
  string Base,
  Main Main,
  int Visibility,
  Wind Wind,
  Rain Rain);

public sealed record Coord(
  double Lon,
  double Lat);

public sealed record Weather(int Id,
  string Main,
  string Description,
  string Icon);

public sealed record Main(
  double Temp,
  double FeelsLike,
  double TempMin,
  double TempMax,
  int Pressure,
  int Humidity,
  int SeaLevel,
  int GrndLevel);

public sealed record Wind(
  double Speed,
  int Deg,
  double Gust);

public sealed record Rain(
  double _1h);

/*
 * {
  "coord": {
    "lon": 10.99,
    "lat": 44.34
  },
  "weather": [
    {
      "id": 501,
      "main": "Rain",
      "description": "moderate rain",
      "icon": "10d"
    }
  ],
  "base": "stations",
  "main": {
    "temp": 298.48,
    "feels_like": 298.74,
    "temp_min": 297.56,
    "temp_max": 300.05,
    "pressure": 1015,
    "humidity": 64,
    "sea_level": 1015,
    "grnd_level": 933
  },
  "visibility": 10000,
  "wind": {
    "speed": 0.62,
    "deg": 349,
    "gust": 1.18
  },
  "rain": {
    "1h": 3.16
  },
  "clouds": {
    "all": 100
  },
  "dt": 1661870592,
  "sys": {
    "type": 2,
    "id": 2075663,
    "country": "IT",
    "sunrise": 1661834187,
    "sunset": 1661882248
  },
  "timezone": 7200,
  "id": 3163858,
  "name": "Zocca",
  "cod": 200
}


{"coord":{"lon":-84.4738,"lat":33.3943},"weather":[{"id":800,"main":"Clear","description":"clear sky","icon":"01d"}],"base":"stations","main":{"temp":65.3,"feels_like":64.72,"temp_min":60.17,"temp_max":66.11,"pressure":1019,"humidity":68},"visibility":10000,"wind":{"speed":5.75,"deg":90},"clouds":{"all":0},"dt":1672438556,"sys":{"type":2,"id":2006982,"country":"US","sunrise":1672404078,"sunset":1672439957},"timezone":-18000,"id":4194465,"name":"Fayette","cod":200}
*/