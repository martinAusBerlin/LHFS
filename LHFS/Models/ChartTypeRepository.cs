using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class ChartTypeRepository : IChartTypeRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<ChartType> All
        {
			get { return context.ChartType; }
        }

        public IQueryable<ChartType> AllIncluding(params Expression<Func<ChartType, object>>[] includeProperties)
        {
            IQueryable<ChartType> query = context.ChartType;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChartType Find(string id)
        {
            return context.ChartType.Find(id);
        }

        public void InsertOrUpdate(ChartType charttype)
        {
            if (charttype.Key == default(string)) {
                // New entity
                context.ChartType.Add(charttype);
            } else {
                // Existing entity
                context.ChartType.Attach(charttype);
                context.Entry(charttype).State = EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var charttype = context.ChartType.Find(id);
            context.ChartType.Remove(charttype);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IChartTypeRepository
    {
		IQueryable<ChartType> All { get; }
		IQueryable<ChartType> AllIncluding(params Expression<Func<ChartType, object>>[] includeProperties);
		ChartType Find(string id);
		void InsertOrUpdate(ChartType charttype);
        void Delete(string id);
        void Save();
    }
}