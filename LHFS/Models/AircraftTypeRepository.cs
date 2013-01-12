using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AircraftTypeRepository : IAircraftTypeRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<AircraftType> All
        {
			get { return context.AircraftType; }
        }

        public IQueryable<AircraftType> AllIncluding(params Expression<Func<AircraftType, object>>[] includeProperties)
        {
            IQueryable<AircraftType> query = context.AircraftType;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AircraftType Find(string id)
        {
            return context.AircraftType.Find(id);
        }

        public void Insert(AircraftType aircrafttype)
        {
                context.AircraftType.Add(aircrafttype);
        }

		public void Update(AircraftType aircrafttype) {
                context.AircraftType.Attach(aircrafttype);
                context.Entry(aircrafttype).State = EntityState.Modified;
            }

        public void Delete(string id)
        {
            var aircrafttype = context.AircraftType.Find(id);
            context.AircraftType.Remove(aircrafttype);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAircraftTypeRepository
    {
		IQueryable<AircraftType> All { get; }
		IQueryable<AircraftType> AllIncluding(params Expression<Func<AircraftType, object>>[] includeProperties);
		AircraftType Find(string id);
		void Insert(AircraftType aircrafttype);
		void Update(AircraftType aircrafttype);
        void Delete(string id);
        void Save();
    }
}