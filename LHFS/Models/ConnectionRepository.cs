using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class ConnectionRepository : IConnectionRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<Connection> All
        {
			get { return context.Connection; }
        }

        public IQueryable<Connection> AllIncluding(params Expression<Func<Connection, object>>[] includeProperties)
        {
            IQueryable<Connection> query = context.Connection;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Connection Find(int id)
        {
            return context.Connection.Find(id);
        }

        public void InsertOrUpdate(Connection connection)
        {
            if (connection.ID == default(int)) {
                // New entity
                context.Connection.Add(connection);
            } else {
                // Existing entity
                context.Connection.Attach(connection);
                context.Entry(connection).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var connection = context.Connection.Find(id);
            context.Connection.Remove(connection);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IConnectionRepository
    {
		IQueryable<Connection> All { get; }
		IQueryable<Connection> AllIncluding(params Expression<Func<Connection, object>>[] includeProperties);
		Connection Find(int id);
		void InsertOrUpdate(Connection connection);
        void Delete(int id);
        void Save();
    }
}