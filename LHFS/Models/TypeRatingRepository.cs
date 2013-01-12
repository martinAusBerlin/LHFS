using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using LHFS.Views.Users.ViewModel;

namespace LHFS.Models
{ 
    public class TypeRatingRepository : ITypeRatingRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<TypeRating> All
        {
			get { return context.TypeRating; }
        }

        public IQueryable<TypeRating> AllIncluding(params Expression<Func<TypeRating, object>>[] includeProperties)
        {
            IQueryable<TypeRating> query = context.TypeRating;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TypeRating Find(int id)
        {
            return context.TypeRating.Find(id);
        }

        public void InsertOrUpdate(TypeRating typerating)
        {
            if (typerating.ID == default(int)) {
                // New entity
                context.TypeRating.Add(typerating);
            } else {
                // Existing entity
                context.TypeRating.Attach(typerating);
                context.Entry(typerating).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var typerating = context.TypeRating.Find(id);
            context.TypeRating.Remove(typerating);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface ITypeRatingRepository
    {
		IQueryable<TypeRating> All { get; }
		IQueryable<TypeRating> AllIncluding(params Expression<Func<TypeRating, object>>[] includeProperties);
		TypeRating Find(int id);
		void InsertOrUpdate(TypeRating typerating);
        void Delete(int id);
        void Save();
    }
}