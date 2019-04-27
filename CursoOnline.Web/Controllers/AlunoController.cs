using System.Linq;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio._Base;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly StudentStorer _studentStorer;
        private readonly IRepository<Student> _studentRepository;

        public AlunoController(StudentStorer studentStorer, IRepository<Student> studentRepository)
        {
            _studentStorer = studentStorer;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            var alunos = _studentRepository.Get();

            if (alunos.Any())
            {
                var dtos = alunos.Select(c => new StudentListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Nif = c.Nif,
                    Email = c.Email
                });
                return View("Index", PaginatedList<StudentListDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<StudentListDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var student = _studentRepository.GetById(id);
            var dto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Nif = student.Nif,
                Email = student.Email,
                TargetAudience = student.TargetAudience.ToString()
            };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new StudentDto());
        }

        [HttpPost]
        public IActionResult Salvar(StudentDto model)
        {
            _studentStorer.Store(model);

            return Ok();
        }
    }
}
