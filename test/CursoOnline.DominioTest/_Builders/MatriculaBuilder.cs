using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.DominioTest._Builders
{
    public class MatriculaBuilder
    {
        protected Student Aluno;
        protected Course Course;
        protected double ValorPago;
        protected bool Cancelada;
        protected bool Concluido;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(TargetAudience.Entrepreneur).Build();

            return new MatriculaBuilder
            {
                Aluno = StudentBuilder.New().WithTargetAudience(TargetAudience.Entrepreneur).Build(),
                Course = curso,
                ValorPago = curso.Amount
            };
        }

        public MatriculaBuilder ComAluno(Student aluno)
        {
            Aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Course course)
        {
            Course = course;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            ValorPago = valorPago;
            return this;
        }

        public MatriculaBuilder ComCancelada(bool cancelada)
        {
            Cancelada = cancelada;
            return this;
        }

        public MatriculaBuilder ComConcluido(bool concluido)
        {
            Concluido = concluido;
            return this;
        }

        public Enrollment Build()
        {
            var matricula = new Enrollment(Aluno, Course, ValorPago);

            if (Cancelada)
                matricula.Cancel();

            if (Concluido)
            {
                const double notaDoAluno = 7;
                matricula.ShowGrade(notaDoAluno);
            }

            return matricula;
        }

        
    }
}
