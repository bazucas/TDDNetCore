using System;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public TargetAudience Converter(string publicoAlvo)
        {
            BaseValidator.New()
                .When(!Enum.TryParse<TargetAudience>(publicoAlvo, out var targetAudienceConverted), Resource.InvalidTargetAudience)
                .TriggersIfExceptionExists();

            return targetAudienceConverted;
        }
    }
}