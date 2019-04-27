using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class CancelamentoDaMatriculaTest
    {
        private readonly Mock<IEnrollmentRepository> _matriculaRepositorio;
        private readonly EnrollmentCancelation _enrollmentCancelation;

        public CancelamentoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IEnrollmentRepository>();
            _enrollmentCancelation = new EnrollmentCancelation(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.GetById(matricula.Id)).Returns(matricula);

            _enrollmentCancelation.Cancelar(matricula.Id);

            Assert.True(matricula.Canceled);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            Enrollment enrollmentInvalida = null;
            const int matriculaIdInvalida = 1;
            _matriculaRepositorio.Setup(r => r.GetById(It.IsAny<int>())).Returns(enrollmentInvalida);

            Assert.Throws<DomainException>(() =>
                    _enrollmentCancelation.Cancelar(matriculaIdInvalida))
                .ComMensagem(Resource.EnrollmentNotFound);
        }
    }
}
