using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos
{
    public interface ICourseRepository : IRepository<Course>
    {
        Course GetByName(string nome);
    }
}