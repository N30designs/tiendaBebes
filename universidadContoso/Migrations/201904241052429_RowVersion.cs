namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departamento", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterStoredProcedure(
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
                      
                      SELECT t0.[DepartamentoID], t0.[RowVersion]
                      FROM [dbo].[Departamento] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartamentoID] = @DepartamentoID"
            );
            
            AlterStoredProcedure(
                "dbo.Departamento_Update",
                p => new
                    {
                        DepartamentoID = p.Int(),
                        Nombre = p.String(maxLength: 50),
                        Presupuesto = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        FechaInicio = p.DateTime(),
                        ProfesorID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Departamento]
                      SET [Nombre] = @Nombre, [Presupuesto] = @Presupuesto, [FechaInicio] = @FechaInicio, [ProfesorID] = @ProfesorID
                      WHERE (([DepartamentoID] = @DepartamentoID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Departamento] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartamentoID] = @DepartamentoID"
            );
            
            AlterStoredProcedure(
                "dbo.Departamento_Delete",
                p => new
                    {
                        DepartamentoID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Departamento]
                      WHERE (([DepartamentoID] = @DepartamentoID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departamento", "RowVersion");
            throw new NotSupportedException("No se admite la técnica scaffolding que crea o modifica operaciones de procedimiento en métodos descendentes.");
        }
    }
}
