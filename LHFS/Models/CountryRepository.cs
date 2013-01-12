using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class CountryRepository : ICountryRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Country> All
        {
			get { return context.Country; }
        }

        public IQueryable<Country> AllIncluding(params Expression<Func<Country, object>>[] includeProperties)
        {
            IQueryable<Country> query = context.Country;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Country Find(string id)
        {
            return context.Country.Find(id);
        }

        public void InsertOrUpdate(Country country)
        {
            if (country.ISO == default(string)) {
                // New entity
                context.Country.Add(country);
            } else {
                // Existing entity
                context.Country.Attach(country);
                context.Entry(country).State = EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var country = context.Country.Find(id);
            context.Country.Remove(country);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface ICountryRepository
    {
		IQueryable<Country> All { get; }
		IQueryable<Country> AllIncluding(params Expression<Func<Country, object>>[] includeProperties);
		Country Find(string id);
		void InsertOrUpdate(Country country);
        void Delete(string id);
        void Save();
    }
}