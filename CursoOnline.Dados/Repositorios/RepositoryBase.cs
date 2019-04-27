using System.Collections.Generic;
using System.Linq;
using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dados.Repositorios
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext Context;

        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity GetById(int id)
        {
            var query = Context.Set<TEntity>().Where(entidade => entidade.Id == id);
            return query.Any() ? query.First() : null;
        }

        public virtual List<TEntity> Get()
        {
            var entidades = Context.Set<TEntity>().ToList();
            return entidades.Any() ? entidades : new List<TEntity>();
        }
    }
}
