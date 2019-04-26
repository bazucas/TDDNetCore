using System;
using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Enums;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest
{
    public class CursosTest: IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly int _carga;
        private readonly PublicoEnum _publico;
        private readonly decimal _valor;
        private readonly string _descricao;

            public CursosTest(ITestOutputHelper output)
            {
                _output = output;
                _output.WriteLine("Construtor executado");
                var faker = new Faker();

                _nome = faker.Person.FullName;
                _carga = faker.Random.Int(1, 100);
                _publico = (PublicoEnum)faker.Random.Int(0, 3);
                _valor = faker.Random.Decimal(400, 1000);
                _descricao = faker.Lorem.Lines(5);
            }

            public void Dispose()
            {
                _output.WriteLine("Dispose executado");
            }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                Carga = _carga,
                Publico = _publico,
                Valor = _valor
            };

            var curso = new Curso(_nome, _carga, _publico, _valor, _descricao);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Fact]
        public void NaoDeveCursoTerUmNomeNulo()
        {
            Assert.Throws<ArgumentException>(() =>
               CursoBuilder.Novo().ComNome(null).Build());
        }

        [Fact]
        public void NaoDeveCursoTerUmNomeVazio()
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComNome(string.Empty).Build());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                    CursoBuilder.Novo().ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void CursoNaoDeveTerCargaHorariaMenorQueUm(int cargaHoraria)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCarga(cargaHoraria).Build()).ComMensagem("Carga inválida");
        }

        [Theory]
        [InlineData(0.0d)]
        [InlineData(-2.0d)]
        public void CursoNaoDeveTerValorMenorQueUm(decimal valor)
        {
            Assert.Throws<ArgumentException>(() =>
                    CursoBuilder.Novo().ComValor(valor).Build()).ComMensagem("Valor inválido");
        }
    }
}