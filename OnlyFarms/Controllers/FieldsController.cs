using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using OnlyFarms.Models;
using OnlyFarms.Models.Decorators;
using OnlyFarms.Services;

namespace OnlyFarms.Controllers {
    public class FieldsController : Controller {
        private readonly FarmContext _context;
        private readonly IWeatherService _weatherService;

        public FieldsController(FarmContext context, IWeatherService weatherService) {
            _context = context;
            _weatherService = weatherService;
        }

        // GET: Fields
        public async Task<IActionResult> Index() {
            return View(await _context.Fields.ToListAsync());
        }

        // GET: Fields/Details/5
        public async Task<IActionResult> Details(int? id) {


            if (id == null) {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);

            if (@field == null) {
                return NotFound();
            }

            WeatherUnit weather = new WeatherUnit();
            StationPrototype weatherStation = Global.weatherStations.Find(p => p.GetFieldID() == field.ID);
            if (weatherStation != null) {
                weather = TransformWeather(weatherStation.GetWeather());
                weather.Field = field;
                weather.FieldID = field.ID;
            }
            else {
                weatherStation = new Weather(field.ID);
                Global.weatherStations.Add(weatherStation);
                weather = TransformWeather(weatherStation.GetWeather());
                weather.Field = field;
                weather.FieldID = field.ID;
            }

            List<Cultivation> cultivations = await _context.Cultivations
                                                    .Include(c => c.Crop)
                                                    .Include(c => c.Field)
                                                    .Where(c => c.FieldID == field.ID)
                                                    .ToListAsync();
            ViewBag.cultivations = cultivations;
            ViewBag.weather = weather;

            return View(@field);
        }

