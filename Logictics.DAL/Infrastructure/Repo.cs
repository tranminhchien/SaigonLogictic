using Logictics.DAL.EFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logictics.DAL.Infrastructure
{
    public abstract class Repo<T> : IDisposable, IRepo<T> where T : class, new()
    {
        protected readonly LogicticsDbContext _db;
        bool _disposed = false;

        protected Repo()
        {
            _db = new LogicticsDbContext();
        }
        protected Repo(DbContextOptions<LogicticsDbContext> options)
        {
            _db = new LogicticsDbContext(options);
        }

        public LogicticsDbContext Context => _db;
        protected DbSet<T> Table => _db.Set<T>();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db.Dispose();
            }
            _disposed = true;
        }

        public int Count => Table.Count();
        public bool HasChanges => Context.ChangeTracker.HasChanges();
        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                //todo: write log
                throw;
            }
        }

        public int Create(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public int CreateMulti(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public T Find(int? id)
        {
            return Table.Find(id);
        }
        public T Get(object id)
        {
            return Table.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return Table;
        }

        public IEnumerable<T> GetRange(int skip, int take)
        {
            return Table.Skip(skip).Take(take);
        }

        public int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public int UpdateMulti(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public int DeleteMulti(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }


    }
}
