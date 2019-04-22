namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estudiante", "Nombre", c => c.String(maxLength: 50));
            AlterColumn("dbo.Estudiante", "Apellidos", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estudiante", "Apellidos", c => c.String());
            AlterColumn("dbo.Estudiante", "Nombre", c => c.String());
        }
    }
}
