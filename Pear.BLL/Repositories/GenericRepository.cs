using Pear.BLL.Interfaces;
using Pear.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pear.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PeroEntities _dbContext;
        public GenericRepository()
        {
            _dbContext = new PeroEntities();
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public  void Delete(T entity)
        {   _dbContext.Set<T>().Remove(entity);
          _dbContext.SaveChanges();
        }
        public T Get(int id)
        {
           var TEntity= _dbContext.Set<T>().Find(id);
            _dbContext.SaveChanges();
            return TEntity;

        }

        public IEnumerable<T> GetAll()
        {
            var products = _dbContext.Set<Products>();

            if (typeof(T) == typeof(Products))
            {
                foreach (var product in products)
                {
                    _dbContext.Entry(product).Reference(p => p.Suppliers).Load();
                    _dbContext.SaveChanges();

                }
                return (IEnumerable<T>)products.ToList();
            }   
            else
                return _dbContext.Set<T>().AsNoTracking().ToList();


        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().AddOrUpdate(entity);
            _dbContext.SaveChanges();

        }
    }
}
