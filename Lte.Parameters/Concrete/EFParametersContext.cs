using Abp.EntityFramework;
using Lte.Parameters.Entities.Kpi;
using System.Data.Entity;
using Abp.EntityFramework.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFParametersContext : AbpDbContext
    {
        public EFParametersContext() : base("EFParametersContext")
        {
        }

        public DbSet<AlarmStat> AlarmStats { get; set; }

        public DbSet<PreciseCoverage4G> PrecisCoverage4Gs { get; set; }

        public DbSet<TownPreciseCoverage4GStat> TownPreciseCoverage4GStats { get; set; }

        public DbSet<InterferenceMatrixStat> InterferenceMatrices { get; set; }
        

    }
}
