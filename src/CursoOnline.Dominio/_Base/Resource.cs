namespace CursoOnline.Dominio._Base
{
    public static class Resource
    {
        public static string InvalidName = "Name inválido";
        public static string InvalidEmail = "Email inválido";
        public static string InvalidHours = "Carga horária inválida";
        public static string InvalidAmount = "Amount inválido";
        public static string CourseAlreadyExists = "Name do course já consta no banco de dados";
        public static string InvalidTargetAudience = "Publico Alvo inválido";
        public static string InvalidNif = "CPF inválido";
        public static string AlreadyRegisteredNif = "CPF já cadastrado";
        public static string InvalidStudent = "Aluno inválido";
        public static string InvalidCourse = "Course inválido";
        public static string PaidAmountBiggerThanCourseValue =
            "Amount pago na matricula não pode ser maior que valor do course";

        public static string DifferentTargetAudience = "Publico alvo do aluno e course são diferentes";
        public static string CourseNotFound = "Course não encontrado";
        public static string StudentNotFound = "Aluno não encontrado";
        public static string InvalidStudentGrade = "Nota do aluno invalida";
        public static string EnrollmentNotFound = "Enrollment não encontrada";
        public static string EnrollmentCanceled = "Ação não permitida por matricula está cancelada";
        public static string EnrollmentFinished = "Ação não permitida por matricula está conclída";
    }
}
