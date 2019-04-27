using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CourseTest
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly TargetAudience _targetAudience;
        private readonly double _valor;
        private readonly string _descricao;
        private readonly Faker _faker;

        public CourseTest()
        {
            _faker = new Faker();

            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _targetAudience = TargetAudience.Student;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _targetAudience,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Course(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<DomainException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.InvalidName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<DomainException>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem(Resource.InvalidHours);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaUmValorInvalido(double valorInvalido)
        {
            Assert.Throws<DomainException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem(Resource.InvalidAmount);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;
            var curso = CursoBuilder.Novo().Build();

            curso.ChangeName(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<DomainException>(() => curso.ChangeName(nomeInvalido))
                .ComMensagem(Resource.InvalidName);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = 20.5;
            var curso = CursoBuilder.Novo().Build();

            curso.ChangeHours(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.Hours);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<DomainException>(() => curso.ChangeHours(cargaHorariaInvalida))
                .ComMensagem(Resource.InvalidHours);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = 234.99;
            var curso = CursoBuilder.Novo().Build();

            curso.ChangeAmount(valorEsperado);

            Assert.Equal(valorEsperado, curso.Amount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<DomainException>(() => curso.ChangeAmount(valorInvalido))
                .ComMensagem(Resource.InvalidAmount);
        }
    }
}
