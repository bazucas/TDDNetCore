using System.Threading.Tasks;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Dados.Contextos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public async Task Commit()
        {
            await SaveChangesAsync();
        }
    }
}
