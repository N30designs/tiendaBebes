using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace universidadContoso.DAL
{
    public class EscuelaConfiguracion : DbConfiguration
    {
        public EscuelaConfiguracion()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}