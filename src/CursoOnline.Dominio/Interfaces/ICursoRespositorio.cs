using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Interfaces
{
    public interface ICursoRespositorio
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}