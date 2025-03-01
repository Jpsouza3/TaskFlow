using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Business.Model;

namespace TaskFlow.Business.Interface
{
    public interface IRepository<TEntity>
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(int id);
    }
}
