using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<TEntity>
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
