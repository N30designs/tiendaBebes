namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        DepartamentoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50),
                        Presupuesto = c.Decimal(nullable: false, storeType: "money"),
                        FechaInicio = c.DateTime(nullable: false),
                        ProfesorID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartamentoID)
                .ForeignKey("dbo.Profesor", t => t.ProfesorID)
                .Index(t => t.ProfesorID);
            
            CreateTable(
                "dbo.Profesor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 100),
                        FechaContratacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DespachoAsignado",
                c => new
                    {
                        ProfesorID = c.Int(nullable: false),
                        Ubicacion = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProfesorID)
                .ForeignKey("dbo.Profesor", t => t.ProfesorID)
                .Index(t => t.ProfesorID);
            
            CreateTable(
                "dbo.CursoProfesor",
                c => new
                    {
                        CursoID = c.Int(nullable: false),
                        ProfesorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CursoID, t.ProfesorID })
                .ForeignKey("dbo.Curso", t => t.CursoID, cascadeDelete: true)
                .ForeignKey("dbo.Profesor", t => t.ProfesorID, cascadeDelete: true)
                .Index(t => t.CursoID)
                .Index(t => t.ProfesorID);
            // Create  a department for course to point to.
            Sql("INSERT INTO dbo.Departamento (Nombre, Presupuesto, FechaInicio) VALUES ('Temp', 0.00, GETDATE())");
            //  default value for FK points to department created above.
            AddColumn("dbo.Curso", "DepartamentoID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Curso", "DepartamentoID", c => c.Int(nullable: false));
            AlterColumn("dbo.Curso", "Nombre", c => c.String(maxLength: 50));
            AlterColumn("dbo.Estudiante", "Nombre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Estudiante", "Apellidos", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Curso", "DepartamentoID");
            AddForeignKey("dbo.Curso", "DepartamentoID", "dbo.Departamento", "DepartamentoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CursoProfesor", "ProfesorID", "dbo.Profesor");
            DropForeignKey("dbo.CursoProfesor", "CursoID", "dbo.Curso");
            DropForeignKey("dbo.Curso", "DepartamentoID", "dbo.Departamento");
            DropForeignKey("dbo.Departamento", "ProfesorID", "dbo.Profesor");
            DropForeignKey("dbo.DespachoAsignado", "ProfesorID", "dbo.Profesor");
            DropIndex("dbo.CursoProfesor", new[] { "ProfesorID" });
            DropIndex("dbo.CursoProfesor", new[] { "CursoID" });
            DropIndex("dbo.DespachoAsignado", new[] { "ProfesorID" });
            DropIndex("dbo.Departamento", new[] { "ProfesorID" });
            DropIndex("dbo.Curso", new[] { "DepartamentoID" });
            AlterColumn("dbo.Estudiante", "Apellidos", c => c.String(maxLength: 50));
            AlterColumn("dbo.Estudiante", "Nombre", c => c.String(maxLength: 50));
            AlterColumn("dbo.Curso", "Nombre", c => c.String());
            DropColumn("dbo.Curso", "DepartamentoID");
            DropTable("dbo.CursoProfesor");
            DropTable("dbo.DespachoAsignado");
            DropTable("dbo.Profesor");
            DropTable("dbo.Departamento");
        }
    }
}
