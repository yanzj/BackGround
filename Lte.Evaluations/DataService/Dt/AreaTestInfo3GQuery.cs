using System.Collections.Generic;
using Abp.EntityFramework.Entities.Test;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Support;
using Lte.Parameters.Abstract.Dt;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfo3GQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordService _service;

        public AreaTestInfo3GQuery(IFileRecordService service, ITownBoundaryRepository boundaryRepository) : base(boundaryRepository)
        {
            _service = service;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data3G = _service.GetFileRecord3Gs(csvFileName);
            UpdateTownRecords(townIds, fileId, data3G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data3G = _service.GetFileRecord3Gs(csvFileName);
            UpdateRoadRecords(townIds, fileId, data3G, results);
            return results;
        }
    }
}