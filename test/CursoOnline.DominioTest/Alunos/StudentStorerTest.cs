using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class StudentStorerTest
    {
        private readonly Faker _faker;
        private readonly StudentDto _studentDto;
        private readonly StudentStorer _studentStorer;
        private readonly Mock<IStudentRepository> _studentRepository;

        public StudentStorerTest()
        {
            _faker = new Faker();
            _studentDto = new StudentDto
            {
                Name = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Nif = _faker.Person.Cpf(),
                TargetAudience = TargetAudience.Employee.ToString(),
            };
            _studentRepository = new Mock<IStudentRepository>();
            var conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _studentStorer = new StudentStorer(_studentRepository.Object, conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _studentStorer.Store(_studentDto);

            _studentRepository.Verify(r => r.Add(It.Is<Student>(a => a.Name == _studentDto.Name)));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaFoiCadastrado()
        {
            var studentWithSameNif = StudentBuilder.New().WithId(34).Build();
            _studentRepository.Setup(r => r.GetByNif(_studentDto.Nif)).Returns(studentWithSameNif);

            Assert.Throws<DomainException>(() => _studentStorer.Store(_studentDto))
                .ComMensagem(Resource.AlreadyRegisteredNif);
        }

        [Fact]
        public void DeveEditarNomeDoAluno()
        {
            _studentDto.Id = 35;
            _studentDto.Name = _faker.Person.FullName;
            var alunoJaSalvo = StudentBuilder.New().Build();
            _studentRepository.Setup(r => r.GetById(_studentDto.Id)).Returns(alunoJaSalvo);

            _studentStorer.Store(_studentDto);

            Assert.Equal(_studentDto.Name, alunoJaSalvo.Name);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoesDoAluno()
        {
            _studentDto.Id = 35;
            var alunoJaSalvo = StudentBuilder.New().Build();
            var cpfEsperado = alunoJaSalvo.Nif;
            var emailEsperado = alunoJaSalvo.Email;
            var publicoAlvoEsperado = alunoJaSalvo.TargetAudience;
            _studentRepository.Setup(r => r.GetById(_studentDto.Id)).Returns(alunoJaSalvo);

            _studentStorer.Store(_studentDto);

            Assert.Equal(cpfEsperado, alunoJaSalvo.Nif);
            Assert.Equal(emailEsperado, alunoJaSalvo.Email);
            Assert.Equal(publicoAlvoEsperado, alunoJaSalvo.TargetAudience);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoForEdicao()
        {
            _studentDto.Id = 35;
            var alunoJaSalvo = StudentBuilder.New().Build();
            _studentRepository.Setup(r => r.GetById(_studentDto.Id)).Returns(alunoJaSalvo);

            _studentStorer.Store(_studentDto);

            _studentRepository.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }
    }
}
