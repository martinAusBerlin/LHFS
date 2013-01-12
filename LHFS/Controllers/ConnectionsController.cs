using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using LHFS.Views.Connections.ViewModel;
using LHFS.Utils;
using System.Globalization;
using LHFS.Resources;
using LHFS.Models.Security;
using System.Diagnostics;

namespace LHFS.Controllers
{

	public class ConnectionsController : ControllerBase
    {
		private LHFSContext DatabaseContext = new LHFSContext();
		public const string AirportAndTimeZonePattern = @"([A-Z]{3})\s(\+|\-)\d{4}";
		public const string FlightPattern = @"(.*)\s(\d{1,2}:\d{1,2})\s(\d{1,2}:\d{1,2})\s([A-Za-z]{2}\d{3,4})\s([A-Za-z]{2})\s([A-Za-z0-9]{1,4})";
		public const string AircraftPattern = @"b>\s\(([A-Z0-9]{3,4})\)\</span\>";

		private readonly IConnectionRepository connectionRepository;
		private readonly IAirportRepository airportRepository;
		private readonly IAircraftTypeRepository aircraftTypeRepository;
		private readonly IScheduleAircraftReplacementRepository scheduleAircraftReplacementRepository;
		private readonly IAirlineRepository airlineRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ConnectionsController() : this(new ConnectionRepository(), new AirportRepository(), new AircraftTypeRepository(), new ScheduleAircraftReplacementRepository(), new AirlineRepository())
        {
        }

		public ConnectionsController(IConnectionRepository connectionRepository, IAirportRepository airportRepository, IAircraftTypeRepository aircraftTypeRepository, IScheduleAircraftReplacementRepository scheduleAircraftReplacementRepository, IAirlineRepository airlineRepository)
        {
			this.connectionRepository = connectionRepository;
			this.airportRepository = airportRepository;
			this.aircraftTypeRepository = aircraftTypeRepository;
			this.scheduleAircraftReplacementRepository = scheduleAircraftReplacementRepository;
			this.airlineRepository = airlineRepository;
        }

        //
        // GET: /Connections/

        public ViewResult Index()
        {
            return View(connectionRepository.All);
        }

		public PartialViewResult ConnectionsDepartingFrom(string icao) {

			var list = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType)
				.Where(t => t.DepartureAirportICAO == icao)
				.ToList();

			return PartialView("List", new SearchModel() { Date = DateTime.Today, Connections = list });
		}

		public PartialViewResult ConnectionsArrivingAt(string icao) {

			var list = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType)
				.Where(t => t.DestinationAirportICAO == icao)
				.ToList();

