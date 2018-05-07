using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp1.Data
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal Tp1Context context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(Tp1Context context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
    }
}
