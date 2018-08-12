using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities.Test
{
    public class RasterTestInfo : Entity
    {
        public int RasterNum { get; set; }
        
        public string CsvFilesName { get; set; }
        
        public string NetworkType { get; set; }
    }
}