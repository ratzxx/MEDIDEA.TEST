using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MEDIDEA.Domain.Entities;

namespace MEDIDEA.Domain
{
    public interface IUoW: IDisposable
    {
        IRepository<Customer> Customers { get; }
        IVisitRepository Visits { get; }
        IRepository<Phone> Phones { get; }
        void Commit();
    }
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, string include = "");
        TEntity Get(object id);
        void Delete(TEntity entity);
    }
}
