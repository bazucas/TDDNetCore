using System.Linq;
using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Alunos;

namespace CursoOnline.Dados.Repositorios
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Student GetByNif(string nif)
        {
            var students = Context.Set<Student>().Where(a => a.Nif == nif);
            return students.Any() ? students.First() : null;
        }
    }
}
