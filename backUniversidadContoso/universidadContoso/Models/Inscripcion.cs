namespace universidadContoso.Models
{
    public enum Nota
    {
        A,B,C,D,F
    }


    public class Inscripcion
    {
        public int InscripcionID { get; set; }
        public int CursoID { get; set; }
        public int EstudianteID { get; set; }
        public Nota? Nota { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Estudiante Estudiante { get; set; }
    }
}