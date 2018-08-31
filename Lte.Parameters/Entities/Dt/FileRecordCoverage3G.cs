using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord3G))]
    public class FileRecordCoverage3G
    {
        [AutoMapPropertyResolve("Longtitute", typeof(FileRecord3G), typeof(NullableZeroTransform))]
        public double Longtitute { get; set; }

        [AutoMapPropertyResolve("Lattitute", typeof(FileRecord3G), typeof(NullableZeroTransform))]
        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("Sinr", typeof(FileRecord3G), typeof(NullableZeroTransform))]
        public double Sinr { get; set; }

        [AutoMapPropertyResolve("RxAgc0", typeof(FileRecord3G), typeof(NullableZeroTransform))]
        public double RxAgc0 { get; set; }

        [AutoMapPropertyResolve("RxAgc1", typeof(FileRecord3G), typeof(NullableZeroTransform))]
        public double RxAgc1 { get; set; }
    }
}