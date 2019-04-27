using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dados.Repositorios
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Course GetByName(string name)
        {
            var entity = Context.Set<Course>().Where(c => c.Name.Contains(name));
            return entity.Any() ? entity.First() : null;
        }
    }
}
