using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Alunos
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetByNif(string nif);
    }
}