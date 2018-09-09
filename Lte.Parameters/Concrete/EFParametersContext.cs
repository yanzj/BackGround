using Abp.EntityFramework;
using Lte.Parameters.Entities.Kpi;
using System.Data.Entity;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;

namespace Lte.Parameters.Concrete
{
    public class EFParametersContext : AbpDbContext
    {
        public EFParametersContext() : base("EFParametersContext")
        {
        }

        public DbSet<AlarmStat> AlarmStats { get; set; }

        public DbSet<PreciseCoverage4G> PrecisCoverage4Gs { get; set; }

        public DbSet<InterferenceMatrixStat> InterferenceMatrices { get; set; }
        

    }
}
