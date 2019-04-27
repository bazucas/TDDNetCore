using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Alunos
{
    public class StudentStorer
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IConversorDePublicoAlvo _targetAudienceConverter;

        public StudentStorer(IStudentRepository studentRepository, IConversorDePublicoAlvo targetAudienceConverter)
        {
            _studentRepository = studentRepository;
            _targetAudienceConverter = targetAudienceConverter;
        }

        public void Store(StudentDto studentDto)
        {
            var studentWithSameNif = _studentRepository.GetByNif(studentDto.Nif);

            BaseValidator.New()
                .When(studentWithSameNif != null && studentWithSameNif.Id != studentDto.Id, Resource.AlreadyRegisteredNif)
                .TriggersIfExceptionExists();

            if (studentDto.Id == 0)
            {
                var convertedTargetAudience = _targetAudienceConverter.Converter(studentDto.TargetAudience);
                var student = new Student(studentDto.Name, studentDto.Email, studentDto.Nif, convertedTargetAudience);
                _studentRepository.Add(student);
            }
            else
            {
                var student = _studentRepository.GetById(studentDto.Id);
                student.ChangeName(studentDto.Nif);
            }
        }
    }
}