using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace universidadContoso.DAL
{
    public class EscuelaConfiguration : DbConfiguration
    {
        public EscuelaConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}