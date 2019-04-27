using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
    public class EnrollmentCreation
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;

        public EnrollmentCreation(
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IRepository<Enrollment> enrollmentRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public void Create(EnrollmentDto enrollmentDto)
        {
            var course = _courseRepository.GetById(enrollmentDto.CourseId);
            var student = _studentRepository.GetById(enrollmentDto.StudentId);

            BaseValidator.New()
                .When(course == null, Resource.CourseNotFound)
                .When(student == null, Resource.StudentNotFound)
                .TriggersIfExceptionExists();

            var enrollment = new Enrollment(student, course, enrollmentDto.PaidAmount);

            _enrollmentRepository.Add(enrollment);
        }
    }
}