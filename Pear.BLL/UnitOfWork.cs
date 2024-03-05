using Pear.BLL.Interfaces;
using Pear.BLL.Repositories;
using Pear.DAL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pear.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
          private readonly PeroEntities _dbContext =new PeroEntities();
        private Hashtable _repositories;
        public UnitOfWork()
        {
            
            _repositories = new Hashtable();
        }
        public async Task<int> Complete()
        {return await _dbContext.SaveChangesAsync(); }

        public void Dispose()
       => _dbContext.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>();
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<TEntity>;
        }

    }
}
