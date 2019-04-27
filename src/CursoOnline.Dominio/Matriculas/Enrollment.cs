using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
    public class Enrollment : Entity
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public double PaidAmount { get; private set; }
        public bool HasDiscount { get; private set; }
        public double StudentGrade { get; private set; }
        public bool FinishedCourse { get; private set; }
        public bool Canceled { get; private set; }

        private Enrollment() {}

        public Enrollment(Student student, Course course, double paidAmount)
        {
            BaseValidator.New()
                .When(student == null, Resource.InvalidStudent)
                .When(course == null, Resource.InvalidCourse)
                .When(paidAmount < 1, Resource.InvalidAmount)
                .When(course != null && paidAmount > course.Amount, Resource.PaidAmountBiggerThanCourseValue)
                .When(student != null && course != null && student.TargetAudience.GetHashCode() != course.TargetAudience.GetHashCode(), Resource.DifferentTargetAudience)
                .TriggersIfExceptionExists();

            Student = student;
            Course = course;
            PaidAmount = paidAmount;
            if (Course != null) HasDiscount = paidAmount < Course.Amount;
        }

        public void ShowGrade(double studentGrade)
        {
            BaseValidator.New()
                .When(studentGrade < 0 || studentGrade > 10, Resource.InvalidStudentGrade)
                .When(Canceled, Resource.EnrollmentCanceled)
                .TriggersIfExceptionExists();

            StudentGrade = studentGrade;
            FinishedCourse = true;
        }

        public void Cancel()
        {
            BaseValidator.New()
                .When(FinishedCourse, Resource.EnrollmentFinished)
                .TriggersIfExceptionExists();

            Canceled = true;}
    }
}