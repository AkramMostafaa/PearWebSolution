﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pear.BLL.Interfaces
{
    internal interface IUnitOfWork:IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
