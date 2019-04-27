namespace CursoOnline.Dominio.PublicosAlvo
{
    public interface IConversorDePublicoAlvo
    {
        TargetAudience Converter(string publicoAlvo);
    }
}