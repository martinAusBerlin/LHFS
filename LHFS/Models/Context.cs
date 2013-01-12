using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LHFS.Models {
	public class LHFSContext : DbContext {

		public DbSet<Airport> Airport { get; set; }
		public DbSet<AirportAlternate> AirportAlternate { get; set; }
		public DbSet<AirportDeparture> AirportDeparture { get; set; }
		public DbSet<AirportArrival> AirportArrival { get; set; }
		public DbSet<AirportChart> AirportChart { get; set; }
		public DbSet<AirportDetail> AirportDetail { get; set; }
		public DbSet<AirportGroundOp> AirportGroundOp { get; set; }
		public DbSet<AirportHazard> AirportHazard { get; set; }
		public DbSet<AirportScenery> AirportScenery { get; set; }
		public DbSet<AirportTerrain> AirportTerrain { get; set; }
		public DbSet<AirportVersion> AirportVersion { get; set; }
		public DbSet<ChartType> ChartType { get; set; }
		public DbSet<Aircraft> Aircraft { get; set; }
		public DbSet<Region> Region { get; set; }
		public DbSet<AircraftType> AircraftType { get; set; }
		public DbSet<Connection> Connection { get; set; }
		public DbSet<Country> Country { get; set; }
		public DbSet<Flight> Flight { get; set; }
		public DbSet<Route> Route { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<ScheduleAircraftReplacement> ScheduleAircraftReplacement { get; set; }
		public DbSet<GalleryImage> GalleryImage { get; set; }
		public DbSet<Rank> Rank { get; set; }
		public DbSet<TypeRating> TypeRating { get; set; }
		public DbSet<Division> Division { get; set; }
		public DbSet<Rotation> Rotation { get; set; }
		public DbSet<Airline> Airline { get; set; }
		public DbSet<UserTypeRating> UserTypeRating { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {

			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  
					
			base.OnModelCreating(modelBuilder);
		}
		
	}

}