using System;
using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities
{
    public class CsvFilesInfo : Entity
    {
        public DateTime TestDate { get; set; }
        
        public string CsvFileName { get; set; }
        
        public double Distance { get; set; }

        public int Count { get; set; }

        public int CoverageCount { get; set; }

        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;
    }
}