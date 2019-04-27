using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CourseDto _courseDto;
        private readonly CourseStorer _courseStorer;
        private readonly Mock<ICourseRepository> _cursoRepositorioMock;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _courseDto = new CourseDto
            {
                Name = fake.Random.Words(),
                Description = fake.Lorem.Paragraph(),
                Hours = fake.Random.Double(50, 1000),
                TargetAudience = "Estudante",
                Amount = fake.Random.Double(1000, 2000)
            };

            _cursoRepositorioMock = new Mock<ICourseRepository>();
            var conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _courseStorer = new CourseStorer(_cursoRepositorioMock.Object, conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _courseStorer.Store(_courseDto);

            _cursoRepositorioMock.Verify(r => r.Add(
                It.Is<Course>(
                    c => c.Name == _courseDto.Name &&
                    c.Description == _courseDto.Description
                )
            ));
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComId(432).ComNome(_courseDto.Name).Build();
            _cursoRepositorioMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(cursoJaSalvo);

            Assert.Throws<DomainException>(() => _courseStorer.Store(_courseDto))
                .ComMensagem(Resource.CourseAlreadyExists);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _courseDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.GetById(_courseDto.Id)).Returns(curso);

            _courseStorer.Store(_courseDto);

            Assert.Equal(_courseDto.Name, curso.Name);
            Assert.Equal(_courseDto.Amount, curso.Amount);
            Assert.Equal(_courseDto.Hours, curso.Hours);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _courseDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.GetById(_courseDto.Id)).Returns(curso);

            _courseStorer.Store(_courseDto);

            _cursoRepositorioMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }
    }
}
