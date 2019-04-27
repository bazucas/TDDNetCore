using System.Linq;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio._Base;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using CursoOnline.Dominio.Alunos;

namespace CursoOnline.Web.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly IEnrollmentRepository _matriculaRepository;
        private readonly IRepository<Student> _alunoRepository;
        private readonly IRepository<Course> _cursoRepository;
        private readonly EnrollmentCreation _enrollmentCreation;

        public MatriculaController(
            IEnrollmentRepository matriculaRepository,
            IRepository<Student> alunoRepository,
            IRepository<Course> cursoRepository,
            EnrollmentCreation enrollmentCreation)
        {
            _matriculaRepository = matriculaRepository;
            _alunoRepository = alunoRepository;
            _cursoRepository = cursoRepository;
            _enrollmentCreation = enrollmentCreation;
        }

        public IActionResult Index()
        {
            var matriculas = _matriculaRepository.Get();

            if (!matriculas.Any()) return View("Index", PaginatedList<EnrollmentListDto>.Create(null, Request));
            var dtos = matriculas.Where(m => !m.Canceled).Select(c => new EnrollmentListDto
            {
                Id = c.Id,
                StudentName = c.Student.Name,
                CourseName = c.Course.Name,
                Amount = c.PaidAmount
            });
            return View("Index", PaginatedList<EnrollmentListDto>.Create(dtos, Request));

        }

        public IActionResult Nova()
        {
            InicializarAlunosECursosNaViewBag();

            return View("Nova", new EnrollmentDto());
        }

        private void InicializarAlunosECursosNaViewBag()
        {
            var alunos = _alunoRepository.Get();
            if (alunos != null && alunos.Any())
                alunos = alunos.OrderBy(a => a.Name).ToList();

            ViewBag.Alunos = alunos;

            var cursos = _cursoRepository.Get();
            if (cursos != null && cursos.Any())
                cursos = cursos.OrderBy(c => c.Name).ToList();

            ViewBag.Cursos = cursos;
        }

        [HttpPost]
        public IActionResult Salvar(EnrollmentDto model)
        {
            _enrollmentCreation.Create(model);
            return Ok();
        }
    }
}
