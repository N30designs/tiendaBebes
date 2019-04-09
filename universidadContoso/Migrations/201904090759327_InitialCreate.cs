namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cursoes",
                c => new
                    {
                        cursoID = c.Int(nullable: false),
                        titulo = c.String(),
                        creditos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cursoID);
            
            CreateTable(
                "dbo.Inscripcions",
                c => new
                    {
                        inscripcionID = c.Int(nullable: false, identity: true),
                        cursoID = c.Int(nullable: false),
                        estudianteID = c.Int(nullable: false),
                        grado = c.Int(),
                    })
                .PrimaryKey(t => t.inscripcionID)
                .ForeignKey("dbo.Cursoes", t => t.cursoID, cascadeDelete: true)
                .ForeignKey("dbo.Estudiantes", t => t.estudianteID, cascadeDelete: true)
                .Index(t => t.cursoID)
                .Index(t => t.estudianteID);
            
            CreateTable(
                "dbo.Estudiantes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        apellido = c.String(),
                        nombre = c.String(),
                        fechaInscripcion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inscripcions", "estudianteID", "dbo.Estudiantes");
            DropForeignKey("dbo.Inscripcions", "cursoID", "dbo.Cursoes");
            DropIndex("dbo.Inscripcions", new[] { "estudianteID" });
            DropIndex("dbo.Inscripcions", new[] { "cursoID" });
            DropTable("dbo.Estudiantes");
            DropTable("dbo.Inscripcions");
            DropTable("dbo.Cursoes");
        }
    }
}
