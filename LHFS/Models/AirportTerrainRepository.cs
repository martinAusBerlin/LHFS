using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportTerrainRepository : IAirportTerrainRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportTerrainRepository(LHFSContext dbContext) {
			context = dbContext;
		}

		public AirportTerrainRepository() { }

        public IQueryable<AirportTerrain> All
        {
			get { return context.AirportTerrain; }
        }

        public IQueryable<AirportTerrain> AllIncluding(params Expression<Func<AirportTerrain, object>>[] includeProperties)
        {
            IQueryable<AirportTerrain> query = context.AirportTerrain;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportTerrain Find(int id)
        {
            return context.AirportTerrain.Find(id);
        }

        public void InsertOrUpdate(AirportTerrain airportterrain)
        {
            if (airportterrain.ID == default(int)) {
                // New entity
                context.AirportTerrain.Add(airportterrain);
            } else {
                // Existing entity
                context.AirportTerrain.Attach(airportterrain);
                context.Entry(airportterrain).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportterrain = context.AirportTerrain.Find(id);
            context.AirportTerrain.Remove(airportterrain);
        }

        public void Save()
        {
            context.SaveChanges();
        }

		public int CountByUserID(int id) {
			return context.AirportDeparture.Count(t => t.CreatedByUserID == id);
		}
    }

	public interface IAirportTerrainRepository
    {
		IQueryable<AirportTerrain> All { get; }
		IQueryable<AirportTerrain> AllIncluding(params Expression<Func<AirportTerrain, object>>[] includeProperties);
		AirportTerrain Find(int id);
		void InsertOrUpdate(AirportTerrain airportterrain);
        void Delete(int id);
        void Save();
		int CountByUserID(int id);
    }
}