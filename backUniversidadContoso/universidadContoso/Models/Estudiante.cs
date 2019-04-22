using System;
using System.Collections.Generic;

namespace universidadContoso.Models
{
    public class Estudiante
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
    }
}