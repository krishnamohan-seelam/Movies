using Movies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Repository
{
    public class DataRepository<T> : IDataRepository<T>, IDisposable where T : class
    {

        bool disposed = false;
        private MovieDBContext moviecontext = null;

        public DataRepository()
        {
            moviecontext = new MovieDBContext();
            DbSet = moviecontext.Set<T>();
        }

        public DataRepository(MovieDBContext moviecontext)

        {

            this.moviecontext = moviecontext;

        }

        protected DbSet<T> DbSet
        {

            get; set;

        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }



        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public void SaveChanges()
        {
            moviecontext.SaveChanges();
        }

        public void Update(T entity)
        {
            moviecontext.Entry<T>(entity).State = EntityState.Modified;

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)

        {

            if (!disposed)
            {

                moviecontext.Dispose();

                disposed = true;

            }

        }
    }
}