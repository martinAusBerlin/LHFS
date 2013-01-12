using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class AirportChartRepository : IAirportChartRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<AirportChart> All
        {
			get { return context.AirportChart; }
        }

        public IQueryable<AirportChart> AllIncluding(params Expression<Func<AirportChart, object>>[] includeProperties)
        {
            IQueryable<AirportChart> query = context.AirportChart;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AirportChart Find(int id)
        {
            return context.AirportChart.Find(id);
        }

        public void InsertOrUpdate(AirportChart airportchart)
        {
            if (airportchart.ID == default(int)) {
                // New entity
                context.AirportChart.Add(airportchart);
            } else {
                // Existing entity
                context.AirportChart.Attach(airportchart);
                context.Entry(airportchart).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var airportchart = context.AirportChart.Find(id);
            context.AirportChart.Remove(airportchart);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IAirportChartRepository
    {
		IQueryable<AirportChart> All { get; }
		IQueryable<AirportChart> AllIncluding(params Expression<Func<AirportChart, object>>[] includeProperties);
		AirportChart Find(int id);
		void InsertOrUpdate(AirportChart airportchart);
        void Delete(int id);
        void Save();
    }
}