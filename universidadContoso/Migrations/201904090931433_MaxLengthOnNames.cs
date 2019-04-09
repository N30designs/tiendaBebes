namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estudiantes", "apellido", c => c.String(maxLength: 50));
            AlterColumn("dbo.Estudiantes", "nombre", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estudiantes", "nombre", c => c.String());
            AlterColumn("dbo.Estudiantes", "apellido", c => c.String());
        }
    }
}
