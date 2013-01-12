using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class ScheduleAircraftReplacementRepository : IScheduleAircraftReplacementRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<ScheduleAircraftReplacement> All
        {
			get { return context.ScheduleAircraftReplacement; }
        }

        public IQueryable<ScheduleAircraftReplacement> AllIncluding(params Expression<Func<ScheduleAircraftReplacement, object>>[] includeProperties)
        {
            IQueryable<ScheduleAircraftReplacement> query = context.ScheduleAircraftReplacement;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ScheduleAircraftReplacement Find(int id)
        {
            return context.ScheduleAircraftReplacement.Find(id);
        }

        public void InsertOrUpdate(ScheduleAircraftReplacement scheduleaircraftreplacement)
        {
            if (scheduleaircraftreplacement.ID == default(int)) {
                // New entity
                context.ScheduleAircraftReplacement.Add(scheduleaircraftreplacement);
            } else {
                // Existing entity
                context.ScheduleAircraftReplacement.Attach(scheduleaircraftreplacement);
                context.Entry(scheduleaircraftreplacement).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var scheduleaircraftreplacement = context.ScheduleAircraftReplacement.Find(id);
            context.ScheduleAircraftReplacement.Remove(scheduleaircraftreplacement);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IScheduleAircraftReplacementRepository
    {
		IQueryable<ScheduleAircraftReplacement> All { get; }
		IQueryable<ScheduleAircraftReplacement> AllIncluding(params Expression<Func<ScheduleAircraftReplacement, object>>[] includeProperties);
		ScheduleAircraftReplacement Find(int id);
		void InsertOrUpdate(ScheduleAircraftReplacement scheduleaircraftreplacement);
        void Delete(int id);
        void Save();
    }
}