using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class DivisionRepository : IDivisionRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Division> All
        {
			get { return context.Division; }
        }

        public IQueryable<Division> AllIncluding(params Expression<Func<Division, object>>[] includeProperties)
        {
            IQueryable<Division> query = context.Division;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Division Find(int id)
        {
            return context.Division.Find(id);
        }

        public void InsertOrUpdate(Division division)
        {
            if (division.ID == default(int)) {
                // New entity
                context.Division.Add(division);
            } else {
                // Existing entity
                context.Division.Attach(division);
                context.Entry(division).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var division = context.Division.Find(id);
            context.Division.Remove(division);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IDivisionRepository
    {
		IQueryable<Division> All { get; }
		IQueryable<Division> AllIncluding(params Expression<Func<Division, object>>[] includeProperties);
		Division Find(int id);
		void InsertOrUpdate(Division division);
        void Delete(int id);
        void Save();
    }
}