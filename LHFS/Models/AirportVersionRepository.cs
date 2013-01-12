using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportVersionRepository : IAirportVersionRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportVersionRepository() {

		}

		public AirportVersionRepository(LHFSContext dbContext) {
			context = dbContext;
		}

        public IQueryable<AirportVersion> All
        {
			get { return context.AirportVersion; }
        }

        public IQueryable<AirportVersion> AllIncluding(params Expression<Func<AirportVersion, object>>[] includeProperties)
        {
            IQueryable<AirportVersion> query = context.AirportVersion;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportVersion Find(int id)
        {
            return context.AirportVersion.Find(id);
        }

		public AirportVersion FindCurrent(string icao) {
			return context.AirportVersion.Where(t => t.Airport.ICAO == icao).OrderByDescending(t => t.ID).First();
		}

		public AirportVersion FindCurrentIncluding(string icao, params Expression<Func<AirportVersion, object>>[] includeProperties) {

			IQueryable<AirportVersion> query = context.AirportVersion.Where(t => t.Airport.ICAO == icao);
			foreach (var includeProperty in includeProperties) {
				query = query.Include(includeProperty);
			}

			return query.OrderByDescending(t => t.CreatedOn).First();
		}

		public AirportVersion FindIncluding(int id, params Expression<Func<AirportVersion, object>>[] includeProperties) {

			IQueryable<AirportVersion> query = context.AirportVersion.Where(t => t.ID == id);
			foreach (var includeProperty in includeProperties) {
				query = query.Include(includeProperty);
			}

			return query.Single();
		}

        public void InsertOrUpdate(AirportVersion airportversion)
        {
            if (airportversion.ID == default(int)) {
                // New entity
                context.AirportVersion.Add(airportversion);
            } else {
                // Existing entity
                context.AirportVersion.Attach(airportversion);
                context.Entry(airportversion).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportversion = context.AirportVersion.Find(id);
            context.AirportVersion.Remove(airportversion);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAirportVersionRepository
    {
		IQueryable<AirportVersion> All { get; }
		IQueryable<AirportVersion> AllIncluding(params Expression<Func<AirportVersion, object>>[] includeProperties);
		AirportVersion Find(int id);
		AirportVersion FindIncluding(int id, params Expression<Func<AirportVersion, object>>[] includeProperties);
		AirportVersion FindCurrent(string icao);
		AirportVersion FindCurrentIncluding(string icao, params Expression<Func<AirportVersion, object>>[] includeProperties);
		void InsertOrUpdate(AirportVersion airportversion);
        void Delete(int id);
        void Save();
    }
}