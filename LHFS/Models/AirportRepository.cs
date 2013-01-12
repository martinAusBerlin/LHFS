using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity.Validation;
using System.Diagnostics;
using LHFS.Models.Interfaces;

namespace LHFS.Models
{ 
    public class AirportRepository : IAirportRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Airport> All
        {
			get { return context.Airport; }
        }

        public IQueryable<Airport> AllIncluding(params Expression<Func<Airport, object>>[] includeProperties)
        {
            IQueryable<Airport> query = context.Airport;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Airport Find(string id)
        {
            return context.Airport.Find(id);
        }

        public void Insert(Airport airport)
        {
			AirportArrival airportArrival = new AirportArrival();
			AirportDeparture airportDeparture = new AirportDeparture();
			AirportDetail airportDetail = new AirportDetail();
			AirportGroundOp airportGroundOp = new AirportGroundOp();
			AirportHazard airportHazard = new AirportHazard();
			AirportTerrain airportTerrain = new AirportTerrain();
			AirportVersion airportVersion = new AirportVersion();

			IAirportArrivalRepository airportArrivalRepository = new AirportArrivalRepository(context);
			IAirportDepartureRepository airportDepartureRepository = new AirportDepartureRepository(context);
			IAirportDetailRepository airportDetailRepository = new AirportDetailRepository(context);
			IAirportGroundOpRepository airportGroundOpRepository = new AirportGroundOpRepository(context);
			IAirportHazardRepository airportHazardRepository = new AirportHazardRepository(context);
			IAirportTerrainRepository airportTerrainRepository = new AirportTerrainRepository(context);
			IAirportVersionRepository airportVersionRepository = new AirportVersionRepository(context);

			IEnumerable<IVersionedContent> entitiesToInitialize = new List<IVersionedContent> { airportArrival, airportDeparture, airportDetail, airportGroundOp, airportHazard, airportTerrain };

			airportArrival.Text = "NIL.";
			airportDeparture.Text = "NIL.";
			airportGroundOp.Text = "NIL.";
			airportHazard.Text = "NIL.";
			airportTerrain.Text = "NIL.";

			// TODO assign correct userID

			foreach (IVersionedContent entity in entitiesToInitialize) {
				entity.CreatedByUserID = 1;
				entity.CreatedOn = DateTime.UtcNow;
				entity.ApprovedByUserID = 1;
				entity.ApprovedOn = DateTime.UtcNow;
				entity.VersionNumber = 1;
			}

			airportArrivalRepository.InsertOrUpdate(airportArrival);
			airportDepartureRepository.InsertOrUpdate(airportDeparture);
			airportDetailRepository.InsertOrUpdate(airportDetail);
			airportGroundOpRepository.InsertOrUpdate(airportGroundOp);
			airportHazardRepository.InsertOrUpdate(airportHazard);
			airportTerrainRepository.InsertOrUpdate(airportTerrain);

			airportVersion.Airport = airport;
			airportVersion.Arrival = airportArrival;
			airportVersion.Departure = airportDeparture;
			airportVersion.Detail = airportDetail;
			airportVersion.GroundOp = airportGroundOp;
			airportVersion.Hazard = airportHazard;
			airportVersion.Terrain = airportTerrain;
			airportVersion.CreatedOn = DateTime.UtcNow;

			context.Airport.Add(airport);

			airportVersionRepository.InsertOrUpdate(airportVersion);
        }

		public void Update(Airport airport)
		{
			context.Airport.Attach(airport);
			context.Entry(airport).State = EntityState.Modified;
		}

		public bool Update(int userID, int versionID, AirportTerrain terrain, AirportArrival arrival, AirportDeparture departure, AirportDetail detail, AirportGroundOp groundOp, AirportHazard hazard) {

			IAirportArrivalRepository airportArrivalRepository = new AirportArrivalRepository(context);
			IAirportDepartureRepository airportDepartureRepository = new AirportDepartureRepository(context);
			IAirportDetailRepository airportDetailRepository = new AirportDetailRepository(context);
			IAirportGroundOpRepository airportGroundOpRepository = new AirportGroundOpRepository(context);
			IAirportHazardRepository airportHazardRepository = new AirportHazardRepository(context);
			IAirportTerrainRepository airportTerrainRepository = new AirportTerrainRepository(context);
			IAirportVersionRepository airportVersionRepository = new AirportVersionRepository(context);

			AirportVersion repositoryVersion = airportVersionRepository.FindIncluding(versionID, t => t.Arrival, t => t.Departure, t => t.Detail, t => t.GroundOp, t => t.Hazard, t => t.Terrain);

			//TODO Approval ist nicht geregelt

			AirportVersion newAirportVersion = new AirportVersion();
			newAirportVersion.AirportICAO = repositoryVersion.AirportICAO;
			newAirportVersion.ID = default(int);
			newAirportVersion.CreatedOn = DateTime.UtcNow;

			bool hasChanged = false;

			if(HasChanged(repositoryVersion.Arrival, arrival)) {
				
				arrival.CreatedByUserID = userID;
				arrival.CreatedOn = DateTime.UtcNow;
				arrival.VersionNumber = repositoryVersion.Arrival.VersionNumber + 1;
				arrival.ID =  default(int);

				newAirportVersion.Arrival = arrival;
				airportArrivalRepository.InsertOrUpdate(arrival);
				hasChanged = true;
				
			} else {
				newAirportVersion.AirportArrivalID = repositoryVersion.AirportArrivalID;
			}

			if (HasChanged(repositoryVersion.Departure, departure)) {

				departure.CreatedByUserID = userID;
				departure.CreatedOn = DateTime.UtcNow;
				departure.VersionNumber = repositoryVersion.Departure.VersionNumber + 1;
				departure.ID = default(int);

				newAirportVersion.Departure = departure;
				airportDepartureRepository.InsertOrUpdate(departure);
				hasChanged = true;
			} else {
				newAirportVersion.AirportDepartureID = repositoryVersion.AirportDepartureID;
			}

			if (HasChanged(repositoryVersion.Detail, detail)) {

				detail.CreatedByUserID = userID;
				detail.CreatedOn = DateTime.UtcNow;
				detail.VersionNumber = repositoryVersion.Detail.VersionNumber + 1;
				detail.ID = default(int);

				newAirportVersion.Detail = detail;
				airportDetailRepository.InsertOrUpdate(detail);
				hasChanged = true;
			} else {
				newAirportVersion.AirportDetailID = repositoryVersion.AirportDetailID;
			}

			if (HasChanged(repositoryVersion.GroundOp, groundOp)) {

				groundOp.CreatedByUserID = userID;
				groundOp.CreatedOn = DateTime.UtcNow;
				groundOp.VersionNumber = repositoryVersion.GroundOp.VersionNumber + 1;
				groundOp.ID = default(int);

				newAirportVersion.GroundOp = groundOp;
				airportGroundOpRepository.InsertOrUpdate(groundOp);
				hasChanged = true;
			} else {
				newAirportVersion.AirportGroundOpID = repositoryVersion.AirportGroundOpID;
			}

			if (HasChanged(repositoryVersion.Hazard, hazard)) {

				hazard.CreatedByUserID = userID;
				hazard.CreatedOn = DateTime.UtcNow;
				hazard.VersionNumber = repositoryVersion.Hazard.VersionNumber + 1;
				hazard.ID = default(int);

				newAirportVersion.Hazard = hazard;
				airportHazardRepository.InsertOrUpdate(hazard);
				hasChanged = true;
			} else {
				newAirportVersion.AirportHazardID = repositoryVersion.AirportHazardID;
			}

			if (HasChanged(repositoryVersion.Terrain, terrain)) {

				terrain.CreatedByUserID = userID;
				terrain.CreatedOn = DateTime.UtcNow;
				terrain.VersionNumber = repositoryVersion.Terrain.VersionNumber + 1;
				terrain.ID = default(int);

				newAirportVersion.Terrain = terrain;
				airportTerrainRepository.InsertOrUpdate(terrain);
				hasChanged = true;
			} else {
				newAirportVersion.AirportTerrainID = repositoryVersion.AirportTerrainID;
			}

			if (hasChanged) {
				airportVersionRepository.InsertOrUpdate(newAirportVersion);
			}

			return hasChanged;

		}

		private bool HasChanged(AirportTerrain repositoryVersion, AirportTerrain currentVersion) {
			return repositoryVersion.Text != currentVersion.Text;
		}

		private bool HasChanged(AirportHazard repositoryVersion, AirportHazard currentVersion) {
			return repositoryVersion.Text != currentVersion.Text;
		}

		private bool HasChanged(AirportGroundOp repositoryVersion, AirportGroundOp currentVersion) {
			return repositoryVersion.Text != currentVersion.Text;
		}

		private bool HasChanged(AirportDetail repositoryVersion, AirportDetail currentVersion) {
			return 
				repositoryVersion.Category != currentVersion.Category || 
				repositoryVersion.Elevation != currentVersion.Elevation || 
				repositoryVersion.Lat != currentVersion.Lat || 
				repositoryVersion.Lon != currentVersion.Lon ||
				repositoryVersion.LongestRunway != currentVersion.LongestRunway || 
				repositoryVersion.MagVar != currentVersion.MagVar;
		}

		private bool HasChanged(AirportArrival repositoryVersion, AirportArrival currentVersion) {
			return repositoryVersion.Text != currentVersion.Text;
		}

		private bool HasChanged(AirportDeparture repositoryVersion, AirportDeparture currentVersion) {
			return repositoryVersion.Text != currentVersion.Text;
		}

        public void Delete(string id)
        {
            var airport = context.Airport.Find(id);
            context.Airport.Remove(airport);
        }

        public void Save()
        {
			context.SaveChanges();
        }

    }

	public interface IAirportRepository
    {
		IQueryable<Airport> All { get; }
		IQueryable<Airport> AllIncluding(params Expression<Func<Airport, object>>[] includeProperties);
		Airport Find(string id);
		void Insert(Airport airport);
		void Update(Airport airport);
		bool Update(int userID, int versionID, AirportTerrain terrain, AirportArrival arrival, AirportDeparture departure, AirportDetail detail, AirportGroundOp groundOp, AirportHazard hazard);
        void Delete(string id);
        void Save();
    }
}