using System;
using System.ComponentModel.DataAnnotations;

namespace universidadContoso.ViewModels
{
    public class FechaInscripcionGroup
    {
        [DataType(DataType.Date)]
        public DateTime? FechaInscripcion { get; set; }

        public int EstudiantesContador { get; set; }
    }
}