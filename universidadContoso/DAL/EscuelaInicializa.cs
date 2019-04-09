using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using universidadContoso.Models;


namespace universidadContoso.DAL
{
    public class EscuelaInicializa : System.Data.Entity. DropCreateDatabaseIfModelChanges<EscuelaContext>
    {
        protected override void Seed(EscuelaContext context)
        {
            var Estudiantes = new List<Estudiante>
            {
            new Estudiante{nombre="Carson",apellido="Alexander",fechaInscripcion=DateTime.Parse("2005-09-01")},
            new Estudiante{nombre="Meredith",apellido="Alonso",fechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{nombre="Arturo",apellido="Anand",fechaInscripcion=DateTime.Parse("2003-09-01")},
            new Estudiante{nombre="Gytis",apellido="Barzdukas",fechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{nombre="Yan",apellido="Li",fechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{nombre="Peggy",apellido="Justice",fechaInscripcion=DateTime.Parse("2001-09-01")},
            new Estudiante{nombre="Laura",apellido="Norman",fechaInscripcion=DateTime.Parse("2003-09-01")},
            new Estudiante{nombre="Nino",apellido="Olivetto",fechaInscripcion=DateTime.Parse("2005-09-01")}
            };

            Estudiantes.ForEach(s => context.estudiantes.Add(s));
            context.SaveChanges();
            var cursos = new List<Curso>
            {
            new Curso{cursoID=1050,titulo="Chemistry",creditos=3,},
            new Curso{cursoID=4022,titulo="Microeconomics",creditos=3,},
            new Curso{cursoID=4041,titulo="Macroeconomics",creditos=3,},
            new Curso{cursoID=1045,titulo="Calculus",creditos=4,},
            new Curso{cursoID=3141,titulo="Trigonometry",creditos=4,},
            new Curso{cursoID=2021,titulo="Composition",creditos=3,},
            new Curso{cursoID=2042,titulo="Literature",creditos=4,}
            };
            cursos.ForEach(s => context.cursos.Add(s));
            context.SaveChanges();
            var inscripciones = new List<Inscripcion>
            {
            new Inscripcion{estudianteID=1,cursoID=1050,grado=Grado.A},
            new Inscripcion{estudianteID=1,cursoID=4022,grado=Grado.C},
            new Inscripcion{estudianteID=1,cursoID=4041,grado=Grado.B},
            new Inscripcion{estudianteID=2,cursoID=1045,grado=Grado.B},
            new Inscripcion{estudianteID=2,cursoID=3141,grado=Grado.F},
            new Inscripcion{estudianteID=2,cursoID=2021,grado=Grado.F},
            new Inscripcion{estudianteID=3,cursoID=1050},
            new Inscripcion{estudianteID=4,cursoID=1050,},
            new Inscripcion{estudianteID=4,cursoID=4022,grado=Grado.F},
            new Inscripcion{estudianteID=5,cursoID=4041,grado=Grado.C},
            new Inscripcion{estudianteID=6,cursoID=1045},
            new Inscripcion{estudianteID=7,cursoID=3141,grado=Grado.A},
            };
            inscripciones.ForEach(s => context.inscripciones.Add(s));
            context.SaveChanges();


        }



    }
}