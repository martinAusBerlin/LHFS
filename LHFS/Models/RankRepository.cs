using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class RankRepository : IRankRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Rank> All
        {
			get { return context.Rank; }
        }

        public IQueryable<Rank> AllIncluding(params Expression<Func<Rank, object>>[] includeProperties)
        {
            IQueryable<Rank> query = context.Rank;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Rank Find(int id)
        {
            return context.Rank.Find(id);
        }

        public void InsertOrUpdate(Rank rank)
        {
            if (rank.ID == default(int)) {
                // New entity
                context.Rank.Add(rank);
            } else {
                // Existing entity
                context.Rank.Attach(rank);
                context.Entry(rank).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var rank = context.Rank.Find(id);
            context.Rank.Remove(rank);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IRankRepository
    {
		IQueryable<Rank> All { get; }
		IQueryable<Rank> AllIncluding(params Expression<Func<Rank, object>>[] includeProperties);
		Rank Find(int id);
		void InsertOrUpdate(Rank rank);
        void Delete(int id);
        void Save();
    }
}