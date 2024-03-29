using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class RegionRepository : IRegionRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Region> All
        {
			get { return context.Region; }
        }

        public IQueryable<Region> AllIncluding(params Expression<Func<Region, object>>[] includeProperties)
        {
            IQueryable<Region> query = context.Region;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Region Find(int id)
        {
            return context.Region.Find(id);
        }

        public void InsertOrUpdate(Region region)
        {
            if (region.ID == default(int)) {
                // New entity
                context.Region.Add(region);
            } else {
                // Existing entity
                context.Region.Attach(region);
                context.Entry(region).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var region = context.Region.Find(id);
            context.Region.Remove(region);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IRegionRepository
    {
		IQueryable<Region> All { get; }
		IQueryable<Region> AllIncluding(params Expression<Func<Region, object>>[] includeProperties);
		Region Find(int id);
		void InsertOrUpdate(Region region);
        void Delete(int id);
        void Save();
    }
}