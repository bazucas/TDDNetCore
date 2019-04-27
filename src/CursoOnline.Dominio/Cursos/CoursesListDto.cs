namespace CursoOnline.Dominio.Cursos
{
    public class CoursesListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Hours { get; set; }
        public string TargetAudience { get; set; }
        public double Amount { get; set; }
    }
}