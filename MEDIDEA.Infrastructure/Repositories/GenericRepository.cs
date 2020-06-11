using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MEDIDEA.Domain;
using MEDIDEA.Domain.Entities;
using MEDIDEA.Domain.Entities.Auditing;

namespace MEDIDEA.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MedideaContext _context;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(MedideaContext context)
        {
            _context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, string include = "")
        {
            IQueryable<TEntity> query = dbSet
                .AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                foreach (var related in include.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(related);
                }
            }

            return query.ToList();
        }

        public TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public void Delete(TEntity entity)
        {
            if (entity is ISoftDelete == false)
            {
                throw new ArgumentException("Wrong entity type");
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            ((ISoftDelete)entity).IsDeleted = true;
        }
    }
}
