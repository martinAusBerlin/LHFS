using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportDetailRepository : IAirportDetailRepository
    {
        LHFSContext context = new LHFSContext();

		public AirportDetailRepository(LHFSContext dbContext) {
			context = dbContext;
		}

		public AirportDetailRepository() { }

        public IQueryable<AirportDetail> All
        {
			get { return context.AirportDetail; }
        }

        public IQueryable<AirportDetail> AllIncluding(params Expression<Func<AirportDetail, object>>[] includeProperties)
        {
            IQueryable<AirportDetail> query = context.AirportDetail;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportDetail Find(int id)
        {
            return context.AirportDetail.Find(id);
        }

		public int CountByUserID(int id) {
			return context.AirportDeparture.Count(t => t.CreatedByUserID == id);
		}

        public void InsertOrUpdate(AirportDetail airportdetail)
        {
            if (airportdetail.ID == default(int)) {
                // New entity
                context.AirportDetail.Add(airportdetail);
            } else {
                // Existing entity
                context.AirportDetail.Attach(airportdetail);
                context.Entry(airportdetail).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportdetail = context.AirportDetail.Find(id);
            context.AirportDetail.Remove(airportdetail);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAirportDetailRepository
    {
		IQueryable<AirportDetail> All { get; }
		IQueryable<AirportDetail> AllIncluding(params Expression<Func<AirportDetail, object>>[] includeProperties);
		AirportDetail Find(int id);
		void InsertOrUpdate(AirportDetail airportdetail);
        void Delete(int id);
        void Save();
		int CountByUserID(int id);
	}
}