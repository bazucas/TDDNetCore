using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using CursoOnline.DominioTest._Util;
using Xunit;

namespace CursoOnline.DominioTest.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

        [Theory]
        [InlineData(TargetAudience.Employee, "Empregado")]
        [InlineData(TargetAudience.Entrepreneur, "Empreendedor")]
        [InlineData(TargetAudience.Student, "Estudante")]
        [InlineData(TargetAudience.Graduate, "Universitário")]
        public void DeveConverterPublicoAlvo(TargetAudience targetAudienceEsperado, string publicoAlvoEmString)
        {
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoEmString);

            Assert.Equal(targetAudienceEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoEhInvalido()
        {
            const string publicoAlvoInvalido = "Invalido";

            Assert.Throws<DomainException>(() =>
                    _conversor.Converter(publicoAlvoInvalido))
                .ComMensagem(Resource.InvalidTargetAudience);
        }
    }
}
