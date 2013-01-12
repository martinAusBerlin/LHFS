using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class UserRepository : IUserRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<User> All
        {
			get { return context.User; }
        }

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = context.User;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public User Find(int id)
        {
            return context.User.Find(id);
        }

		public User FindByMail(string mail) {
			return context.User.Single(t => t.Mail == mail);
		}

		public IEnumerable<UserContributions> GetUserContributions() {

			return context.Database.SqlQuery<UserContributions>(
				@"SELECT 
					Users.ID AS UserID,
					NumberOfDepartures.Number AS NumberOfDepartures,
					NumberOfArrivals.Number AS NumberOfArrivals,
					NumberOfGroundOps.Number AS NumberOfGroundOps,
					NumberOfAirportHazards.Number AS NumberOfAirportHazards,
					NumberOfAirportDetails.Number AS NumberOfAirportDetails,
					NumberOfAirportTerrains.Number AS NumberOfAirportTerrains
				FROM Users
				OUTER APPLY (
					SELECT COUNT(*) AS Number FROM AirportDepartures WHERE CreatedByUserID = Users.ID
				) AS NumberOfDepartures
				OUTER APPLY (
					SELECT COUNT(*) AS Number FROM AirportArrivals WHERE CreatedByUserID = Users.ID
				) AS NumberOfArrivals
				OUTER APPLY (
					SELECT COUNT(*) AS Number FROM AirportGroundOps WHERE CreatedByUserID = Users.ID
				) AS NumberOfGroundOps
				OUTER APPLY (
					SELECT COUNT(*) AS Number FROM AirportHazards WHERE CreatedByUserID = Users.ID
				) AS NumberOfAirportHazards
				OUTER APPLY (
					SELECT COUNT(*) AS Number FROM AirportDetails WHERE CreatedByUserID = Users.ID
				) AS NumberOfAirportDetails
				OUTER APPLY (
					SELECT COUNT(*) AS Number FROM AirportTerrains WHERE CreatedByUserID = Users.ID
				) AS NumberOfAirportTerrains");
		}

        public void InsertOrUpdate(User user)
        {
            if (user.ID == default(int)) {
                // New entity
                context.User.Add(user);
            } else {
                // Existing entity
                context.User.Attach(user);
                context.Entry(user).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var user = context.User.Find(id);
            context.User.Remove(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

		public class UserContributions {
			public int UserID { get; set; }
			public int NumberOfDepartures { get; set; }
			public int NumberOfArrivals { get; set; }
			public int NumberOfGroundOps { get; set; }
			public int NumberOfAirportHazards { get; set; }
			public int NumberOfAirportDetails { get; set; }
			public int NumberOfAirportTerrains { get; set; }
		}
    }

	public interface IUserRepository
    {
		IQueryable<User> All { get; }
		IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties);
		User Find(int id);
		User FindByMail(string mail);
		void InsertOrUpdate(User user);
        void Delete(int id);
        void Save();
		IEnumerable<UserRepository.UserContributions> GetUserContributions();
    }
}