using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportArrivalRepository : IAirportArrivalRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportArrivalRepository(LHFSContext dbContext) {
			context = dbContext;
		}

		public AirportArrivalRepository() { }

        public IQueryable<AirportArrival> All
        {
			get { return context.AirportArrival; }
        }

        public IQueryable<AirportArrival> AllIncluding(params Expression<Func<AirportArrival, object>>[] includeProperties)
        {
            IQueryable<AirportArrival> query = context.AirportArrival;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportArrival Find(int id)
        {
            return context.AirportArrival.Find(id);
        }

        public void InsertOrUpdate(AirportArrival airportarrival)
        {
            if (airportarrival.ID == default(int)) {
                // New entity
                context.AirportArrival.Add(airportarrival);
            } else {
                // Existing entity
                context.AirportArrival.Attach(airportarrival);
                context.Entry(airportarrival).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportarrival = context.AirportArrival.Find(id);
            context.AirportArrival.Remove(airportarrival);
        }

        public void Save()
        {
            context.SaveChanges();
        }

		public int CountByUserID(int id) {
			return context.AirportArrival.Count(t => t.CreatedByUserID == id);
		}
    }

	public interface IAirportArrivalRepository
    {
		IQueryable<AirportArrival> All { get; }
		IQueryable<AirportArrival> AllIncluding(params Expression<Func<AirportArrival, object>>[] includeProperties);
		AirportArrival Find(int id);
		int CountByUserID(int id);
		void InsertOrUpdate(AirportArrival airportarrival);
        void Delete(int id);
        void Save();
    }
}