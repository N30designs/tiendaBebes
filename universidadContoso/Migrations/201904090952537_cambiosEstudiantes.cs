namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosEstudiantes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estudiantes", "apellido", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Estudiantes", "Nombre", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estudiantes", "Nombre", c => c.String(maxLength: 50));
            AlterColumn("dbo.Estudiantes", "apellido", c => c.String(maxLength: 50));
        }
    }
}
