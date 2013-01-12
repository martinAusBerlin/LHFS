using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportAlternateRepository : IAirportAlternateRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<AirportAlternate> All
        {
			get { return context.AirportAlternate; }
        }

        public IQueryable<AirportAlternate> AllIncluding(params Expression<Func<AirportAlternate, object>>[] includeProperties)
        {
            IQueryable<AirportAlternate> query = context.AirportAlternate;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportAlternate Find(int id)
        {
            return context.AirportAlternate.Find(id);
        }

        public void InsertOrUpdate(AirportAlternate airportalternate)
        {
            if (airportalternate.ID == default(int)) {
                // New entity
                context.AirportAlternate.Add(airportalternate);
            } else {
                // Existing entity
                context.AirportAlternate.Attach(airportalternate);
                context.Entry(airportalternate).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportalternate = context.AirportAlternate.Find(id);
            context.AirportAlternate.Remove(airportalternate);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAirportAlternateRepository
    {
		IQueryable<AirportAlternate> All { get; }
		IQueryable<AirportAlternate> AllIncluding(params Expression<Func<AirportAlternate, object>>[] includeProperties);
		AirportAlternate Find(int id);
		void InsertOrUpdate(AirportAlternate airportalternate);
        void Delete(int id);
        void Save();
    }
}