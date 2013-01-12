using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirlineRepository : IAirlineRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Airline> All
        {
			get { return context.Airline; }
        }

        public IQueryable<Airline> AllIncluding(params Expression<Func<Airline, object>>[] includeProperties)
        {
            IQueryable<Airline> query = context.Airline;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Airline Find(string id)
        {
            return context.Airline.Find(id);
        }

        public void Insert(Airline airline)
        {
            context.Airline.Add(airline);
        }

		public void Update(Airline airline) {
			context.Airline.Attach(airline);
			context.Entry(airline).State = EntityState.Modified;
		}

        public void Delete(string id)
        {
            var airline = context.Airline.Find(id);
            context.Airline.Remove(airline);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAirlineRepository
    {
		IQueryable<Airline> All { get; }
		IQueryable<Airline> AllIncluding(params Expression<Func<Airline, object>>[] includeProperties);
		Airline Find(string id);
		void Insert(Airline airline);
		void Update(Airline airline);
        void Delete(string id);
        void Save();
    }
}