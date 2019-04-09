using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace universidadContoso.ViewModels
{
    public class FechaInscripcionGrupo
    {
        [DataType(DataType.Date)]
        public DateTime? fechaInscripcion { get; set; }

        public int estudiantesContador { get; set; }

    }
}