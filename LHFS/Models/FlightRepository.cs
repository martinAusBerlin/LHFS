using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class FlightRepository : IFlightRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Flight> All
        {
			get { return context.Flight; }
        }

        public IQueryable<Flight> AllIncluding(params Expression<Func<Flight, object>>[] includeProperties)
        {
            IQueryable<Flight> query = context.Flight;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Flight Find(int id)
        {
            return context.Flight.Find(id);
        }

		public IQueryable<Flight> FindBookedFlightByUserID(int id) {
			return context.Flight.Where(t => t.UserID == id && t.BookedOn != null && t.TakeOff == null);
		}

        public void InsertOrUpdate(Flight flight)
        {
			if (flight.OnBlock.HasValue && flight.OffBlock.HasValue) {
				flight.FlightTimeTicks = (flight.OnBlock.Value - flight.OffBlock.Value).Ticks;
			} else {
				flight.FlightTimeTicks = null;
			}

            if (flight.ID == default(int)) {
                // New entity
                context.Flight.Add(flight);
            } else {
                // Existing entity
                context.Flight.Attach(flight);
                context.Entry(flight).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var flight = context.Flight.Find(id);
            context.Flight.Remove(flight);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IFlightRepository
    {
		IQueryable<Flight> All { get; }
		IQueryable<Flight> AllIncluding(params Expression<Func<Flight, object>>[] includeProperties);
		IQueryable<Flight> FindBookedFlightByUserID(int id);
		Flight Find(int id);
		void InsertOrUpdate(Flight flight);
        void Delete(int id);
        void Save();
    }
}