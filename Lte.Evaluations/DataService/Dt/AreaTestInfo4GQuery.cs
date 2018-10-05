using System.Collections.Generic;
using Abp.EntityFramework.Entities.Test;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Support;
using Lte.Parameters.Abstract.Dt;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfo4GQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordService _service;

        public AreaTestInfo4GQuery(IFileRecordService service, ITownBoundaryRepository boundaryRepository) : base(boundaryRepository)
        {
            _service = service;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data4G = _service.GetFileRecord4Gs(csvFileName);
            UpdateTownRecords(townIds, fileId, data4G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data4G = _service.GetFileRecord4Gs(csvFileName);
            UpdateRoadRecords(townIds, fileId, data4G, results);
            return results;
        }
    }
}