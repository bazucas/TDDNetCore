using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class ConclusaoDaMatriculaTest
    {
        private readonly Mock<IEnrollmentRepository> _matriculaRepositorio;
        private readonly EnrollmentConclusion _enrollmentConclusion;

        public ConclusaoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IEnrollmentRepository>();
            _enrollmentConclusion = new EnrollmentConclusion(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveInformarNotaDoAluno()
        {
            const double notaDoAlunoEsperada = 8;
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.GetById(matricula.Id)).Returns(matricula);

            _enrollmentConclusion.Concluir(matricula.Id, notaDoAlunoEsperada);

            Assert.Equal(notaDoAlunoEsperada, matricula.StudentGrade);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            const int matriculaIdInvalida = 1;
            const double notaDoAluno = 2;
            _matriculaRepositorio.Setup(r => r.GetById(It.IsAny<int>())).Returns((Enrollment) null);

            Assert.Throws<DomainException>(() =>
                    _enrollmentConclusion.Concluir(matriculaIdInvalida, notaDoAluno))
                .ComMensagem(Resource.EnrollmentNotFound);
        }
    }
}
