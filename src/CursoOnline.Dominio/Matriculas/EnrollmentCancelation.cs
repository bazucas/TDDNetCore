using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
    public class EnrollmentCancelation
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentCancelation(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public void Cancelar(int enrollmentId)
        {
            var enrollment = _enrollmentRepository.GetById(enrollmentId);

            BaseValidator.New()
                .When(enrollment == null, Resource.EnrollmentNotFound)
                .TriggersIfExceptionExists();

            enrollment?.Cancel();
        }
    }
}