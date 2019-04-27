using System.Text.RegularExpressions;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Alunos
{
    public class Student : Entity
    {
        private readonly Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex _nifRegex = new Regex(@"^[0-9]\d{6}$");
        public string Name { get; private set; }
        public string Nif { get; private set; }
        public string Email { get; private set; }
        public TargetAudience TargetAudience { get; private set; }

        private Student() { }

        public Student(string name, string email, string nif, TargetAudience targetAudience)
        {
            BaseValidator.New()
                .When(string.IsNullOrEmpty(name), Resource.InvalidName)
                .When(string.IsNullOrEmpty(email) || !_emailRegex.Match(email).Success, Resource.InvalidEmail)
                .When(string.IsNullOrEmpty(nif), Resource.InvalidNif)
                .TriggersIfExceptionExists();

            Name = name;
            Nif = nif;
            Email = email;
            TargetAudience = targetAudience;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}