using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportGroundOpRepository : IAirportGroundOpRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportGroundOpRepository(LHFSContext dbContext) {
			context = dbContext;
		}

		public AirportGroundOpRepository() { }

        public IQueryable<AirportGroundOp> All
        {
			get { return context.AirportGroundOp; }
        }

        public IQueryable<AirportGroundOp> AllIncluding(params Expression<Func<AirportGroundOp, object>>[] includeProperties)
        {
            IQueryable<AirportGroundOp> query = context.AirportGroundOp;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportGroundOp Find(int id)
        {
            return context.AirportGroundOp.Find(id);
        }

        public void InsertOrUpdate(AirportGroundOp airportgroundop)
        {
            if (airportgroundop.ID == default(int)) {
                // New entity
                context.AirportGroundOp.Add(airportgroundop);
            } else {
                // Existing entity
                context.AirportGroundOp.Attach(airportgroundop);
                context.Entry(airportgroundop).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportgroundop = context.AirportGroundOp.Find(id);
            context.AirportGroundOp.Remove(airportgroundop);
        }

        public void Save()
        {
            context.SaveChanges();
        }

		public int CountByUserID(int id) {
			return context.AirportDeparture.Count(t => t.CreatedByUserID == id);
		}
    }

	public interface IAirportGroundOpRepository
    {
		IQueryable<AirportGroundOp> All { get; }
		IQueryable<AirportGroundOp> AllIncluding(params Expression<Func<AirportGroundOp, object>>[] includeProperties);
		AirportGroundOp Find(int id);
		void InsertOrUpdate(AirportGroundOp airportgroundop);
        void Delete(int id);
        void Save();
		int CountByUserID(int id);
    }
}