using System.Linq;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly CourseStorer _courseStorer;
        private readonly IRepository<Course> _cursoRepository;

        public CursoController(CourseStorer courseStorer, IRepository<Course> cursoRepository)
        {
            _courseStorer = courseStorer;
            _cursoRepository = cursoRepository;
        }

        public IActionResult Index()
        {
            var cursos = _cursoRepository.Get();

            if (cursos.Any())
            {
                var dtos = cursos.Select(c => new CoursesListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Hours = c.Hours,
                    TargetAudience = c.TargetAudience.ToString(),
                    Amount = c.Amount
                });
                return View("Index", PaginatedList<CoursesListDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<CoursesListDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var curso = _cursoRepository.GetById(id);
            var dto = new CourseDto
            {
                Id = curso.Id,
                Name = curso.Name,
                Description = curso.Description,
                Hours = curso.Hours,
                Amount = curso.Amount
            };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CourseDto());
        }

        [HttpPost]
        public IActionResult Salvar(CourseDto model)
        {
            _courseStorer.Store(model);
            return Ok();
        }
    }
}
