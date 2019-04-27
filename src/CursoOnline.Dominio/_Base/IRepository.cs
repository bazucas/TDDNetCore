using System.Collections.Generic;

namespace CursoOnline.Dominio._Base
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        List<TEntity> Get();
        void Add(TEntity entity);
    }
}