        // GET: Fields/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Fields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ID,Tag,City,Street,FieldSurface")] Field @field) {
            if (ModelState.IsValid) {
                _context.Add(@field);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        // GET: Fields/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var @field = await _context.Fields.FindAsync(id);
            if (@field == null) {
                return NotFound();
            }
            return View(@field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Tag,City,Street,FieldSurface")] Field @field) {
            if (id != @field.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(@field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!FieldExists(@field.ID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        // GET: Fields/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@field == null) {
                return NotFound();
            }

            return View(@field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var @field = await _context.Fields.FindAsync(id);
            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldExists(int id) {
            return _context.Fields.Any(e => e.ID == id);
        }
        public async Task<IActionResult> UpdateWeather(int? id) {
            if (id == null) {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);

            if (@field == null) {
                return NotFound();
            }

            WeatherUnit weather = new WeatherUnit();
            StationPrototype weatherStation = Global.weatherStations.Find(p => p.GetFieldID() == field.ID);
            if (weatherStation != null) {
                weatherStation = weatherStation.Clone();
                weatherStation.UpdateWeather();
                weather = TransformWeather(weatherStation.GetWeather());
            }
            else {
                weatherStation = new Weather(field.ID);
                Global.weatherStations.Add(weatherStation);
            }

            var harvestObserver = new HarvestObserver();
            var fertilizationObserver = new FertilizationObserver();

            _weatherService.Attach(harvestObserver);
            _weatherService.Attach(fertilizationObserver);

            Console.WriteLine("Updating Weather Status...");

            _weatherService.UpdateWeather(weather);
            Global.weatherStations[Global.weatherStations.FindIndex(p => p.GetFieldID() == field.ID)] = weatherStation; //updating the values in server storage


            List<Cultivation> cultivations = await _context.Cultivations
                                                    .Include(c => c.Crop)
                                                    .Include(c => c.Field)
                                                    .Where(c => c.FieldID == field.ID)
                                                    .ToListAsync();
            ViewBag.cultivations = cultivations;
            ViewBag.weather = weather;

            return View("Details", @field);
        }
        private WeatherUnit TransformWeather(string weatherString) {
            WeatherUnit weather = InitializeWeatherUnit();
            List<string> partials = weatherString.Split(" ").ToList();
            partials.RemoveAll(p => p.Length < 1);
            foreach (string singlePartial in partials) {
                List<string> nameMeasurement = singlePartial.Split(";").ToList();
                if (nameMeasurement.Last() == "unmeasured") continue; //skip not measured units
                switch (nameMeasurement.First()) {
                    case "barometer":
                        weather.AirPressure = Int32.Parse(nameMeasurement.Last());
                        break;
                    case "directionAnemometer":
                        weather.WindDirection = nameMeasurement.Last();
                        break;
                    case "moistureMeter":
                        weather.Moisture = Int32.Parse(nameMeasurement.Last());
                        break;
                    case "rainGauge":
                        weather.RainfallAmount = Int32.Parse(nameMeasurement.Last());
                        break;
                    case "speedAnemometer":
                        weather.WindSpeed = Int32.Parse(nameMeasurement.Last());
                        break;
                    case "thermometer":
                        weather.Temperature = Double.Parse(nameMeasurement.Last());
                        break;
                    case "timestamp":
                        weather.Date = DateTime.Parse(nameMeasurement[1] + " " + nameMeasurement.Last());
                        break;
                    default:
                        Console.WriteLine("Error while transforming weather");
                        break;
                }
            }
            return weather;
        }
        private WeatherUnit InitializeWeatherUnit() {
            WeatherUnit weather = new WeatherUnit();

            weather.AirPressure = 2000000;
            weather.RainfallAmount = 2000000;
            weather.Moisture = 2000000;
            weather.Temperature = 2000000;
            weather.WindDirection = "unknown";
            weather.WindSpeed = 2000000;

            return weather;
        }
        public async Task<IActionResult> PressSpeed(int? id) { //buttons for addint tools to station
            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            await ButtonPress(id, "addSpeedAnemometer");
            return View("Details", @field);
        }
        public async Task<IActionResult> PressDirection(int? id) {
            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            await ButtonPress(id, "addDirectionAnemometer");
            return View("Details", @field);
        }
        public async Task<IActionResult> PressTemperature(int? id) {
            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            await ButtonPress(id, "addThermometer");
            return View("Details", @field);
        }
        public async Task<IActionResult> PressAirPressure(int? id) {
            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            await ButtonPress(id, "addBarometer");
            return View("Details", @field);
        }
        public async Task<IActionResult> PressMoisture(int? id) {
            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            await ButtonPress(id, "addMoistureMeter");
            return View("Details", @field);
        }
        public async Task<IActionResult> PressRain(int? id) {
            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            await ButtonPress(id, "addRainGauge");
            return View("Details", @field);
        }
        public async Task<IActionResult> ButtonPress(int? id, string command) {


            if (id == null) {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);

            if (@field == null) {
                return NotFound();
            }

            WeatherUnit weather = new WeatherUnit();
            StationPrototype weatherStation = Global.weatherStations.Find(p => p.GetFieldID() == field.ID);
            if (weatherStation != null) {
                weatherStation = weatherStation.Clone(); //clone static connection to it's own object so multiple users can access
                weatherStation = AddToolToStation(weatherStation, command);
                weatherStation.UpdateWeather();
                weather = TransformWeather(weatherStation.GetWeather());
                weather.Field = field;
                weather.FieldID = field.ID;
            }
            else {
                return NotFound();
            }
            Global.weatherStations[Global.weatherStations.FindIndex(p => p.GetFieldID() == field.ID)] = weatherStation; //updating the values in server storage

            List<Cultivation> cultivations = await _context.Cultivations
                                                    .Include(c => c.Crop)
                                                    .Include(c => c.Field)
                                                    .Where(c => c.FieldID == field.ID)
                                                    .ToListAsync();
            ViewBag.cultivations = cultivations;
            ViewBag.weather = weather;

            return View(@field);
        }
        private StationPrototype AddToolToStation(StationPrototype station, string command) {
            StationPrototype finishedStation = new Weather(0); //initializing finished station to default state
            switch (command) { //decorate the station with correct decorator
                case "addRainGauge":
                    finishedStation = new RainGauge(station);
                    break;
                case "addMoistureMeter":
                    finishedStation = new MoistureMeter(station);
                    break;
                case "addBarometer":
                    finishedStation = new Barometer(station);
                    break;
                case "addThermometer":
                    finishedStation = new Thermometer(station);
                    break;
                case "addDirectionAnemometer":
                    finishedStation = new DirectionAnemometer(station);
                    break;
                case "addSpeedAnemometer":
                    finishedStation = new SpeedAnemometer(station);
                    break;
            }
            return finishedStation;
        }
    }
}
