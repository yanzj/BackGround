using Abp.EntityFramework;
using Lte.Parameters.Entities.Kpi;
using System.Data.Entity;
using Abp.EntityFramework.Entities.Mr;

namespace Lte.Parameters.Concrete
{
    public class EFParametersContext : AbpDbContext
    {
        public EFParametersContext() : base("EFParametersContext")
        {
        }


    }
}
