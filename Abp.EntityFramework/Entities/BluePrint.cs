using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities
{
    public class BluePrint : Entity
    {
        public string FslNumber { get; set; }

        public string FileName { get; set; }

        public string Folder { get; set; }

        public string DesignBrief { get; set; }
    }
}