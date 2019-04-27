﻿// <auto-generated />

using CursoOnline.Dados.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CursoOnline.Dados.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    internal partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CursoOnline.Dominio.Alunos.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<int>("PublicoAlvo");

                    b.HasKey("Id");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("CursoOnline.Dominio.Cursos.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CargaHoraria");

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome");

                    b.Property<int>("PublicoAlvo");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("CursoOnline.Dominio.Matriculas.Matricula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlunoId");

                    b.Property<bool>("Cancelada");

                    b.Property<bool>("CursoConcluido");

                    b.Property<int?>("CursoId");

                    b.Property<double>("NotaDoAluno");

                    b.Property<bool>("TemDesconto");

                    b.Property<double>("ValorPago");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("CursoId");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("CursoOnline.Dominio.Matriculas.Matricula", b =>
                {
                    b.HasOne("CursoOnline.Dominio.Alunos.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId");

                    b.HasOne("CursoOnline.Dominio.Cursos.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId");
                });
#pragma warning restore 612, 618
        }
    }
}