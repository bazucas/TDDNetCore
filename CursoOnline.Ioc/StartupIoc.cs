﻿using System;
using CursoOnline.Dados.Contextos;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CursoOnline.Dominio.Matriculas;

namespace CursoOnline.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionString"]));
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(ICourseRepository), typeof(CourseRepository));
            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddScoped(typeof(IEnrollmentRepository), typeof(EnrollmentRepository));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IConversorDePublicoAlvo), typeof(ConversorDePublicoAlvo));
            services.AddScoped<CourseStorer>();
            services.AddScoped<StudentStorer>();
            services.AddScoped<EnrollmentCreation>();
            services.AddScoped<EnrollmentConclusion>();
            services.AddScoped<EnrollmentCancelation>();
        }
    }
}
