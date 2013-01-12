using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class RotationRepository : IRotationRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Rotation> All
        {
			get { return context.Rotation; }
        }

        public IQueryable<Rotation> AllIncluding(params Expression<Func<Rotation, object>>[] includeProperties)
        {
            IQueryable<Rotation> query = context.Rotation;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Rotation Find(int id)
        {
            return context.Rotation.Find(id);
        }

        public void InsertOrUpdate(Rotation rotation)
        {
            if (rotation.ID == default(int)) {
                // New entity
                context.Rotation.Add(rotation);
            } else {
                // Existing entity
                context.Rotation.Attach(rotation);
                context.Entry(rotation).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var rotation = context.Rotation.Find(id);
            context.Rotation.Remove(rotation);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IRotationRepository
    {
		IQueryable<Rotation> All { get; }
		IQueryable<Rotation> AllIncluding(params Expression<Func<Rotation, object>>[] includeProperties);
		Rotation Find(int id);
		void InsertOrUpdate(Rotation rotation);
        void Delete(int id);
        void Save();
    }
}