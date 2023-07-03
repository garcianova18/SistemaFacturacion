using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Application.Repository.Interfaces
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class
    {

        Task<List<TEntity>> GetAll();
        Task<TEntity> GetByid(int id);
        Task<TEntity> Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filters = null);
        Task<int> ExistsUpdate(Expression<Func<TEntity, bool>> filters = null);

    }
}
