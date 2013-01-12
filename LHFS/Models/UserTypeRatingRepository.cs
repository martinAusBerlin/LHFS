using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using LHFS.Views.Users.ViewModel;
using LHFS.Utils;

namespace LHFS.Models
{ 
    public class UserTypeRatingRepository : IUserTypeRatingRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<UserTypeRating> All
        {
			get { return context.UserTypeRating; }
        }

        public IQueryable<UserTypeRating> AllIncluding(params Expression<Func<UserTypeRating, object>>[] includeProperties)
        {
            IQueryable<UserTypeRating> query = context.UserTypeRating;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

		public IQueryable<UsersTypeRatingListItem> CurrentUserTypeRatings(int userID) {
			
			IQueryable<UsersTypeRatingListItem> query =
				from typeRatings in context.TypeRating
				join userTypeRatings in context.UserTypeRating.Where(t => t.ValidTo == null && t.UserID == userID) on typeRatings.ID equals userTypeRatings.TypeRatingID into joinedRatings
				from ratings in joinedRatings.DefaultIfEmpty()
				select new UsersTypeRatingListItem {
					TypeRatingID = typeRatings.ID,
					UserTypeRatingID = ratings.ID,
					Title = typeRatings.Title,
					ValidFrom = ratings.ValidFrom
				};

			return query;
		}

        public UserTypeRating Find(int id)
        {
            return context.UserTypeRating.Find(id);
        }

        public void InsertOrUpdate(UserTypeRating usertyperating)
        {
            if (usertyperating.ID == default(int)) {
                // New entity
                context.UserTypeRating.Add(usertyperating);
            } else {
                // Existing entity
				context.UserTypeRating.Attach(usertyperating);
				context.Entry(usertyperating).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var usertyperating = context.UserTypeRating.Find(id);
            context.UserTypeRating.Remove(usertyperating);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IUserTypeRatingRepository
    {
		IQueryable<UserTypeRating> All { get; }
		IQueryable<UserTypeRating> AllIncluding(params Expression<Func<UserTypeRating, object>>[] includeProperties);
		UserTypeRating Find(int id);
		void InsertOrUpdate(UserTypeRating usertyperating);
        void Delete(int id);
        void Save();
		IQueryable<UsersTypeRatingListItem> CurrentUserTypeRatings(int userID);
    }
}