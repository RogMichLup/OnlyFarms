using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class StationConnections {
        private static StationConnections instance;
        private List<StationPrototype> weatherStations;
        private StationConnections() {
            weatherStations = new List<StationPrototype>();
        }
        public static StationConnections GetInstance() {
            if(instance == null) {
                instance = new StationConnections();
            }
            return instance;
        }
        public bool DoesConnectionExist(int? stationID) {
            if (weatherStations.Find(p => p.GetFieldID() == stationID) != null)
                return true;
            return false;
        }
        public StationPrototype GetConnection(int? stationID) {
            return weatherStations.Find(p => p.GetFieldID() == stationID);
        }
        public void ConnectNewStation(StationPrototype station) {
            weatherStations.Add(station);
        }
        public void UpdateStation(StationPrototype station) {
            weatherStations[weatherStations.FindIndex(p => p.GetFieldID() == station.GetFieldID())] = station;
        }
    }
}
