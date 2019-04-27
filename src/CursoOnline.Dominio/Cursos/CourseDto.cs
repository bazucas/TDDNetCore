namespace CursoOnline.Dominio.Cursos
{
    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public string TargetAudience { get; set; }
        public double Amount { get; set; }
        public int Id { get; set; }
    }
}