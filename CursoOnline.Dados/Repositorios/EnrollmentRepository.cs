using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Dados.Repositorios
{
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override List<Enrollment> Get()
        {
            var query = Context.Set<Enrollment>()
                .Include(i => i.Student)
                .Include(i => i.Course)
                .ToList();

            return query;
        }
    }
}
