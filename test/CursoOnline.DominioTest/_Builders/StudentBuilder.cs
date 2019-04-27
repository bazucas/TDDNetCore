using System;
using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.DominioTest._Builders
{
    public class StudentBuilder
    {
        protected int Id;
        protected string Name;
        protected string Email;
        protected string Nif;
        protected TargetAudience TargetAudience;

        public static StudentBuilder New()
        {
            var faker = new Faker();

            return new StudentBuilder
            {
                Name = faker.Person.FullName,
                Email = faker.Person.Email,
                Nif = faker.Person.Cpf(),
                TargetAudience = TargetAudience.Employee
            };
        }

        public StudentBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public StudentBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        public StudentBuilder WithNif(string nif)
        {
            Nif = nif;
            return this;
        }

        public StudentBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public StudentBuilder WithTargetAudience(TargetAudience targetAudience)
        {
            TargetAudience = targetAudience;
            return this;
        }

        public Student Build()
        {
            var student = new Student(Name, Email, Nif, TargetAudience);

            if (Id <= 0) return student;

            var propertyInfo = student.GetType().GetProperty("Id");
            propertyInfo.SetValue(student, Convert.ChangeType(Id, propertyInfo.PropertyType), null);

            return student;
        }
    }
}