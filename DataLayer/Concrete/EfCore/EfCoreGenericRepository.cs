using DataLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        protected readonly DbContext context;

        public EfCoreGenericRepository(DbContext ctx)
        {
            context = ctx;
        }

        public bool Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return true;
        }

        public bool Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
            return true;
        }

        public virtual List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual bool Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            context.SaveChanges();
            return true;
        }
    }
}
