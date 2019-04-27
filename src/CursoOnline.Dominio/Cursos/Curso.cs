using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Valor { get; private set; }

        private Curso() { }

        public Curso(string nome, string descricao, double cargaHoraria, TargetAudience targetAudience, double valor)
        {
            BaseValidator.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            TargetAudience = targetAudience;
            Valor = valor;
        }

        public void AlterarNome(string nome)
        {
            BaseValidator.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            BaseValidator.Novo()
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .DispararExcecaoSeExistir();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            BaseValidator.Novo()
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            Valor = valor;
        }
    }
}