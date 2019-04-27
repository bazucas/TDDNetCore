using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(TargetAudience.Entrepreneur).Build();
            var matriculaEsperada = new
            {
                Aluno = StudentBuilder.New().WithTargetAudience(TargetAudience.Entrepreneur).Build(),
                Curso = curso,
                ValorPago = curso.Amount
            };

            var matricula = new Enrollment(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Assert.Throws<DomainException>(() =>
                    MatriculaBuilder.Novo().ComAluno(null).Build())
                .ComMensagem(Resource.InvalidStudent);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            Assert.Throws<DomainException>(() =>
                    MatriculaBuilder.Novo().ComCurso(null).Build())
                .ComMensagem(Resource.InvalidCourse);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoInvalido()
        {
            const double valorPagoInvalido = 0;

            Assert.Throws<DomainException>(() =>
                    MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build())
                .ComMensagem(Resource.InvalidAmount);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            var valorPagoMaiorQueCurso = curso.Amount + 1;

            Assert.Throws<DomainException>(() =>
                    MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoMaiorQueCurso).Build())
                .ComMensagem(Resource.PaidAmountBiggerThanCourseValue);
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            var curso = CursoBuilder.Novo().ComValor(100).ComPublicoAlvo(TargetAudience.Entrepreneur).Build();
            var valorPagoComDesconto = curso.Amount - 1;

            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoComDesconto).Build();

            Assert.True(matricula.HasDiscount);
        }

        [Fact]
        public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(TargetAudience.Employee).Build();
            var aluno = StudentBuilder.New().WithTargetAudience(TargetAudience.Graduate).Build();

            Assert.Throws<DomainException>(() =>
                    MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
                .ComMensagem(Resource.DifferentTargetAudience);
        }

        [Fact]
        public void DeveInformarANotaDoAlunoParMatricula()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.ShowGrade(notaDoAlunoEsperada);

            Assert.Equal(notaDoAlunoEsperada, matricula.StudentGrade);
        }

        [Fact]
        public void DeveIndicarQueCuroFoiConcluido()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.ShowGrade(notaDoAlunoEsperada);

            Assert.True(matricula.FinishedCourse);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NaoDeveInformarComNotaInvalida(double notaDoAlunoInvalida)
        {
            var matricula = MatriculaBuilder.Novo().Build();

            Assert.Throws<DomainException>(() =>
                    matricula.ShowGrade(notaDoAlunoInvalida))
                .ComMensagem(Resource.InvalidStudentGrade);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.Cancel();

            Assert.True(matricula.Canceled);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaEstiverCancelada()
        {
            const double notaDoAluno = 3;
            var matricula = MatriculaBuilder.Novo().ComCancelada(true).Build();

            Assert.Throws<DomainException>(() =>
                    matricula.ShowGrade(notaDoAluno))
                .ComMensagem(Resource.EnrollmentCanceled);
        }

        [Fact]
        public void NaoDeveCancelarQuandoMatriculaEstiverConcluida()
        {
            var matricula = MatriculaBuilder.Novo().ComConcluido(true).Build();

            Assert.Throws<DomainException>(() =>
                    matricula.Cancel())
                .ComMensagem(Resource.EnrollmentFinished);
        }
    }
}
