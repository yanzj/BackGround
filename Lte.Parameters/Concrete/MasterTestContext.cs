using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using Lte.Parameters.Entities.Dt;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using Abp.EntityFramework;
using MySql.Data.Entity;

namespace Lte.Parameters.Concrete
{
    //实施数据库迁移前，请解除注释；迁移完成后，请再次注释编译后发布，否则在IIS上程序会报错
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MasterTestContext : AbpDbContext
    {
        public MasterTestContext()
            : base("MasterTest")
        {

        }
        
        public DbSet<FileRecord4G> FileRecord4Gs { get;set; }

        public DbSet<FileRecordVolte> FileRecordVoltes { get; set; }
        
        public DbSet<FileRecord3G> FileRecord3Gs { get; set; }
        
        public DbSet<FileRecord2G> FileRecord2Gs { get;set; }
        
    }
}
