using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Dt
{
    [AutoMapFrom(typeof(FileRecord4G), typeof(FileRecordVolte))]
    public class FileRecordCoverage4G
    {
        [AutoMapPropertyResolve("Longtitute", typeof(FileRecord4G), typeof(NullableZeroTransform))]
        [AutoMapPropertyResolve("Longtitute", typeof(FileRecordVolte), typeof(NullableZeroTransform))]
        public double Longtitute { get; set; }

        [AutoMapPropertyResolve("Lattitute", typeof(FileRecord4G), typeof(NullableZeroTransform))]
        [AutoMapPropertyResolve("Lattitute", typeof(FileRecordVolte), typeof(NullableZeroTransform))]
        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("Sinr", typeof(FileRecord4G), typeof(NullableZeroTransform))]
        [AutoMapPropertyResolve("Sinr", typeof(FileRecordVolte), typeof(NullableZeroTransform))]
        public double Sinr { get; set; }

        [AutoMapPropertyResolve("Rsrp", typeof(FileRecord4G), typeof(NullableZeroTransform))]
        [AutoMapPropertyResolve("Rsrp", typeof(FileRecordVolte), typeof(NullableZeroTransform))]
        public double Rsrp { get; set; }
    }
}