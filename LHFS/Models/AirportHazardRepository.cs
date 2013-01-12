using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportHazardRepository : IAirportHazardRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportHazardRepository(LHFSContext dbContext) {
			context = dbContext;
		}

		public AirportHazardRepository() { }

        public IQueryable<AirportHazard> All
        {
			get { return context.AirportHazard; }
        }

        public IQueryable<AirportHazard> AllIncluding(params Expression<Func<AirportHazard, object>>[] includeProperties)
        {
            IQueryable<AirportHazard> query = context.AirportHazard;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportHazard Find(int id)
        {
            return context.AirportHazard.Find(id);
        }

        public void InsertOrUpdate(AirportHazard airporthazard)
        {
            if (airporthazard.ID == default(int)) {
                // New entity
                context.AirportHazard.Add(airporthazard);
            } else {
                // Existing entity
                context.AirportHazard.Attach(airporthazard);
                context.Entry(airporthazard).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airporthazard = context.AirportHazard.Find(id);
            context.AirportHazard.Remove(airporthazard);
        }

        public void Save()
        {
            context.SaveChanges();
        }

		public int CountByUserID(int id) {
			return context.AirportDeparture.Count(t => t.CreatedByUserID == id);
		}
    }

	public interface IAirportHazardRepository
    {
		IQueryable<AirportHazard> All { get; }
		IQueryable<AirportHazard> AllIncluding(params Expression<Func<AirportHazard, object>>[] includeProperties);
		AirportHazard Find(int id);
		void InsertOrUpdate(AirportHazard airporthazard);
        void Delete(int id);
        void Save();
		int CountByUserID(int id);
    }
}