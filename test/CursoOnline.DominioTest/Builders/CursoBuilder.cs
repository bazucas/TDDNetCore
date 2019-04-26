using System;
using System.Collections.Generic;
using System.Text;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.DominioTest.Builders
{
    public class CursoBuilder
    {
        private string _nome = "Matemática";
        private int _carga = 40;
        private PublicoEnum _publico = PublicoEnum.Estudante;
        private decimal _valor = 950.0M;
        private string _descricao = "descricao";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCarga(int carga)
        {
            _carga = carga;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublico(PublicoEnum publico)
        {
            _publico = publico;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _carga, _publico, _valor, _descricao);
        }
    }
}
