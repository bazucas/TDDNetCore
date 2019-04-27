using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos
{
    public class CourseStorer
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IConversorDePublicoAlvo _targetAudienceConverter;

        public CourseStorer(ICourseRepository courseRepository, IConversorDePublicoAlvo targetAudienceConverter)
        {
            _courseRepository = courseRepository;
            _targetAudienceConverter = targetAudienceConverter;
        }

        public void Store(CourseDto courseDto)
        {
            var savedCourse = _courseRepository.GetByName(courseDto.Name);

            BaseValidator.New()
                .When(savedCourse != null && savedCourse.Id != courseDto.Id, Resource.CourseAlreadyExists)
                .TriggersIfExceptionExists();

            var publicoAlvo = _targetAudienceConverter.Converter(courseDto.TargetAudience);
                
            var course = 
                new Course(courseDto.Name, courseDto.Description, courseDto.Hours, publicoAlvo, courseDto.Amount);

            if (courseDto.Id > 0)
            {
                course = _courseRepository.GetById(courseDto.Id);
                course.ChangeName(courseDto.Name);
                course.ChangeAmount(courseDto.Amount);
                course.ChangeHours(courseDto.Hours);
            }

            if(courseDto.Id == 0)
                _courseRepository.Add(course);
        }
    }
}