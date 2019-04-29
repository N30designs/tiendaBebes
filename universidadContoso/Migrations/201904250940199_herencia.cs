namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class herencia : DbMigration
    {
        public override void Up()
        {
            // Drop foreign keys and indexes that point to tables we're going to drop.
            DropForeignKey("dbo.Inscripcion", "EstudianteID", "dbo.Estudiante");
            DropIndex("dbo.Inscripcion", new[] { "EstudianteID" });

            RenameTable(name: "dbo.Profesor", newName: "Persona");
            AddColumn("dbo.Persona", "FechaInscripcion", c => c.DateTime());
            AddColumn("dbo.Persona", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Instructor"));
            AlterColumn("dbo.Persona", "FechaContratacion", c => c.DateTime());
            AddColumn("dbo.Persona", "OldId", c => c.Int(nullable: true));

            // Copy existing Student data into new Person table.
            Sql("INSERT INTO dbo.Persona (Nombre, Apellidos, FechaContratacion, FechaInscripcion, Discriminator, OldId) SELECT Apellidos, Nombre, null AS FechaContratacion, FechaInscripcion, 'Estudiante' AS Discriminator, ID AS OldId FROM dbo.Estudiante");

            // Fix up existing relationships to match new PK's.
            Sql("UPDATE dbo.Inscripcion SET EstudianteID= (SELECT ID FROM dbo.Persona WHERE OldId = Inscripcion.EstudianteID AND Discriminator = 'Estudiante')");

            // Remove temporary key
            DropColumn("dbo.Persona", "OldId");

            DropTable("dbo.Estudiante");

            // Re-create foreign keys and indexes pointing to new table.
            AddForeignKey("dbo.Inscripcion", "EstudianteID", "dbo.Persona", "ID", cascadeDelete: true);
            CreateIndex("dbo.Inscripcion", "EstudianteID");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Estudiante",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 100),
                        FechaInscripcion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Persona", "FechaContratacion", c => c.DateTime(nullable: false));
            DropColumn("dbo.Persona", "Discriminator");
            DropColumn("dbo.Persona", "FechaInscripcion");
            RenameTable(name: "dbo.Persona", newName: "Profesor");
        }
    }
}
