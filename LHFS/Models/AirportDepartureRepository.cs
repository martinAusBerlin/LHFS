using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportDepartureRepository : IAirportDepartureRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportDepartureRepository(LHFSContext dbContext) {
			context = dbContext;
		}

		public AirportDepartureRepository() { }

        public IQueryable<AirportDeparture> All
        {
			get { return context.AirportDeparture; }
        }

        public IQueryable<AirportDeparture> AllIncluding(params Expression<Func<AirportDeparture, object>>[] includeProperties)
        {
            IQueryable<AirportDeparture> query = context.AirportDeparture;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportDeparture Find(int id)
        {
            return context.AirportDeparture.Find(id);
        }

		public int CountByUserID(int id) {
			return context.AirportDeparture.Count(t => t.CreatedByUserID == id);
		}

        public void InsertOrUpdate(AirportDeparture airportdeparture)
        {
            if (airportdeparture.ID == default(int)) {
                // New entity
                context.AirportDeparture.Add(airportdeparture);
            } else {
                // Existing entity
                context.AirportDeparture.Attach(airportdeparture);
                context.Entry(airportdeparture).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportdeparture = context.AirportDeparture.Find(id);
            context.AirportDeparture.Remove(airportdeparture);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAirportDepartureRepository
    {
		IQueryable<AirportDeparture> All { get; }
		IQueryable<AirportDeparture> AllIncluding(params Expression<Func<AirportDeparture, object>>[] includeProperties);
		AirportDeparture Find(int id);
		void InsertOrUpdate(AirportDeparture airportdeparture);
        void Delete(int id);
        void Save();
		int CountByUserID(int id);
    }
}