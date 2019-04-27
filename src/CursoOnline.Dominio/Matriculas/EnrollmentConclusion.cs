using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
    public class EnrollmentConclusion
    {
        private readonly IEnrollmentRepository _matriculaRepository;

        public EnrollmentConclusion(IEnrollmentRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public void Concluir(int matriculaId, double notaDoAluno)
        {
            var matricula = _matriculaRepository.GetById(matriculaId);

            BaseValidator.New()
                .When(matricula == null, Resource.EnrollmentNotFound)
                .TriggersIfExceptionExists();

            matricula?.ShowGrade(notaDoAluno);
        }
    }
}