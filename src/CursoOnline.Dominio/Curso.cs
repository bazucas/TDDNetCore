using System;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio
{
    public class Curso
    {
        public Curso(string nome, int carga, PublicoEnum publico, decimal valor, string descricao)
        {
            if (string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome inválido");

            if (carga < 1) throw new ArgumentException("Carga inválida");

            if (valor < 1) throw new ArgumentException("Valor inválido");

            Nome = nome;
            Carga = carga;
            Publico = publico;
            Valor = valor;
            Descricao = descricao;
        }

        public string Nome { get; }
        public int Carga { get; }
        public PublicoEnum Publico { get; }
        public decimal Valor { get; }
        public string Descricao { get; }
    }
}