using System.Collections.Generic;
using Abp.EntityFramework.Entities.Test;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Support;
using Lte.Parameters.Abstract.Dt;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfo2GQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordService _service;

        public AreaTestInfo2GQuery(IFileRecordService service, ITownBoundaryRepository boundaryRepository)
            : base(boundaryRepository)
        {
            _service = service;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data2G = _service.GetFileRecord2Gs(csvFileName);
            UpdateTownRecords(townIds, fileId, data2G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data2G = _service.GetFileRecord2Gs(csvFileName);
            UpdateRoadRecords(townIds, fileId, data2G, results);
            return results;
        }
    }
}