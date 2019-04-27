using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTest
    {
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = Dominio.PublicosAlvo.TargetAudience.Entrepreneur
            };

            var aluno = new Student(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var novoNomeEsperado = _faker.Person.FullName;
            var aluno = StudentBuilder.New().Build();

            aluno.ChangeName(novoNomeEsperado);

            Assert.Equal(novoNomeEsperado, aluno.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarComNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<DomainException>(() =>
                    StudentBuilder.New().WithName(nomeInvalido).Build())
                .ComMensagem(Resource.InvalidName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveCriarComEmailInvalido(string emailInvalido)
        {
            Assert.Throws<DomainException>(() =>
                    StudentBuilder.New().WithEmail(emailInvalido).Build())
                .ComMensagem(Resource.InvalidEmail);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("CPF Invalido")]
        [InlineData("0000000000")]
        public void NaoDeveCriarComCpfInvalido(string cpfInvalido)
        {
            Assert.Throws<DomainException>(() =>
                    StudentBuilder.New().WithNif(cpfInvalido).Build())
                .ComMensagem(Resource.InvalidNif);
        }
    }
}