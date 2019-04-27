using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos
{
    public class Course : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Hours { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Amount { get; private set; }

        private Course() { }

        public Course(string name, string description, double hours, TargetAudience targetAudience, double amount)
        {
            BaseValidator.New()
                .When(string.IsNullOrEmpty(name), Resource.InvalidName)
                .When(hours < 1, Resource.InvalidHours)
                .When(amount < 1, Resource.InvalidAmount)
                .TriggersIfExceptionExists();

            Name = name;
            Description = description;
            Hours = hours;
            TargetAudience = targetAudience;
            Amount = amount;
        }

        public void ChangeName(string name)
        {
            BaseValidator.New()
                .When(string.IsNullOrEmpty(name), Resource.InvalidName)
                .TriggersIfExceptionExists();

            Name = name;
        }

        public void ChangeHours(double hours)
        {
            BaseValidator.New()
                .When(hours < 1, Resource.InvalidHours)
                .TriggersIfExceptionExists();

            Hours = hours;
        }

        public void ChangeAmount(double amount)
        {
            BaseValidator.New()
                .When(amount < 1, Resource.InvalidAmount)
                .TriggersIfExceptionExists();

            Amount = amount;
        }
    }
}