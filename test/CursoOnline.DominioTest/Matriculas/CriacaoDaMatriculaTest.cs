using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class CriacaoDaMatriculaTest
    {
        private readonly Mock<ICourseRepository> _cursoRepositorio;
        private readonly Mock<IStudentRepository> _alunoRepositorio;
        private readonly EnrollmentCreation _enrollmentCreation;
        private readonly EnrollmentDto _enrollmentDto;
        private readonly Mock<IEnrollmentRepository> _matriculaRepositorio;
        private readonly Student _aluno;
        private readonly Course _course;

        public CriacaoDaMatriculaTest()
        {
            _cursoRepositorio = new Mock<ICourseRepository>();
            _alunoRepositorio = new Mock<IStudentRepository>();
            _matriculaRepositorio = new Mock<IEnrollmentRepository>();

            _aluno = StudentBuilder.New().WithId(23).WithTargetAudience(TargetAudience.Graduate).Build();
            _alunoRepositorio.Setup(r => r.GetById(_aluno.Id)).Returns(_aluno);

            _course = CursoBuilder.Novo().ComId(45).ComPublicoAlvo(TargetAudience.Graduate).Build();
            _cursoRepositorio.Setup(r => r.GetById(_course.Id)).Returns(_course);

            _enrollmentDto = new EnrollmentDto { StudentId = _aluno.Id, CourseId = _course.Id, PaidAmount = _course.Amount };
           
            _enrollmentCreation = new EnrollmentCreation(_alunoRepositorio.Object, _cursoRepositorio.Object, _matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveNotificarQuandoCursoNaoForEncontrado()
        {
            _cursoRepositorio.Setup(r => r.GetById(_enrollmentDto.CourseId)).Returns((Course) null);

            Assert.Throws<DomainException>(() =>
                    _enrollmentCreation.Create(_enrollmentDto))
                .ComMensagem(Resource.CourseNotFound);
        }

        [Fact]
        public void DeveNotificarQuandoAlunoNaoForEncontrado()
        {
            _alunoRepositorio.Setup(r => r.GetById(_enrollmentDto.StudentId)).Returns((Student) null);

            Assert.Throws<DomainException>(() =>
                    _enrollmentCreation.Create(_enrollmentDto))
                .ComMensagem(Resource.StudentNotFound);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _enrollmentCreation.Create(_enrollmentDto);

            _matriculaRepositorio.Verify(r => r.Add(It.Is<Enrollment>(m => m.Student == _aluno && m.Course == _course)));
        }
    }
}
