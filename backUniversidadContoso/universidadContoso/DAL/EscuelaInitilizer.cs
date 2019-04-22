using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using universidadContoso.Models;

namespace universidadContoso.DAL
{
    public class EscuelaInitilizer : System.Data.Entity. DropCreateDatabaseIfModelChanges<EscuelaContext>
    {

        protected override void Seed(EscuelaContext context)
        {
            var estudiantes = new List<Estudiante>
            {
            new Estudiante{Nombre="Carson",Apellidos="Alexander",FechaInscripcion=DateTime.Parse("2005-09-01")},
            new Estudiante{Nombre="Meredith",Apellidos="Alonso",FechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{Nombre="Arturo",Apellidos="Anand",FechaInscripcion=DateTime.Parse("2003-09-01")},
            new Estudiante{Nombre="Gytis",Apellidos="Barzdukas",FechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{Nombre="Yan",Apellidos="Li",FechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{Nombre="Peggy",Apellidos="Justice",FechaInscripcion=DateTime.Parse("2001-09-01")},
            new Estudiante{Nombre="Laura",Apellidos="Norman",FechaInscripcion=DateTime.Parse("2003-09-01")},
            new Estudiante{Nombre="Nino",Apellidos="Olivetto",FechaInscripcion=DateTime.Parse("2005-09-01")}
            };

            estudiantes.ForEach(s => context.Estudiantes.Add(s));
            context.SaveChanges();
            var cursos = new List<Curso>
            {
            new Curso{CursoID=1050,Nombre="Química",Creditos=3,},
            new Curso{CursoID=4022,Nombre="Microeconomia",Creditos=3,},
            new Curso{CursoID=4041,Nombre="Macroeconomia",Creditos=3,},
            new Curso{CursoID=1045,Nombre="Cálculo",Creditos=4,},
            new Curso{CursoID=3141,Nombre="Trigonometria",Creditos=4,},
            new Curso{CursoID=2021,Nombre="Composición",Creditos=3,},
            new Curso{CursoID=2042,Nombre="Literatura",Creditos=4,}
            };
            cursos.ForEach(s => context.Cursos.Add(s));
            context.SaveChanges();

            var inscripciones = new List<Inscripcion>
            {
            new Inscripcion{EstudianteID=1,CursoID=1050,Nota=Nota.A},
            new Inscripcion{EstudianteID=1,CursoID=4022,Nota=Nota.C},
            new Inscripcion{EstudianteID=1,CursoID=4041,Nota=Nota.B},
            new Inscripcion{EstudianteID=2,CursoID=1045,Nota=Nota.B},
            new Inscripcion{EstudianteID=2,CursoID=3141,Nota=Nota.F},
            new Inscripcion{EstudianteID=2,CursoID=2021,Nota=Nota.F},
            new Inscripcion{EstudianteID=3,CursoID=1050},
            new Inscripcion{EstudianteID=4,CursoID=1050,},
            new Inscripcion{EstudianteID=4,CursoID=4022,Nota=Nota.F},
            new Inscripcion{EstudianteID=5,CursoID=4041,Nota=Nota.C},
            new Inscripcion{EstudianteID=6,CursoID=1045},
            new Inscripcion{EstudianteID=7,CursoID=3141,Nota=Nota.A},
            };
            inscripciones.ForEach(s => context.Inscripciones.Add(s));
            context.SaveChanges();
        }



    }
}