			return PartialView("List", new SearchModel() { Date = DateTime.Today, Connections = list });
		}


		
		public PartialViewResult LongestFlights(string aircraftTypeID) {

			var list = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType)
				.Where(t => t.AircraftTypeICAO == aircraftTypeID)
				.ToList()
				.OrderByDescending(t => t.FlightTime(DateTime.Today))
				.Take(10);

			return PartialView("List", new SearchModel() { Date = DateTime.Today, Connections = list });
		}

		public PartialViewResult ShortestFlights(string aircraftTypeID) {

			var list = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType)
				.Where(t => t.AircraftTypeICAO == aircraftTypeID)
				.ToList()
				.OrderBy(t => t.FlightTime(DateTime.Today))
				.Take(10);

			return PartialView("List", new SearchModel() { Date = DateTime.Today, Connections = list });
		}

		public PartialViewResult Reverse(int id) {

			var forward = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType)
				.Single(t => t.ID == id);

			var reverse = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType)
				.Where(t => t.DepartureAirportICAO == forward.DestinationAirportICAO && t.DestinationAirportICAO == forward.DepartureAirportICAO);

			return PartialView("List", new SearchModel() { Date = DateTime.Today, Connections = reverse, ShowReverseButton = true });
		}

		public PartialViewResult GetConnections(SearchModel viewModel) {

			TimeSpan minStartTime, maxStartTime;
			TimeSpan minDuration, maxDuration;

			if (viewModel.WithDepTime) {
				MatchCollection startTimeCollection = Regex.Matches(viewModel.StartTime, @"([0-9]{1,2}:[0-9]{2})");
				minStartTime = TimeSpan.Parse(startTimeCollection[0].Value);

				maxStartTime = new TimeSpan(int.Parse(startTimeCollection[1].Value.Split(':')[0]),    // hours
							   int.Parse(startTimeCollection[1].Value.Split(':')[1]),    // minutes
							   0);
			} else {
				minStartTime = maxStartTime = TimeSpan.Zero;
			}

			if (viewModel.WithMaxDuration) {
				MatchCollection durationCollection = Regex.Matches(viewModel.Duration, @"([0-9]{1,2}:[0-9]{2})");
				minDuration = TimeSpan.Parse(durationCollection[0].Value);
				maxDuration = new TimeSpan(int.Parse(durationCollection[1].Value.Split(':')[0]),    // hours
				   int.Parse(durationCollection[1].Value.Split(':')[1]),    // minutes
				   0);
			} else {
				minDuration = maxDuration = TimeSpan.Zero;
			}

			DayOfWeek weekday = viewModel.Date.DayOfWeek;

			var list = connectionRepository.AllIncluding(t => t.AircraftType)
				.Where(t =>
					weekday == DayOfWeek.Monday ? t.Monday : 
						weekday == DayOfWeek.Tuesday ? t.Tuesday : 
							weekday == DayOfWeek.Wednesday ? t.Wednesday : 
								weekday == DayOfWeek.Thursday ? t.Thursday : 
									weekday == DayOfWeek.Friday ? t.Friday : 
										weekday == DayOfWeek.Saturday ? t.Saturday : 
											weekday == DayOfWeek.Sunday ? t.Sunday : 
												false
					)
				.Where(t => (!t.StartsOn.HasValue || t.StartsOn <= viewModel.Date) && (!t.EndsOn.HasValue || t.EndsOn >= viewModel.Date))
				.WhereIf(viewModel.AircraftTypeIDs != null && viewModel.AircraftTypeIDs.Any(), t => viewModel.AircraftTypeIDs.Contains(t.AircraftTypeICAO))
				.WhereIf(!string.IsNullOrEmpty(viewModel.DepartureAirport), t => t.DepartureAirportICAO == viewModel.DepartureAirport)
				.WhereIf(!string.IsNullOrEmpty(viewModel.DestinationAirport), t => t.DestinationAirportICAO == viewModel.DestinationAirport)
				.WhereIf(!viewModel.AirlineIDs.Contains("all") && viewModel.AirlineIDs.Any(), t => viewModel.AirlineIDs.Contains(t.AirlineICAO))
				.WhereIf(viewModel.FlightNumber.HasValue, t => t.FlightNumber == viewModel.FlightNumber).ToList();
				

			if(viewModel.WithDepTime) {
				list = list.Where(t => t.DepTimeUtc(viewModel.Date) >= minStartTime && t.DepTimeUtc(viewModel.Date) <= maxStartTime).ToList();
			}

			if (viewModel.WithMaxDuration) {
				list = list.Where(t => t.FlightTime(viewModel.Date) <= maxDuration && t.FlightTime(viewModel.Date) >= minDuration).ToList();
			}

			viewModel.Connections = list.OrderBy(t => t.DepTimeUtc(viewModel.Date)).ToList();
			viewModel.ShowReverseButton = true;

			return PartialView("List", viewModel);
		}

		public ViewResult Search(SearchModel connSearch) {

			var allAvailableConnections = connectionRepository.AllIncluding(t => t.Departure, t => t.Destination, t => t.Departure.IcaoCountryCode.Country, t => t.Destination.IcaoCountryCode.Country, t => t.AircraftType).OrderBy(t => t.Departure.Name.Length).ThenBy(t => t.Destination.Name.Length);

			connSearch.PossibleDepartureAirports = allAvailableConnections.Select(t => t.Departure).Distinct().OrderBy(t => t.Name);
			connSearch.PossibleDestinationAirports = allAvailableConnections.Select(t => t.Destination).Distinct().OrderBy(t => t.Name);
			connSearch.PossibleAircraftTypes = new MultiSelectList(allAvailableConnections.Select(t => t.AircraftType).Distinct().ToList().OrderBy(t => t.Longname), "ICAO", "Longname");

			connSearch.AircraftTypeIDs = UserContext.GetCurrent().User.UserTypeRatings.Where(t => t.ValidTo == null).SelectMany(t => t.TypeRating.AircraftTypes.Select(g => g.ICAO)).ToArray();

			connSearch.PossibleAirlines = new List<SelectListItem>();
			connSearch.PossibleAirlines.Add(new SelectListItem() { Text = Schedule.AllAirlines, Value = "all" });
			connSearch.PossibleAirlines.AddRange(allAvailableConnections.Select(t => t.Airline).Distinct().Select(t => new SelectListItem() { Text = t.Name, Value = t.ICAO }).ToList());

			connSearch.Date = DateTime.UtcNow;

			return View(connSearch);
		}

        //
        // GET: /Connections/Details/5

        public ViewResult Details(int id)
        {
            return View(connectionRepository.Find(id));
        }

        //
        // GET: /Connections/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Connections/Create

        [HttpPost]
        public ActionResult Create(Connection connection)
        {
            if (ModelState.IsValid) {
                connectionRepository.InsertOrUpdate(connection);
                connectionRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Connections/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(connectionRepository.Find(id));
        }

        //
        // POST: /Connections/Edit/5

        [HttpPost]
        public ActionResult Edit(Connection connection)
        {
            if (ModelState.IsValid) {
                connectionRepository.InsertOrUpdate(connection);
                connectionRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Connections/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(connectionRepository.Find(id));
        }

        //
        // POST: /Connections/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            connectionRepository.Delete(id);
            connectionRepository.Save();

            return RedirectToAction("Index");
        }

		private Dictionary<string, string> _iataToIcaoTable;
		public Dictionary<string, string> IataToIcaoTable {

			get {

				if(_iataToIcaoTable == null) {
					_iataToIcaoTable = airportRepository.All.Where(t => t.IATA != null).Select(t => new { t.IATA, t.ICAO }).ToDictionary(t => t.IATA, t => t.ICAO);
				}

				return _iataToIcaoTable;
			}
		}

		private Connection GetConnection(Match match, string dep, string dest, IEnumerable<ScheduleAircraftReplacement> scheduleAircraftReplacements, IEnumerable<Airline> airlines) {

			Connection connectionToUpdate = new Connection();

			string weekdays = match.Groups[1].Value;
			string depTime = match.Groups[2].Value;
			string arrTime = match.Groups[3].Value;
			int flightNumber = int.Parse(Regex.Match(match.Groups[4].Value, @"(\d){1,4}").Groups[0].Value);

			string airline = match.Groups[5].Value;
			string aircraftType = GetCorrectedAircraftType(match.Groups[6].Value, match.Groups[4].Value.Substring(0, 2), flightNumber, scheduleAircraftReplacements);

			connectionToUpdate.Prefix = match.Groups[4].Value.Substring(0, 2);

			if (airline != "LH") {
				connectionToUpdate.AirlineICAO = airlines.SingleOrDefault(t => t.IATA == airline) != null ? airlines.SingleOrDefault(t => t.IATA == airline).ICAO : airline;
			} else {
				connectionToUpdate.AirlineICAO = "DLH";
			}
			connectionToUpdate.DepTimeLocal = TimeSpan.Parse(depTime);
			connectionToUpdate.ArrTimeLocal = TimeSpan.Parse(arrTime);
			connectionToUpdate.DepartureAirportICAO = dep;
			connectionToUpdate.DestinationAirportICAO = dest;
			connectionToUpdate.FlightNumber = flightNumber;
			connectionToUpdate.Monday = weekdays.Contains("1");
			connectionToUpdate.Tuesday = weekdays.Contains("2");
			connectionToUpdate.Wednesday = weekdays.Contains("3");
			connectionToUpdate.Thursday = weekdays.Contains("4");
			connectionToUpdate.Friday = weekdays.Contains("5");
			connectionToUpdate.Saturday = weekdays.Contains("6");
			connectionToUpdate.Sunday = weekdays.Contains("7");
			connectionToUpdate.CreatedOn = DateTime.UtcNow;
			connectionToUpdate.AircraftTypeICAO = aircraftType;

			return connectionToUpdate;
		}

		private string GetCorrectedAircraftType(string sourceAircraftType, string prefix, int flightNumber, IEnumerable<ScheduleAircraftReplacement> scheduleAircraftReplacements) {

			ScheduleAircraftReplacement currentReplacement = scheduleAircraftReplacements.Where(t => t.IsActive).SingleOrDefault(t => t.Source == sourceAircraftType);

			string outputAircraftType;

			if (currentReplacement != null && currentReplacement.AskTheWeb) {

				WebClient w = new WebClient();

				try {

					string page = w.DownloadString(@"http://data.flight24.com/flights/" + prefix + flightNumber + "/");

					MatchCollection matches = Regex.Matches(page, AircraftPattern);

					if (matches.Count > 0) {

						List<string> aircraftTypes = new List<string>();

						foreach (Match match in matches) {
							aircraftTypes.Add(match.Groups[1].Value);
						}

						outputAircraftType = aircraftTypes.GroupBy(t => t).Select(g => new { AircraftType = g.Key, NumberOfUses = g.Count() }).OrderByDescending(t => t.NumberOfUses).First().AircraftType;

					} else {
						outputAircraftType = sourceAircraftType;
					}

				} catch (WebException) {
					Debug.WriteLine("Web exception occured for " + prefix + flightNumber);
					outputAircraftType = sourceAircraftType;
				}
				
			} else if (currentReplacement != null && currentReplacement.DirectReplacementID != null) {
				outputAircraftType = currentReplacement.DirectReplacementID;
			} else {
				outputAircraftType = sourceAircraftType;
			}

			return outputAircraftType;

		}



		private static Airport GetAirport(Match match) {

			Airport airportToUpdate = new Airport();

			string IATA = match.Value.Substring(0, 3);
			airportToUpdate.IATA = IATA;

			return airportToUpdate;
		}

		public ActionResult ImportCargoSchedule() {

			
			string[] csvFiles = Directory.GetFiles(HttpContext.Server.MapPath("~") + @"\Schedule\", "*.csv", SearchOption.TopDirectoryOnly);

			StreamReader file = new StreamReader(csvFiles.First());

			string line;

			Dictionary<string, Airport> airportsDictionary = new Dictionary<string, Airport>();
			Dictionary<string, Connection> scheduleDictionary = new Dictionary<string, Connection>();

			int lineNumber = 1;

			IEnumerable<ScheduleAircraftReplacement> scheduleAircraftReplacements = scheduleAircraftReplacementRepository.All;

			while ((line = file.ReadLine()) != null) {
				string[] c = line.Split(';');

				if (lineNumber > 2) {
					

					if (c.Length >= 22 && c[7] != "0" && (c[21].Equals("77F") || c[21].Equals("M1F"))) {

						Connection cargoConn = GetCargoConnection(c, scheduleAircraftReplacements);

						if (cargoConn.FlightNumber > 8000) {

							if (!airportsDictionary.ContainsKey(cargoConn.DepartureAirportICAO)) {
								airportsDictionary.Add(cargoConn.DepartureAirportICAO, new Airport() { ICAO = cargoConn.DepartureAirportICAO });
							}

							if (!airportsDictionary.ContainsKey(cargoConn.DestinationAirportICAO)) {
								airportsDictionary.Add(cargoConn.DestinationAirportICAO, new Airport() { ICAO = cargoConn.DestinationAirportICAO });
							}

							scheduleDictionary.Add(cargoConn.FlightNumber + "," + cargoConn.DepartureAirportICAO + "," + cargoConn.DestinationAirportICAO + "," + cargoConn.StartsOn.Value.Date + "," + cargoConn.EndsOn.Value.Date, cargoConn);
						}
					}
				}
				
				lineNumber++;
			}


			DatabaseContext.Database.ExecuteSqlCommand("DELETE FROM Connections WHERE FlightNumber > 8000");

			IEnumerable<Airport> airports = airportRepository.All;

			foreach (var airportItem in airportsDictionary) {
				Airport currentAirport = airports.SingleOrDefault(t => t.ICAO == airportItem.Value.ICAO);

				if (currentAirport == null) {
					throw new Exception("Airport not found!");
					//airportRepository.Insert(airportItem.Value);
				}
			}

			airportRepository.Save();

			IEnumerable<AircraftType> aircraftTypes = aircraftTypeRepository.All;

			foreach (var aircraftType in scheduleDictionary.Select(t => t.Value.AircraftTypeICAO).Distinct()) {
				if (!aircraftTypes.Any(t => t.ICAO == aircraftType)) {
					AircraftType currentAircraftType = new AircraftType();
					currentAircraftType.ICAO = aircraftType;
					aircraftTypeRepository.Insert(currentAircraftType);
				}
			}

			aircraftTypeRepository.Save();

			IEnumerable<Airline> airlines = airlineRepository.All;

			foreach (var airline in scheduleDictionary.Select(t => t.Value.AirlineICAO).Distinct()) {
				if (!airlines.Any(t => t.ICAO == airline)) {
					Airline currentAirline = new Airline();
					currentAirline.ICAO = airline;
					airlineRepository.Insert(currentAirline);
				}
			}

			airlineRepository.Save();

			foreach (var connection in scheduleDictionary) {
				Connection conn = connection.Value;
				connectionRepository.InsertOrUpdate(conn);
			}

			connectionRepository.Save();


			return View();
			
		}

		private Connection GetCargoConnection(string[] c, IEnumerable<ScheduleAircraftReplacement> scheduleAircraftReplacements) {

			Connection connectionToUpdate = new Connection();

			connectionToUpdate.Prefix = c[6].Substring(0, 2);
			
			connectionToUpdate.DepTimeLocal = new TimeSpan(int.Parse(c[10].Substring(0, 2)), int.Parse(c[10].Substring(2, 2)), 0);
			connectionToUpdate.ArrTimeLocal = new TimeSpan(int.Parse(c[12].Substring(0, 2)), int.Parse(c[12].Substring(2, 2)), 0);
			connectionToUpdate.DepartureAirportICAO = IataToIcaoTable[c[8]];
			connectionToUpdate.DestinationAirportICAO = IataToIcaoTable[c[9]];
			connectionToUpdate.FlightNumber = int.Parse(Regex.Match(c[6], @"(\d){1,4}").Groups[0].Value);
			
			connectionToUpdate.Monday = c[14].Equals("1");
			connectionToUpdate.Tuesday = c[15].Equals("2");
			connectionToUpdate.Wednesday = c[16].Equals("3");
			connectionToUpdate.Thursday = c[17].Equals("4");
			connectionToUpdate.Friday = c[18].Equals("5");
			connectionToUpdate.Saturday = c[19].Equals("6");
			connectionToUpdate.Sunday = c[20].Equals("7");
			connectionToUpdate.CreatedOn = DateTime.UtcNow;
			connectionToUpdate.AircraftTypeICAO = GetCorrectedAircraftType(c[21], connectionToUpdate.Prefix, connectionToUpdate.FlightNumber, scheduleAircraftReplacements);

			switch (connectionToUpdate.AircraftTypeICAO) {
				case "MD11":
					connectionToUpdate.AirlineICAO = "GEC";
					break;
				case "B77L":
					connectionToUpdate.AirlineICAO = "BOX";
					break;
				default:
					throw new NotSupportedException(String.Format("Airline is unknown: {0}", connectionToUpdate.AircraftTypeICAO));
			}
			
			connectionToUpdate.StartsOn = DateTime.Parse(c[25]);
			connectionToUpdate.EndsOn = DateTime.Parse(c[26]);

			return connectionToUpdate;
		}


		public ActionResult ImportSchedule() {

			DirectoryInfo directoryInfo = new DirectoryInfo(HttpContext.Server.MapPath("~") + @"Schedule\");
			FileInfo[] fileInfos = directoryInfo.GetFiles("*.txt");
			string line;

			Dictionary<string, Airport> airportsDictionary = new Dictionary<string, Airport>();
			Dictionary<string, Connection> scheduleDictionary = new Dictionary<string, Connection>();

			IEnumerable<ScheduleAircraftReplacement> scheduleAircraftReplacements = scheduleAircraftReplacementRepository.All;
			IEnumerable<Airline> airlines = airlineRepository.All;

			foreach (FileInfo info in fileInfos) {
				StreamReader file = new StreamReader(info.FullName);

				string dep = null;
				string dest = null;

				while ((line = file.ReadLine()) != null) {

					MatchCollection airportMatches = Regex.Matches(line, AirportAndTimeZonePattern, RegexOptions.Singleline);
					Match connectionMatch = Regex.Match(line, FlightPattern, RegexOptions.Singleline);

					if (airportMatches.Count > 0) {
						try {
							dep = IataToIcaoTable[airportMatches[0].Value.Substring(0, 3)];
						} catch (KeyNotFoundException e) {
							Debug.WriteLine("No ICAO code found for IATA code: " + airportMatches[0].Value.Substring(0, 3));
							dep = null;
						}

						try {
							dest = IataToIcaoTable[airportMatches[1].Value.Substring(0, 3)];
						} catch (KeyNotFoundException e) {
							Debug.WriteLine("No ICAO code found for IATA code: " + airportMatches[1].Value.Substring(0, 3));
							dest = null;
						}

						if (dep != null && dest != null) {

							foreach (Match airportMatch in airportMatches) {

								string airportICAO = IataToIcaoTable[airportMatch.Value.Substring(0, 3)];

								if (!airportsDictionary.ContainsKey(airportICAO)) {
									airportsDictionary.Add(airportICAO, GetAirport(airportMatch));
								}
							}
						}

					} else if (connectionMatch.Success && dep != null && dest != null) {

						int flightNumber = int.Parse(Regex.Match(connectionMatch.Groups[4].Value, @"(\d){1,4}").Groups[0].Value);
						string prefix = connectionMatch.Groups[4].Value.Substring(0, 2);
						string sourceAircraftType = connectionMatch.Groups[6].Value;

						bool excluded = !(flightNumber > 0 && flightNumber < 5000 && prefix == "LH" && sourceAircraftType != "BUS");

						string key = connectionMatch.Groups[4].Value + "," + dep + "," + dest;

						// adds only non-codeshare flights of Lufthansa, they are between 0 and approx. 5000 (see wikipedia for more information)
						if (!scheduleDictionary.ContainsKey(key) && line.Contains("non stop") && !excluded) {
							scheduleDictionary.Add(key, GetConnection(connectionMatch, dep, dest, scheduleAircraftReplacements, airlines));
						}
					}

				}
				
			}


			// 1. delete all Connections
			// 2. update Airports
			// 3. add all missing AircraftTypes
			// 4. add all new Connections

			DatabaseContext.Database.ExecuteSqlCommand("DELETE FROM Connections WHERE FlightNumber < 8000");

			IEnumerable<Airport> airports = airportRepository.All;

			foreach (var airportItem in airportsDictionary) {
				Airport currentAirport = airports.SingleOrDefault(t => t.IATA == airportItem.Value.IATA);

				if (currentAirport == null) {
					throw new Exception("Airport not found!");
					//airportRepository.Insert(airportItem.Value);
				}
			}

			airportRepository.Save();

			IEnumerable<AircraftType> aircraftTypes = aircraftTypeRepository.All;

			foreach (var aircraftType in scheduleDictionary.Select(t => t.Value.AircraftTypeICAO).Distinct()) {
				if (!aircraftTypes.Any(t => t.ICAO == aircraftType)) {
					AircraftType currentAircraftType = new AircraftType();
					currentAircraftType.ICAO = aircraftType;
					aircraftTypeRepository.Insert(currentAircraftType);
				}
			}

			aircraftTypeRepository.Save();

			foreach (var airline in scheduleDictionary.Select(t => t.Value.AirlineICAO).Distinct()) {
				if (!airlines.Any(t => t.ICAO == airline)) {
					Airline currentAirline = new Airline();
					currentAirline.ICAO = airline;
					airlineRepository.Insert(currentAirline);
				}
			}

			airlineRepository.Save();

			foreach (var connection in scheduleDictionary) {
				connectionRepository.InsertOrUpdate(connection.Value);
			}

			connectionRepository.Save();

			return View();

		}

    }
}

