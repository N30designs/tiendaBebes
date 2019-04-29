namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartamentSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Departamento_Insert",
                p => new
                    {
                        Nombre = p.String(maxLength: 50),
                        Presupuesto = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        FechaInicio = p.DateTime(),
                        ProfesorID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Departamento]([Nombre], [Presupuesto], [FechaInicio], [ProfesorID])
                      VALUES (@Nombre, @Presupuesto, @FechaInicio, @ProfesorID)
                      
                      DECLARE @DepartamentoID int
                      SELECT @DepartamentoID = [DepartamentoID]
                      FROM [dbo].[Departamento]
                      WHERE @@ROWCOUNT > 0 AND [DepartamentoID] = scope_identity()
                      
                      SELECT t0.[DepartamentoID]
                      FROM [dbo].[Departamento] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartamentoID] = @DepartamentoID"
            );
            
            CreateStoredProcedure(
                "dbo.Departamento_Update",
                p => new
                    {
                        DepartamentoID = p.Int(),
                        Nombre = p.String(maxLength: 50),
                        Presupuesto = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        FechaInicio = p.DateTime(),
                        ProfesorID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Departamento]
                      SET [Nombre] = @Nombre, [Presupuesto] = @Presupuesto, [FechaInicio] = @FechaInicio, [ProfesorID] = @ProfesorID
                      WHERE ([DepartamentoID] = @DepartamentoID)"
            );
            
            CreateStoredProcedure(
                "dbo.Departamento_Delete",
                p => new
                    {
                        DepartamentoID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Departamento]
                      WHERE ([DepartamentoID] = @DepartamentoID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Departamento_Delete");
            DropStoredProcedure("dbo.Departamento_Update");
            DropStoredProcedure("dbo.Departamento_Insert");
        }
    }
}
