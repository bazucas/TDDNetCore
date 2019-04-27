using System;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public TargetAudience Converter(string publicoAlvo)
        {
            BaseValidator.Novo()
                .Quando(!Enum.TryParse<TargetAudience>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            return publicoAlvoConvertido;
        }
    }
}