using System.Collections.Generic;
using universidadContoso.Models;

namespace universidadContoso.ViewModels
{
    public class ProfesorIndexData
    {
        public IEnumerable<Profesor> Profesores{ get; set; }
        public IEnumerable<Curso> Cursos { get; set; }
        public IEnumerable<Inscripcion> Inscripciones { get; set; }
    }
}